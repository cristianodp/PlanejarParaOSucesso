using System;
using System.Collections.Generic;
using System.Text;

namespace SharedPlanejar.Models
{
    public class Categoria
    {
        public int id { get; set; }
        public String descricao { get; set; }
        private String tipo { get; set; }
        public int cor { get; set; }
        public int visivel { get; set; }
        public int usu_id { get; set; }
        public int ativo { get; set; }

        public String getTipo(int pTipo)
        {

            String ret = tipo;
            if (pTipo == 1)
            {
                switch (this.tipo.ToUpper())
                {
                    case "R":
                        ret = "Receita";
                        break;
                    case "D":
                        ret = "Despesa";
                        break;
                        //default: ret = new Utils.Retorno(false,"Tipo de categoria invalido");
                }
            }
            return ret;
        }

        public void setTipo(String tipoCategoria)
        {

            switch (tipoCategoria.ToUpper())
            {
                case "RECEITA":
                    this.tipo = "R";
                    break;
                case "DESPESA":
                    this.tipo = "D";
                    break;
                case "R":
                    this.tipo = "R";
                    break;
                case "D":
                    this.tipo = "D";
                    break;
                    //default: ret = new Utils.Retorno(false,"Tipo de categoria invalido");
            }

        }

        public Boolean isValid(String[] err)
        {
            if (descricao == null)
            {
                err[0] = "Descrição da categoria não foi informada";
                return false;
            }

            if (tipo == null)
            {
                err[0] = "Tipo da categoria foi informado";
                return false;
            }

            if (cor == 0)
            {
                err[0] = "Cor da categoria não foi informada";
                return false;
            }

            if (usu_id == 0)
            {
                err[0] = "Usuário da categoria não foi informado";
                return false;
            }

            

            return true;
        }

    }
}
