using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARpe19TodoApp.Models.Dto.Response
{
    public class GetUserResponse 
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public List<ContactResponse> Contacts { get; set; }
    }
}
