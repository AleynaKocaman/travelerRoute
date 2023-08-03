using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Services.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly  UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private User? _user;
        public AuthenticationManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);
            user.UserName = userForRegistrationDto.Email;
            var result=await _userManager.CreateAsync(user,userForRegistrationDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
               
            }
            return result;
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {

            _user=await _userManager.FindByEmailAsync(userForAuthenticationDto.Email);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.Password));
            if (!result)
            {
                throw new InvalidOperationException();
            }
            return result;
        }

        //kimlik doğrulama doğru ise refresh token'ı oluşturan metot 
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            //true olursa refresh token ile süre uzatması olacak false olursa dokunulmayacak

            var signinCredentials = GetSiginCredentials(); // JWT için gerekli olan nesneleri döndüren metottur
            var claims = await GetClaims(); //Tokena eklenecek olan bilgilerini oluşturan metot
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims); //  yeni bir JWT oluşturan metot
                                                                             

            var refreshToken = GenerateRefreshToken();//refresh token için random dize oluşturan metot
            _user.RefreshToken = refreshToken;
            if (populateExp)
            {
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            }
            await _userManager.UpdateAsync(_user);//veritabanında güncelleme işlemi
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions); //oluşturulan token dizeye dönüştürme işlemi
            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

        }
        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var princpal = GetPrincipalFromExpiredToken(tokenDto.AccessToken); //token doğrulama
           //Teyit işlemi veritabanına gidip üretilen tokennun kullanıcısı hala mevcut mu
            var user = await _userManager.FindByNameAsync(princpal.Identity.Name);//veri tabanı kısmı

            if (user is null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {

                throw new RefreshTokenBadRequestException();
            }
            //Kullanıcı bilgileri doğruysa, yeni bir token çifti oluşturmak için CreateToken metodu çağrılır.
            _user = user;
            return await CreateToken(populateExp: false); 
           
        }
        public async Task LogOut(string accessToken)
        {

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                // user.RefreshToken = null; //???????????????
                user.RefreshToken = "";
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                await _userManager.UpdateAsync(user);
            }
            /*
            var user = await _userManager.FindByNameAsync(_user.Email); // Aktif kullanıcıyı bulma
            var refreshToken = GenerateRefreshToken();

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new RefreshTokenBadRequestException();
            }

            user.RefreshToken = null; // Refresh token'ı temizle
            user.RefreshTokenExpiryTime = DateTime.MinValue; // Refresh token süresini sıfırla

            await _userManager.UpdateAsync(user); // Kullanıcıyı güncelle

            // İsteğe bağlı olarak, kullanıcı oturumunu sonlandırıldı mesajı döndürebilirsiniz
            // return "User logged out successfully";
            */
        }


        private SigningCredentials GetSiginCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings"); //ilgili bölüm alınacak
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]); //encoding anahtarına bağlı olarak alınacak seçilen değer formatına dönüştürecek (UTF-8) tür byte
            var secret = new SymmetricSecurityKey(key);//SymmetricSecurityKey bu anahtar nesnesini oluşturur
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256); //seçilen algoritma kullanılarak SigningCredentials nesne oluşturulup dönüş yapmış olanacak

        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,_user.Email)//listeye claims ekle 
            };

            var roles = await _userManager.GetRolesAsync(_user);//rolleri alıyoruz
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));//her bir rolu claims nesnesi oluşturarak ekleme yapıyoruz
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
    List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings"); //ilgili bölüm alınacak
            var tokenOpptions = new JwtSecurityToken( //verilen bilgiler ile jwt token nesnesini oluştur
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signinCredentials
                );
            return tokenOpptions;

        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber); //random dizi oluşturulur tür byte
                return Convert.ToBase64String(randomNumber); //string türüne çevrilir
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            //bize bir token geliyor
            var jwtSettings = _configuration.GetSection("JwtSettings");//appsetting dosyasından ilgili bölmü çağır
            var secretKey = jwtSettings["secretKey"]; //istenilen kısmı al ve bir değere ata
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

            };
           
            var tokenHanndler = new JwtSecurityTokenHandler();//doğrulama yapılıyor
            SecurityToken securityToken;
           
            var principal = tokenHanndler.ValidateToken(token, tokenValidationParameters, //doğrulama işleminden kullanıcı bilgileri almak istiyoruz
                out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;//eğer referans almaazsa null değer döner
            
            if (jwtSecurityToken is null ||
               !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token"); //geçerli değil ise hata gönder

            }
            return principal;
        }


    }
}
