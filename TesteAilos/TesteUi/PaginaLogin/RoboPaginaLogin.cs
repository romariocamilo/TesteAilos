using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TesteAilos.Configuracao;
using TesteAilos.RoboGeralAuxiliar;

namespace TesteAilos
{
    [Binding]
    public class RoboPaginaLogin : RogoGeral
    {
        IWebDriver driver = new Driver().driver;
        PaginaLogin paginaLogin;
        int contador = 0;

        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/index.html");
        }


        [Given(@"que eu esteja na tela de login")]
        public RoboPaginaLogin DadoQueEuEstejaNaTelaDeLogin()
        {
            paginaLogin = new PaginaLogin(driver);
            Printar(driver, "Login Bloqueado", contador.ToString() + " - que eu esteja na tela de login" + contador.ToString());
            contador++;
            return this;
        }

        [When(@"eu preenchi o campo usuario com usuario bloqueado")]
        public RoboPaginaLogin QuandoEuPreenchiOCampoUsuarioComUsuarioBloqueado()
        {
            paginaLogin.campoUsuario.SendKeys(paginaLogin.usuarioBloqueado);
            Printar(driver, "Login Bloqueado", contador.ToString() + " - eu preenchi o campo usuario com usuario bloqueado");
            contador++;
            return this;
        }

        [When(@"preenchi o campo senha com senha valida")]
        public RoboPaginaLogin QuandoPreenchiOCampoSenhaComSenhaValida()
        {
            paginaLogin.campoSenha.SendKeys(paginaLogin.senha);
            Printar(driver, "Login Bloqueado", contador.ToString() + " - preenchi o campo senha com senha valida" + contador.ToString());
            contador++;
            return this;
        }

        [When(@"cliquei no botão de login")]
        public RoboPaginaLogin QuandoCliqueiNoBotaoDeLogin()
        {
            paginaLogin.botaoLogin.Click();
            Printar(driver, "Login Bloqueado", contador.ToString() + " - cliquei no botão de login" + contador.ToString());
            contador++;
            return this;
        }

        [Then(@"sistema emite aviso de usuário bloqueado")]
        public RoboPaginaLogin EntaoSistemaEmiteAvisoDeUsuarioBloqueado()
        {
            string mensagemErroUsuarioBloqueadoFront = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
            Assert.IsTrue(mensagemErroUsuarioBloqueadoFront == paginaLogin.mensagemErroUsuarioBloqueadoFront);
            Printar(driver, "Login Bloqueado", contador.ToString() + " - sistema emite aviso de usuário bloqueado" + contador.ToString());
            contador++;
            return this;
        }

        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}
