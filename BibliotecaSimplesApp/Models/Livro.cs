using System;
using BibliotecaSimplesApp.Models;

namespace BibliotecaSimples.Models
{
    public class Livro : ItemBiblioteca, ILocavel
    {
        public string Autor { get; set; }
        public string ISBN { get; set; }

        public bool EstaEmprestado { get; private set; }
        public Membro EmprestadoPara { get; private set; }
        public DateTime? DataEmprestimo { get; private set; }
        public DateTime? DataDevolucaoPrevista { get; private set; }

        public Livro(string titulo, int anoPublicacao, Categoria categoria, string autor, string isbn)
            : base(titulo, anoPublicacao, categoria)
        {
            Autor = autor;
            ISBN = isbn;
            EstaEmprestado = false;
            EmprestadoPara = null;
        }

        public override string ExibirDetalhes()
        {
            return $"LIVRO: {Titulo}\nAutor: {Autor}\nAno: {AnoPublicacao}\nISBN: {ISBN}\nCategoria: {Categoria.Nome}\nStatus: {(EstaEmprestado ? $"Emprestado para {EmprestadoPara?.Nome ?? "Desconhecido"}" : "Disponível")}";
        }

        public bool Emprestar(Membro membro)
        {
            if (!EstaEmprestado && membro != null)
            {
                EstaEmprestado = true;
                EmprestadoPara = membro;
                DataEmprestimo = DateTime.Now;
                DataDevolucaoPrevista = DateTime.Now.AddDays(14); // Prazo de 14 dias
                return true;
            }
            return false;
        }

        public bool Devolver()
        {
            if (EstaEmprestado)
            {
                EstaEmprestado = false;
                EmprestadoPara = null;
                DataEmprestimo = null;
                DataDevolucaoPrevista = null;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Id}: {Titulo} ({Autor}) - {(EstaEmprestado ? "Emprestado" : "Disponível")}";
        }
    }
}
