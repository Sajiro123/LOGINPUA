using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class Generar_pogLN
    {
        private Object bdConn;
        private Generar_pogAD _Generar_pogAD;

        public Generar_pogLN()
        {
            _Generar_pogAD = new Generar_pogAD(ref bdConn);
        }


        public void Limpiar_Temporal_POG(ref string mensaje, ref int tipo)
        {
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    _Generar_pogAD.Limpiar_Temporal_POG();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
            }
            Conexion.finalizar(ref bdConn);

        }

        public DataSet Listar_Franja_POG(ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Generar_pogAD.Listar_Franja_POG();
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
        public RPTA_GENERAL registrar_Maestro_POG(string fecha_programada, int TipoDia, int id_ruta, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = _Generar_pogAD.registrar_Maestro_POG(fecha_programada, TipoDia, id_ruta, usuario);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
          
            return resultado;
        }

        public RPTA_GENERAL registrar_Detalle_Maestro_POG(int id_maestro_pog, string p_franja_hi, string p_franja_hf, string t_viaje_prom_a, string t_viaje_prom_b, string intervalo_a, string intervalo_b, string v_promedio_a, string v_promedio_b, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = _Generar_pogAD.registrar_Detalle_Maestro_POG(id_maestro_pog, p_franja_hi, p_franja_hf, t_viaje_prom_a, t_viaje_prom_b, intervalo_a, intervalo_b, v_promedio_a, v_promedio_b, usuario);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
          
            return resultado;
        }

        public RPTA_GENERAL ValidarFecha_Programados(string fecha_programada, int id_ruta, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            var array_fecha = fecha_programada.Split('-');

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    foreach (var fecha_final in array_fecha)
                    {
                        Conexion.finalizar(ref bdConn);
                        resultado = _Generar_pogAD.ValidarFecha_Programados(fecha_final, id_ruta);
                        if (resultado.AUX > 0)
                        {
                            return resultado;
                        }


                    }
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
        public RPTA_GENERAL Eliminar_Maestro_POG(int id_maestro, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Generar_pogAD.Eliminar_Maestro_POG(id_maestro);
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

        public DataSet Listar_Data_Pog(int id_maestro_det, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Generar_pogAD.Listar_Data_Pog(id_maestro_det);
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

        public DataSet Listar_Recorrido(ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Generar_pogAD.Listar_Recorrido();
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

        //public DataSet Get_IdMaestro_Pog(int id_ruta,string fecha, ref string mensaje, ref int tipo)
        //{
        //    DataSet resultado = new DataSet();
        //    try
        //    {
        //        if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
        //        {
        //            resultado = _Generar_pogAD.Get_IdMaestro_Pog(id_ruta,fecha);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = ex.Message;
        //        tipo = 0;
        //        resultado = null;
        //    }
        //    Conexion.finalizar(ref bdConn);
        //    return resultado;

        //}

    }
}

