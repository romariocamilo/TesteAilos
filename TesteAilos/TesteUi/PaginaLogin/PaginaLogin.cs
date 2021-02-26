using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TesteAilos
{
    public class PaginaLogin
    {
        public IWebDriver driver { get; private set; }
        public WebDriverWait espera { get; private set; }

        public IWebElement campoUsuario { get; private set; }
        public IWebElement campoSenha { get; private set; }
        public IWebElement botaoLogin { get; private set; }

        public string usuarioValido { get; private set; }
        public string usuarioBloqueado { get; private set; }
        public string senha { get; private set; }
        public string mensagemErroUsuarioBloqueadoFront { get; private set; } = "Epic sadface: Sorry, this user has been locked out.";

        //public IWebElement usuarioBloqueado{ get; private set; }

        public PaginaLogin(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageObject();
        }

        public void PageObject()
        {
            //Aguarda o último elemento da pagina aparecer
            string textoUsuarios = espera.Until(ExpectedConditions.ElementExists(By.Id("login_credentials"))).Text;

            campoUsuario = driver.FindElement(By.Id("user-name"));
            campoSenha = driver.FindElement(By.Id("password"));
            botaoLogin = driver.FindElement(By.Id("login-button"));
            
            string[] listaSessaoUsuarios = textoUsuarios.Split('\n');
            usuarioValido = listaSessaoUsuarios[1];
            usuarioBloqueado = listaSessaoUsuarios[2];

            string textoSenha = driver.FindElement(By.ClassName("login_password")).Text;
            string[] listaSessaoSenha = textoSenha.Split('\n');
            senha = listaSessaoSenha[1];
        }
    }
}
