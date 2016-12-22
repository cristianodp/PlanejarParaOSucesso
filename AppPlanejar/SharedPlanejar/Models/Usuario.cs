using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedPlanejar.Models
{
    public class Usuario
    {
        public Int32 id { get; set; }
        public String nome { get; set; }
        public String email { get; set; }
        public String senha { get; set; }
        public int ativo { get; set; }
        public int logado { get; set; }

        public Boolean isValid(String[] err) {
            if (nome == null || nome.Length <= 0 ) {
                err[0] = "Nome do usuário não foi informado";
                return false;
            }

            if (email == null || email.Length <= 0)
            {
                err[0] = "E-mail do usuário não foi informado";
                return false;
            }

            if (senha == null || senha.Length <= 0)
            {
                err[0] = "Senha do usuário não foi informada";
                return false;
            }
           
            if (!new RegexUtilities().IsValidEmail(email))
            {
                err[0] = "Email Inválido!";
                return false;
            }

            if (!new RegexUtilities().IsValidPasswd(senha))
            {
                err[0] = "Senha deve conter números e letras e ter mais que 8 caracteres";
                return false;
            }

            
            return true;
        }
    }
}
