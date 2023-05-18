using QueueForChildren.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Web.Extensions
{
    public static class UserExtensions
    {
        public static bool IsParentNull(this User user)
        {
            return !user.ParentId.HasValue;
        }
    }
}
