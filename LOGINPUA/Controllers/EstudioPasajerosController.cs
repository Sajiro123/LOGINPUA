using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class EstudioPasajerosController : Controller
    {
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly ModalidadTransporteLN _modalidadTransporteLN;
        //private readonly RutaLN _rutaLN;
        //private readonly EstudioPasajerosLN _estudioPasajerosLN;

        //Util.Util utilidades = new Util.Util();

        //public EstudioPasajerosController()
        //{
        //    _CorredoresLN = new CorredoresLN();
        //    _rutaLN = new RutaLN();
        //    _modalidadTransporteLN = new ModalidadTransporteLN();
        //    _estudioPasajerosLN = new EstudioPasajerosLN();
        //}
        // GET: EstudioPasajeros
        public ActionResult Pasajeros_Campo()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();
            ModalidadTransporteLN _modalidadTransporteLN = new ModalidadTransporteLN();

            String mensaje = "";
            Int32 tipo = 0;
            //var id_persona= Convert.ToInt32(Session["ID_PERSONA"].ToString());
            ViewBag.name_title = "Estudio Pasajeros O/D";
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
            return View();

        }

        public ActionResult Pasajeros_Origen_Destino()
        {

            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();
            ModalidadTransporteLN _modalidadTransporteLN = new ModalidadTransporteLN();

            String mensaje = "";
            Int32 tipo = 0;
            //var id_persona= Convert.ToInt32(Session["ID_PERSONA"].ToString());
            ViewBag.name_title = "Encuesta Origen y Destino";
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
            return View();

        }

        public ActionResult Pasajeros_Colectivo()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();
            ModalidadTransporteLN _modalidadTransporteLN = new ModalidadTransporteLN();

            String mensaje = "";
            Int32 tipo = 0;
            //var id_persona= Convert.ToInt32(Session["ID_PERSONA"].ToString());
            ViewBag.name_title = "Estudio de Colectivo";
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
            return View();

        }
        public ActionResult ReportePasajeros()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();

            String mensaje = "";
            Int32 tipo = 0;
            ////var id_persona= Convert.ToInt32(Session["ID_PERSONA"].ToString());
            //ViewBag.name_title = "Estudio de Colectivo";
            //var datosCorredores = _CorredoresLN.obtenerListaCorredores();
            //ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            //ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            //ViewBag.rutas = _rutaLN.getRutaCorredor();
            //ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte();
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();

        }

        public ActionResult ReportePasajerosDetallado()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();

        }

        public string registroPasajeros_Campo(CC_ESTUDIO_PASAJERO ModelPasajerosCampo)
        {

            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();

            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            ModelPasajerosCampo.ID_PERSONA = Convert.ToInt32(Session["ID_PERSONA"].ToString());
            ModelPasajerosCampo.USU_REG = usuario;

            var registroPasajeros_Campo = JsonConvert.SerializeObject(_estudioPasajerosLN.registroPasajeros_Campo(ModelPasajerosCampo, ref mensaje, ref tipo));
            return registroPasajeros_Campo;
        }

        public string registroPasajeros_OrigenDestino(int id_corredor, int id_ruta, int id_paradero_orig, int id_paradero_dest, int id_tarjeta)
        {
            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();

            String mensaje = "";
            Int32 tipo = 0;
            var id_persona = Convert.ToInt32(Session["ID_PERSONA"].ToString());

            var registroPasajeros_OrigenDestino = JsonConvert.SerializeObject(_estudioPasajerosLN.registroPasajeros_OrigenDestino(id_corredor, id_ruta, id_persona, id_paradero_orig,
                                        id_paradero_dest, id_tarjeta, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return registroPasajeros_OrigenDestino;
        }

        public string registroPasajeros_Colectivo(int id_corredor, int id_ruta, int id_paradero, string tipo_vehiculo, int suben, int bajan, string placa)
        {

            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();

            String mensaje = "";
            Int32 tipo = 0;
            var id_persona = Convert.ToInt32(Session["ID_PERSONA"].ToString());

            var registroPasajeros_Colectivo = JsonConvert.SerializeObject(_estudioPasajerosLN.registroPasajeros_Colectivo(id_corredor, id_ruta, id_paradero, tipo_vehiculo,
                                        suben, bajan, placa, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return registroPasajeros_Colectivo;
        }

        public string Listar_Oferta_Demada(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin)
        {
            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();

            String mensaje = "";
            Int32 tipo = 0;
            var lista = _estudioPasajerosLN.Listar_Oferta_Demada(id_ruta, fechaConsultaInicio, fechaConsultaFin, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
        public string Listar_Origen_Destino(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin)
        {
            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _estudioPasajerosLN.Listar_Origen_Destino(id_ruta, fechaConsultaInicio, fechaConsultaFin, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
        public string Listar_Colectivo(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin)
        {
            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _estudioPasajerosLN.Listar_Colectivo(id_ruta, fechaConsultaInicio, fechaConsultaFin, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Listar_Reporte_Dias(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin,string tipo_estado)
        {
            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _estudioPasajerosLN.Listar_Reporte_Dias(id_ruta, fechaConsultaInicio, fechaConsultaFin,tipo_estado, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Listar_Reporte_Paraderos(int id_ruta, string fecha,string tipo_estado,string lado)
        {
            EstudioPasajerosLN _estudioPasajerosLN = new EstudioPasajerosLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _estudioPasajerosLN.Listar_Reporte_Paraderos(id_ruta,fecha,tipo_estado, lado, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
    }
}