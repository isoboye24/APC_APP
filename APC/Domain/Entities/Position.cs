using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Position
    {
        public int PositionId { get; private set; }
        public string PositionName { get; private set; }

        public Position(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Position name cannot be empty");

            PositionName = name.Trim();
        }

        public static Position Rehydrate(int id, string name)
        {
            return new Position(name)
            {
                PositionId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Position name cannot be empty");

            PositionName = newName.Trim();
        }

        public void SetId(int id)
        {
            PositionId = id;
        }
    }
}
