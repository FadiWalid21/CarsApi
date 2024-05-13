using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CarDto
    {
        public DateTime Year { get; set; }
        public bool IsAvailable { get; set; }
        public int Cylinder { get; set; }
        public int Doors { get; set; }
        public List<string> ImagesUrls { get; set; } = new List<string>();
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Motor { get; set; } = String.Empty;
        public List<string> Colors { get; set; } = new List<string>();
        public int Tank { get; set; }
        public string GearBox { get; set; } = String.Empty;
        public int PowerHorse { get; set; }
        public int CategoryId { get; set; }
        public int ModelId { get; set; }
        public int BrandId { get; set; }
    }
}