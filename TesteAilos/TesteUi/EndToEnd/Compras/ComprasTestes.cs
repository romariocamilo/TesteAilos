using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteAilos.TesteUi.EndToEnd.Compras
{
    public class ComprasTestes
    {
        RoboCompras robo;

        [SetUp]
        public void t()
        {
            robo = new RoboCompras();
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

        [Test]
        public void ValidaTotalCompra()
        {
            robo.DadoQueEuEstejaNaAreaLogada()
                .QuandoEuSelecioneiTodosOsProdutos()
                .QuandoCliqueiNoCarrinho()
                .QuandoCliqueiNoCheckout()
                .QuandoPreenchiOsCamposDeEntrega()
                .QuandoCliqueiEmContinue()
                .EntaoOValorTotalDaComprarDeveSerIgualAoValorDoCampoItemTotal();
        }

        [TearDown]
        public void t2()
        {
            robo.TearDown();
        }
    }
}
