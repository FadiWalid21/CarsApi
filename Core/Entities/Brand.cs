using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public List<Model> Models { get; set; } = new List<Model>();
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}