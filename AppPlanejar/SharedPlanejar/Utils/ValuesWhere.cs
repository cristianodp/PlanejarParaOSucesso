using System;
using System.Collections.Generic;
using System.Text;

namespace SharedPlanejar.Utils
{
    public class ValuesWhere
    {

        private List<Value> values  = new List<Value>();

        public void clear()
        {
            values.Clear();
        }

        /*******| strings |************/
        public void add(String pCampo, String[] pValores)
        {
            Value v = new Value();

            v.set(pCampo, pValores);

            values.Add(v);
        }

        public void add(String pCampo, String[] pValores, String pTipoData)
        {
            Value v = new Value();

            v.set(pCampo, pValores, pTipoData);

            values.Add(v);
        }

        public void add(String pCampo, String pValor, String pTipoData)
        {
            Value v = new Value();
            String[] valor = new String[1];
            valor[0] = pValor;
            v.set(pCampo, valor, pTipoData);

            values.Add(v);
        }

        public void add(String pCampo, String[] pValores, String pTipoData, String oper)
        {
            Value v = new Value();

            v.set(pCampo, pValores, pTipoData, oper);

            values.Add(v);
        }

        
        public void add(String pCampo, String pValor, String pTipoData, String oper)
        {
            Value v = new Value();
            String[] valor = new String[1];
            valor[0] = pValor;
            v.set(pCampo, valor, pTipoData, oper);

            values.Add(v);
        }

        public void add(String pCampo, String pValor)
        {
            Value v = new Value();
            String[] valor = new String[1];
            valor[0] = pValor;
            v.set(pCampo, valor);

            values.Add(v);
        }

        /************* int ***********/
        public void add(String pCampo, int[] pValores)
        {
            Value v = new Value();

            v.set(pCampo, pValores);

            values.Add(v);
        }

        public void add(String pCampo, int[] pValores, String pTipoData)
        {
            Value v = new Value();

            v.set(pCampo, pValores, pTipoData);

            values.Add(v);
        }

        public void add(String pCampo, int pValor, String pTipoData)
        {
            Value v = new Value();
            int[] valor = new int[1];
            valor[0] = pValor;
            v.set(pCampo, valor, pTipoData);

            values.Add(v);
        }

        public void add(String pCampo, int[] pValores, String pTipoData, String oper)
        {
            Value v = new Value();

            v.set(pCampo, pValores, pTipoData, oper);

            values.Add(v);
        }

        public void add(String pCampo, int pValor, String pTipoData, String oper)
        {
            Value v = new Value();
            int[] valor = new int[1];
            valor[0] = pValor;
            v.set(pCampo, valor, pTipoData, oper);

            values.Add(v);
        }

        public void add(String pCampo, int pValor)
        {
            Value v = new Value();
            int[] valor = new int[1];
            valor[0] = pValor;
            v.set(pCampo, valor);

            values.Add(v);
        }

        /*******| boolean |************/
        public void add(String pCampo, Boolean pValores)
        {
            Value v = new Value();

            v.set(pCampo, pValores);

            values.Add(v);
        }

        public void add(String pCampo, Boolean pValores, String oper)
        {
            Value v = new Value();

            v.set(pCampo, pValores, oper);

            values.Add(v);
        }
        /*******| boolean |************/
        public void add(String pCampo, DateTime pValores)
        {
            Value v = new Value();

            v.set(pCampo, pValores);

            values.Add(v);
        }

        public void add(String pCampo, DateTime pValores, String oper)
        {
            Value v = new Value();

            v.set(pCampo, pValores, oper);

            values.Add(v);
        }


        /************| complementar |*************/
        public void addComplem(String pValor)
        {
            Value v = new Value();

            String[] valor = new String[1];
            valor[0] = pValor;
            v.setComplem(valor);
            values.Add(v);
        }
        public String getScrFilter()
        {
            String ret = null;

            if (values.Count > 0)
            {

                foreach(Value item in values)
                {
                    if (ret == null)
                    {
                        ret = item.getScrFilter();
                    }
                    else {
                        ret += " AND " + item.getScrFilter();
                    }
                }

            }

            return ret;
        }

        private class Value
        {

            private String campo = null;
            private String[] txtVlr = null;
            private int[] NumVlr = null;
            private Boolean BolVlr = false;
            private DateTime DateVlr;

            /*Tipos Data
            * C  = CARACTER
            * N  = NUMERICO
            * D  = DATA
            * B  = BOOLEAN
            * E  = EXISTS
            * R  = RELACIONAMENTO*/
            private String tipoData = null;

            /*Operacao
            * = (Igual)
            * != (DIFERENTE)
            * LIKE (CONTENTO)
            * NOT LIKE (NÃO CONTENTO)
            * < (MENOR QUE)
            * > (MAIOR QUE)
            * >= MAIOR E IGUAL
            * <= MENOR E IGUAL
            * EXIST (SUBSELECT COM EXISTS)*/
            private String operacao = null;

            public void set(String pCampo, String[] pValores)
            {
                campo = pCampo;
                txtVlr = pValores;
                tipoData = "C";
                operacao = "=";
            }

            public void set(String pCampo, String[] pValores, String pTipoData)
            {
                campo = pCampo;
                txtVlr = pValores;
                tipoData = pTipoData;
                operacao = "=";
            }

            public void set(String pCampo, String[] pValores, String pTipoData, String oper)
            {
                campo = pCampo;
                txtVlr = pValores;
                tipoData = pTipoData;
                operacao = oper;
            }

