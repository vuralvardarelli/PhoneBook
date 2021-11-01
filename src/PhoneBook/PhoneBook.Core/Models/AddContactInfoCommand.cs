using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Models
{
    public class AddContactInfoCommand
    {
        public int RecordId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
