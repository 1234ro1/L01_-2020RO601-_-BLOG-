using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

namespace laboratorio1.Models
{
    public class blog : DbContext
    {
        public blog(DbContextOptions<blog> options) : base(options)
        {
        }
            
        public DbSet<usuarios>usuarios { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set; }
    }
}
