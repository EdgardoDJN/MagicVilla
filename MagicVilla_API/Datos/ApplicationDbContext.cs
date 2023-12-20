using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext:DbContext
    {
        //Le vamos a mandar mediante inyección de dependencias la configuración que tengamos en el servicio
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        //Detallamos los modelos que queremos que se creen en la base de datos
        //Normalmente se coloca el nombre en plural
        public DbSet<Villa> Villas { get; set; }


        //Cuando se ejecuta el comando add-migration
        //Estos se graban en nuestra base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Nombre = "Villa 1",
                    Detalle="Villa 1",
                    Tarifa=100,
                    Ocupantes = 4,
                    MetrosCuadrados = 100,
                    ImagenUrl="",
                    Amenidad="Amenidad 1",
                    FechaCreacion= DateTime.Now,
                    FechaActualizacion= DateTime.Now
                },
                               new Villa
                               {
                                   Id = 2,
                                   Nombre = "Villa 2",
                                   Detalle = "Villa 2",
                                   Tarifa = 150,
                                   Ocupantes = 6,
                                   MetrosCuadrados = 150,
                                   ImagenUrl = "",
                                   Amenidad = "Amenidad 2",
                                   FechaCreacion = DateTime.Now,
                                   FechaActualizacion = DateTime.Now
                               },
                                              new Villa
                                              {
                                                  Id = 3,
                                                  Nombre = "Villa 3",
                                                  Detalle = "Villa 3",
                                                  Tarifa = 200,
                                                  Ocupantes = 8,
                                                  MetrosCuadrados = 200,
                                                  ImagenUrl = "",
                                                  Amenidad = "Amenidad 3",
                                                  FechaCreacion = DateTime.Now,
                                                  FechaActualizacion = DateTime.Now
                                              });
        }
    }
}
