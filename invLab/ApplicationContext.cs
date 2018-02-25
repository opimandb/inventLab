using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace invLab
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Camera> Cameras { get; set; }
    }
}
