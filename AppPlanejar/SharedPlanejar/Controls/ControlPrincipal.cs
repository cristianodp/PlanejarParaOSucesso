using Models.ADO;
using SharedPlanejar.Models;
using SharedPlanejar.Utils;
using SharedPlanejar.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Utils;
//using ControlUsuario.getUsrIdLogado;

namespace Controls
{
    class ControlPrincipal
    {
        private Usuario usr;
        private UsuariosADO usrADO;
        

        public ControlPrincipal() {
            usrADO = new UsuariosADO();

        }

        public Boolean isLogado()
        {

            try
            {
                usrADO.ClearWhere();
                usrADO.where.add(dbTables.TUSUARIOS.COLUMN_LOGADO, 1);
                usr = usrADO.consultar().FirstOrDefault();
                if (usr != null)
                {
                    return true;

                }
            }
            catch (Exception e)
            {
                return false;
            }

            return false;
        }

        


    }
}
