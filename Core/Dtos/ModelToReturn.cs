using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class ModelToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<CarToReturnDto> Cars { get; set; } = new List<CarToReturnDto>();

    }
}