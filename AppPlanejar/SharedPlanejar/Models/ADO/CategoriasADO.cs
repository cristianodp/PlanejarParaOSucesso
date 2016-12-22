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
    class CategoriasADO : BaseADO<Categoria>
    {
        public override void apagar(Categoria item)
        {
            ValuesWhere values = new ValuesWhere();
            values.add(dbTables.TCATEGORIAS.COLUMN_ID, item.id);

            long ret = dataBase.ExecuteNoQuery(dbTables.TCATEGORIAS.scriptDelete(values));

        }

        public override List<Categoria> consultar(ValuesWhere where)
        {
            List<Categoria> lista = new List<Categoria>();
            string SQL = dbTables.TCATEGORIAS.scriptSelect(where);
            var r = dataBase.ExecuteQuery(SQL);

            while (r.Read())
            {
                Categoria item = new Categoria();
                item.id = Convert.ToInt32(r[dbTables.TCATEGORIAS.COLUMN_ID].ToString());
                item.descricao = r[dbTables.TCATEGORIAS.COLUMN_DESCRICAO].ToString();
                item.cor = Convert.ToInt32(r[dbTables.TCATEGORIAS.COLUMN_COR]);
                item.setTipo(r[dbTables.TCATEGORIAS.COLUMN_TIPO].ToString());
                item.usu_id = Convert.ToInt32(r[dbTables.TCATEGORIAS.COLUMN_USU_ID]);
                item.visivel = Convert.ToInt32(r[dbTables.TCATEGORIAS.COLUMN_VISIVEL]);
                item.ativo = Convert.ToInt32(r[dbTables.TCATEGORIAS.COLUMN_ATIVO]);

                lista.Add(item);

            }

            return lista;
        }

        public override long insert(Categoria item)
        {

            SqliteParameter[] values = new SqliteParameter[6];

            values[0] = new SqliteParameter(DbType.String) { Value = item.descricao };
            values[1] = new SqliteParameter(DbType.String) { Value = item.getTipo(0) };
            values[2] = new SqliteParameter(DbType.Int32) { Value = item.cor };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.usu_id };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.visivel };
            values[5] = new SqliteParameter(DbType.Int32) { Value = item.ativo };

            long ret = dataBase.ExecuteNoQuery(dbTables.TCATEGORIAS.scriptInsert(), values);

            return ret;
        }

        public override long update(Categoria item)
        {
            SqliteParameter[] values = new SqliteParameter[7];

            values[0] = new SqliteParameter(DbType.String) { Value = item.descricao };
            values[1] = new SqliteParameter(DbType.String) { Value = item.getTipo(0) };
            values[2] = new SqliteParameter(DbType.Int32) { Value = item.cor };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.usu_id };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.visivel };
            values[5] = new SqliteParameter(DbType.Int32) { Value = item.ativo };
            values[6] = new SqliteParameter(DbType.Int32) { Value = item.id };

            long ret = dataBase.ExecuteNoQuery(dbTables.TCATEGORIAS.scriptUpdate(), values);

            return ret;
        }

    }
}
