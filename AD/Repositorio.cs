using Dapper;
using Dapper.Contrib.Extensions;
using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using DA;
using System.Data;
using ENTIDADES;
using System.Linq;

namespace AD
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly OracleConnection conn;
        public Repositorio()
        {
            conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
            SqlMapperExtensions.TableNameMapper = (type) => { return $"{ type.Name }"; };
        }
        public int ActualizarDapper(T modelo)
        {
            return conn.Update(modelo) ? 1 : 0;
        }

        public int BorrarDapper(T modelo)
        {
            return conn.Delete(modelo) ? 1 : 0;
        }

        public int InsertarDapper(T modelo)
        {
            return (int)conn.Insert(modelo);
        }

        public IEnumerable<T> ObtenerListadoDapper()
        {
            return conn.GetAll<T>();
        }

        public T ObtenerPorCodigoDapper(int id)
        {
            return conn.Get<T>(id);
        }
        public bool ModificarActivo(
            string nombretabla, int activo, string nombrecampoID, int ID, string P_USUMOD)
        {
            //bool result = true;
            //var query = "UPDATE " + nombretabla + " " +
            //        "SET ESTREG = " + activo + " ," +
            //        "USUMOD" + " = '" + P_USUMOD + "' ," +
            //        "FECMOD" + " = '" + P_FECMOD.Day + "/" + P_FECMOD.Month + "/" + P_FECMOD.Year + "' " +
            //        "WHERE " + nombrecampoID + " = " + ID;
            var bdParameters = new OracleDynamicParameters();
            bdParameters.Add("P_NOMBRETABLA", OracleDbType.Varchar2, ParameterDirection.Input, nombretabla);
            bdParameters.Add("P_ACTIVO", OracleDbType.Int32, ParameterDirection.Input, activo);
            bdParameters.Add("P_NOMBRECAMPOID", OracleDbType.Varchar2, ParameterDirection.Input, nombrecampoID);
            bdParameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.Input, ID);
            bdParameters.Add("P_USUMOD", OracleDbType.Varchar2, ParameterDirection.Input, P_USUMOD);
            var packages = "PKG_GENERAL_SP.SP_MODIFICARACTIVO";
            var SP = CommandType.StoredProcedure;
            var result = SqlMapper.QueryFirstOrDefault<bool>(conn, packages, param: bdParameters, commandType: SP);
            return result;
        }
        public int CodigoDeUltimoRegistro(string Tabla, string Nombreid, string Asid)
        {
            var bdParameters = new OracleDynamicParameters();
            var query = "SELECT NVL(MAX(" + Nombreid + "), 0) AS " + Asid + " FROM " + Tabla + "";
            var result = SqlMapper.QueryFirst<int>(conn, query);
            return result;
        }
        public T ObtenerListadoPorTablaFiltros(string Tabla, string Filtro, string FiltroValor)
        {
            var bdParameters = new OracleDynamicParameters();
            var query = "SELECT * FROM " + Tabla + " WHERE " + Filtro + "=" + FiltroValor;
            var result = SqlMapper.QueryFirst<T>(conn, query);
            return result;
        }
        public T ObtenerPorTablaFiltroId(string Tabla, string Filtro, int FiltroValor)
        {
            var bdParameters = new OracleDynamicParameters();
            var query = "SELECT * FROM " + Tabla + " WHERE " + Filtro + "=" + FiltroValor;
            var result = SqlMapper.QueryFirst<T>(conn, query);
            return result;
        }
        public IEnumerable<T> ObtenerListaPorTablaFiltroId(string Tabla, string Filtro, int FiltroValor)
        {
            var bdParameters = new OracleDynamicParameters();
            var query = "SELECT * FROM " + Tabla + " WHERE " + Filtro + "=" + FiltroValor;
            var result = SqlMapper.Query<T>(conn, query);
            return result;
        }


        public List<TB_USUARIO> Listar_Usuarios_CC(int empresa)
        {
            var bdParameters = new OracleDynamicParameters();
            var query = "SELECT TB.LOGUSU,TM.USUEMPUSU USUNOM,TM.USUEMPCON USUMCONT,TM.USUREG USUREG,TM.EMPCOD CODEMP FROM TM_USUARIOS_EMPRESA TM INNER JOIN  TB_LOGIN TB ON TM.LOGCOD=TB.USUCOD WHERE TM.EMPCOD = '" + empresa + "'";
            var result = SqlMapper.Query<TB_USUARIO>(conn, query).ToList();
            return result;
        }

        public List<TB_USUARIO> Listar_Usuarios_Hacom()
        {
            var bdParameters = new OracleDynamicParameters();
            var query = "SELECT UH_ID,UH_USUARIO USUREG,UH_PASSWORD PASSWORD   FROM usuarios_hacom WHERE UH_USUARIO NOT IN (select USUEMPUSU from tm_usuarios_empresa WHERE EMPCOD = '1' AND USUEMPUSU IS NOT NULL)";
            var result = SqlMapper.Query<TB_USUARIO>(conn, query).ToList();
            return result;
        }


    }
}
