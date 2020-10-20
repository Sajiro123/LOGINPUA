using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        int BorrarDapper(T modelo);
        int ActualizarDapper(T modelo);
        int InsertarDapper(T modelo);
        IEnumerable<T> ObtenerListadoDapper();
        T ObtenerPorCodigoDapper(int id);
    }
}
