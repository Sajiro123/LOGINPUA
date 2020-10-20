using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LN
{
    public interface InterfaceLN<T> where T : class
    {
        List<T> Datos(int activo);
        int Eliminar(T modelo);
        int Modificar(T modelo);
        int Insertar(T modelo);
        T ObtenerPorCodigo(int id);
    }
}
