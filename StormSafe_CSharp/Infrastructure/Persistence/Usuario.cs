using StormSafe.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StormSafe.Infrastructure.Persistence
{
    public class Usuario
    {
        public Guid IdUsuario { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public TipoUsuario TipoUsuario { get; private set; }


       

        public Usuario(string nome, string email, string senha, TipoUsuario tipoUsuario = TipoUsuario.ADMIN)
        {
            ValidarNome(nome);
            ValidarEmail(email);
            ValidarSenha(senha);


            Nome = nome;
            Email = email;
            Senha = senha;
            TipoUsuario = tipoUsuario;
        }

        public void AtualizarUsuario(string nome, string email, string senha, TipoUsuario tipoUsuario)
        {
            ValidarNome(nome);
            ValidarEmail(email);
            ValidarSenha(senha);

            Nome = nome;
            Email = email;
            Senha = senha;
            TipoUsuario = tipoUsuario;
        }



        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome não pode ser vazio.");
            if (nome.Length > 100)
                throw new Exception("Nome deve ter no máximo 100 caracteres.");
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email não pode ser vazio.");
            var attr = new EmailAddressAttribute();
            if (!attr.IsValid(email))
                throw new Exception("Email inválido.");
        }

        private void ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Senha não pode ser vazia.");
            if (senha.Length > 100)
                throw new Exception("Senha deve ter no máximo 100 caracteres.");
        }

        

        internal static Usuario Create(string nome, string email, string senha, TipoUsuario tipoUsuario = TipoUsuario.ADMIN)
        {
            return new Usuario(nome, email, senha, tipoUsuario);
        }
    }

}
