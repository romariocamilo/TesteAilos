#language: pt-br

Funcionalidade: Login Bloqueado
	Tela para logar

Cenário: Realiza login com usuario bloqueado
	Dado que eu esteja na tela de login
	Quando eu preenchi o campo usuario com usuario bloqueado
	E preenchi o campo senha com senha valida
	E cliquei no botão de login
	Então sistema emite aviso de usuário bloqueado