using Models.ADO;
using SharedPlanejar.Models;
using SharedPlanejar.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    class ControlUsuario
    {
        private Usuario usr;
        private UsuariosADO usrADO;

        public ControlUsuario() {

            usrADO = new UsuariosADO();
        }

        public Boolean ExistUsr() {

            usrADO.ClearWhere();
            if (usrADO.consultar().Count > 0){
                return true;
            }
            return false;
        }

        public void addUsuario(String nome, String email, String Senha) {

            Usuario item = new Usuario();
            item.nome = nome;
            item.email = email;
            item.senha = Senha;
            item.ativo = 1;
            item.logado = 1;
       
            String[] err = new String[1];
            if (!item.isValid(err)) {
                throw new Exception(err[0]);
            };

            usrADO.insert(item);
        }

        public Boolean login(String usr, String senha) {

            usrADO.ClearWhere();
            usrADO.where.add(dbTables.TUSUARIOS.COLUMN_EMAIL, usr);
            usrADO.where.add(dbTables.TUSUARIOS.COLUMN_SENHA, senha);
            Usuario usuario = usrADO.consultar().FirstOrDefault();
            if (usuario != null) {
                usuario.logado = 1;
                usrADO.update(usuario);
                return true;
            };
            return false;
        }

        public int getUsrIdLogado()
        {
            UsuariosADO usrADO = new UsuariosADO();
            usrADO.ClearWhere();
            usrADO.where.add(dbTables.TUSUARIOS.COLUMN_LOGADO, 1);
            try
            {
                int usrid = usrADO.consultar().FirstOrDefault().id;
                return usrid;
            }
            catch (Exception e) {
                return 0;
            }
           
        }
    }
}
