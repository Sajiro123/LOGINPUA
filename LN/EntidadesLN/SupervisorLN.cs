using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using Oracle.DataAccess.Client;
using System.Web;
using System.Linq;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class SupervisorLN
    {
        private Object bdConn;
        private SupervisorAD _SupervisorAD;

        public SupervisorLN()
        {
            _SupervisorAD = new SupervisorAD(ref bdConn);
        }

        public List<BUSES_DESPACHO> Lista_Conceptos_Exterior(string id_maestro, string accion, ref string mensaje, ref int tipo)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _SupervisorAD.Lista_Conceptos_Exterior(id_maestro, accion);
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

        public List<BUSES_DESPACHO> Lista_Revisiones_Mecanicas(string id_maestro, string accion, ref string mensaje, ref int tipo)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _SupervisorAD.Lista_Revisiones_Mecanicas(id_maestro, accion);
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

        public List<BUSES_DESPACHO> Lista_Cabina_Vehiculo(string id_maestro, string accion, ref string mensaje, ref int tipo)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _SupervisorAD.Lista_Cabina_Vehiculo(id_maestro, accion);
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

        public List<BUSES_DESPACHO> Lista_Conceptos_Dentro(string id_maestro, string accion, ref string mensaje, ref int tipo)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _SupervisorAD.Lista_Conceptos_Dentro(id_maestro, accion);
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

        public RPTA_GENERAL Insertar_Formato_campo(string usuario, int[] Conceptos, string[] estado, string[] calidad, string[] observacion, string placa, string Concesionario, int? km, string direccion, string longitud, string latitud, string Foto_Cargar1, string Foto_Cargar2, string Foto_Cargar3, string Foto_Cargar4, string mensaje2, string comentario, int id_corredor, string accion, string id_maestro, ref string mensaje, ref int tipo)
        {
            var rpta_2 = new RPTA_GENERAL();
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    var rpta = _SupervisorAD.Registrar_Formato_Campo(usuario, id_corredor, placa, direccion, km, Concesionario, longitud, latitud, Foto_Cargar1, Foto_Cargar2, Foto_Cargar3, Foto_Cargar4, comentario);
                    Conexion.finalizar(ref bdConn);


                    if (accion == "EDITAR")
                    {
                        _SupervisorAD = new SupervisorAD(ref bdConn);
                        _SupervisorAD.Eliminar_Maestro(Convert.ToInt32(id_maestro));
                        Conexion.finalizar(ref bdConn);

                    }

                    List<BUSES_DESPACHO> lista = new List<BUSES_DESPACHO>();
                    int a = 0;
                    int b = 0;


                    for (int i = 0; i < Conceptos.Length; i++)
                    {
                        BUSES_DESPACHO ruta = new BUSES_DESPACHO();
                        ruta.ID_MAESTRO_DESPACHO = rpta.AUX;
                        ruta.ID_CONCEPTOS = Conceptos[i];
                        ruta.BD_ESTADO = Convert.ToInt32(estado[i]);
                        if (i >= 7)
                        {
                            a++;
                            ruta.BD_CALIDAD = Convert.ToInt32(calidad[a]);
                        }

                        ruta.BD_OBSERVACION = observacion[i];
                        ruta.USUREG = usuario;
                        lista.Add(ruta);
                    }


                    foreach (BUSES_DESPACHO item in lista)
                    {
                        _SupervisorAD = new SupervisorAD(ref bdConn);
                        rpta_2 = _SupervisorAD.Insertar_Buses_Despacho(item.ID_MAESTRO_DESPACHO, item.ID_CONCEPTOS, item.BD_ESTADO, item.BD_CALIDAD, item.BD_OBSERVACION, item.USUREG);
                        Conexion.finalizar(ref bdConn);
                    }

                    //TRAER LA INFORMACION DE LOS CAMPOS PARA VALIDAR 
                    _SupervisorAD = new SupervisorAD(ref bdConn);
                    var Data_formato_buses = _SupervisorAD.Listarvalida_ruta_franjas(rpta.AUX);
                    Conexion.finalizar(ref bdConn);


                    //CREAR LISTA 
                    List<BUSES_DESPACHO> Lista_buses_despacho = new List<BUSES_DESPACHO>();

                    foreach (DataRow row in Data_formato_buses.Rows)
                    {

                        var ID_BUSES_DESPACHO = int.Parse(row[0].ToString());
                        var PLACA = row[1].ToString();
                        var ID_CONCEPTOS = int.Parse(row[2].ToString());
                        var BD_ESTADO = int.Parse(row[3].ToString());
                        var BD_CALIDAD = int.Parse(row[4].ToString());


                        var BUSES_DESPACHO = new BUSES_DESPACHO();

                        BUSES_DESPACHO.ID_BUSES_DESPACHO = ID_BUSES_DESPACHO;
                        BUSES_DESPACHO.BS_PLACA = PLACA;
                        BUSES_DESPACHO.ID_CONCEPTOS = ID_CONCEPTOS;
                        BUSES_DESPACHO.BD_ESTADO = BD_ESTADO;
                        BUSES_DESPACHO.BD_CALIDAD = BD_CALIDAD;


                        Lista_buses_despacho.Add(BUSES_DESPACHO);
                        //VALIDAR EL FORMATO PROGRAMABLE O NO PROGRABLAME
                        Cambiar_estado_Supervision(Lista_buses_despacho, rpta.AUX,ref mensaje,ref tipo);
                        Conexion.finalizar(ref bdConn);
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
            //return resultado;
            //var rpta_2 = new RPTA_GENERAL();
            //REGISTRAR MAESTRO 
            //_SupervisorAD = new SupervisorAD(ref bdConn);
            return rpta_2;
        }

        public DataTable Listar_Maestro_Filtros(int id_corredor, string fecha, ref string mensaje, ref int tipo)
        {
            DataTable resultado = new DataTable();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _SupervisorAD.Listar_Maestro_Filtros(id_corredor, fecha);
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

        public void Cambiar_estado_Supervision(List<BUSES_DESPACHO> lista, int id_maestro, ref string mensaje, ref int tipo)
        {
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    int[] array = { 8, 10, 11, 13, 16, 18, 21, 24, 25, 26, 27, 28, 29, 30, 32, 33, 35, 38, 47, 50, 52, 53, 55, 57, 58, 59, 60, 61, 62, 66, 71, 73, 74, 77, 78, 80, 85, 86, 87, 88, 89, 90, 91, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104 };
                    int[] array2 = { 7 };
                    Boolean estado = true;

                    int i = 0;
                    int j = 0;

                    //PONER TODOS A PROGRAMABLE
                    _SupervisorAD = new SupervisorAD(ref bdConn);
                    _SupervisorAD.Actualizar_programable(id_maestro);
                    Conexion.finalizar(ref bdConn);


                    foreach (var item in lista)
                    {
                        var lengt = array.Length;
                        if (i < 54)
                        {

                            if (item.ID_CONCEPTOS == array[i])
                            {
                                if (item.BD_ESTADO == 0)
                                {

                                    if (estado)
                                    {
                                        _SupervisorAD = new SupervisorAD(ref bdConn);
                                        _SupervisorAD.Actualizar_maestro_np(id_maestro);
                                        Conexion.finalizar(ref bdConn);

                                        estado = false;
                                    }
                                    _SupervisorAD = new SupervisorAD(ref bdConn);
                                    _SupervisorAD.Actualizar_No_programable(item.ID_BUSES_DESPACHO);
                                    Conexion.finalizar(ref bdConn);


                                }
                                if (item.BD_CALIDAD == 0)
                                {
                                    if (estado)
                                    {
                                        _SupervisorAD = new SupervisorAD(ref bdConn);
                                        _SupervisorAD.Actualizar_maestro_np(id_maestro);
                                        Conexion.finalizar(ref bdConn);

                                        estado = false;
                                    }
                                    _SupervisorAD = new SupervisorAD(ref bdConn);
                                    _SupervisorAD.Actualizar_No_programable(item.ID_BUSES_DESPACHO);
                                    Conexion.finalizar(ref bdConn);

                                }
                                i++;
                            }
                        }
                        //Fuga de fluidos
                        if (item.ID_CONCEPTOS == array2[0])
                        {
                            if (item.BD_ESTADO == 1)
                            {
                                if (estado)
                                {
                                    _SupervisorAD = new SupervisorAD(ref bdConn);
                                    _SupervisorAD.Actualizar_maestro_np(id_maestro);
                                    Conexion.finalizar(ref bdConn);
                                    estado = false;
                                }
                                _SupervisorAD = new SupervisorAD(ref bdConn);
                                _SupervisorAD.Actualizar_No_programable(item.ID_BUSES_DESPACHO);
                                Conexion.finalizar(ref bdConn);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
            }
            Conexion.finalizar(ref bdConn);

        }
    }
}

