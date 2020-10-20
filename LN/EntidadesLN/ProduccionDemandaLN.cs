using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using System.IO;
using SpreadsheetLight;
using LOGINPUA.Util;


namespace LN.EntidadesLN
{
    public class ProduccionDemandaLN
    {
        private Object bdConn;
        private ProduccionDemandaAD _PasajeroValidadacion;

        public ProduccionDemandaLN()
        {
            _PasajeroValidadacion = new ProduccionDemandaAD(ref bdConn);
        }



        public RPTA_GENERAL nombre_excel(int id, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.nombre_excel(id);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }


        public RPTA_GENERAL ValidarFecha_Programados(string fecha, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.ValidarFecha_Programados(fecha);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }




        public RPTA_GENERAL eliminar_Archivo(int id, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.eliminar_Archivo(id);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }
        public RPTA_GENERAL RegistroInforme(string nombre, string fecha, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.RegistroInforme(nombre, fecha, SessionHelper.user_login);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }



        public DataSet Listar_Informes(int mes, int año, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.Listar_Informes(mes, año);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

    }
}

