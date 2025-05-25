using System.Collections.Generic;
using BibliotecaSimplesApp.Models;

namespace BibliotecaSimples.Models
{
    public class Membro
    {
        private static int proximoIdMembro = 1;
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Emprestimo> Emprestimos { get; private set; }

        public Membro(string nome, string email)
        {
            Id = proximoIdMembro++;
            Nome = nome;
            Email = email;
            Emprestimos = new List<Emprestimo>();
        }

        public override string ToString()
        {
            return $"{Id}: {Nome} ({Email})";
        }
    }
}
