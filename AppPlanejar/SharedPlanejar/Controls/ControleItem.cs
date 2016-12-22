using SharedPlanejar.Models;
using SharedPlanejar.Models.ADO;
using SharedPlanejar.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    class ControleItem
    {
        private ItensADO itemADO;
        

        public ControleItem()
        {
            itemADO = new ItensADO();
        }

       
        public Boolean ExistRecords()
        {

            itemADO.ClearWhere();
            if (itemADO.consultar().Count > 0)
            {
                return true;
            }
            return false;
        }

        public void Atualizar(Item item)
        {
            if (item == null)
            {
                throw new Exception("objeto é nulo");
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
                itemADO.update(item);
            }
            else
            {
                itemADO.insert(item);
            }
        }

        public void Delete(Item item)
        {
            itemADO.apagar(item);

        }

        public Item GetItem(int id)
        {

            itemADO.ClearWhere();
            itemADO.where.add(dbTables.TITENS.COLUMN_ID, id);
            return itemADO.consultar().FirstOrDefault();

        }

        public List<Item> GetItens(string tipo)
        {

            itemADO.ClearWhere();
            if (tipo.Equals("C"))
            {
                itemADO.where.add(dbTables.TITENS.COLUMN_TIPO, tipo);
                //itemADO.where.add(dbTables.TITENS.COLUMN_DESCRICAO, text, "C", "LIKE");
            }
            else
            {
               /* if (!todasCat)
                {
                    itemADO.where.add(dbTables.TITENS.COLUMN_CAT_ID, CatId);
                }
                */
                itemADO.where.add(dbTables.TITENS.COLUMN_TIPO, "N");
               // itemADO.where.add(dbTables.TITENS.COLUMN_DESCRICAO, "%" + text + "%", "C", "LIKE");
                itemADO.where.add(dbTables.TITENS.COLUMN_ATIVO, 1);
               /* itemADO.where.addComplem("EXISTS(SELECT 1"
                 + "  FROM " + dbTables.TCATEGORIAS.TABLE_NAME + " CAT "
                 + "WHERE cat." + dbTables.TCATEGORIAS.COLUMN_ID + " = "
                 + dbTables.TITENS.TABLE_NAME + "." + dbTables.TITENS.COLUMN_CAT_ID
                 + " and cat." + dbTables.TCATEGORIAS.COLUMN_TIPO + " = '" + tipo + "')");
                 */
            }

            return itemADO.consultar().OrderBy(a=>a.descricao).ToList();

        }

        
    }
}
