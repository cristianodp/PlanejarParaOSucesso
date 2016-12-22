using System;
using System.Collections.Generic;
using System.Text;
using SharedPlanejar.Models;
using SharedPlanejar.Models.ADO;
using SharedPlanejar.Models.DB;
using Models.ADO;
using System.Linq;
using Java.Sql;

namespace Controls
{
    class ControleMovimento
    {
        private Lancamento Lanc;
        private LancamentosADO LancADO;
        

        public ControleMovimento()
        {
            LancADO = new LancamentosADO();
        }

        public Lancamento getLanc(int lancId)
        {

            LancADO.ClearWhere();
            LancADO.where.add(dbTables.TLANCAMENTOS.COLUMN_ID, lancId);
            return LancADO.consultar().FirstOrDefault();

        }

        public void deleta(Lancamento item)
        {
            LancADO.apagar(item);

        }


        public Boolean ExistRecords()
        {

            LancADO.ClearWhere();
            if (LancADO.consultar().Count > 0)
            {
                return true;
            }
            return false;
        }

        public void Atualizar(Lancamento item)
        {
            if (item == null)
            {
                throw new Exception("objeto lancamento é nulo");
            }

            if (item.usu_id == 0)
            {
                item.usu_id = new ControlUsuario().getUsrIdLogado();
            }

            String[] err = new String[1];
            if (!item.isValid(err))
            {
                throw new Exception(err[0]);
            };
            //se tiver marcado como pago e nao estiver informado o valor pago pega o valor do titulo 
            if ((item.status == 1) && (item.vlr_pgto == 0))
            {
                item.vlr_pgto = item.valor;
            } else if (item.status == 0){
                item.vlr_pgto = 0;
            };

            if (item.id != 0)
            {
                LancADO.update(item);
            }
            else
            {
                LancADO.insert(item);
            }
        }

        public List<Lancamento> Consultar(DateTime mesAno)
        {
            int mes = mesAno.Month;
            int ano = mesAno.Year;

            int usrid = new ControlUsuario().getUsrIdLogado();
            LancADO.ClearWhere();
            LancADO.where.add(dbTables.TLANCAMENTOS.COLUMN_USU_ID, usrid);
            
            LancADO.where.add("SUBSTR(" + dbTables.TLANCAMENTOS.COLUMN_DT_VCTO+ ",1, 7)", ano + "-" + mes.ToString("00"));
            return LancADO.consultar();


        }


        


        public decimal getSaldo(int it_id, DateTime data) {
            var movs = getMovimentos();
            decimal ent = movs.Where(a => a.GetDataTit().Date <= data.Date && a.itdebt_id == it_id && a.tipo.Equals("R")).Sum(s => s.vlr_pgto);
            decimal sai = movs.Where(a => a.GetDataTit().Date <= data.Date && a.itdebt_id == it_id && a.tipo.Equals("D")).Sum(s => s.vlr_pgto);
            return ent - sai;
        }


        public List<Lancamento> getMovimentos()
        {
            int usrid = new ControlUsuario().getUsrIdLogado();
            LancADO.ClearWhere();
            LancADO.where.add(dbTables.TLANCAMENTOS.COLUMN_USU_ID, usrid);
            return LancADO.consultar();

        }



        public List<Lancamento> GetMovsItem(int item_id)
        {
            //int mes = mesAno.Month;
            //int ano = mesAno.Year;

            int usrid = new ControlUsuario().getUsrIdLogado();
            LancADO.ClearWhere();
            LancADO.where.add(dbTables.TLANCAMENTOS.COLUMN_USU_ID, usrid);

           // LancADO.where.add("SUBSTR(" + dbTables.TLANCAMENTOS.COLUMN_DT_VCTO + ",1, 7)", ano + "-" + mes.ToString("00"));
            LancADO.where.add(dbTables.TLANCAMENTOS.COLUMN_ITDEBT_ID, item_id);
            return LancADO.consultar();


        }


    }
}
