Feature: Registro de donante
	Como nuevo usuario, Quiero registrarme como donante ingresando mis datos,
	Para que el sistema me recuerde

Scenario: Registrando un donante satisfactoriamente
	Given Soy un usuario no registrado
	When Voy a la pagina de registro de donante e ingreso los siguientes datos
		| field           | value              |
		| firstname       | Laura              |
		| lastname        | Garcia             |
		| sex             | F                  |
		| birthdate       | 1998-05-20         |
		| email           | l.garcia@gmail.com |
		| phone           | 8094820985         |
		| username        | l.garcia           |
		| password        | Hola123*           |
		| confirmpassword | Hola123*           |
		| bloodtype		  | O+                 |
		| weight	      | 80                 |
	And Hago clic en registrarme
	Then Soy registrado exitosamente
	And Recibo un correo de confirmacion con un codigo
	And El sistema me muestra la pagina de confirmacion de correo que contiene el campo "Codigo de confirmacion"
	When Cuando lleno el campo "Codigo de confirmacion" con el condigo recibido <confirmationCode>
	And Hago clic en el boton "Enviar"
	Then El sistema confirma mi correo
	And Me muestra el mensaje "Correo electronico confirmado exitosamente"
	And El sistema me lleva a la pagina de Preguntas de Validacion de Donante


Scenario: Registrando un usuario con campos vacios
	Given Soy un usuario no registrado
	When Voy a la pagina de registro de donante e ingreso los siguientes datos
		| field           | value      |
		| firstname       | Jorge      |
		| lastname        | Perez     |
		| sex             | M          |
		| birthdate       | 1990-05-20 |
		| email           |            |
		| phone           | 8094820754 |
		| username        | l.garcia   |
		| password        | Hola123*   |
		| confirmpassword | Hola123*   |
		| bloodtype       | O+         |
		| weight          | 80         |
	Then Soy registrado exitosamente
	And Recibo un correo de confirmacion