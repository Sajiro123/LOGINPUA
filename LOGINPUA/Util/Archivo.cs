using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LOGINPUA.WEB.Util
{
    public static class Archivo
    {
        internal static void EliminarArchivos(string rutaBase)
        {
            string fic = System.Web.HttpContext.Current.Server.MapPath(rutaBase + "fecha.txt");
            System.IO.StreamReader objReader = new System.IO.StreamReader(fic);
            string fecha = objReader.ReadLine();
            objReader.Close();
            if (!System.DateTime.Now.Date.ToShortDateString().Equals(fecha))
            {
                foreach (string Archivo in Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(rutaBase), "*.*", SearchOption.TopDirectoryOnly))
                {
                    if (Archivo != fic)
                    {
                        System.IO.File.Delete(Archivo);
                    }
                }
                string texto = System.DateTime.Now.Date.ToShortDateString();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
                sw.Write(texto);
                sw.Close();
            }
        }

        internal static byte[] ConvertirArchivoBytes(string rutaArchivo)
        {
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo), FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] archivo = new byte[(int)fs.Length];
            br.Read(archivo, 0, (int)fs.Length);
            br.Close();
            fs.Close();
            return archivo;
        }

        internal static string FormatoFechaArchivo()
        {
            return System.DateTime.Now.Day + "" + System.DateTime.Now.Month + "" + System.DateTime.Now.Year + "" + System.DateTime.Now.Hour + "" + System.DateTime.Now.Minute + "" + System.DateTime.Now.Second;
        }

        internal static string NumeroAletorio()
        {
            return new Random().Next(10000, 99999).ToString();
        }



    }
}