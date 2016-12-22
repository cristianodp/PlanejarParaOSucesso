using Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedPlanejar.Models
{
    public class Lancamento
    {
        public int id { get; set; }
        public DateTime dt_cad { get; set; }
        /*legenda
         d= despesa
         r=receita
         */
        public String tipo { get; set; }
        public int numero { get; set; }
        public int parcelas { get; set; }
        public DateTime dt_vcto { get; set; }
        public Nullable<DateTime> dt_pgto { get; set; }
        public Decimal valor { get; set; }
        public Decimal vlr_pgto { get; set; }
        public int status { get; set; }
        public int lanc_id { get; set; }
        public int usu_id { get; set; }
        public int cat_id { get; set; }
        public int item_id { get; set; }
        public int itdebt_id { get; set; }

        public string dt_mesAno { get
            {
                try
                {
                    var data = (DateTime)dt_pgto;

                    return data.ToString("MM/yyyy");
                }
                catch (Exception e)

                {
                    return "000000";
                }


            }

        }

        public bool isValid(string[] err)
        {
            if (dt_cad == null)
            {
                err[0] = "dt_cad do lancamento não foi informada";
                return false;
            }

            if (tipo == null)
            {
                err[0] = "Tipo do lancamento não foi informado";
                return false;
            }

            if (tipo != "R" && tipo != "D")
            {
                err[0] = "Tipo do lancamento informado náo é valido";
                return false;
            }
            
            if (dt_vcto == null)
            {
                err[0] = "dt_vcto do lancamento não foi informada";
                return false;
            }

            if (valor == 0)
            {
                err[0] = "valor do lancamento não foi informado";
                return false;
            }

            if (valor < 0)
            {
                err[0] = "valor do lancamento não pode ser menor que zero";
                return false;
            }

            if (cat_id == 0)
            {
                err[0] = "categoria do lancamento não foi informada";
                return false;
            }

            
           /* if (dt_vcto < DateTime.Now.AddDays(-1))
            {
                err[0] = "Data do lancamento informanda é invalida";
                return false;
            }
            */

            if (usu_id == 0)
            {
                err[0] = "O usuário não do lancamento não foi informada";
                return false;
            }

            if (item_id == 0)
            {
                err[0] = "O item_id não do lancamento não foi informada";
                return false;
            }

            return true;
        }

       public Item getItem() {

            return new ControleItem().GetItem(item_id);
        }

        public Item getItemDebt()
        {
            return new ControleCategoria().getConta(itdebt_id);
        }

        public Categoria getCategoria()
        {
            return new ControleCategoria().getCategoria(cat_id);
        }

        public String getDtCad()
        {
            return dt_cad.ToString("dd/MM/yyyy");
        }

        public String getDtVecto()
        {
            return dt_vcto.ToString("dd/MM/yyyy");
        }

        public String getDtPagto()
        {
            if (dt_pgto != null)
            {
                return Convert.ToDateTime(dt_pgto).ToString("dd/MM/yyyy");
            }
            else
            {
                return null;
            }
            
        }
        public DateTime GetDataTit()
        {

            if (this.status == 0)
            {
                return dt_vcto;
            }
            else
            {
                return Convert.ToDateTime(dt_pgto);
            }


        }
        public Decimal GetValorTit() {

            if (this.status == 0)
            {
                return valor;
            }
            else {
                return vlr_pgto;
            }


        }

    }


}
