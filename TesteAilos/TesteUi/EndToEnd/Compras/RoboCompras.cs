using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TesteAilos.Configuracao;
using TesteAilos.PageObject;
using TesteAilos.RoboGeralAuxiliar;

namespace TesteAilos
{
    public class RoboCompras : RogoGeral
    {
        IWebDriver driver = new Driver().driver;
        PaginaLogin paginaLogin;
        PaginaItens paginaItens;
        PaginaCarrinho paginaCarrinho;
        PaginaEntrega paginaEntrega;
        PaginaCheckout paginaCheckout;
        PaginaSucesso paginaSucesso;

        int contador = 0;

        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/index.html");
        }

        [Given(@"que eu esteja na area logada")]
        public RoboCompras DadoQueEuEstejaNaAreaLogada()
        {
            paginaLogin = new PaginaLogin(driver);
            Preencher(paginaLogin.campoUsuario, paginaLogin.usuarioValido);
            Preencher(paginaLogin.campoSenha, paginaLogin.senha);
            Clicar(paginaLogin.botaoLogin);
            return this;
        }

        [When(@"eu selecionei alguns produos")]
        public RoboCompras QuandoEuSelecioneiAlgunsProduos()
        {
            paginaItens = new PaginaItens(driver);

            Clicar(paginaItens.item1);
            Clicar(paginaItens.item2);
            Clicar(paginaItens.item3);

            return this;
        }

        [When(@"cliquei no carrinho")]
        public RoboCompras QuandoCliqueiNoCarrinho()
        {
            Clicar(paginaItens.carrinho);

            return this;
        }

        [When(@"cliquei no checkout")]
        public RoboCompras QuandoCliqueiNoCheckout()
        {
            paginaCarrinho = new PaginaCarrinho(driver);
            Clicar(paginaCarrinho.botaoCheckout);

            return this;
        }

        [When(@"preenchi os campos de entrega")]
        public RoboCompras QuandoPreenchiOsCamposDeEntrega()
        {
            paginaEntrega = new PaginaEntrega(driver);
            Preencher(paginaEntrega.campoPrimeiroNome, ("Romário"));
            Preencher(paginaEntrega.campoSegundoNome, ("Camilo"));
            Preencher(paginaEntrega.campoCep, ("38410-729"));
            return this;
        }

        [When(@"cliquei em continue")]
        public RoboCompras QuandoCliqueiEmContinue()
        {
            Clicar(paginaEntrega.botaoContinue);
            return this;
        }

        [When(@"cliquei em finish")]
        public RoboCompras QuandoCliqueiEmFinish()
        {
            paginaCheckout = new PaginaCheckout(driver);
            Clicar(paginaCheckout.botaoFinish);
            return this;
        }

        [Then(@"o sistema exibe mensagem de sucesso")]
        public RoboCompras EntaoOSistemaExibeMensagemDeSucesso()
        {
            paginaSucesso = new PaginaSucesso(driver);
            Assert.IsTrue(Visivel(paginaSucesso.mensagemAgradecimento));
            Assert.IsTrue(Visivel(paginaSucesso.imagemLoja));
            return this;
        }

        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

    }
}
