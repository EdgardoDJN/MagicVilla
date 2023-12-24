using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    //Interfaz generica
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);

        //? Para que no sea obligatorio, si se envia un filtro se devuelve toda la lista sino el filtro
        Task<List<T>> ObtenerTodos(Expression<Func<T,bool>>? filtro = null);

        //Se nos puede presentar el error que estamos trabajando con el mismo registro y justamente lo queremos modificar
        //tracked nos permite saber si el registro esta siendo modificado por otro usuario
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked=true);

        Task Remover(T entidad);

        Task Grabar();
    }
}
