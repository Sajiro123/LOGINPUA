using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;


namespace LN.EntidadesLN
{
    public class TipoServicioLN
    {
        private Object bdConn;
        private TipoServicioAD tipoServicio;
        public TipoServicioLN()
        {
            tipoServicio = new TipoServicioAD(ref bdConn);
        }

        public List<CC_RUTA_TIPO_SERVICIO> getRutaTipoDeServicioOper(int id_ruta, ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.getRutaTipoDeServicioOper(id_ruta);
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

        public List<CC_RUTA_TIPO_SERVICIO> getTiposServicio(ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.getTiposServicio();
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

        public List<CC_RUTA_TIPO_SERVICIO> getTiposServicioOperacional(ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.getTiposServicioOperacional();
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

        public RPTA_GENERAL registrarRutaTipoServicio(int idRuta, int idTipoServicio, int idTipoServicioOper, string nombre,string color, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.registrarRutaTipoServicio(idRuta, idTipoServicio, idTipoServicioOper, nombre, color, usuarioRegistra);
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

        public RPTA_GENERAL editarRutaTservic(int idRutaTipoServ, int idTipoServicio, int idTipoServicioOper, string nombre,string color, string usuarioModifica, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.editarRutaTservic(idRutaTipoServ, idTipoServicio, idTipoServicioOper, nombre, color, usuarioModifica);
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

        public RPTA_GENERAL anularRutaTipoServicio(int idRutaServicioOperacional, string usu_anula, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.anularRutaTipoServicio(idRutaServicioOperacional, usu_anula);
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

        public RPTA_GENERAL registraRecorridoServicioOperacional(int idRutaTipoServicio, string lado, string sentido, string usuarioReg, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.registraRecorridoServicioOperacional(idRutaTipoServicio, lado, sentido, usuarioReg);
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

        public RPTA_GENERAL actualizarParaderoTservic(int idParaderoTipoServicio, int idEstado, string usu_modif, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.actualizarParaderoTservic(idParaderoTipoServicio, idEstado, usu_modif);
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

        public List<CC_PARADERO> getParaderoByIdRecorridTServ(int idRecorridoTipoServicio, ref string mensaje, ref int tipo)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoServicio.getParaderoByIdRecorridTServ(idRecorridoTipoServicio);
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
