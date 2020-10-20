using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class PersonaLN
    {
        private Object bdConn;
        private PersonaAD persona;
 
        public PersonaLN()
        {
            persona = new PersonaAD(ref bdConn);
        }

        public List<CC_TIPODOCUMENTO> ListarTipoDocumento(ref string mensaje, ref int tipo)
        {
            List<CC_TIPODOCUMENTO> resultado = new List<CC_TIPODOCUMENTO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.ListarTipoDocumento();
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

        public List<CC_ROL> listarTipoRol(ref string mensaje, ref int tipo)
        {
            List<CC_ROL> resultado = new List<CC_ROL>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.listarTipoRol();
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

        public RPTA_GENERAL registrarPersona(string nombre, string apepaterno, string apematerno, string numdocu, int tipodocu, string correo, int tiporol, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.registrarPersona(nombre, apepaterno, apematerno, numdocu, tipodocu, correo, tiporol, usuario);
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

        public List<CC_PERSONA> getlistaPersonas(ref string mensaje, ref int tipo)
        {
            List<CC_PERSONA> resultado = new List<CC_PERSONA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.getlistaPersonas();
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
        public RPTA_GENERAL ModificarPersonas(int idpersona, string nombre, string apepaterno, string apematerno, int numdocu, int tipodocu, string correo, int tiporol, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.ModificarPersonas(idpersona, nombre, apepaterno, apematerno, numdocu, tipodocu, correo, tiporol, usuario);
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

        public RPTA_GENERAL EliminarPersonas(int idpersona, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.EliminarPersonas(idpersona, usuario);
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

        public RPTA_GENERAL EditarDatosPersonas(int idpersona, string nombre, string apepaterno, string apematerno, string numdocu, string correo,string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = persona.EditarDatosPersonas(idpersona, nombre, apepaterno, apematerno, numdocu, correo, usuario);
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

