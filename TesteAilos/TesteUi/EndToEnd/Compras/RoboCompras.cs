using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TesteAilos.Configuracao;
using TesteAilos.PageObject;
using TesteAilos.RoboGeralAuxiliar;

namespace TesteAilos
{
    [Binding]
    public class RoboCompras : RogoGeral
    {
        IWebDriver driver = new Driver().driver;
        PaginaLogin paginaLogin;
        PaginaItens paginaItens;
        PaginaCarrinho paginaCarrinho;
        PaginaEntrega paginaEntrega;
        PaginaCheckout paginaCheckout;
        PaginaSucesso paginaSucesso;
        public double precoTotalItens { get; private set; } = 0;

        int contador = 0;

        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/index.html");
        }


        //Realizar comprar completa no site com mais de um produto
        [Given(@"que eu esteja na area logada")]
        public RoboCompras DadoQueEuEstejaNaAreaLogada()
        {
            paginaLogin = new PaginaLogin(driver);
            Preencher(paginaLogin.campoUsuario, paginaLogin.usuarioValido);
            Preencher(paginaLogin.campoSenha, paginaLogin.senha);
            Clicar(paginaLogin.botaoLogin);
            PrintaStep("Realizar comprar completa no site com mais de um produto", "que eu esteja na area logada", contador);
            return this;
        }

        [When(@"eu selecionei alguns produos")]
        public RoboCompras QuandoEuSelecioneiAlgunsProduos()
        {
            paginaItens = new PaginaItens(driver);
            Clicar(paginaItens.item1);
            Clicar(paginaItens.item2);
            Clicar(paginaItens.item3);
            PrintaStep("Realizar comprar completa no site com mais de um produto", "eu selecionei alguns produos", contador);
            return this;
        }

        [When(@"cliquei no carrinho")]
        public RoboCompras QuandoCliqueiNoCarrinho()
        {
            paginaItens = new PaginaItens(driver);
            Clicar(paginaItens.carrinho);
            PrintaStep("Realizar comprar completa no site com mais de um produto", "cliquei no carrinho", contador);
            return this;
        }

        [When(@"cliquei no checkout")]
        public RoboCompras QuandoCliqueiNoCheckout()
        {
            paginaCarrinho = new PaginaCarrinho(driver);
            Clicar(paginaCarrinho.botaoCheckout);
            PrintaStep("Realizar comprar completa no site com mais de um produto", "cliquei no checkout", contador);
            return this;
        }

        [When(@"preenchi os campos de entrega")]
        public RoboCompras QuandoPreenchiOsCamposDeEntrega()
        {
            paginaEntrega = new PaginaEntrega(driver);
            Preencher(paginaEntrega.campoPrimeiroNome, ("Romário"));
            Preencher(paginaEntrega.campoSegundoNome, ("Camilo"));
            Preencher(paginaEntrega.campoCep, ("38410-729"));
            PrintaStep("Realizar comprar completa no site com mais de um produto", "preenchi os campos de entrega", contador);
            return this;
        }

        [When(@"cliquei em continue")]
        public RoboCompras QuandoCliqueiEmContinue()
        {
            Clicar(paginaEntrega.botaoContinue);
            PrintaStep("Realizar comprar completa no site com mais de um produto", "cliquei em continue", contador);
            return this;
        }

        [When(@"cliquei em finish")]
        public RoboCompras QuandoCliqueiEmFinish()
        {
            paginaCheckout = new PaginaCheckout(driver);
            Clicar(paginaCheckout.botaoFinish);
            PrintaStep("Realizar comprar completa no site com mais de um produto", "cliquei em finish", contador);
            return this;
        }

        [Then(@"o sistema exibe mensagem de sucesso")]
        public RoboCompras EntaoOSistemaExibeMensagemDeSucesso()
        {
            paginaSucesso = new PaginaSucesso(driver);
            Assert.IsTrue(Visivel(paginaSucesso.mensagemAgradecimento));
            Assert.IsTrue(Visivel(paginaSucesso.imagemLoja));
            PrintaStep("Realizar comprar completa no site com mais de um produto", "o sistema exibe mensagem de sucesso", contador);
            contador = 0;
            return this;
        }

        //Realizar comprar e validar valor final com valor do carrinho - SEM PRINT
        [When(@"eu selecionei todos os produtos")]
        public RoboCompras QuandoEuSelecioneiTodosOsProdutos()
        {
            var listaItens = driver.FindElements(By.CssSelector("button[class='btn_primary btn_inventory'"));
            var listaPrecosElementos = driver.FindElements(By.CssSelector("div[class='inventory_item_price'"));

            foreach (var varredor in listaItens)
            {
                Clicar(varredor);
            }

            foreach (var varredor in listaPrecosElementos)
            {
                string[] teste = varredor.Text.Replace('.', ',').Split('$');
                double preco = Convert.ToDouble(teste[1]);
                precoTotalItens = precoTotalItens + preco;
            }

            return this;
        }

        [Then(@"o valor total da comprar deve ser igual ao valor do campo Item total")]
        public RoboCompras EntaoOValorTotalDaComprarDeveSerIgualAoValorDoCampoItemTotal()
        {
            paginaCheckout = new PaginaCheckout(driver);

            bool validacao = (precoTotalItens == paginaCheckout.valorFinalCompra);
            Assert.True(validacao);
            return this;
        }

        public void PrintaStep(string nomeCenario, string nomeStep, int contadorParametro)
        {
            Printar(driver, nomeCenario, contadorParametro.ToString() + " " + nomeStep);
            contadorParametro++;
            contador = contadorParametro;
        }
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

    }
}
