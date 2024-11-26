using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data.Models;
using Proyecto_Final.Domain.Enum;

namespace Proyecto_Final.Data.Contexto;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Taxistas> Taxistas { get; set; }
    public DbSet<Viajes> Viajes { get; set; }
    public DbSet<Imagen> Imagenes { get; set; }
    public DbSet<Reservaciones> Reservaciones { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Taxistas>().HasData(new List<Taxistas>()
        {
        new Taxistas()
            {TaxistaId = 1,
            ExisteLicencia = true,
            ExisteVehiculo = true,
            NickName = "Federico Liranzo",
            FechaNacimiento = new DateTime(1985, 4, 15),
            Correo = "federicoLiranzo01@gmail.com",
            Password = "elcompa1985",
            Role = Roles.Empleado,
            Status = EstadosTaxistas.Disponible,
            },
        new Taxistas()
        {
            TaxistaId = 2,
            ExisteLicencia = true,
            ExisteVehiculo = true,
            NickName = "Cesar Polanco",
            FechaNacimiento = new DateTime(1995, 7, 25),
            Correo = "cesarpolanco1@gmail.com",
            Password = "sorollo1995",
            Role = Roles.Empleado,
            Status = EstadosTaxistas.Disponible,
        },
        new Taxistas()
        {
            TaxistaId = 3,
            ExisteLicencia = true,
            ExisteVehiculo = false,
            NickName = "Martin Perez",
            FechaNacimiento = new DateTime(1990, 12, 5),
            Correo = "martinperez90@gmail.com",
            Password = "eltino1990",
            Role = Roles.Empleado,
            Status = EstadosTaxistas.Disponible,
        },
        new Taxistas()
        {
            TaxistaId = 4,
            ExisteLicencia = false,
            ExisteVehiculo = true,
            NickName = "Juan Rodríguez",
            FechaNacimiento = new DateTime(1987, 3, 17),
            Correo = "juanrodriguez87@gmail.com",
            Password = "rapido1987",
            Status = EstadosTaxistas.Disponible,
            Role = Roles.Empleado,
        },
        new Taxistas()
        {
            TaxistaId = 5,
            ExisteLicencia = true,
            ExisteVehiculo = true,
            NickName = "Carlos Herrera",
            FechaNacimiento = new DateTime(1992, 8, 30),
            Correo = "carlosherrera92@gmail.com",
            Password = "chico1992",
            Status = EstadosTaxistas.Disponible,
            Role = Roles.Empleado,
        }
        });
        modelBuilder.Entity<Viajes>().HasData(new List<Viajes>()
        {
            new Viajes() {
                ViajeId = 1,
                TaxistaId = 1,
                Destino = " Bania de las aguilas",
                Estado = EstadosViajes.EnCurso,
                Fecha = DateTime.Now,
                Precio = 2500,
                personas = 5,
                ClienteId = 2,
            },
             new Viajes() {
                ViajeId = 2,
                TaxistaId = 3,
                Destino = "Punta Cana",
                Estado = EstadosViajes.EnCurso,
                Fecha = DateTime.Now,
                Precio = 3500,
                personas = 5,
                ClienteId = 2,

              }
        });

        modelBuilder.Entity<Imagen>().HasData(new List<Imagen>
        {
            new Imagen { ImagenId = 1, ImagenUrl = "Images/Bahia-Aguilas.jpg", Alt = "Imagen 1", Titulo="Bahia de las aguilas", ViajeId = 1 },
            new Imagen { ImagenId = 2, ImagenUrl = "Images/camino-rocoso.jpg", Alt = "Imagen 2", Titulo="Camino roscoso", ViajeId = 1 },
            new Imagen { ImagenId = 3, ImagenUrl = "Images/Punta-Cana.jpg", Alt = "Imagen 3",  Titulo="Punta cana", ViajeId = 2 }
        });
    }
}
