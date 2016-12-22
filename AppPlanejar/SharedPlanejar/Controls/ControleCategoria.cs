using Models.ADO;
using SharedPlanejar.Models;
using SharedPlanejar.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SharedPlanejar.Models.ADO;

namespace Controls
{
    class ControleCategoria
    {
        private Categoria cat;
        private CategoriasADO catADO;
        private ItensADO itemADO;
        private MetasADO metaADO;


        public ControleCategoria()
        {

            catADO = new CategoriasADO();
            itemADO = new ItensADO();
            metaADO = new MetasADO();
        }

        public Categoria getCategoria(int catId)
        {

            catADO.ClearWhere();
            catADO.where.add(dbTables.TCATEGORIAS.COLUMN_ID, catId);
            return catADO.consultar().FirstOrDefault();

        }

        public void deleta(Categoria item)
        {
            catADO.apagar(item);

        }


        public Boolean ExistRecords()
        {

            catADO.ClearWhere();
            if (catADO.consultar().Count > 0)
            {
                return true;
            }
            return false;
        }

        public void Atualizar(Categoria item)
        {
            if (item == null)
            {
                throw new Exception("objeto categoria é nulo");
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

            if (item.id != 0)
            {
                catADO.update(item);
            }
            else
            {
                catADO.insert(item);
            }
        }
        
        public List<Categoria> Consultar()
        {
            int usrid = new ControlUsuario().getUsrIdLogado();
            catADO.ClearWhere();
            catADO.where.add(dbTables.TCATEGORIAS.COLUMN_USU_ID, usrid);
            return catADO.consultar();


        }

        internal Item getConta(int itemId)
        {
            itemADO.ClearWhere();
            itemADO.where.add(dbTables.TITENS.COLUMN_ID, itemId);
            return itemADO.consultar().FirstOrDefault();

        }

       
        public Meta GetMeta(int metCatId)
        {
            metaADO.ClearWhere();
            metaADO.where.add(dbTables.TMETAS.COLUMN_ID, metCatId);
            return metaADO.consultar().FirstOrDefault();

        }

         

        public void delelaItens(Item item)
        {
            itemADO.apagar(item);
        }

        public void AtualizaItem(Item item)
        {

            if (item == null)
            {
                throw new Exception("objeto item é nulo");
            }

            if (item.usu_id == 0)
            {
                item.usu_id = new ControlUsuario().getUsrIdLogado();
            }


            //            if (item.cat_id == 0 && item.tipo = "N")
            //            {
            //                throw new Exception("a categoria não foi selecionada");
            //            }

            String[] err = new String[1];
            if (!item.isValid(err))
            {
                throw new Exception(err[0]);
            };

            if (item.id != 0)
            {
                itemADO.update(item);
            }
            else
            {
                itemADO.insert(item);
            }
        }


        public List<Meta> GetMetas(int CatId)
        {

            metaADO.ClearWhere();
            metaADO.where.add(dbTables.TMETAS.COLUMN_CAT_ID, CatId);
            return metaADO.consultar();
        }

        public void delelaMeta(Meta item)
        {
            metaADO.apagar(item);
        }

        public void AtualizaMeta(Meta item)
        {

            if (item == null)
            {
                throw new Exception("objeto meta é nulo");
            }

            if (item.cat_id == 0)
            {
                throw new Exception("a categoria não foi selecionada");
            }

            String[] err = new String[1];
            if (!item.isValid(err))
            {
                throw new Exception(err[0]);
            };

            if (item.id != 0)
            {
                metaADO.update(item);
            }
            else
            {
                metaADO.insert(item);
            }
        }

        public List<Item> getContas()
        {

            int usrid = new ControlUsuario().getUsrIdLogado();

            itemADO.ClearWhere();
            itemADO.where.add(dbTables.TITENS.COLUMN_TIPO, "C");
            itemADO.where.add(dbTables.TITENS.COLUMN_USU_ID, usrid);


            return itemADO.consultar();
        }

        public void addConta(Categoria cat, Item item)
        {


            if (item == null)
            {
                throw new Exception("objeto item é nulo");
            }

            if (item.tipo != "C")
            {
                throw new Exception("Tipo de item informado inválido");
            }
           

        }


    }
}
