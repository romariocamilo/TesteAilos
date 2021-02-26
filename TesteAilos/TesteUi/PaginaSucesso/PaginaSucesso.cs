using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.PageObject
{
    public class PaginaSucesso
    {
        public IWebDriver driver { get; private set; }
        public WebDriverWait espera { get; private set; }
        public IWebElement mensagemAgradecimento { get; private set; }
        public IWebElement imagemLoja { get; private set; }

        public PaginaSucesso(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageObject();
        }

        public void PageObject()
        {
            //Aguarda o último elemento da pagina aparecer
            imagemLoja = espera.Until(ExpectedConditions.ElementExists(By.ClassName("pony_express")));
            mensagemAgradecimento = driver.FindElement(By.ClassName("complete-header"));
        }
    }
}
