using NUnit.Framework;

namespace TesteAilos.TesteUi.PaginaLogin
{
    public class PaginaLoginTeste
    {
        RoboPaginaLogin robo = new RoboPaginaLogin();
        
        [SetUp]
        public void t()
        {
            robo.Setup();
        }

        [Test]
        public void LoginUsuarioBloqueado()
        {
            robo.DadoQueEuEstejaNaTelaDeLogin()
                .QuandoEuPreenchiOCampoUsuarioComUsuarioBloqueado()
                .QuandoPreenchiOCampoSenhaComSenhaValida()
                .QuandoCliqueiNoBotaoDeLogin();    
        }

        [TearDown]
        public void t2()
        {
            robo.TearDown();
        }
    }
}
