using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARpe19TodoApp.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public User User { get; set; }
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
