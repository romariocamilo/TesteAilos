using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TesteAilos.Configuracao
{
    public class Driver
    {
        public IWebDriver driver { get; private set; } = new ChromeDriver();
    }
}
