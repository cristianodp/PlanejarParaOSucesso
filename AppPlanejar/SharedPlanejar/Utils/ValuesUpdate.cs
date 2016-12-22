using System;
using System.Collections.Generic;
using System.Text;

namespace SharedPlanejar.Utils
{
    public class ValuesUpdate
    {
        private List<Value> values = new List<Value>();

        public List<Value> getList() {
            return values;
        }

        public ValuesUpdate() {
            values = new List<Value>();
        }

        public void clear() {
            values.Clear();
        }

        /*******| strings |************/
        public void add(String pCampo, String pValor)
        {
            Value v = new Value();

            v.set(pCampo, pValor);

            values.Add(v);
        }

        /*******| int |************/
        public void add(String pCampo, int pValor)
        {
            Value v = new Value();

            v.set(pCampo, pValor);

            values.Add(v);
        }

        /*******| Boolean |************/
        public void add(String pCampo, Boolean pValor)
        {
            Value v = new Value();

            v.set(pCampo, pValor);

            values.Add(v);
        }

        /*******| Decimal |************/
        public void add(String pCampo, Decimal pValor)
        {
            Value v = new Value();

            v.set(pCampo, pValor);

            values.Add(v);
        }

        /*******| DateTime |************/
        public void add(String pCampo, DateTime pValor)
        {
            Value v = new Value();

            v.set(pCampo, pValor);

            values.Add(v);
        }


        public class Value
        {

            private String campo = null;
            private String tipo;

            private String txtVlr = null;
            private int intVlr = 0;
            private Boolean bolVlr = false;
            private Decimal decVlr;
            private DateTime dateVlr;

            public void set(String campo, String valor) {
                this.campo = campo;
                this.txtVlr = valor;
                this.tipo = "S";
            }
            
            public void set(String campo, int valor)
            {
                this.campo = campo;
                this.intVlr = valor;
                this.tipo = "I";
            }

            public void set(String campo, Boolean valor)
            {
                this.campo = campo;
                this.bolVlr = valor;
                this.tipo = "B";
            }

            public void set(String campo, Decimal valor)
            {
                this.campo = campo;
                this.decVlr = valor;
                this.tipo = "D";
            }

            public void set(String campo, DateTime valor)
            {
                this.campo = campo;
                this.dateVlr = valor;
                this.tipo = "D";
            }
        }
    }
}
