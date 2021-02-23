using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.PageObject
{
    public class PaginaItens
    {
        public IWebDriver driver { get; private set; }
        public WebDriverWait espera { get; private set; }

        public IWebElement item1 { get; private set; }
        public IWebElement item2 { get; private set; }
        public IWebElement item3 { get; private set; }
        public IWebElement carrinho { get; private set; }

        public double totalElemento { get; private set; }
        public PaginaItens(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            PageObject();
        }

        public void PageObject()
        {
            //Aguarda o último elemento da pagina aparecer
            item3 = espera.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='inventory_container']/div/div[6]/div[3]/button")));
            item1 = driver.FindElement(By.XPath("//*[@id='inventory_container']/div/div[1]/div[3]/button"));
            item2 = driver.FindElement(By.XPath("//*[@id='inventory_container']/div/div[2]/div[3]/button"));

            //carrinho = driver.FindElement(By.CssSelector("svg[data-icon='shopping-cart'"));
            carrinho = driver.FindElement(By.XPath("//*[@id='shopping_cart_container']/a"));

            var listaItens = driver.FindElements(By.CssSelector("button[class='btn_primary btn_inventory'"));
            var listaPrecosElementos = driver.FindElements(By.CssSelector("div[class='inventory_item_price'"));

            List<double> listaPrecosFormatados = new List<double>();

            foreach(var varredo in listaPrecosElementos)
            {
                string[] teste = varredo.Text.Replace('.',',').Split('$');
                double preco  = Convert.ToDouble(teste[1]);      
            }
        }   
    }
}
