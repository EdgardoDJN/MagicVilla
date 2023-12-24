using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        //Toca pasarle el db al padre por eso hacemos : base(db)
        //Debido a que el hijo tiene db y el padre tambien
        public VillaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
