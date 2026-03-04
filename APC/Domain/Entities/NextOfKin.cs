using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class NextOfKin
    {
        public int NextOfKinId { get; private set; }
        public string NextOfKinName { get; private set; }

        public NextOfKin(string name)
        {
            NextOfKinName = name;
        }

        public static NextOfKin Rehydrate(int id, string name)
        {
            return new NextOfKin(name)
            {
                NextOfKinId = id
            };
        }

        public void SetId(int id)
        {
            NextOfKinId = id;
        }
    }
}
