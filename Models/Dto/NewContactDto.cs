using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARpe19TodoApp.Models.Dto
{
    public class NewContactDto
    {
        public int UserId { get; set; }
        public int ContactTypeId { get; set; }

        public string ContactValue { get; set; }
    }
}
