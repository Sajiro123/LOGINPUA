using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;


namespace LN.EntidadesLN
{
    public class ParaderoLN
    {
        private Object bdConn;
        private ParaderoAD paradero;

        public ParaderoLN()
        {
            paradero = new ParaderoAD(ref bdConn);
        }

        public List<CC_PARADERO> getParaderosByIdRuta(int id_ruta, ref string mensaje, ref int tipo)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = paradero.getParaderosByIdRuta(id_ruta);
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
        public List<CC_PARADERO> getParaderosByStrRecorrido(string strRecorrido, ref string mensaje, ref int tipo)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = paradero.getParaderosByStrRecorrido(strRecorrido);
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
        public RPTA_GENERAL registrarParadero( int idRecorrido, int idTipoParadero, int idVia, string nombre, string nombreEtiqueta, 
                                                    double distanciaParcial, double latitud, double longitud, int nroOrden, string usuarioReg, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = paradero.registrarParadero(idRecorrido, idTipoParadero, idVia, nombre, nombreEtiqueta,
                                                        distanciaParcial, latitud, longitud, nroOrden, usuarioReg);
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

        public RPTA_GENERAL anularParadero(int idParadero, string usuarioReg, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = paradero.anularParadero(idParadero, usuarioReg);
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

        public RPTA_GENERAL modificarParadero(int idParadero, string nombre, string etiquetaNombre,
                                                int idTipoParadero, double distanciaParcial,
                                                double latitud, double longitud, int idVia,int numero_orden,
                                                string usuarioModif, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = paradero.modificarParadero(idParadero, nombre, etiquetaNombre,
                                                    idTipoParadero, distanciaParcial,
                                                    latitud, longitud, idVia, numero_orden,
                                                    usuarioModif);
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

