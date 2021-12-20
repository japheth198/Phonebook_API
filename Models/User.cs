using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARpe19TodoApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }

        public ICollection<Contact> Contacts{ get; set; }
    }
}
