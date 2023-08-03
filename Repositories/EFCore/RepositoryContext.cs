using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext :IdentityDbContext<User>
    {
        //veri tabanını oluşturmak
        public RepositoryContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<City> city { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Place> place { get; set; }
        // public DbSet<User> user { get; set; }
        public DbSet<TravelList> travelList { get; set; }
        public DbSet<TravelListContent> travelListContent { get; set; }
        public DbSet<PlaceContent> placeContent { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
