using APC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Month
    {
        public int MonthId { get; private set; }
        public string MonthName { get; private set; }

        public Month(string name)
        {
            MonthName = name;
        }

        public static Month Rehydrate(int id, string name)
        {
            return new Month(name)
            {
                MonthId = id
            };
        }

        public void SetId(int id)
        {
            MonthId = id;
        }
    }
}
