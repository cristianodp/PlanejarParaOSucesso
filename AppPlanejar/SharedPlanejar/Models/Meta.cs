using System;
using System.Collections.Generic;
using System.Text;

namespace SharedPlanejar.Models
{
    public class Meta
    {
        public int id { get; set; }
        public DateTime dt_inicio { get; set; }
        public DateTime dt_final { get; set; }
        public Decimal valor { get; set; }
        public int cat_id { get; set; }
        public int ativo { get; set; }
        public Decimal getRealizado() {
            return 0;
        }

        public String getDtInicio() {

            return dt_inicio.ToString("dd/MM/yyyy");

        }

        public String getDtFinal()
        {
            return dt_final.ToString("dd/MM/yyyy");

        }

        public Boolean isValid(String[] err)
        {
            if (dt_inicio == null)
            {
                err[0] = "A data inicial da meta não foi informada";
                return false;
            }

            if (dt_final == null)
            {
                err[0] = "A data final da meta não foi informada";
                return false;
            }

            if (dt_final < dt_inicio)
            {
                err[0] = "A data final é menor que a dada inicial informada";
                return false;
            }

            if (valor == null)
            {
                err[0] = "O valor da meta não foi informado";
                return false;
            }

            if (valor == 0)
            {
                err[0] = "O valor da meta não pode ser zero";
                return false;
            }

            if (valor < 0)
            {
                err[0] = "O valor da meta não pode ser negativo";
                return false;
            }
            
            if (cat_id == 0)
            {
                err[0] = "A categoria da meta não foi informada";
                return false;
            }



            return true;
        }

    }
}
