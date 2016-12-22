using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedPlanejar.Models
{
    public class Item
    {
        public int id { get; set; }
        public String descricao { get; set; }
        /* Tipo n = item normal de categoria
           tipo c = item de conta financeira */
        public String tipo { get; set; }

        /* LEGANDAS
        C= CONTA CORRENTE
        P= CONTA POUPANÇA
        O= OUTRAS
        */

        private String tpCTA { get; set; }
        public int cor { get; set; }
       // public int cat_id { get; set; }
        public int usu_id { get; set; }
        public int ativo { get; set; }

        public decimal getSaldo()
        {
            var control = new ControleMovimento();
            DateTime date = DateTime.Now;
          
            return getSaldo(date);
        }

        public decimal getSaldo(DateTime date)
        {
            var control = new ControleMovimento();
          
            decimal saldo = control.getSaldo(id, date);
            return saldo;
        }

        public decimal getSaldoAnterior()
        {
            var control = new ControleMovimento();
            DateTime date = DateTime.Now;
            DateTime firstOfNextMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

            decimal saldo = control.getSaldo(id, lastOfThisMonth);
            return saldo;
        }

        public Lancamento getLastMov() {

            return new ControleMovimento().getMovimentos().Where(a => a.item_id == this.id).OrderByDescending(b => b.dt_vcto).FirstOrDefault();
        }

        public List<Lancamento> getMovitos()
        {
            
            DateTime data = DateTime.Now;
            
            return getMovitos(data);
        }

        public List<Lancamento> getMovitos(DateTime data)
        {
            var control = new ControleMovimento();
            
            var lista = control.getMovimentos().Where(a=>(a.item_id == id || a.itdebt_id == id)&& a.GetDataTit() <= data).ToList();
            return lista;
        }

        public List<Lancamento> getMovReceitas()
        {
            var control = new ControleMovimento();
            DateTime data = DateTime.Now;
            var lista = control.getMovimentos();

            return lista.Where(a=>a.GetDataTit().Month == data.Month && 
                a.GetDataTit().Year == data.Year &&
                a.tipo.Equals("R")&&
                a.itdebt_id == id).ToList();
        }

        public List<Lancamento> getMovDespesas()
        {
            var control = new ControleMovimento();
            DateTime data = DateTime.Now;
            var lista = control.getMovimentos();

            return lista.Where(a => a.GetDataTit().Month == data.Month &&
                a.GetDataTit().Year == data.Year &&
                a.tipo.Equals("D") &&
                a.itdebt_id == id).ToList();
        }

        public decimal getReceitas()
        {

            return getMovReceitas().Sum(a => a.vlr_pgto);
            
        }

         public decimal getDespesas()
        {

            return getMovDespesas().Sum(a => a.vlr_pgto);
            
        }

      /*  private Categoria categoria
        {

            get
            {   var cat = new ControleCategoria().getCategoria(cat_id);
                return cat;
            }

        }
        */
       /* public Categoria getCategoria() {

            return new ControleCategoria().getCategoria(cat_id);
           
        }
        */

        public Boolean isValid(String[] err)
        {
            if (String.IsNullOrEmpty(descricao))
            {
                err[0] = "Descrição do item não foi informada";
                return false;
            }

            if (String.IsNullOrEmpty(tipo))            {
                err[0] = "Tipo do item não foi informado";
                return false;
            }

            if (tipo != "C" && tipo != "N")
            {
                err[0] = "Tipo do item informado náo é valido";
                return false;
            }

            if (tipo == "C")
            {
                if (tpCTA == null) { 
                    err[0] = "Tipo da conta informado náo foi informado";
                    return false;
                }
                if (tpCTA != "C" && tpCTA != "P" && tpCTA != "O")
                {
                    err[0] = "Tipo da conta informado náo é valido";
                    return false;
                }

                if (cor == 0)
                {
                    err[0] = "Cor do item não foi informada";
                    return false;
                }

            }
           /* else { 

                if (cat_id == 0)
                {
                    err[0] = "A categoria do item não foi informada";
                    return false;
                }
            }*/

            if (usu_id == 0)
            {
                err[0] = "O usuário não do item não foi informada";
                return false;
            }
            


            return true;
        }

        public String getTipoCta(int pTipo)
        {

            String ret = tpCTA;
            if (pTipo == 1)
            {
                switch (this.tpCTA.ToUpper())
                {
                    case "C":
                        ret = "Corrente";
                        break;
                    case "P":
                        ret = "Poupança";
                        break;
                    case "O":
                        ret = "Outras";
                        break;
                        //default: ret = new Utils.Retorno(false,"Tipo de categoria invalido");
                }
            }
            return ret;
        }

        public void setTipoCta(String tipo)
        {

            switch (tipo.ToUpper())
            {
                case "CORRENTE":
                    this.tpCTA = "C";
                    break;
                case "POUPANÇA":
                    this.tpCTA = "P";
                    break;
                case "OUTRAS":
                    this.tpCTA = "O";
                    break;
                case "C":
                    this.tpCTA = "C";
                    break;
                case "P":
                    this.tpCTA = "P";
                    break;
                case "O":
                    this.tpCTA = "O";
                    break;
            }

        }

        
    }
}
