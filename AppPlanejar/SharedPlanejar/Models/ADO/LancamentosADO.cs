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
    class LancamentosADO : BaseADO<Lancamento>
    {

        public override void apagar(Lancamento item)
        {
            ValuesWhere values = new ValuesWhere();
            values.add(dbTables.TLANCAMENTOS.COLUMN_ID, item.id);

            long ret = dataBase.ExecuteNoQuery(dbTables.TLANCAMENTOS.scriptDelete(values));

        }

        public override List<Lancamento> consultar(ValuesWhere where)
        {
            List<Lancamento> lista = new List<Lancamento>();

            var r = dataBase.ExecuteQuery(dbTables.TLANCAMENTOS.scriptSelect(where));

            while (r.Read())
            {
                Lancamento item = new Lancamento();
                item.id = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_ID]);
                item.dt_cad = (DateTime)r[dbTables.TLANCAMENTOS.COLUMN_DT_CAD];
                item.tipo = (String)r[dbTables.TLANCAMENTOS.COLUMN_TIPO];
                item.numero = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_NUMERO]);
                item.parcelas = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_PARCELAS]);
                item.dt_vcto = Convert.ToDateTime(r[dbTables.TLANCAMENTOS.COLUMN_DT_VCTO]);
                try
                {
                    item.dt_pgto = Convert.ToDateTime(r[dbTables.TLANCAMENTOS.COLUMN_DT_PGTO]);
                }
                catch (Exception e) {
                    item.dt_pgto = null;
                }
                item.valor = Convert.ToDecimal(r[dbTables.TLANCAMENTOS.COLUMN_VALOR]);
                item.vlr_pgto = Convert.ToDecimal(r[dbTables.TLANCAMENTOS.COLUMN_VLR_PGTO]);
                item.lanc_id = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_LANC_ID]);
                item.usu_id = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_USU_ID]);
                item.cat_id = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_CAT_ID]);
                item.item_id = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_ITEM_ID]);
                item.itdebt_id = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_ITDEBT_ID]);
                item.status = Convert.ToInt32(r[dbTables.TLANCAMENTOS.COLUMN_STATUS]);
                lista.Add(item);

            }

            return lista;
        }

        public override long insert(Lancamento item)
        {

            SqliteParameter[] values = new SqliteParameter[14];
            String sql;
         
            values[00] = new SqliteParameter(DbType.DateTime) { Value = item.dt_cad };
            values[01] = new SqliteParameter(DbType.String) { Value = item.tipo };
            values[02] = new SqliteParameter(DbType.Int32) { Value = item.numero };
            values[03] = new SqliteParameter(DbType.Int32) { Value = item.parcelas };
            values[04] = new SqliteParameter(DbType.DateTime) { Value = item.dt_vcto };
            values[05] = new SqliteParameter(DbType.DateTime) { Value = item.dt_pgto };
            values[06] = new SqliteParameter(DbType.Decimal) { Value = item.valor };
            values[07] = new SqliteParameter(DbType.Decimal) { Value = item.vlr_pgto };
            values[08] = new SqliteParameter(DbType.String) { Value = item.lanc_id };
            values[09] = new SqliteParameter(DbType.Int32) { Value = item.usu_id };
            values[10] = new SqliteParameter(DbType.Int32) { Value = item.cat_id };
            values[11] = new SqliteParameter(DbType.Int32) { Value = item.item_id };
            values[12] = new SqliteParameter(DbType.Int32) { Value = item.itdebt_id };
            values[13] = new SqliteParameter(DbType.Int32) { Value = item.status };

            long ret = dataBase.ExecuteNoQuery(dbTables.TLANCAMENTOS.scriptInsert(), values);

            return ret;
        }

        public override long update(Lancamento item)
        {
            SqliteParameter[] values = new SqliteParameter[15];


            values[00] = new SqliteParameter(DbType.DateTime) { Value = item.dt_cad };
            values[01] = new SqliteParameter(DbType.String) { Value = item.tipo };
            values[02] = new SqliteParameter(DbType.Int32) { Value = item.numero };
            values[03] = new SqliteParameter(DbType.Int32) { Value = item.parcelas };
            values[04] = new SqliteParameter(DbType.DateTime) { Value = item.dt_vcto };
            values[05] = new SqliteParameter(DbType.DateTime) { Value = item.dt_pgto };
            values[06] = new SqliteParameter(DbType.Decimal) { Value = item.valor };
            values[07] = new SqliteParameter(DbType.Decimal) { Value = item.vlr_pgto };
            values[08] = new SqliteParameter(DbType.String) { Value = item.lanc_id };
            values[09] = new SqliteParameter(DbType.Int32) { Value = item.usu_id };
            values[10] = new SqliteParameter(DbType.Int32) { Value = item.cat_id };
            values[11] = new SqliteParameter(DbType.Int32) { Value = item.item_id };
            values[12] = new SqliteParameter(DbType.Int32) { Value = item.itdebt_id };
            values[13] = new SqliteParameter(DbType.Int32) { Value = item.status };
            values[14] = new SqliteParameter(DbType.Int32) { Value = item.id };

            long ret = dataBase.ExecuteNoQuery(dbTables.TLANCAMENTOS.scriptUpdate(), values);

            return ret;
        }

    }
}
