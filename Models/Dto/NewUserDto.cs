using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARpe19TodoApp.Models.Dto
{
    public class NewUserDto
    {
        public String firstName { get; set; }
        public String lastName { get; set; }

        public int ContactTypeId { get; set; }

        public string ContactValue { get; set; }

      
    }
}
