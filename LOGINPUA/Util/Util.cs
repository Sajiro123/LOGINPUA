using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using ENTIDADES;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LOGINPUA.Util
{
    public class Util
    {
        public Boolean validaVistaxPerfil(String ActionController, DataTable lista)
        {
            if (lista == null) { return false; }
            Boolean flagAuxValida = false;
            //
            foreach (DataRow item in lista.Rows)
            {
                var urlAction = item[3].ToString();
                int index = urlAction.IndexOf('/');
                string txtController = "";
                string txtAction = "";
                //
                if (index != -1)
                {
                    String[] dataSplit = urlAction.Split('/');
                    txtController = dataSplit[0];
                    txtAction = dataSplit[1];
                    if (ActionController.ToUpper() == txtAction.ToUpper())
                    {
                        flagAuxValida = true;
                    }
                }
            }
            return flagAuxValida;
        }

        public List<CC_MENUPERFIL_ACCION> validadAccionMenu(List<CC_MENUPERFIL_ACCION> Lista_acciones, string nombreActionCurrent, string controller)
        {

            List<CC_MENUPERFIL_ACCION> Lista_acciones_ = new List<CC_MENUPERFIL_ACCION>();


            foreach (var item_acciones in Lista_acciones)
            {

                var urlAction = item_acciones.URL;
                String[] dataSplit = urlAction.Split('/');
                var txtAction = dataSplit[1];
                var txtController = dataSplit[0];

                if (nombreActionCurrent == txtAction && txtController == controller)
                {
                    CC_MENUPERFIL_ACCION acciones = new CC_MENUPERFIL_ACCION();
                    acciones.ID_ACCION = item_acciones.ID_ACCION;
                    acciones.NOMBRE = item_acciones.NOMBRE;
                    acciones.ICON_ACCION = item_acciones.ICON_ACCION;
                    acciones.MENU = item_acciones.MENU;
                    Lista_acciones_.Add(acciones);
                }
            }
            return Lista_acciones_;
        }
    }
    public class Servicio
    {
        public static void EnvioCorreo(List<string> para, List<string> copia, string correo, string pass, string asunto, List<string> parametro_cuerpo, string cuerpo, string adjunto, ref int tipo, ref string mensaje)
        {
            try
            {
                //string mailUser = "comunicaciones@atu.gob.pe";
                //string UserCredencial = "comunicaciones@atu.gob.pe";
                //string PasswordCredencial = "Atu30902$";
                //string DominioCredencial = "atu.gob.pe";
                //string Host = "outlook.office365.com";

                string mailUser = correo;
                string UserCredencial = correo;
                string PasswordCredencial = pass;
                string DominioCredencial = "atu.gob.pe";
                string Host = "ATUMAIL-LIMA";

                MailMessage mailNoti = new MailMessage();
                mailNoti.From = new MailAddress(mailUser);
                foreach (string mailTo in para)
                {
                    mailNoti.To.Add(mailTo);
                }
                foreach (string mailCC in copia)
                {
                    mailNoti.CC.Add(mailCC);
                }
                for (var i = 0; i < parametro_cuerpo.Count; i++)
                {
                    cuerpo = cuerpo.Replace("{{param_" + (i + 1) + "}}", parametro_cuerpo[i]);
                }
                mailNoti.Subject = asunto;
                mailNoti.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(cuerpo.ToString(), Encoding.UTF8, MediaTypeNames.Text.Html);
                //foreach (var archivo in adjunto)
                //{
                Attachment adj = new Attachment(adjunto);
                //Attachment adj = new Attachment(archivo);
                mailNoti.Attachments.Add(adj);
                //}
                mailNoti.AlternateViews.Add(htmlView);
                mailNoti.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mailNoti.Priority = System.Net.Mail.MailPriority.High;

                SmtpClient smtpNoti = new SmtpClient();
                smtpNoti.Host = Host;
                smtpNoti.UseDefaultCredentials = false;
                smtpNoti.Credentials = new System.Net.NetworkCredential(UserCredencial, PasswordCredencial, DominioCredencial);
                smtpNoti.Send(mailNoti);

                //SmtpClient smtpNoti = new SmtpClient();

                //smtpNoti.UseDefaultCredentials = false;
                //smtpNoti.Credentials = new System.Net.NetworkCredential(UserCredencial, PasswordCredencial, DominioCredencial);
                //smtpNoti.Host = Host;
                //smtpNoti.DeliveryMethod = SmtpDeliveryMethod.Network;

                //smtpNoti.EnableSsl = true;
                //smtpNoti.TargetName = "STARTTLS/smtp.office365.com";
                //smtpNoti.Port = 587;

                //smtpNoti.Send(mailNoti);

                //smtpNoti.Dispose();
                //smtpNoti = null;
                //mailNoti.Dispose();
                //mailNoti = null;
                tipo = 1;
                mensaje = "Se envío Correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un problema al enviar el correo";
                tipo = 0;
            }
        }

    }
}
