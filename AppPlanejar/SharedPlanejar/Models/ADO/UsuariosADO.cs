using SharedPlanejar.Models;
using SharedPlanejar.Models.ADO;
using System;
using System.Collections.Generic;
using System.Text;
using SharedPlanejar.Utils;
using SharedPlanejar.Models.DB;
using Mono.Data.Sqlite;
using System.Data;

namespace Models.ADO
{
    class UsuariosADO : BaseADO<Usuario>
    {
        
        public override void apagar(Usuario item)
        {
            ValuesWhere values = new ValuesWhere();
            values.add(dbTables.TUSUARIOS.COLUMN_ID, item.id);

            long ret = dataBase.ExecuteNoQuery(dbTables.TUSUARIOS.scriptDelete(values));

        }

        public override List<Usuario> consultar(ValuesWhere where)
        {
            List<Usuario> lista = new List<Usuario>();
            String scr = dbTables.TUSUARIOS.scriptSelect(where);
            var r = dataBase.ExecuteQuery(scr);

            while (r.Read())
            {
                Usuario item = new Usuario();
                item.id =  Convert.ToInt32(r[dbTables.TUSUARIOS.COLUMN_ID]);
                item.nome = r[dbTables.TUSUARIOS.COLUMN_NOME].ToString();
                item.senha = r[dbTables.TUSUARIOS.COLUMN_SENHA].ToString();
                item.email = r[dbTables.TUSUARIOS.COLUMN_EMAIL].ToString();
                item.ativo = Convert.ToInt16(r[dbTables.TUSUARIOS.COLUMN_ATIVO]);
                item.logado = Convert.ToInt16(r[dbTables.TUSUARIOS.COLUMN_LOGADO]);

                lista.Add(item);

            }

            return lista;
        }

        public override long insert(Usuario item)
        {

            SqliteParameter[] values = new SqliteParameter[5];

            values[0] = new SqliteParameter(DbType.String) { Value = item.nome};
            values[1] = new SqliteParameter(DbType.String) { Value = item.email};
            values[2] = new SqliteParameter(DbType.String) { Value = item.senha};
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.ativo};
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.logado};

            long ret = dataBase.ExecuteNoQuery(dbTables.TUSUARIOS.scriptInsert(), values);

            return ret;
        }

        public override long update(Usuario item)
        {
            SqliteParameter[] values = new SqliteParameter[6];


            values[0] = new SqliteParameter(DbType.String) { Value = item.nome };
            values[1] = new SqliteParameter(DbType.String) { Value = item.email };
            values[2] = new SqliteParameter(DbType.String) { Value = item.senha };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.ativo };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.logado };
            values[5] = new SqliteParameter(DbType.Int32) { Value = item.id };

            long ret = dataBase.ExecuteNoQuery(dbTables.TUSUARIOS.scriptUpdate(), values);

            return ret;
        }
    }
}
