using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Dtos
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ModelDto> Models { get; set; } = new List<ModelDto>();
        public List<CarToReturnDto> Cars { get; set; } = new List<CarToReturnDto>();
    }
}