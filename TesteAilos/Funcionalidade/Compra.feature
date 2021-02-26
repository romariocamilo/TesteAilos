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

Cenário: Realizar comprar e validar valor final com valor do carrinho
	Dado que eu esteja na area logada
	Quando eu selecionei todos os produtos
	E cliquei no carrinho
	E cliquei no checkout
	E preenchi os campos de entrega
	E cliquei em continue
	Então o valor total da comprar deve ser igual ao valor do campo Item total
