using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using TesteAilos.Modelo;

namespace TesteAilos.TesteApi
{
    public class ApiUsuariosTeste
    {
        [Test]
        public void PostUsuario()
        {
            _ = new RoboApiUsuarios()
                .DadoEuTenhaAUrlDaApi()
                .QuandoEuEnvieiUmaRequisicaoPostComJsonValido()
                .EntaoApiRetornouStatusOK();
        }

        [Test]
        public void DeleteUsuario()
        {
            _ = new RoboApiUsuarios()
                .DadoEuTenhaAUrlDaApi()
                .QuandoEuEnvieiUmaRequisicaoDeleteComIdValido()
                .EntaoApiRetornouStatusOKComMensagemDeSucessoDoDelete();
        }

        [Test]
        public void PutUsuario()
        {
            _ = new RoboApiUsuarios()
                .DadoEuTenhaAUrlDaApi()
                .QuandoEuEnvieiUmaRequisicaoPutComIdValido()
                .EntaoApiRetornouStatusOKComMensagemDeSucessoDaAlteracao();
        }

        [Test]
        public void GetTodosUsuario()
        {
            _ = new RoboApiUsuarios()
                .DadoEuTenhaAUrlDaApi()
                .QuandoEuEnvieiUmaRequisicaoGet()
                .EntaoApiRetornouStatusOKComAListaDeUsuariosNoContents();
        }

        [Test]
        public void Serializa()
        {


        }
    }
}
