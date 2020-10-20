using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Configuration;

namespace AD
{
    public class Conexion
    {
        public static OracleConnection iniciar(ref OracleConnection bdConn, Object _bdConn = null)
        {
            if (bdConn == null)
            {
                if (_bdConn != null)
                {
                    bdConn = (OracleConnection)_bdConn;
                }
                else
                {
                    bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
                }
            } 
            if (bdConn.State == ConnectionState.Closed)
            {

                bdConn.Open();
            }
            return bdConn;
        }

        public static void finalizar(ref Object bdConn)
        {
            try
            {
                if (bdConn != null)
                {
                    if (((OracleConnection)bdConn).State == ConnectionState.Open)
                    {
                        ((OracleConnection)bdConn).Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
