using FSVentasCoreAs.Models;
using FSVentasCoreAs.Models.Dirreciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.DAL
{
    public class FSVentasCoreDb : DbContext
    {
        public FSVentasCoreDb() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = adolfosanchez.database.windows.net; Initial Catalog = FSVentasCoreDb; Integrated Security = False; User ID = Themaster.56; Password = ASM199411056asm; Connect Timeout = 15; Encrypt = True; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<TipoUsuarios> TipoUsuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<CategoriasArticulos> CategoriasArticulos { get; set; }
        public DbSet<MarcasArticulos> MarcasArticulos { get; set; }
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<Provincias> Provincias { get; set; }
        public DbSet<Municipios> Municipios { get; set; }
        public DbSet<DistritosMunicipales> DistritosMunicipales { get; set; }
        public DbSet<Sectores> Sectores { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<VentasDetalles> VentasDetalles { get; set; }
    }
}
