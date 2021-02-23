using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.TesteUi.EndToEnd.Compras
{
    public class ComprasTestes
    {
        RoboCompras robo = new RoboCompras();

        [SetUp]
        public void t()
        {
            robo.Setup();
        }

        [Test]
        public void CompraCompleta()
        {
            robo.DadoQueEuEstejaNaAreaLogada()
                .QuandoEuSelecioneiAlgunsProduos()
                .QuandoCliqueiNoCarrinho()
                .QuandoCliqueiNoCheckout()
                .QuandoPreenchiOsCamposDeEntrega()
                .QuandoCliqueiEmContinue()
                .QuandoCliqueiEmFinish();
        }

        [TearDown]
        public void t2()
        {
            robo.TearDown();
        }
    }
}
