using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Columns
{
    public class Member
    {
        private int id;
        private string name;
        private string email;
        private DateTime membershipDate;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public DateTime MembershipDate { get => membershipDate; set => membershipDate = value; }

        public Member(int id, string name, string email, DateTime membershipDate)
        {
            ID = id;
            Name = name;
            Email = email;
            MembershipDate = membershipDate;
        }
    }
}
