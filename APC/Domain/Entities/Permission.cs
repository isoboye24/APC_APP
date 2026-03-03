using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; private set; }
        public string PermissionName { get; private set; }


        public Permission(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Permission name cannot be empty");

            PermissionName = name.Trim();
        }

        public static Permission Rehydrate(int id, string name)
        {
            return new Permission(name)
            {
                PermissionId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Permission name cannot be empty");

            PermissionName = newName.Trim();
        }

        public void SetId(int id)
        {
            PermissionId = id;
        }
    }
}
