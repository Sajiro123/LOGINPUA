using AD;
using AD.EntidadesAD;
using ENTIDADES;
using System;
using System.Collections.Generic;
 using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class BUSESLN /*: InterfaceLN<CC_BUSES>*/
    {
        private BusesAD _loginRepositorio;
        private Object bdConn;

        public BUSESLN()
        {
            _loginRepositorio = new BusesAD(ref bdConn);
        }
        public CC_BUSES Consultar_x_placa(string placa, ref string mensaje, ref int tipo)
        {
            CC_BUSES resultado = new CC_BUSES();
            try
            {
                 
                    resultado = _loginRepositorio.Consultar_x_placa(placa);
                
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

        public List<CC_BUSES> Listar_Buses(ref string mensaje, ref int tipo)
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Buses();
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

        public List<CC_BUSES> Listar_Buses_by_Id(int idCorredor, ref string mensaje, ref int tipo)
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Buses_by_Id(idCorredor);
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
        public List<CC_BUSES> Listar_Buses_by_IdRuta(int idCorredor, ref string mensaje, ref int tipo)
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Buses_by_Id(idCorredor);
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

        public RPTA_GENERAL Insertar_Buses_Nuevos(CC_BUSES modelo, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = _loginRepositorio.Insertar_Buses_Nuevos(modelo);
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

        public RPTA_GENERAL anularBus(int idBus, string usuarioAnula, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.anularBus(idBus, usuarioAnula);
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

        public void LimpiarTabla(ref string mensaje, ref int tipo)
        {
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    _loginRepositorio.LimpiarTabla();
                }
            }
            catch (Exception ex)
            {
            }
            Conexion.finalizar(ref bdConn);
        }

        public RPTA_GENERAL Modificar_Bus(CC_BUSES modelo, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Modificar_Bus(modelo);
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


        public RPTA_GENERAL desafectarBus(int id_placa_reemplazada, string placa_nueva, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.desafectarBus(id_placa_reemplazada, placa_nueva);
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

        public List<CC_BUSES> Listar_Buses_Desafectados(ref string mensaje, ref int tipo)
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Buses_Desafectados();
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

        public RPTA_GENERAL Insertar_Buses_Afectados(CC_BUSES modelo, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Insertar_Buses_Afectados(modelo);
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

        public List<BUSES_DESPACHO> FIltrar_Archivo_Despacho(string fecha, int id_corredor, ref string mensaje, ref int tipo)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.FIltrar_Archivo_Despacho(fecha, id_corredor);
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

        public void Cambiar_Estado_Bus(string placa, int cod_despacho, ref string mensaje, ref int tipo)
        {
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    _loginRepositorio.Cambiar_Estado_Bus(placa, cod_despacho);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
            }
            Conexion.finalizar(ref bdConn);
        }
      

        public RPTA_GENERAL Verifica_Placa_Existente(string placa, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Verifica_Placa_Existente(placa);
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
 
        public List<CC_CONDUCTORES> Listar_Placa(ref string mensaje, ref int tipo)
        {
            List<CC_CONDUCTORES> resultado = new List<CC_CONDUCTORES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Placa();
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

        //public int Eliminar(CC_BUSES modelo)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Insertar(CC_BUSES modelo)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Modificar(CC_BUSES modelo)
        //{
        //    throw new NotImplementedException();
        //}

        //List<CC_BUSES> InterfaceLN<CC_BUSES>.Datos(int activo)
        //{
        //    throw new NotImplementedException();
        //}

        //CC_BUSES InterfaceLN<CC_BUSES>.ObtenerPorCodigo(int id)
        //{
        //    throw new NotImplementedException();
        //}
        public CC_BUSES Estado_Vigencias(string placa, ref string mensaje, ref int tipo)
        {
            CC_BUSES resultado = new CC_BUSES();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Estado_Vigencias(placa);
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
