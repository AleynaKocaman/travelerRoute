using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TravelListContent
    {
        public int Id { get; set; }
        public DateTime? VisitDate { get; set; }  //gezmek istediği tarihi girecek
        public int TravelListId { get; set; }
        public int PlaceId { get; set; }

        public virtual Place? Place { get; set; }
        public virtual TravelList? TravelList { get; set; }
    }
}
