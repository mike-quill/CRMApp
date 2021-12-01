using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Utility
{
    public static class PermissionLevelHelper
    {
        public static readonly SortedDictionary<int, string> PermissionLevelToRole = new SortedDictionary<int, string>()
        {
            { 0, "Employee" },
            { 1, "Supervisor" },
            { 2, "Admin" }
        };

        public static string GetRole(int permissionLevel)
        {
            return PermissionLevelToRole[permissionLevel];
        }

        public static int GetPermissionLevel(string role)
        {
            return PermissionLevelToRole.FirstOrDefault(x => x.Value == role).Key;
        }
    }
}