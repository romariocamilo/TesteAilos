using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TesteAilos.Modelo.CarrinhoColection;
using TesteAilos.Modelo.ProdutoColection;
using TesteAilos.TesteApi.ApiProdutos;

namespace TesteAilos.TesteApi
{

    //PAREI AQUI ->>>>>>>>>>> esses métodos só estão nessa classe porque estava realidando alguns testes
    //API DE CARRINHO NÃO CONCLUI

    public class ApiCarrinhoTeste
    {
        [Test]
        public void GetCarrinhoQtdTotalMaiorCinco()
        {
            _ = new RoboApiCarrinho()
                .DadoEuTenhaAUrlDaApiDeCarrinhos()
                .QuandoEuEnvieiUmaRequisicaoDeConsulta()
                .EntaoApiRetornouStatusOKComOsCarrinho();
        }


        //MÉTODO PARA TESTAR ALGUNS CENÁRIOS NÃO FINALIZADO
       [Test]
        public void CadastraCarrinho()
        {
            string token = RetornaToken();
            ProdutoApi produtoApi = RetornaProdutos();

            ProdutoPost produtoPost = new ProdutoPost();
            produtoPost.idProduto = produtoApi.produtos.FirstOrDefault()._id;
            produtoPost.quantidade = 10;

            List<ProdutoPost> listaProdutoPost = new List<ProdutoPost>();
            listaProdutoPost.Add(produtoPost);


            var clientePost = new RestClient("http://localhost:3000/carrinhos");
            var requisicaoPost = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            requisicaoPost.AddHeader("Authorization", token);

            requisicaoPost.AddJsonBody(new
            {
                produtos = listaProdutoPost
            });

            var respostas = clientePost.Execute(requisicaoPost);

            Assert.Fail();
        }

        public string RetornaToken()
        {
            var clientePost = new RestClient("http://localhost:3000/login");
            var requisicaoPost = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            requisicaoPost.AddJsonBody(new
            {
                email = "fulano@qa.com",
                password = "teste"
            });
            var respostaLogin = clientePost.Execute(requisicaoPost);
            CarrinhoResponse carrinhoResponse = new CarrinhoResponse();
            carrinhoResponse = JsonSerializer.Deserialize<CarrinhoResponse>(respostaLogin.Content);

            return carrinhoResponse.authorization;
        }

        public ProdutoApi RetornaProdutos()
        {
            var clienteGet = new RestClient("http://localhost:3000/produtos");
            var requisicaoGet = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            string token = RetornaToken();
            requisicaoGet.AddHeader("Authorization", token);

            var respotaGetProdutos = clienteGet.Execute(requisicaoGet);


            ProdutoApi produtoResponse = new ProdutoApi();
            produtoResponse = JsonSerializer.Deserialize<ProdutoApi>(respotaGetProdutos.Content);

            return produtoResponse;
        }
    }
}
