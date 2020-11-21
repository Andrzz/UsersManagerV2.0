using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;

namespace UserManager.Data
{
    public class UserManagerContext : DbContext
    {
        public UserManagerContext (DbContextOptions<UserManagerContext> options)
            : base(options)
        {
        }

        public DbSet<UserManager.Models.UserViewModel> UserViewModel { get; set; }
    }
}
