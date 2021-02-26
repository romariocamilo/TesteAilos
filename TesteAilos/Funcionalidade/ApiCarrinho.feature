#language: pt-br

Funcionalidade: ApiProdutos
	Endpoint para CRUD em produtos


Cenário: Realizar consulta de produtos com quantidade total superior a cinco
	Dado eu tenha a url da api de carrinhos
	Quando eu enviei uma requisicao de consulta
	Então api retornou status OK com os carrinho com quantidade total maiores que cinco