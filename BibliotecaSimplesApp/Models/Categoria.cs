using System.Collections.Generic;

namespace BibliotecaSimples.Models
{
    public class Categoria
    {
        private static int proximoIdCategoria = 1;
        public int Id { get; private set; }
        public string Nome { get; set; }
        public List<ItemBiblioteca> Itens { get; private set; }

        public Categoria(string nome)
        {
            Id = proximoIdCategoria++;
            Nome = nome;
            Itens = new List<ItemBiblioteca>();
        }

        public void AdicionarItem(ItemBiblioteca item)
        {
            if (item != null && !Itens.Contains(item))
            {
                Itens.Add(item);
                if (item.Categoria != this)
                {
                    item.Categoria = this;
                    item.CategoriaId = this.Id;
                }
            }
        }

        public override string ToString()
        {
            return $"{Id}: {Nome}";
        }
    }
}
