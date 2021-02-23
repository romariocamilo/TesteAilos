using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.PageObject
{
    public class PaginaEntrega
    {
        public IWebDriver driver { get; private set; }
        public WebDriverWait espera { get; private set; }

        public IWebElement campoPrimeiroNome { get; private set; }
        public IWebElement campoSegundoNome { get; private set; }
        public IWebElement campoCep { get; private set; }
        public IWebElement botaoContinue { get; private set; }

        public PaginaEntrega(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageObject();
        }

        public void PageObject()
        {
            //Aguarda o último elemento da pagina aparecer
            botaoContinue = espera.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='checkout_info_container']/div/form/div[2]/input")));

            campoPrimeiroNome = driver.FindElement(By.Id("first-name"));
            campoSegundoNome = driver.FindElement(By.Id("last-name"));
            campoCep = driver.FindElement(By.Id("postal-code"));
        }
    }
}
