using System;
using BibliotecaSimplesApp.Models;

namespace BibliotecaSimplesApp.Models
{
    public abstract class ItemBiblioteca
    {
        private static int proximoIdItem = 1;
        public int Id { get; private set; }
        public string Titulo { get; set; }
        public int AnoPublicacao { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        protected ItemBiblioteca(string titulo, int anoPublicacao, Categoria categoria)
        {
            Id = proximoIdItem++;
            Titulo = titulo;
            AnoPublicacao = anoPublicacao;

            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria), "A categoria não pode ser nula para um ItemBiblioteca.");
            }
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public abstract string ExibirDetalhes();

        public override string ToString()
        {
            return $"{Id}: {Titulo} ({AnoPublicacao}) - Cat: {Categoria?.Nome ?? "N/A"}";
        }
    }
}
