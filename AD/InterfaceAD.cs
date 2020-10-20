using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD
{
    public interface InterfaceAD<T> where T : class
    {
        IEnumerable<T> Datos(int activo);
        T Insertar(T modelo);
        dynamic Modificar(T modelo);
        int Eliminar(int id);
        T ObtenerPorCodigo(int id);
    }
}
