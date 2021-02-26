using System;
using System.Collections.Generic;
using System.Text;
using TesteAilos.Modelo.ProdutoColection;

namespace TesteAilos.Modelo.CarrinhoColection
{
    public class Carrinho
    {
        public List<Produto> produtos { get; set; }
        public int precoTotal { get; set; }
        public int quantidadeTotal { get; set; }
        public string idUsuario { get; set; }
        public string _id { get; set; }

    }
}
