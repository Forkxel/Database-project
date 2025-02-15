using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
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
            this.ID = id;
            this.Name = name;
            this.Email = email;
            this.MembershipDate = membershipDate;
        }
    }
}
