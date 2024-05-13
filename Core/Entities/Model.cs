using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Model : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string ImageUrl { get; set; }=String.Empty;
        public List<Car> Cars { get; set; } = new List<Car>();
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        
    }
}