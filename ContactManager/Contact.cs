using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class Contact
    {
        public enum GenderType { male, female }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public GenderType Gender { get; set; }

        public Contact(string name, string surname, string mail, string phone, GenderType gender)
        {
            Name = name;
            Surname = surname;
            Email = mail;
            Phone = phone;
            Gender = gender;
        }
        public Contact()
        {
            Name = null;
        }
    }
}
