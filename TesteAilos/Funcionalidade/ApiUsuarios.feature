#language: pt-br

Funcionalidade: ApiUsuarios
	Endpoint para CRUD em usuarios


Cenário: Realizar cadastro de usuário
	Dado eu tenha a url da api
	Quando eu enviei uma requisicao POST com json valido
	Então api retornou status OK

Cenário: Realizar delete de usuário
	Dado eu tenha a url da api
	Quando eu enviei uma requisicao Delete com id valido
	Então api retornou status OK com mensagem de sucesso do delete

Cenário: Realizar alteração de usuário
	Dado eu tenha a url da api
	Quando eu enviei uma requisicao Put com id valido
	Então api retornou status OK com mensagem de sucesso da alteração

Cenário: Realizar busca geral de usuário
	Dado eu tenha a url da api
	Quando eu enviei uma requisicao Get
	Então api retornou status OK com a lista de usuarios no contents
