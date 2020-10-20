using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AD.ConexionSQL
{
    public class Conexion
    {
        public string CadenaConexion = "";
        public DataSet CargarDs(string Sql, SqlParameter[] Parametros)
        {
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings[CadenaConexion].ConnectionString);
            DataSet Ds = new DataSet();
            SqlDataAdapter Da;
            try
            {
                Da = new SqlDataAdapter(Sql, Con);
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.AddRange(Parametros);
                Da.SelectCommand.CommandTimeout = 1000;
                Da.Fill(Ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                Con.Dispose();
                Con.Close();
            }
            return Ds;
        }
        public string Cargar(string Sql, SqlParameter[] Parametros, string SeparadorColumna = "|", string SeparadorFila = "*")
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaConexionSistemaRecaudo"].ConnectionString);
            StringBuilder Constructor = new StringBuilder();
            DataSet Ds = new DataSet();
            //SqlDataAdapter Da;
            try
            {
                Ds = CargarDs(Sql, Parametros);
                foreach (DataTable Dt in Ds.Tables)
                {
                    Constructor.Append(Serializar(Dt, SeparadorColumna, SeparadorFila) + "^");
                }
                if (Constructor.Length > 0)
                {
                    Constructor.Remove(Constructor.Length - 1, 1);
                }
            }
            catch
            {
                throw;
            }
            return Constructor.ToString();
        }
        public string Serializar(DataTable Dt, string SeparadorColumna, string SeparadorFila)
        {
            StringBuilder Constructor = new StringBuilder();
            int CantFilas = Dt.Rows.Count;
            int CantColumnas = Dt.Columns.Count;
            foreach (DataRow Fila in Dt.Rows)
            {
                foreach (DataColumn Columna in Dt.Columns)
                {
                    Constructor.Append(Fila[Columna.ToString()] + SeparadorColumna);
                }
                Constructor.Remove(Constructor.Length - 1, 1).Append(SeparadorFila);
            }
            if (Constructor.Length > 0)
            {
                Constructor.Remove(Constructor.Length - 1, 1);
            }
            return Constructor.ToString();
        }

        public DataTable HaciaDataTable<T>(List<T> lista)
        {
            DataTable dt = new DataTable(typeof(T).Name);

            //Obtener todos las propiedades
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Agregar propiedades a columnas
                dt.Columns.Add(prop.Name);
            }
            foreach (T item in lista)
            {
                var valor = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //insertar valores de propiedad
                    valor[i] = Props[i].GetValue(item, null);
                }
                dt.Rows.Add(valor);
            }
            return dt;
        }

        public DataSet CargarTran(string Sql, SqlParameter[] Parametros, SqlConnection Con, SqlTransaction Tran)
        //public DataSet CargarTran(string Sql, SqlParameter[] Parametros)
        {
            //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaConexionSistemaRecaudo"].ConnectionString);
            DataSet Ds = new DataSet();
            SqlDataAdapter Da;
            try
            {
                Da = new SqlDataAdapter(Sql, Con);
                Da.SelectCommand.Transaction = Tran;
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.AddRange(Parametros);
                Da.Fill(Ds);
            }
            catch
            {
                throw;
            }
            return Ds;
        }

        public string Cargar_SalidaUnidad(string Sql, SqlParameter[] Parametros, string SeparadorColumna = "|", string SeparadorFila = "*")
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString);
            StringBuilder Constructor = new StringBuilder();
            DataSet Ds = new DataSet();
            //SqlDataAdapter Da;
            try
            {
                Ds = CargarDsMapa(Sql, Parametros);
                foreach (DataTable Dt in Ds.Tables)
                {
                    Constructor.Append(Serializar(Dt, SeparadorColumna, SeparadorFila) + "^");
                }
                if (Constructor.Length > 0)
                {
                    Constructor.Remove(Constructor.Length - 1, 1);
                }
            }
            catch
            {
                throw;
            }
            return Constructor.ToString();
        }
        public DataSet CargarDsMapa(string Sql, SqlParameter[] Parametros)
        {
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString);

            DataSet Ds = new DataSet();
            SqlDataAdapter Da;
            try
            {
                Da = new SqlDataAdapter(Sql, Con);
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.AddRange(Parametros);
                Da.Fill(Ds);
            }
            catch
            {
                throw;
            }
            return Ds;
        }
    }
}
