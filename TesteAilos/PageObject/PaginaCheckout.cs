using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.PageObject
{
    public class PaginaCheckout
    {
        public IWebDriver driver { get; private set; }
        public WebDriverWait espera { get; private set; }
        public IWebElement botaoFinish { get; private set; }

        public PaginaCheckout(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageObject();
        }

        public void PageObject()
        {
            //Aguarda o último elemento da pagina aparecer
            botaoFinish = espera.Until(ExpectedConditions.ElementExists(By.CssSelector("a[href='./checkout-complete.html'")));
        }
    }
}
