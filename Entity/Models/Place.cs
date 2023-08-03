using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Town { get; set; }
        public string District{ get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public int CityId { get; set; }
        public int CategoryId { get; set; }

        public virtual City? City { get; set; }
        public virtual Category? Category { get; set; }


    }
}
