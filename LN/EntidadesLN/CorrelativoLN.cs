using AD;
using AD.EntidadesAD;
using ENTIDADES;
using System;
using System.Collections.Generic;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class CorrelativoLN 
    {
        private CorrelativoAD _loginRepositorio;
        private Object bdConn;

        public CorrelativoLN()
        {
            _loginRepositorio = new CorrelativoAD(ref bdConn);
        }

        public List<CC_CORRELATIVO_INFORME> Listar_InfCorr(ref string mensaje, ref int tipo)
        {
            List<CC_CORRELATIVO_INFORME> resultado = new List<CC_CORRELATIVO_INFORME>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_InfCorr();
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

        public RPTA_GENERAL Registrar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Registrar_InfCorr(Model_Inf_Corr);
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

        public RPTA_GENERAL Editar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Editar_InfCorr(Model_Inf_Corr);
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

        public RPTA_GENERAL Actualizar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Actualizar_InfCorr(Model_Inf_Corr);
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

        public RPTA_GENERAL Anular_InfCorr(int idInfCorr, string usuarioAnula, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Anular_InfCorr(idInfCorr, usuarioAnula);
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


        public List<CC_CORRELATIVO_INFORME> Ultimo_InfCorr(ref string mensaje, ref int tipo)
        {
            List<CC_CORRELATIVO_INFORME> resultado = new List<CC_CORRELATIVO_INFORME>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Ultimo_InfCorr();
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
