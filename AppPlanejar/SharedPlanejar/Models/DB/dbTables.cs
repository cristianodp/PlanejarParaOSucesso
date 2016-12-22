using Mono.Data.Sqlite;
using SharedPlanejar.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedPlanejar.Models.DB
{
    public class dbTables
    {

        public static String sqlDelete(String TABLE_NAME, ValuesWhere pValues)
        {
            String vQuery = null;
            String vFilter = null;

            if (pValues != null)
            {
                vFilter = pValues.getScrFilter();
            }

            vQuery = "DELETE FROM " + TABLE_NAME;
            if (vFilter != null)
            {
                vQuery += " WHERE " + vFilter;
            }

            return vQuery;
        }

        public static String sqlSelect(String TABLE_NAME, String[] pColunms, ValuesWhere pValues, String[] pOrderBy)
        {
            String vQuery = null;
            String vColunms = null;
            String vTables = null;
            String vFilter = null;
            String vOrderBy = null;

            if (pColunms != null)
            {
                for (int x = 0; x < pColunms.Length; x++)
                {
                    if (vColunms == null)
                    {
                        vColunms = pColunms[x];
                    }
                    else {
                        vColunms += ',' + pColunms[x];
                    }
                }
            }
            else {

                vColunms = " * ";

            }
            vTables = TABLE_NAME;

            if (pValues != null)
            {
                vFilter = pValues.getScrFilter();
            }

            if (pOrderBy != null)
            {
                for (int x = 0; x < pOrderBy.Length; x++)
                {
                    if (vOrderBy == null)
                    {
                        vOrderBy = pOrderBy[x];
                    }
                    else {
                        vOrderBy += ',' + pOrderBy[x];
                    }
                }
            }
            else {
                vOrderBy = " ORDER BY " + vOrderBy;
            }

            vQuery = " SELECT " + vColunms;
            vQuery += "  FROM " + vTables;
            if (vFilter != null)
            {
                vQuery += " WHERE " + vFilter;
            }
            vQuery += " ORDER BY " + vOrderBy;

            return vQuery;
        }

        public static class TUSUARIOS
        {

            public static String TABLE_NAME = "TUSUARIOS";
            public static String COLUMN_ID = "ID";
            public static String COLUMN_NOME = "NOME";
            public static String COLUMN_EMAIL = "EMAIL";
            public static String COLUMN_SENHA = "SENHA";
            public static String COLUMN_ATIVO = "ATIVO";
            public static String COLUMN_LOGADO = "LOGADO";

            public static String scriptInsert()
            {

                String sql;
                sql = "INSERT INTO " + TABLE_NAME;
                sql += " ( " + COLUMN_NOME;
                sql += " , " + COLUMN_EMAIL;
                sql += " , " + COLUMN_SENHA;
                sql += " , " + COLUMN_ATIVO;
                sql += " , " + COLUMN_LOGADO;
                sql += " ) VALUES ( ?, ?, ?, ?, ?)";

                return sql;
            }

            public static String scriptUpdate()
            {
                String sql;
                sql = "UPDATE " + TABLE_NAME;
                sql += " SET " + COLUMN_NOME + " = ? ";
                sql += " , " + COLUMN_EMAIL + " = ? ";
                sql += " , " + COLUMN_SENHA + " = ? ";
                sql += " , " + COLUMN_ATIVO + " = ? ";
                sql += " , " + COLUMN_LOGADO + " = ? ";
                sql += " WHERE " + COLUMN_ID + " = ? ";

                return sql;
            }

            public static String scriptSelect(ValuesWhere pValues)
            {
                return scriptSelect(null, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues)
            {
                return scriptSelect(pColunms, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues, String[] pOrderBy)
            {

                String[] vColunms;
                String[] vOrderBy;
                if (pColunms != null)
                {
                    vColunms = pColunms;
                }
                else {

                    vColunms = new String[6];

                    vColunms[0] = TUSUARIOS.COLUMN_ID;
                    vColunms[1] = TUSUARIOS.COLUMN_NOME;
                    vColunms[2] = TUSUARIOS.COLUMN_EMAIL;
                    vColunms[3] = TUSUARIOS.COLUMN_SENHA;
                    vColunms[4] = TUSUARIOS.COLUMN_ATIVO;
                    vColunms[5] = TUSUARIOS.COLUMN_LOGADO;

                }

                if (pOrderBy != null)
                {
                    vOrderBy = pOrderBy;
                }
                else {
                    vOrderBy = new String[1];
                    vOrderBy[0] = TUSUARIOS.COLUMN_NOME;
                }


                return sqlSelect(TUSUARIOS.TABLE_NAME, vColunms, pValues, vOrderBy);
            }

            public static String scriptDelete(ValuesWhere pValues)
            {

                return sqlDelete(TUSUARIOS.TABLE_NAME, pValues);
            }

            public static String scriptCreate(int version)
            {
                String sql = null;
                switch (version) {
                    case 1:{
                            sql = "CREATE TABLE " + TABLE_NAME;
                            sql += " ( " + COLUMN_ID + " INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT ";
                            sql += " , " + COLUMN_NOME + " NOME VARCHAR(70)  NOT NULL ";
                            sql += " , " + COLUMN_EMAIL + " VARCHAR(100) NOT NULL ";
                            sql += " , " + COLUMN_SENHA + " VARCHAR(70)  NOT NULL ";
                            sql += " , " + COLUMN_ATIVO + " INTEGER NOT NULL DEFAULT 1 ";
                            sql += " , " + COLUMN_LOGADO + " INTEGER NOT NULL DEFAULT 0 ";
                            sql += " ); ";
                        }
                        break;
                    case 2: {
                            sql = null;
                        }
                        break;
                }
                
                return sql;
            }

        }

        public static class TCATEGORIAS
        {

            public static String TABLE_NAME = "TCATEGORIAS";
            public static String COLUMN_ID = "ID";
            public static String COLUMN_DESCRICAO = "DESCRICAO";
            public static String COLUMN_TIPO = "TIPO";
            public static String COLUMN_COR = "COR";
            public static String COLUMN_USU_ID = "USU_ID";
            public static String COLUMN_VISIVEL = "VISIVEL";
            public static String COLUMN_ATIVO = "ATIVO";

            public static String scriptInsert()
            {

                String sql;
                sql = "INSERT INTO " + TABLE_NAME;
                sql += " ( " + COLUMN_DESCRICAO;
                sql += " , " + COLUMN_TIPO;
                sql += " , " + COLUMN_COR;
                sql += " , " + COLUMN_USU_ID;
                sql += " , " + COLUMN_VISIVEL;
                sql += " , " + COLUMN_ATIVO;
                sql += " ) VALUES ( ?, ?, ?, ?, ?, ?)";

                return sql;
            }

            public static String scriptUpdate()
            {
                String sql;
                sql = "UPDATE " + TABLE_NAME;
                sql += " SET " + COLUMN_DESCRICAO + " = ? ";
                sql += " , " + COLUMN_TIPO + " = ? ";
                sql += " , " + COLUMN_COR + " = ? ";
                sql += " , " + COLUMN_USU_ID + " = ? ";
                sql += " , " + COLUMN_VISIVEL + " = ? ";
                sql += " , " + COLUMN_ATIVO + " = ? ";
                sql += " WHERE " + COLUMN_ID + " = ? ";

                return sql;
            }

            public static String scriptSelect(ValuesWhere pValues)
            {
                return scriptSelect(null, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues)
            {
                return scriptSelect(pColunms, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues, String[] pOrderBy)
            {

                String[] vColunms;
                String[] vOrderBy;
                if (pColunms != null)
                {
                    vColunms = pColunms;
                }
                else {

                    vColunms = new String[7];

                    vColunms[0] = TCATEGORIAS.COLUMN_ID;
                    vColunms[1] = TCATEGORIAS.COLUMN_DESCRICAO;
                    vColunms[2] = TCATEGORIAS.COLUMN_TIPO;
                    vColunms[3] = TCATEGORIAS.COLUMN_COR;
                    vColunms[4] = TCATEGORIAS.COLUMN_VISIVEL;
                    vColunms[5] = TCATEGORIAS.COLUMN_USU_ID;
                    vColunms[6] = TCATEGORIAS.COLUMN_ATIVO;
                }

                if (pOrderBy != null)
                {
                    vOrderBy = pOrderBy;
                }
                else {
                    vOrderBy = new String[1];
                    vOrderBy[0] = TCATEGORIAS.COLUMN_DESCRICAO;
                }


                return sqlSelect(TCATEGORIAS.TABLE_NAME, vColunms, pValues, vOrderBy);
            }

            public static String scriptDelete(ValuesWhere pValues)
            {

                return sqlDelete(TCATEGORIAS.TABLE_NAME, pValues);
            }

            public static String scriptCreate(int version)
            {
                String sql = null;
                switch (version)
                {
                    case 1:
                        {
                            sql = "CREATE TABLE " + TABLE_NAME;
                            sql += " ( " + COLUMN_ID + " INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT ";
                            sql += " , " + COLUMN_DESCRICAO + " VARCHAR(70)  NOT NULL ";
                            sql += " , " + COLUMN_TIPO + " VARCHAR(1)   NOT NULL ";
                            sql += " , " + COLUMN_COR + " INTEGER  NOT NULL ";
                            sql += " , " + COLUMN_USU_ID + " INTEGER  NOT NULL REFERENCES TUSUARIOS(ID) ON DELETE CASCADE "; 
                            sql += " , " + COLUMN_VISIVEL + " INTEGER  NOT NULL DEFAULT 1 ";
                            sql += " , " + COLUMN_ATIVO + " INTEGER  NOT NULL DEFAULT 1 ";
                            sql += " ); ";
                        }
                        break;
                    case 2:
                        {
                            sql = null;
                        }
                        break;
                }

                return sql;
            }
           

        }

        public static class TITENS
        {

            public static String TABLE_NAME = "TITENS";
            public static String COLUMN_ID = "ID";
            public static String COLUMN_DESCRICAO = "DESCRICAO";
            public static String COLUMN_TIPO = "TIPO";
            public static String COLUMN_TP_CTA = "TP_CTA";
            public static String COLUMN_COR = "COR";
          //  public static String COLUMN_CAT_ID = "CAT_ID";
            public static String COLUMN_USU_ID = "USU_ID";
            public static String COLUMN_ATIVO = "ATIVO";


            public static String scriptInsert()
            {

                String sql;
                sql = "INSERT INTO " + TABLE_NAME;
                sql += " ( " + COLUMN_DESCRICAO;
                sql += " , " + COLUMN_TIPO;
                sql += " , " + COLUMN_TP_CTA;
                sql += " , " + COLUMN_COR;
              //  sql += " , " + COLUMN_CAT_ID;
                sql += " , " + COLUMN_USU_ID;
                sql += " , " + COLUMN_ATIVO;
                sql += " ) VALUES ( ?, ?, ?, ?, ?, ?)";

                return sql;
            }

            public static String scriptUpdate()
            {
                String sql;
                sql = "UPDATE " + TABLE_NAME;
                sql += " SET " + COLUMN_DESCRICAO + " = ? ";
                sql += " , " + COLUMN_TIPO + " = ? ";
                sql += " , " + COLUMN_TP_CTA + " = ? ";
                sql += " , " + COLUMN_COR + " = ? ";
               // sql += " , " + COLUMN_CAT_ID + " = ? ";
                sql += " , " + COLUMN_USU_ID + " = ? ";
                sql += " , " + COLUMN_ATIVO + " = ? ";
                sql += " WHERE " + COLUMN_ID + " = ? ";

                return sql;
            }
            public static String scriptSelect(ValuesWhere pValues)
            {
                return scriptSelect(null, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues)
            {
                return scriptSelect(pColunms, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues, String[] pOrderBy)
            {

                String[] vColunms;
                String[] vOrderBy;
                if (pColunms != null)
                {
                    vColunms = pColunms;
                }
                else {

                    vColunms = new String[7];

                    vColunms[0] = TITENS.COLUMN_ID;
                    vColunms[1] = TITENS.COLUMN_DESCRICAO;
                    vColunms[2] = TITENS.COLUMN_TIPO;
                    vColunms[3] = TITENS.COLUMN_TP_CTA;
                    vColunms[4] = TITENS.COLUMN_COR;
                  //  vColunms[5] = TITENS.COLUMN_CAT_ID;
                    vColunms[5] = TITENS.COLUMN_USU_ID;
                    vColunms[6] = TITENS.COLUMN_ATIVO;
                }

                if (pOrderBy != null)
                {
                    vOrderBy = pOrderBy;
                }
                else {
                    vOrderBy = new String[1];
                    vOrderBy[0] = TITENS.COLUMN_DESCRICAO;
                }


                return sqlSelect(TITENS.TABLE_NAME, vColunms, pValues, vOrderBy);
            }

            public static String scriptDelete(ValuesWhere pValues)
            {

                return sqlDelete(TITENS.TABLE_NAME, pValues);
            }

            public static String scriptCreate(int version)
            {
                String sql = null;
                switch (version)
                {
                    case 1:
                        {
                            sql = "CREATE TABLE " + TABLE_NAME;
                            sql += " ( " + COLUMN_ID + " INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT ";
                            sql += " , " + COLUMN_DESCRICAO + " VARCHAR(70)  NOT NULL ";
                            sql += " , " + COLUMN_TIPO + " VARCHAR(1)   NOT NULL ";
                            sql += " , " + COLUMN_TP_CTA + " VARCHAR(1)  ";
                            sql += " , " + COLUMN_COR + " INTEGER  ";
                          //  sql += " , " + COLUMN_CAT_ID + " INTEGER REFERENCES TCATEGORIAS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_USU_ID + " INTEGER NOT NULL REFERENCES TUSUARIOS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_ATIVO + " INTEGER NOT NULL DEFAULT 1";
                            sql += " ); ";
                        }
                        break;
                    case 2:
                        {
                            sql = null;
                        }
                        break;
                }

                return sql;
            }
            
        }

        public static class TMETAS
        {

            public static String TABLE_NAME = "TMETAS";
            public static String COLUMN_ID = "ID";
            public static String COLUMN_DT_INICIO = "DT_INICIO";
            public static String COLUMN_DT_FINAL = "DT_FINAL";
            public static String COLUMN_VALOR = "VALOR";
            public static String COLUMN_CAT_ID = "CAT_ID";
            public static String COLUMN_ATIVO = "ATIVO";

            public static String scriptInsert()
            {

                String sql;
                sql = "INSERT INTO " + TABLE_NAME;
                sql += " ( " + COLUMN_DT_INICIO;
                sql += " , " + COLUMN_DT_FINAL;
                sql += " , " + COLUMN_VALOR;
                sql += " , " + COLUMN_CAT_ID;
                sql += " , " + COLUMN_ATIVO;
                sql += " ) VALUES ( ?, ?, ?, ?, ?) ";

                return sql;
            }

            public static String scriptUpdate()
            {
                String sql;
                sql = "UPDATE " + TABLE_NAME;
                sql += " SET " + COLUMN_DT_INICIO + " = ? ";
                sql += " , " + COLUMN_DT_FINAL + " = ? ";
                sql += " , " + COLUMN_VALOR + " = ? ";
                sql += " , " + COLUMN_ATIVO + " = ? ";
                sql += " WHERE " + COLUMN_ID + " = ? ";

                return sql;
            }

            public static String scriptSelect(ValuesWhere pValues)
            {
                return scriptSelect(null, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues)
            {
                return scriptSelect(pColunms, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues, String[] pOrderBy)
            {

                String[] vColunms;
                String[] vOrderBy;
                if (pColunms != null)
                {
                    vColunms = pColunms;
                }
                else {

                    vColunms = new String[6];

                    vColunms[0] = TMETAS.COLUMN_ID;
                    vColunms[1] = TMETAS.COLUMN_DT_INICIO;
                    vColunms[2] = TMETAS.COLUMN_DT_FINAL;
                    vColunms[3] = TMETAS.COLUMN_VALOR;
                    vColunms[4] = TMETAS.COLUMN_CAT_ID;
                    vColunms[5] = TMETAS.COLUMN_ATIVO;
                }

                if (pOrderBy != null)
                {
                    vOrderBy = pOrderBy;
                }
                else {
                    vOrderBy = new String[1];
                    vOrderBy[0] = TMETAS.COLUMN_DT_INICIO;
                    vOrderBy[0] = TMETAS.COLUMN_DT_FINAL;
                }


                return sqlSelect(TMETAS.TABLE_NAME, vColunms, pValues, vOrderBy);
            }

            public static String scriptDelete(ValuesWhere pValues)
            {

                return sqlDelete(TMETAS.TABLE_NAME, pValues);
            }

            public static String scriptCreate(int version)
            {
                String sql = null;
                switch (version)
                {
                    case 1:
                        {
                            sql = "CREATE TABLE " + TABLE_NAME;
                            sql += " ( " + COLUMN_ID + " INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT ";
                            sql += " , " + COLUMN_DT_INICIO + " DATE NOT NULL ";
                            sql += " , " + COLUMN_DT_FINAL + " DATE NOT NULL ";
                            sql += " , " + COLUMN_VALOR + " DECIMAL(17,8)  NOT NULL ";
                            sql += " , " + COLUMN_CAT_ID + " INTEGER NOT NULL REFERENCES TCATEGORIAS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_ATIVO + " INTEGER NOT NULL DEFAULT 1";
                            sql += " ); ";
                        }
                        break;
                    case 2:
                        {
                            sql = null;
                        }
                        break;
                }

                return sql;
            }

        }

        public static class TLANCAMENTOS
        {

            public static String TABLE_NAME = "TLANCAMENTOS";
            public static String COLUMN_ID = "ID";
            public static String COLUMN_DT_CAD = "DT_CAD";
            public static String COLUMN_TIPO = "TIPO";
            public static String COLUMN_NUMERO = "NUMERO";
            public static String COLUMN_PARCELAS = "PARCELAS";
            public static String COLUMN_DT_VCTO = "DT_VCTO";
            public static String COLUMN_DT_PGTO = "DT_PGTO";
            public static String COLUMN_VALOR = "VALOR";
            public static String COLUMN_VLR_PGTO = "VLR_PGTO";
            public static String COLUMN_LANC_ID = "LANC_ID";
            public static String COLUMN_USU_ID = "USU_ID";
            public static String COLUMN_CAT_ID = "CAT_ID";
            public static String COLUMN_ITEM_ID = "ITEM_ID";
            public static String COLUMN_ITDEBT_ID = "ITDEBT_ID";
            public static String COLUMN_STATUS = "STATUS";

            public static String scriptInsert()
            {

                String sql;
                sql = "INSERT INTO " + TABLE_NAME;
                sql += " ( " + COLUMN_DT_CAD;
                sql += " , " + COLUMN_TIPO;
                sql += " , " + COLUMN_NUMERO;
                sql += " , " + COLUMN_PARCELAS;
                sql += " , " + COLUMN_DT_VCTO;
                sql += " , " + COLUMN_DT_PGTO;
                sql += " , " + COLUMN_VALOR;
                sql += " , " + COLUMN_VLR_PGTO;
                sql += " , " + COLUMN_LANC_ID;
                sql += " , " + COLUMN_USU_ID;
                sql += " , " + COLUMN_CAT_ID;
                sql += " , " + COLUMN_ITEM_ID;
                sql += " , " + COLUMN_ITDEBT_ID;
                sql += " , " + COLUMN_STATUS;
                sql += " ) VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ";

                return sql;
            }

            public static String scriptUpdate()
            {
                String sql;
                sql = "UPDATE " + TABLE_NAME;
                sql += " SET " + COLUMN_DT_CAD + " = ? ";
                sql += " , " + COLUMN_TIPO + " = ? ";
                sql += " , " + COLUMN_NUMERO + " = ? ";
                sql += " , " + COLUMN_PARCELAS + " = ? ";
                sql += " , " + COLUMN_DT_VCTO + " = ? ";
                sql += " , " + COLUMN_DT_PGTO + " = ? ";
                sql += " , " + COLUMN_VALOR + " = ? ";
                sql += " , " + COLUMN_VLR_PGTO + " = ? ";
                sql += " , " + COLUMN_LANC_ID + " = ? ";
                sql += " , " + COLUMN_USU_ID + " = ? ";
                sql += " , " + COLUMN_CAT_ID + " = ? ";
                sql += " , " + COLUMN_ITEM_ID + " = ? ";
                sql += " , " + COLUMN_ITDEBT_ID + " = ? ";
                sql += " , " + COLUMN_STATUS + " = ? ";
                sql += " WHERE " + COLUMN_ID + " = ? ";

                return sql;
            }


            public static String scriptSelect(ValuesWhere pValues)
            {
                return scriptSelect(null, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues)
            {
                return scriptSelect(pColunms, pValues, null);
            }

            public static String scriptSelect(String[] pColunms, ValuesWhere pValues, String[] pOrderBy)
            {

                String[] vColunms;
                String[] vOrderBy;
                if (pColunms != null)
                {
                    vColunms = pColunms;
                }
                else {

                    vColunms = new String[15];

                    vColunms[00] = TLANCAMENTOS.COLUMN_ID;
                    vColunms[01] = TLANCAMENTOS.COLUMN_DT_CAD;
                    vColunms[02] = TLANCAMENTOS.COLUMN_TIPO;
                    vColunms[03] = TLANCAMENTOS.COLUMN_NUMERO;
                    vColunms[04] = TLANCAMENTOS.COLUMN_PARCELAS;
                    vColunms[05] = TLANCAMENTOS.COLUMN_DT_VCTO;
                    vColunms[06] = TLANCAMENTOS.COLUMN_DT_PGTO;
                    vColunms[07] = TLANCAMENTOS.COLUMN_VALOR;
                    vColunms[08] = TLANCAMENTOS.COLUMN_VLR_PGTO;
                    vColunms[09] = TLANCAMENTOS.COLUMN_LANC_ID;
                    vColunms[10] = TLANCAMENTOS.COLUMN_USU_ID;
                    vColunms[11] = TLANCAMENTOS.COLUMN_CAT_ID;
                    vColunms[12] = TLANCAMENTOS.COLUMN_ITEM_ID;
                    vColunms[13] = TLANCAMENTOS.COLUMN_ITDEBT_ID;
                    vColunms[14] = TLANCAMENTOS.COLUMN_STATUS;

                }

                if (pOrderBy != null)
                {
                    vOrderBy = pOrderBy;
                }
                else {
                    vOrderBy = new String[1];
                    vOrderBy[0] = TLANCAMENTOS.COLUMN_LANC_ID;
                    vOrderBy[0] = TLANCAMENTOS.COLUMN_NUMERO;
                }


                return sqlSelect(TLANCAMENTOS.TABLE_NAME, vColunms, pValues, vOrderBy);
            }

            public static String scriptDelete(ValuesWhere pValues)
            {

                return sqlDelete(TLANCAMENTOS.TABLE_NAME, pValues);
            }


            public static String scriptCreate(int version)
            {
                String sql = null;
                switch (version)
                {
                    case 1:
                        {
                            sql = "CREATE TABLE " + TABLE_NAME;
                            sql += " ( " + COLUMN_ID + " INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT ";
                            sql += " , " + COLUMN_DT_CAD + " DATE NOT NULL ";
                            sql += " , " + COLUMN_TIPO + " VARCHAR(1) NOT NULL ";
                            sql += " , " + COLUMN_NUMERO + " INTEGER NOT NULL ";
                            sql += " , " + COLUMN_PARCELAS + " INTEGER NOT NULL ";
                            sql += " , " + COLUMN_DT_VCTO + " DATE NOT NULL ";
                            sql += " , " + COLUMN_DT_PGTO + " DATE ";
                            sql += " , " + COLUMN_VALOR + " DECIMAL(17,8)  NOT NULL ";
                            sql += " , " + COLUMN_VLR_PGTO + " DECIMAL(17,8) ";
                            sql += " , " + COLUMN_LANC_ID + " INTEGER NOT NULL REFERENCES " + TABLE_NAME + "(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_USU_ID + " INTEGER NOT NULL REFERENCES TUSUARIOS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_CAT_ID + " INTEGER NOT NULL REFERENCES TCATEGORIAS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_ITEM_ID + " INTEGER NOT NULL REFERENCES TITENS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_ITDEBT_ID + " INTEGER NOT NULL REFERENCES TITENS(ID) ON DELETE CASCADE ";
                            sql += " , " + COLUMN_STATUS + " INTEGER NOT NULL DEFAULT 0 ";
                            sql += " ); ";
                        }
                        break;
                    case 2:
                        {
                            sql = "ALTER TABLE ADD " + COLUMN_CAT_ID + " INTEGER NOT NULL REFERENCES TCATEGORIAS(ID) ON DELETE CASCADE "; 
                        }
                        break;
                }

                return sql;
            }

           
        }

        public static List<String> scriptCreate(int version)
        {

            List<String> listSql = new List<string>();
            String sql;

            sql = TUSUARIOS.scriptCreate(version);
            if (sql != null) {
                listSql.Add(sql);
            };

            sql = TCATEGORIAS.scriptCreate(version);
            if (sql != null)
            {
                listSql.Add(sql);
            };
            
            sql = TITENS.scriptCreate(version);
            if (sql != null)
            {
                listSql.Add(sql);
            };

            sql = TMETAS.scriptCreate(version);
            if (sql != null)
            {
                listSql.Add(sql);
            };

            sql = TLANCAMENTOS.scriptCreate(version);
            if (sql != null)
            {
                listSql.Add(sql);
            };

            return listSql;
        }
    }
}