            public void set(String pCampo, int[] pValores)
            {
                campo = pCampo;
                NumVlr = pValores;
                tipoData = "N";
                operacao = "=";
            }

            public void set(String pCampo, int[] pValores, String pTipoData)
            {
                campo = pCampo;
                NumVlr = pValores;
                tipoData = pTipoData;
                operacao = "=";
            }

            public void set(String pCampo, int[] pValores, String pTipoData, String oper)
            {
                campo = pCampo;
                NumVlr = pValores;
                tipoData = pTipoData;
                operacao = oper;
            }

            public void set(String pCampo, Boolean pValor)
            {
                campo = pCampo;
                BolVlr = pValor;
                tipoData = "B";
                operacao = "=";
            }

            public void set(String pCampo, Boolean pValor, String oper)
            {
                campo = pCampo;
                BolVlr = pValor;
                tipoData = "B";
                operacao = oper;
            }


            /**** date *****/
            public void set(String pCampo, DateTime pValor)
            {
                campo = pCampo;
                DateVlr = pValor;
                tipoData = "D";
                operacao = "=";
            }

            public void set(String pCampo, DateTime pValor, String oper)
            {
                campo = pCampo;
                DateVlr = pValor;
                tipoData = "D";
                operacao = oper;
            }

            public void setComplem(String[] pValor)
            {
                campo = " ";
                txtVlr = pValor;
                tipoData = "E";
                operacao = "complem";
            }
            public String getScrFilter()
            {
                String retorno = null;

                if ((campo != null) && (tipoData != null))
                {

                    switch (tipoData.ToUpper())
                    {
                        case "C":
                            {

                                if (txtVlr.Length > 1)
                                {
                                    if (operacao == "=")
                                    {
                                        retorno = " (" + campo + " in('" + txtVlr.ToString().Replace(",", "','") + "'))";
                                    }
                                    else if (operacao.Equals( "!="))
                                    {
                                        retorno = " (" + campo + " not in('" + txtVlr.ToString().Replace(",", "','") + "'))";
                                    }
                                    else {
                                        if (operacao.Equals("LIKE") || operacao.Equals("NOT LIKE"))
                                        {
                                            retorno = " ( ";
                                            for (int x = 0; x >= txtVlr.Length; x++)
                                            {
                                                if (x == 0)
                                                {
                                                    retorno += " (UPPER(" + campo + ") " + operacao + " UPPER('" + txtVlr[x] + "'))";
                                                }
                                                else {
                                                    retorno += " OR (UPPER(" + campo + ") " + operacao + " 'UPPER(" + txtVlr[x] + "'))";
                                                }
                                            }
                                            retorno = " ) ";
                                        }
                                        else {
                                            retorno += " (" + campo + " " + operacao + " '" + txtVlr[0] + "')";
                                        }
                                    }
                                }
                                else {
                                    if ((operacao == "=") || (operacao == "!="))
                                    {
                                        retorno = " (" + campo + " " + operacao + " '" + txtVlr[0] + "')";
                                    }
                                    else if (operacao == "LIKE" || operacao == "NOT LIKE")
                                    {
                                        retorno = " ( ";
                                        for (int x = 0; x < txtVlr.Length; x++)
                                        {
                                            if (x == 0)
                                            {
                                                retorno += " (UPPER(" + campo + ") " + operacao + " UPPER('" + txtVlr[x] + "'))";
                                            }
                                            else {
                                                retorno += " OR (UPPER(" + campo + ") " + operacao + " UPPER('" + txtVlr[x] + "'))";
                                            }
                                        }
                                        retorno += " ) ";
                                    }
                                    else {
                                        retorno += " (" + campo + " " + operacao + " '" + txtVlr[0] + "')";
                                    }
                                }

                            }
                            break;
                        case "N":
                            {
                                if (NumVlr.Length > 1)
                                {

                                    if (operacao == "=")
                                    {
                                        retorno = " (" + campo + " in(" + NumVlr.ToString() + "))";
                                    }
                                    else if (operacao == "!=")
                                    {
                                        retorno = " (" + campo + " not in('" + txtVlr.ToString().Replace(",", "','") + "'))";
                                    }
                                    else if (operacao == "<=" || operacao == ">=" || operacao == ">" || operacao == "<")
                                    {
                                        retorno = " ( ";
                                        for (int x = 0; x >= txtVlr.Length; x++)
                                        {
                                            if (x == 0)
                                            {
                                                retorno += " (" + campo + " " + operacao + " " + txtVlr[x] + ")";
                                            }
                                            else {
                                                retorno += " OR (" + campo + " " + operacao + " " + txtVlr[x] + ")";
                                            }
                                        }
                                        retorno += " ) ";
                                    }


                                }
                                else {
                                    retorno = " " + campo + " = " + NumVlr[0];
                                }
                            }
                            break;
                        case "D":
                            {
                                retorno = " " + campo + " " + operacao + " '" + DateVlr.ToString("dd/mm/yyyy")+"'";
                            }
                            break;
                        case "B":
                            {
                                if (BolVlr)
                                {
                                    retorno = " " + campo + " " + operacao + " 1 ";
                                }
                                else {
                                    retorno = " " + campo + " " + operacao + " 0 ";
                                }

                            }
                            break;
                        case "E":
                            {
                                if (txtVlr != null)
                                {
                                    retorno = " " + txtVlr[0];
                                }
                            }
                            break;
                        case "R":
                            {
                                if (txtVlr != null)
                                {
                                    retorno = " (" + campo + " " + operacao + " " + txtVlr[0] + ")";
                                }
                            }
                            break;
                    }

                }

                return retorno;

            }
        }
    }

}
