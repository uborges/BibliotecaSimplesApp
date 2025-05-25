using System;

namespace BibliotecaSimples.Models
{
    public class Emprestimo
    {
        private static int proximoIdEmprestimo = 1;
        public int Id { get; private set; }

        public int MembroId { get; set; }
        public Membro Membro { get; set; }

        public int LivroId { get; set; }
        public Livro Livro { get; set; } // Especificamente Livro, pois ILocavel não tem Id diretamente

        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }

        public Emprestimo(Membro membro, Livro livro)
        {
            if (livro == null)
            {
                throw new ArgumentNullException(nameof(livro), "O livro não pode ser nulo para um empréstimo.");
            }
            if (membro == null)
            {
                throw new ArgumentNullException(nameof(membro), "O membro não pode ser nulo para um empréstimo.");
            }

            ILocavel itemLocavel = livro as ILocavel; // Livro implementa ILocavel

            if (itemLocavel == null) // Verificação extra, embora Livro sempre implemente
            {
                throw new ArgumentException("O item fornecido não é locável.");
            }

            if (itemLocavel.EstaEmprestado)
            {
                throw new InvalidOperationException("Este livro já está emprestado.");
            }

            Id = proximoIdEmprestimo++;
            Membro = membro;
            MembroId = membro.Id;
            Livro = livro;
            LivroId = livro.Id;

            if (itemLocavel.Emprestar(membro))
            {
                DataEmprestimo = itemLocavel.DataEmprestimo.Value;
                DataDevolucaoPrevista = itemLocavel.DataDevolucaoPrevista.Value;
                DataDevolucaoReal = null;
                membro.Emprestimos.Add(this);
            }
            else
            {
                throw new Exception("Falha ao registrar o empréstimo no item locável.");
            }
        }

        public void RegistrarDevolucao()
        {
            if (Livro is ILocavel itemLocavel)
            {
                itemLocavel.Devolver();
                DataDevolucaoReal = DateTime.Now;
            }
        }

        public override string ToString()
        {
            return $"ID Emp.{Id}: {Livro.Titulo} -> {Membro.Nome} (Dev: {DataDevolucaoPrevista:dd/MM/yy})";
        }
    }
}