using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.PageObject
{
    public class PaginaCarrinho
    {
        public IWebDriver driver { get; private set; }
        public WebDriverWait espera { get; private set; }

        public IWebElement botaoCheckout { get; private set; }

        public PaginaCarrinho(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageObject();
        }

        public void PageObject()
        {
            //Aguarda o último elemento da pagina aparecer
            botaoCheckout = espera.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='cart_contents_container']/div/div[2]/a[2]")));
        }
    }
}
