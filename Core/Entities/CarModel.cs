using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CarModel
    {
        public int CarId { get; set; }
        public Car Car { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}