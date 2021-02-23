using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TesteAilos.RoboGeralAuxiliar
{
    public class RogoGeral
    {
        public void Clicar(IWebElement elemento)
        {
            elemento.Click();
        }

        public void Preencher(IWebElement elemento, string texto)
        {
            elemento.SendKeys(texto);
        }

        public bool Visivel(IWebElement elemento)
        {
            if (elemento == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Printar(IWebDriver driver, string nomeCenario, string nomeImagem)
        {
            string nomeCenarioF = nomeCenario.Replace(' ', '_');
            string nomeImagemF = nomeImagem.Replace(' ', '_');
            string path = Directory.GetCurrentDirectory() + "\\Evidencias\\" + nomeCenarioF + "\\";
            ITakesScreenshot camera = driver as ITakesScreenshot;
            Screenshot foto = camera.GetScreenshot();

            if (Directory.Exists(path))
            {
                foto.SaveAsFile(path + nomeImagemF + ".png", ScreenshotImageFormat.Png);
            }
            else
            {
                Directory.CreateDirectory(path);
                foto.SaveAsFile(path + nomeImagemF + ".png", ScreenshotImageFormat.Png);
            }
        }

        public void Fechar(IWebDriver driver)
        {
            driver.Close();
            driver.Dispose();
        }

        public void Relatorio(string nomeCenario)
        {
            var htmlReporter = new ExtentHtmlReporter("Relatorios\\" + nomeCenario + "\\");
            var extent = new ExtentReports();
            extent.AttachReporter((IExtentReporter)(htmlReporter));
            var test = extent.CreateTest(nomeCenario);
            extent.Flush();
        }
    }
}
