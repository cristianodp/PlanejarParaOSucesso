using System;
using System.Collections.Generic;
using System.Text;
using SharedPlanejar.Utils;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

namespace SharedPlanejar.Models.ADO
{
    class ItensADO : BaseADO<Item>
    {
        public override void apagar(Item item)
        {
            ValuesWhere values = new ValuesWhere();
            values.add(DB.dbTables.TITENS.COLUMN_ID,item.id);
            
            long ret = dataBase.ExecuteNoQuery(DB.dbTables.TITENS.scriptDelete(values)); 

        }

        public override List<Item> consultar(ValuesWhere where)
        {
            List<Item> lista = new List<Item>();

            var r = dataBase.ExecuteQuery(DB.dbTables.TITENS.scriptSelect(where));

            while (r.Read())
            {
                Item item = new Item();
                item.id = Convert.ToInt32(r[DB.dbTables.TITENS.COLUMN_ID]);
                item.descricao =  r[DB.dbTables.TITENS.COLUMN_DESCRICAO].ToString();
                item.tipo = r[DB.dbTables.TITENS.COLUMN_TIPO].ToString();
                item.setTipoCta(r[DB.dbTables.TITENS.COLUMN_TP_CTA].ToString());
                item.cor= Convert.ToInt32(r[DB.dbTables.TITENS.COLUMN_COR]);
               // item.cat_id = Convert.ToInt32(r[DB.dbTables.TITENS.COLUMN_CAT_ID]);
                item.usu_id = Convert.ToInt32(r[DB.dbTables.TITENS.COLUMN_USU_ID]);
                item.ativo = Convert.ToInt32(r[DB.dbTables.TITENS.COLUMN_ATIVO]);

                lista.Add(item);

            }

            return lista;
        }

        public override long insert(Item item)
        {

            SqliteParameter[] values = new SqliteParameter[6];

            values[0] = new SqliteParameter(DbType.String) { Value = item.descricao };
            values[1] = new SqliteParameter(DbType.String) { Value = item.tipo };
            values[2] = new SqliteParameter(DbType.String) { Value = item.getTipoCta(0) };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.cor };
           // values[4] = new SqliteParameter(DbType.Int32) { Value = item.cat_id };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.usu_id };
            values[5] = new SqliteParameter(DbType.Int32) { Value = item.ativo };
            
            long ret = dataBase.ExecuteNoQuery(DB.dbTables.TITENS.scriptInsert(), values);

            ValuesWhere where = new ValuesWhere();
            where.add(DB.dbTables.TITENS.COLUMN_ID, (int)ret);
            item = consultar(where).FirstOrDefault();

            return ret;
        }

        public override long update(Item item)
        {
            SqliteParameter[] values = new SqliteParameter[7];

            values[0] = new SqliteParameter(DbType.String) { Value = item.descricao };
            values[1] = new SqliteParameter(DbType.String) { Value = item.tipo };
            values[2] = new SqliteParameter(DbType.String) { Value = item.getTipoCta(0) };
            values[3] = new SqliteParameter(DbType.Int32) { Value = item.cor };
            //values[4] = new SqliteParameter(DbType.Int32) { Value = item.cat_id };
            values[4] = new SqliteParameter(DbType.Int32) { Value = item.usu_id };
            values[5] = new SqliteParameter(DbType.Int32) { Value = item.ativo };
            values[6] = new SqliteParameter(DbType.Int32) { Value = item.id };


            long ret = dataBase.ExecuteNoQuery(DB.dbTables.TITENS.scriptUpdate(), values);

            ValuesWhere where = new ValuesWhere();
            where.add(DB.dbTables.TITENS.COLUMN_ID, item.id);
            item = consultar(where).FirstOrDefault();

            return ret;
        }
    }
}
