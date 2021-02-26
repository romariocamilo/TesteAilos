using NUnit.Framework;
using RestSharp;
using System.Net;
using TesteAilos.Modelo;
using System.Linq;
using TechTalk.SpecFlow;
using System.Text.Json;

namespace TesteAilos.TesteApi
{
    [Binding]
    public class RoboApiUsuarios
    {
        string urlBase = "http://localhost:3000/usuarios";
        string emailUsuario = "romario@qa.com";

        UsuarioApi usuarioApi;

        IRestResponse respostaPost;
        IRestResponse respostaApaga;
        IRestResponse respostaPut;
        IRestResponse respostaGet;


        //Realizar cadastro de usuário
        [Given(@"eu tenha a url da api")]
        public RoboApiUsuarios DadoEuTenhaAUrlDaApi()
        {
            urlBase = "http://localhost:3000/usuarios";
            return this;
        }

        [When(@"eu enviei uma requisicao POST com json valido")]
        public RoboApiUsuarios QuandoEuEnvieiUmaRequisicaoPostComJsonValido()
        {
            var cliente = new RestClient(urlBase);
            var requisicao = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json }; ;

            requisicao.AddJsonBody(new
            {
                nome = "Romario Camilo Corrêa de Souza",
                email = emailUsuario,
                password = "teste",
                administrador = "true"
            });

            respostaPost = cliente.Execute(requisicao);

            return this;
        }

        [Then(@"api retornou status OK")]
        public RoboApiUsuarios EntaoApiRetornouStatusOK()
        {
            usuarioApi = GetUsuariosSerializados();

            if (respostaPost.StatusCode == HttpStatusCode.Created && respostaPost.Content != null)
            {
                DeletaUsuario(usuarioApi, emailUsuario);
                Assert.Pass();
            }
            else
            {
                DeletaUsuario(usuarioApi, emailUsuario);
                Assert.Fail();
            }
            return this;
        }


        //Realizar delete de usuário
        [When(@"eu enviei uma requisicao Delete com id valido")]
        public RoboApiUsuarios QuandoEuEnvieiUmaRequisicaoDeleteComIdValido()
        {
            CadastraUsuario(emailUsuario);

            usuarioApi = GetUsuariosSerializados();
            Usuario usuarioDeletado = usuarioApi.usuarios.FirstOrDefault(u => u.email == emailUsuario);

            string idApagado = "/" + usuarioDeletado._id;

            var clienteApaga = new RestClient(urlBase + idApagado);
            var requisicaoApaga = new RestRequest(Method.DELETE) { RequestFormat = DataFormat.Json };
            respostaApaga = clienteApaga.Execute(requisicaoApaga);
            return this;
        }

