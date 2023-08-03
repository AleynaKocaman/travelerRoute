using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PlaceContent
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string? Comment { get; set; }

      
        [Range(0, 5)]
        public int? Point { get; set; }
        public DateTime? IsVisitDate { get; set; }   //gezildiği tarih girelecek
       // public int UserId { get; set; }


        public virtual Place? Place { get; set; }
        //public virtual User? User { get; set; }

    }
}
