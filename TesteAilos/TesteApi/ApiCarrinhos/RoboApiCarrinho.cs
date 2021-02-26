using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;
using System.Text.Json;
using TechTalk.SpecFlow;
using TesteAilos.Modelo.CarrinhoColection;

namespace TesteAilos.TesteApi.ApiProdutos
{
    [Binding]
    public class RoboApiCarrinho
    {
        string urlBase = "http://localhost:3000/carrinhos";
        string urlConsultaQtdTotal = "http://localhost:3000/carrinhos?quantidadeTotal=";
        int qtdTotalCarrinho = 3;

        CarinhoApi carrinhoApi = new CarinhoApi();

        IRestResponse respostaGet;

        [Given(@"eu tenha a url da api de carrinhos")]
        public RoboApiCarrinho DadoEuTenhaAUrlDaApiDeCarrinhos()
        {
            urlBase = "http://localhost:3000/carrinhos";
            return this;
        }

        [When(@"eu enviei uma requisicao de consulta")]
        public RoboApiCarrinho QuandoEuEnvieiUmaRequisicaoDeConsulta()
        {
            var clienteGet = new RestClient(urlConsultaQtdTotal + qtdTotalCarrinho.ToString());
            var requisicaoGet = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
            respostaGet = clienteGet.Execute(requisicaoGet);

            carrinhoApi = JsonSerializer.Deserialize<CarinhoApi>(respostaGet.Content);
            return this;
        }

        [Then(@"api retornou status OK com os carrinho com quantidade total maiores que cinco")]
        public RoboApiCarrinho EntaoApiRetornouStatusOKComOsCarrinho()
        {
            
            CarinhoApi carrinhoApi = GetCarrinhoSerializados();
            var validacaoQtdTotal = carrinhoApi.carrinhos.FirstOrDefault(c => c.quantidadeTotal < 5);
            

            if (respostaGet.StatusCode == HttpStatusCode.OK && validacaoQtdTotal == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
            return this;
        }


        public CarinhoApi GetCarrinhoSerializados()
        {
            IRestResponse respostaGet;

            var cliente = new RestClient(urlBase);
            var requisicao = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json }; ;
            respostaGet = cliente.Execute(requisicao);

            carrinhoApi = JsonSerializer.Deserialize<CarinhoApi>(respostaGet.Content);

            return carrinhoApi;
        }

    }
}
