using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class RutasLN
    {
        private Object bdConn;
        private RutasAD rutas;
        public RutasLN()
        {
            rutas = new RutasAD(ref bdConn);
        }

        public RPTA_GENERAL registrarRuta(int idCorredor, string nombre, string nroRuta, string distritos, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutas.registrarRuta(idCorredor, nombre, nroRuta, distritos, usuarioRegistra);
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

        public RPTA_GENERAL registrarRecorrido(int idRuta, string sentido, string lado, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutas.registrarRecorrido(idRuta, sentido, lado, usuarioRegistra);
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
        
        public List<CC_RUTA> listarRutasByIdCorredor(int idCorredor, ref string mensaje, ref int tipo)
        {
            List<CC_RUTA> resultado = new List<CC_RUTA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutas.listarRutasByIdCorredor(idCorredor);
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

        public RPTA_GENERAL editarRuta(int idRuta, string nroRuta, string nombre, string distritos, string usuarioModifica, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutas.editar_Ruta(idRuta, nroRuta, nombre, distritos, usuarioModifica);
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

        public RPTA_GENERAL ActualizarRecorrido(int idRuta,string nombre, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutas.ActualizarRecorrido(idRuta, nombre);
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
        
        public RPTA_GENERAL anularRuta(int idRuta, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutas.anularRuta(idRuta, usuarioRegistra);
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
