#language: pt-br

Funcionalidade: Compra
	Compras no site

Cenário: Realizar comprar completa no site com mais de um produto
	Dado que eu esteja na area logada
	Quando eu selecionei alguns produos
	E cliquei no carrinho
	E cliquei no checkout
	E preenchi os campos de entrega
	E cliquei em continue
	E cliquei em finish
	Então o sistema exibe mensagem de sucesso
