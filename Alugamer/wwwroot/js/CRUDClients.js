$(document).ready(function () {
	$("#cpfCli").mask('000.000.000-00');
	$("#celCli").mask('(00) 00000-0000');
});

function buscaCliente() {
	let id = $("#clienteCli").val();
	if (id == 0) return;

	$.ajax({
		url: "Cliente/Busca/" + id,
		type: "GET",
		success: function (data) { successBuscaCliente(data); }
	});
}

function successBuscaCliente(data) {
	cliente = JSON.parse(data);

	$("#idCli").val(cliente.Id);
	$("#nomeCli").val(cliente.Nome);
	$("#emailCli").val(cliente.Email);
	$("#celCli").val(cliente.Telefone);
	$("#dataNascCli").val(cliente.DataNascimento.split('T')[0]);
	$("#endCli").val(cliente.Endereco);
	$("#sexoCli").val(cliente.Sexo);
	$("#cpfCli").val(cliente.Cpf);

	editaCliente();
}

function limpaCampos() {
	$("#nomeCli").val("");
	$("#emailCli").val("");
	$("#celCli").val("");
	$("#dataNascCli").val("");
	$("#endCli").val("");
	$("#sexoCli").val("");
	$("#cpfCli").val("");

	$("#nomeCli").attr("disabled");
	$("#emailCli").attr("disabled");
	$("#celCli").attr("disabled");
	$("#dataNascCli").attr("disabled");
	$("#endCli").attr("disabled");
	$("#sexoCli").attr("disabled");
	$("#cpfCli").attr("disabled");
}

function salvaCliente() {
	let id = $("#idCli").val();

	if (id == -1) {
		alert("Selecione um Cliente ou clique em Novo!");
	}

	let cliente = {
		id: Number(id),
		nome: $("#nomeCli").val(),
		email: $("#emailCli").val(),
		telefone: $("#celCli").val(),
		dataNascimento: $("#dataNascCli").val(),
		endereco: $("#endCli").val(),
		sexo: $("#sexoCli").val(),
		cpf: $("#cpfCli").val()
	};

	//let cliente = {};
	//cliente.Id = Number(id);
	//cliente.Nome = $("#nomeCli").val();
	//cliente.Email = $("#emailCli").val();
	//cliente.Telefone = $("#celCli").val();
	//cliente.Endereco = $("#endCli").val();
	//cliente.DataNascimento = $("#dataNascCli").val() + "T00:00:00";
	//cliente.Sexo = $("#sexoCli").val();
	//cliente.Cpf = $("#cpfCli").val();

	cliente = JSON.stringify(cliente);

	$.ajax({
		url: "Cliente/" + (id == 0 ? "Novo" : "Edita") + "/",
		data: cliente,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) { successSalvaCliente(data); }
	});
}

function successSalvaCliente(data) {
	data = JSON.parse(data);
	if (data == "")
		res = alert("Cliente salvo com sucesso!");
	else
		alert(data);
	location.reload();
}


function criaCliente() {
	limpaCampos();
	$("#idCli").val("0");

	$(":input").removeAttr("disabled");
}

function editaCliente() {
	$(":input").removeAttr("disabled");
}

function excluiCliente() {
	let id = $("#idCli").val();
	if (id <= 0) return;

	if (confirm("Você realmente deseja excluir este cliente?")) {
		$.ajax({
			url: "Cliente/Remove/" + id,
			type: "DELETE",
			success: function (data) { successExcluiCliente(data); }
		});
	}
}

function successExcluiCliente(data) {
	location.reload();
}


function cancela() {
	limpaCampos();

}