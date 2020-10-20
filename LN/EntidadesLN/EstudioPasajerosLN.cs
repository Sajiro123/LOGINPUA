using AD;
using AD.EntidadesAD;
using ENTIDADES;
using System;
using System.Collections.Generic;
using LOGINPUA.Util;
using System.Data;

namespace LN.EntidadesLN
{
    public class EstudioPasajerosLN
    {
        private EstudioPasajerosAD _loginRepositorio;
        private Object bdConn;

        public EstudioPasajerosLN()
        {
            _loginRepositorio = new EstudioPasajerosAD(ref bdConn);
        }

        public RPTA_GENERAL registroPasajeros_Campo(CC_ESTUDIO_PASAJERO ModelPasajerosCampo, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.registroPasajeros_Campo(ModelPasajerosCampo);
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

        public RPTA_GENERAL registroPasajeros_OrigenDestino(int id_corredor, int id_ruta, int id_persona, int id_paradero_orig, int id_paradero_dest, int id_tarjeta, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.registroPasajeros_OrigenDestino(id_corredor, id_ruta, id_persona, id_paradero_orig,
                                        id_paradero_dest, id_tarjeta, usuario);
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
        public RPTA_GENERAL registroPasajeros_Colectivo(int id_corredor, int id_ruta, int id_paradero, string tipo_vehiculo, int suben, int bajan, string placa, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.registroPasajeros_Colectivo(id_corredor, id_ruta, id_paradero, tipo_vehiculo,
                                        suben, bajan, placa, usuario);
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

        public List<CC_ESTUDIO_PASAJERO> Listar_Oferta_Demada(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin, ref string mensaje, ref int tipo)
        {
            List<CC_ESTUDIO_PASAJERO> resultado = new List<CC_ESTUDIO_PASAJERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Oferta_Demada(id_ruta, fechaConsultaInicio, fechaConsultaFin);
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

        public List<CC_ESTUDIO_PASAJERO> Listar_Origen_Destino(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin, ref string mensaje, ref int tipo)
        {
            List<CC_ESTUDIO_PASAJERO> resultado = new List<CC_ESTUDIO_PASAJERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Origen_Destino(id_ruta, fechaConsultaInicio, fechaConsultaFin);
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

        public List<CC_ESTUDIO_PASAJERO> Listar_Colectivo(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin, ref string mensaje, ref int tipo)
        {
            List<CC_ESTUDIO_PASAJERO> resultado = new List<CC_ESTUDIO_PASAJERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Colectivo(id_ruta, fechaConsultaInicio, fechaConsultaFin);
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

        public DataSet Listar_Reporte_Dias(int id_ruta, string inicio, string fin, string tipo_estado, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Reporte_Dias(id_ruta, inicio, fin, tipo_estado, ref mensaje, ref tipo);
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
        public DataSet Listar_Reporte_Paraderos(int id_ruta, string fecha, string tipo_estado,string lado, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Reporte_Paraderos(id_ruta, fecha, tipo_estado, lado, ref mensaje, ref tipo);
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