        [Then(@"api retornou status OK com mensagem de sucesso do delete")]
        public RoboApiUsuarios EntaoApiRetornouStatusOKComMensagemDeSucessoDoDelete()
        {
            if (respostaApaga.StatusCode == HttpStatusCode.OK && respostaApaga.Content.Contains("Registro excluído com sucesso"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
            return this;
        }


        //Relizar alteração de usuario
        [When(@"eu enviei uma requisicao Put com id valido")]
        public RoboApiUsuarios QuandoEuEnvieiUmaRequisicaoPutComIdValido()
        {
            CadastraUsuario(emailUsuario);

            usuarioApi = GetUsuariosSerializados();
            Usuario usuarioEditado = usuarioApi.usuarios.FirstOrDefault(u => u.email == emailUsuario);
            string idEditado = "/" + usuarioEditado._id;

            var clientePut = new RestClient(urlBase + idEditado);
            var requisicaoPut = new RestRequest(Method.PUT) { RequestFormat = DataFormat.Json }; ;
            requisicaoPut.AddJsonBody(new
            {
                nome = "Romario Editado",
                email = emailUsuario,
                password = "teste",
                administrador = "true"
            });

            respostaPut = clientePut.Execute(requisicaoPut);
            return this;
        }

        [Then(@"api retornou status OK com mensagem de sucesso da alteração")]
        public RoboApiUsuarios EntaoApiRetornouStatusOKComMensagemDeSucessoDaAlteracao()
        {
            usuarioApi = GetUsuariosSerializados();

            if (respostaPut.StatusCode == HttpStatusCode.OK && respostaPut.Content.Contains("Registro alterado com sucesso"))
            {
                DeletaUsuario(usuarioApi, emailUsuario);
                Assert.Pass();
            }
            else
            {
                DeletaUsuario(usuarioApi, emailUsuario);
                Assert.Fail();
            }
            return this;
        }


        //Realizar busca geral de usuarios
        [When(@"eu enviei uma requisicao de consulta")]
        public RoboApiUsuarios QuandoEuEnvieiUmaRequisicaoGet()
        {
            var clienteGet = new RestClient(urlBase);
            var requisicaoGet = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
            respostaGet = clienteGet.Execute(requisicaoGet);
            return this;
        }

        [Then(@"api retornou status OK com a lista de usuarios no contents")]
        public RoboApiUsuarios EntaoApiRetornouStatusOKComAListaDeUsuariosNoContents()
        {
            if (respostaGet.StatusCode == HttpStatusCode.OK && respostaGet.Content != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
            return this;
        }
  

        public void CadastraUsuario(string emailUsuario)
        {
            var clientePost = new RestClient(urlBase);
            var requisicaoPost = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json }; ;
            requisicaoPost.AddJsonBody(new
            {
                nome = "Romario Apagar",
                email = emailUsuario,
                password = "teste",
                administrador = "true"
            });
            clientePost.Execute(requisicaoPost);
        }

        public void DeletaUsuario(UsuarioApi usuarioApi, string emailUsuario)
        {
            Usuario usuarioDeletado = usuarioApi.usuarios.FirstOrDefault(u => u.email == emailUsuario);
            string idApagado = "/" + usuarioDeletado._id;
            var clienteDelete = new RestClient(urlBase + idApagado);
            var requisicaoDelete = new RestRequest(Method.DELETE) { RequestFormat = DataFormat.Json };
            var respostaDelete = clienteDelete.Execute(requisicaoDelete);
        }

        public UsuarioApi GetUsuariosSerializados()
        {
            IRestResponse respostaGet;

            var cliente = new RestClient(urlBase);
            var requisicao = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json }; ;
            respostaGet = cliente.Execute(requisicao);

            usuarioApi = JsonSerializer.Deserialize<UsuarioApi>(respostaGet.Content);

            return usuarioApi;
        }


        //public IRestResponse CriaJsonDeUsuarios()
        //{
        //    var clienteGet = new RestClient(urlBase);
        //    var requisicaoGet = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
        //    var respostaGet = clienteGet.Execute(requisicaoGet);

        //    return respostaGet;
        //}


        //Aqui o filho chora e a mãe não vê
        //Pega lista de usuarios na bruta

        //public List<Usuario> RetornaUsuariosCadastrados(IRestResponse resposta)
        //{
        //    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\APIJson");
        //    File.WriteAllText(Directory.GetCurrentDirectory() + "\\APIJson\\usuarios.json", resposta.Content);

        //    string[] texto = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\APIJson\\usuarios.json");
        //    List<string> add = new List<string>();

        //    foreach (var varredor in texto)
        //    {
        //        if (varredor.Contains("nome") || varredor.Contains("email") || varredor.Contains("password") || varredor.Contains("administrador") || varredor.Contains("id"))
        //        {
        //            add.Add(varredor.Replace('"', ' ').Replace(':', '-').Replace(',', ' ').Trim());
        //        }
        //    }

        //    add.Remove(add[0]);

        //    List<string> listaNomes = new List<string>();
        //    List<string> listaEmails = new List<string>();
        //    List<string> listaSenhas = new List<string>();
        //    List<string> listaAdm = new List<string>();
        //    List<string> listaId = new List<string>();

        //    foreach (var varredor in add)
        //    {
        //        if (varredor.Contains("nome"))
        //        {
        //            string[] nome = varredor.Split('-');
        //            listaNomes.Add(nome[1]);
        //        }
        //        if (varredor.Contains("email"))
        //        {
        //            string[] email = varredor.Split('-');
        //            listaEmails.Add(email[1]);
        //        }
        //        if (varredor.Contains("password"))
        //        {
        //            string[] senha = varredor.Split('-');
        //            listaSenhas.Add(senha[1]);
        //        }
        //        if (varredor.Contains("administrador"))
        //        {
        //            string[] administrador = varredor.Split('-');
        //            listaAdm.Add(administrador[1]);
        //        }
        //        if (varredor.Contains("_id"))
        //        {
        //            string[] _id = varredor.Split('-');
        //            listaId.Add(_id[1]);
        //        }
        //    }

        //    List<Usuario> listaUsuariosCadastrados = new List<Usuario>();

        //    for (int contador = 0; contador < listaNomes.Count; contador++)
        //    {
        //        Usuario usuarioCadastrado = new Usuario();
        //        usuarioCadastrado.nome = listaNomes[contador].TrimStart();
        //        usuarioCadastrado.email = listaEmails[contador].TrimStart();
        //        usuarioCadastrado.password = listaSenhas[contador].TrimStart();
        //        usuarioCadastrado.administrador = listaAdm[contador].TrimStart();
        //        usuarioCadastrado._id = listaId[contador].TrimStart();

        //        listaUsuariosCadastrados.Add(usuarioCadastrado);
        //    }

        //    return listaUsuariosCadastrados;
        //}
    }
}
