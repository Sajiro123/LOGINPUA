using AD;
using AD.EntidadesAD;
using ENTIDADES;
using System;
using System.Collections.Generic;
using LOGINPUA.Util;
using System.Data;
using System.IO;
using System.Web;

namespace LN.EntidadesLN
{
    public class IncidenciasLN
    {
        private IncidenciasAD _loginRepositorio;
        private Object bdConn;

        public IncidenciasLN()
        {
            _loginRepositorio = new IncidenciasAD(ref bdConn);
        }

        public DataSet Listar_Incidencias(int id_ruta, ref string mensaje, ref int tipo)
        {
            var DataList = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    DataList = _loginRepositorio.Listar_Incidencias(id_ruta);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            Conexion.finalizar(ref bdConn);
            return DataList;
        }


        public RPTA_GENERAL AgregarIncidencia(CC_INCIDENCIA Model_Incidencia, string ruta_base, string carpeta, ref string mensaje, ref int tipo)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int codigo_archivo = 0;

            string fotoNombreGeneral = "";
            string fotoNombre1 = "";
            string fotoNombre2 = "";
            string fotoNombre3 = "";

            //Insertando los archivos de la carpeta en variables
            foreach (var archivo in System.IO.Directory.GetFiles(carpeta))
            {
                codigo_archivo = r.Next();
                string nombre_file1 = Path.GetFileName(archivo).Replace(" ", "");
                fotoNombreGeneral = codigo_archivo + "_1_" + nombre_file1;
                string r1 = Path.Combine(ruta_base, fotoNombreGeneral);
                File.Copy(archivo, r1);

                if (fotoNombre1 == "")
                {
                    fotoNombre1 = nombre_file1;
                }
                else if (fotoNombre2 == "")
                {
                    fotoNombre2 = nombre_file1;
                }
                else if (fotoNombre3 == "")
                {
                    fotoNombre3 = nombre_file1;
                }

            }

            //INSERTAR EL FORMATO COMPLETO
            var data = Registrar_Incidencia(Model_Incidencia, ref mensaje, ref tipo, fotoNombre1, fotoNombre2, fotoNombre3);

            //Limpiando la carpeta despues del registro
            foreach (var archivo in Directory.GetFiles(carpeta))
            {
                File.Delete(archivo);
            }
            Directory.Delete(carpeta);

            return data;
        }

        public RPTA_GENERAL Registrar_Incidencia(CC_INCIDENCIA Model_Incidencia, ref string mensaje, ref int tipo, string fotoNombre1, string fotoNombre2, string fotoNombre3)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Registrar_Incidencia(Model_Incidencia, fotoNombre1, fotoNombre2, fotoNombre3);
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

        public RPTA_GENERAL Actualizar_Incidencia(CC_INCIDENCIA Model_Incidencia, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Actualizar_Incidencia(Model_Incidencia);
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

        public RPTA_GENERAL Editar_Incidencia(CC_INCIDENCIA Model_Incidencia, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Editar_Incidencia(Model_Incidencia);
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

        public RPTA_GENERAL Estado_Incidencia(int id_incidencias, string estado, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Estado_Incidencia(id_incidencias, estado);
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



        public RPTA_GENERAL AnularIncidencia(int idIncidencia, string usuarioAnula, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.AnularIncidencia(idIncidencia, usuarioAnula);
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

        public List<CC_CONCESIONARIOS> ListarConcesionarios(int id_corredor, ref string mensaje, ref int tipo)
        {
            List<CC_CONCESIONARIOS> resultado = new List<CC_CONCESIONARIOS>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.ListarConcesionarios(id_corredor);
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

        public List<CC_INFRACCION> Listar_Infracciones(ref string mensaje, ref int tipo)
        {
            List<CC_INFRACCION> resultado = new List<CC_INFRACCION>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Infracciones();
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

        public List<CC_PERSONA_INCIDENCIA> Listar_Persona_Incidencia(ref string mensaje, ref int tipo)
        {
            List<CC_PERSONA_INCIDENCIA> resultado = new List<CC_PERSONA_INCIDENCIA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Persona_Incidencia();
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

        public RPTA_GENERAL Registrar_Infraccion(CC_INFRACCION Model_Infraccion, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Registrar_Infraccion(Model_Infraccion);
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


        public RPTA_GENERAL Editar_Infraccion(CC_INFRACCION Model_Infraccion, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Editar_Infraccion(Model_Infraccion);
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
