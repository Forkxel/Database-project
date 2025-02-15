using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Member : Methods<Member>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MembershipDate { get; set; }

        public Member(int id, string name, string email, DateTime membershipDate)
        {
            ID = id;
            Name = name;
            Email = email;
            MembershipDate = membershipDate;
        }

        public Member()
        {
        }

        public void InsertData(Member element)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(Member element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Member element)
        {
            throw new NotImplementedException();
        }
    }
}
