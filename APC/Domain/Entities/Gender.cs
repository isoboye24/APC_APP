using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Gender
    {

        public int GenderId { get; private set; }
        public string GenderName { get; private set; }

        public Gender(string name)
        {
            GenderName = name;
        }

        public static Gender Rehydrate(int id, string name)
        {
            return new Gender(name)
            {
                GenderId = id
            };
        }

        public void SetId(int id)
        {
            GenderId = id;
        }
    }
}
