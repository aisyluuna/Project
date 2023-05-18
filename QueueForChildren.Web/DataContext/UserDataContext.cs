using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Identity;

namespace QueueForChildren.Web.DataContext
{
    public class UserDataContext : IdentityDbContext<User>
    {
        public UserDataContext(DbContextOptions<UserDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
