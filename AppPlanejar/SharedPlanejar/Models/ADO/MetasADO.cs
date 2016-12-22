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
    class MetasADO : BaseADO<Meta>
    {
        public override void apagar(Meta item)
        {
            ValuesWhere values = new ValuesWhere();
            values.add(dbTables.TMETAS.COLUMN_ID, item.id);

            long ret = dataBase.ExecuteNoQuery(dbTables.TMETAS.scriptDelete(values));

        }

        public override List<Meta> consultar(ValuesWhere where)
        {
            List<Meta> lista = new List<Meta>();
            string sql = dbTables.TMETAS.scriptSelect(where);
            var r = dataBase.ExecuteQuery(dbTables.TMETAS.scriptSelect(where));

            while (r.Read())
            {
                Meta item = new Meta();
                item.id = Convert.ToInt32(r[dbTables.TMETAS.COLUMN_ID]);
                try{
                    item.dt_inicio = Convert.ToDateTime(r[dbTables.TMETAS.COLUMN_DT_INICIO]);
                }
                catch (Exception e) {

                }
                try{
                    item.dt_final = Convert.ToDateTime(r[dbTables.TMETAS.COLUMN_DT_FINAL]);
                }
                catch (Exception e){

                }
                try{
                    item.valor = Convert.ToDecimal(r[dbTables.TMETAS.COLUMN_VALOR]);
                }
                catch (Exception e){

                }
                item.cat_id = Convert.ToInt32(r[dbTables.TMETAS.COLUMN_CAT_ID]);
                item.ativo = Convert.ToInt32(r[dbTables.TMETAS.COLUMN_ATIVO]);

                lista.Add(item);

            }

            return lista;
        }

        public override long insert(Meta item)
        {

            SqliteParameter[] values = new SqliteParameter[5];

            values[0] = new SqliteParameter(DbType.DateTime) { Value = item.dt_inicio };
            values[1] = new SqliteParameter(DbType.DateTime) { Value = item.dt_final };
            values[2] = new SqliteParameter(DbType.Decimal) { Value = item.valor };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.cat_id };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.ativo };

            long ret = dataBase.ExecuteNoQuery(dbTables.TMETAS.scriptInsert(), values);

            return ret;
        }

        public override long update(Meta item)
        {
            SqliteParameter[] values = new SqliteParameter[6];


            values[0] = new SqliteParameter(DbType.DateTime) { Value = item.dt_inicio };
            values[1] = new SqliteParameter(DbType.DateTime) { Value = item.dt_final };
            values[2] = new SqliteParameter(DbType.Decimal) { Value = item.valor };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.cat_id };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.ativo };
            values[5] = new SqliteParameter(DbType.Int32) { Value = item.id };

            long ret = dataBase.ExecuteNoQuery(dbTables.TMETAS.scriptUpdate(), values);

            return ret;
        }
    }
}
