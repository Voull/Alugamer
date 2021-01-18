$(document).ready(function () {
	var telMask = ['(99) 9999-99999', '(99) 99999-9999'];
	var cpfMask = ["999.999.999-99", "999.999.999-99"];

	VMasker($("#Telefone")).maskPattern(telMask[1]);
	VMasker($("#Cpf")).maskPattern(cpfMask[0]);

	$("#Telefone")[0].addEventListener('input', inputHandler.bind(undefined, telMask, 14), false);
	$("#Cpf")[0].addEventListener('input', inputHandler.bind(undefined, cpfMask, 14), false);
});

function inputHandler(masks, max, event) {
	var c = event.target;
	var v = c.value.replace(/\D/g, '');
	var m = c.value.length > max ? 1 : 0;
	VMasker(c).unMask();
	VMasker(c).maskPattern(masks[m]);
	c.value = VMasker.toPattern(v, masks[m]);
}


function salvaCliente(e) {
	e.preventDefault();

	if (!validaCampos())
		return;

	let id = Number($("#Id").val());
	let nome = $("#Nome").val();
	let email = $("#Email").val();
	let telefone = $("#Telefone").val();
	let endereco = $("#Endereco").val();
	let dataNascimento = $("#DataNascimento").val();
	let sexo = $("#Sexo").val()
	let cpf = $("#Cpf").val();

	let cliente = {
		Id: id,
		Nome: nome,
		Email: email,
		Telefone: telefone,
		Endereco: endereco,
		DataNascimento: dataNascimento,
		Sexo: sexo,
		Cpf: cpf
	};

	cliente = JSON.stringify(cliente);

	$.ajax({
		url: "/Funcionario/" + (id == 0 ? "Novo" : "Edita") + "/",
		data: cliente,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) { successSalvaItem(data); },
		error: function (data) { errorGenerico(data); }
	})
}

function validaCampos() {
	let email = $("#Email").val();
	let telefone = $("#Telefone").val();
	let dataNascimento = $("#DataNascimento").val().split("-");
	let cpf = $("#Cpf").val();

	let regexpEmail = new RegExp(/^[^@\s]+@[a-zA-Z0-9]+\.\w+/g);
	let regexpTelefone = new RegExp(/^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}/g);
	let regexpCpf = new RegExp(/^([0-9]{3}\.){2}[0-9]{3}-[0-9]{2}$/g);

	let erros = "";

	if (!email.match(regexpEmail))
		erros += "Formato de Email inválido!\n";
	if (!telefone.match(regexpTelefone))
		erros += "Formato de Telefone Inválido!\n";
	if (!cpf.match(regexpCpf))
		erros += "Formato de CPF inválido!\n";
	if (new Date(dataNascimento[0], dataNascimento[1], dataNascimento[2]).getTime() >= new Date().setHours(0,0,0,0))
		erros += "Data de Nascimento Inválida!";

	if (erros != "") {
		alert(erros);
		return false;
	}

	return true;

}