using System;
using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class RegistroPicoPlacaLN
    {
        private RegistroPicoPlacaAD registroPicoplaca;
        private Object bdConn;

        public RegistroPicoPlacaLN()
        {
            registroPicoplaca = new RegistroPicoPlacaAD(ref bdConn);
        }
        public List<CC_REPORTE_PICO_PLACA> getReporte_Comparativo(string fechaInicio, int id_ruta, string turno, string ida, string vuelta, ref string mensaje, ref int tipo)
        {
            List<CC_REPORTE_PICO_PLACA> resultado = new List<CC_REPORTE_PICO_PLACA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = registroPicoplaca.getReporte_Comparativo(fechaInicio, id_ruta, turno, ida, vuelta);
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

        public List<CC_REPORTE_PICO_PLACA> getHora_Comparativo(string fechaInicio, int id_ruta, string turno, ref string mensaje, ref int tipo)
        {
            List<CC_REPORTE_PICO_PLACA> resultado = new List<CC_REPORTE_PICO_PLACA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = registroPicoplaca.getHora_Comparativo(fechaInicio, id_ruta, turno);
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

        public RPTA_GENERAL registrarVelocidadPPlaca(int idRuta,
                                                    int idEstado,
                                                    string turno,
                                                    string fechaRegistro,
                                                    string horaInicio,
                                                    string horaFin,
                                                    double velocidadPromedio_ab,
                                                    double velocidadPromedio_ba,
                                                    double distancia_A,
                                                    double distancia_B,
                                                    double tiempo_A,
                                                    double tiempo_B,
                                                    string observacion,
                                                    string nomUsuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = registroPicoplaca.registrarPicoPlaca(idRuta,
                                                                idEstado,
                                                                turno,
                                                                fechaRegistro,
                                                                horaInicio,
                                                                horaFin,
                                                                velocidadPromedio_ab,
                                                                velocidadPromedio_ba,
                                                                distancia_A,
                                                                distancia_B,
                                                                tiempo_A,
                                                                tiempo_B,
                                                                observacion,
                                                                nomUsuario);
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

        public RPTA_GENERAL anularRegistrosPorFecha(string fechaRegistro, int idRuta, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = registroPicoplaca.anularRegistrosPorFecha(fechaRegistro, idRuta);
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

        public CC_RECORRIDO getKmPorLadoYRecorrido(int idRuta, string lado, ref string mensaje, ref int tipo)
        {
            CC_RECORRIDO resultado = new CC_RECORRIDO();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = registroPicoplaca.getKmPorLadoYRecorrido(idRuta, lado);
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

        public List<CC_REPORTE_PICO_PLACA> getReportePicoPlacaByFechas(string fechaInicio, string fechaFin, int idRuta, ref string mensaje, ref int tipo)
        {
            List<CC_REPORTE_PICO_PLACA> resultado = new List<CC_REPORTE_PICO_PLACA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = registroPicoplaca.getReportePicoPlacaByFechas(fechaInicio, fechaFin, idRuta);
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
