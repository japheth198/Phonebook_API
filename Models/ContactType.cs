using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARpe19TodoApp.Models
{
    public class ContactType
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
