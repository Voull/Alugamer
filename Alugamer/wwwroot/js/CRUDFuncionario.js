$(document).ready(function () {
	$("#cpfFunc").mask('000.000.000-00');
	$("#celFunc").mask('(00) 00000-0000');
});

function buscaFuncionario() {
	let id = $("#funcionarioFunc").val();
	if (id == 0) return;

	$.ajax({
		url: "Funcionario/Busca/" + id,
		type: "GET",
		success: function (data) { successBuscaFuncionario(data); }
	});
}

function successBuscaFuncionario(data) {
	funcionario = JSON.parse(data);

	$("#idFunc").val(funcionario.Id);
	$("#nomeFunc").val(funcionario.Nome);
	$("#emailFunc").val(funcionario.Email);
	$("#celFunc").val(funcionario.Telefone);
	$("#dataNascFunc").val(funcionario.DataNascimento.split('T')[0]);
	$("#endFunc").val(funcionario.Endereco);
	$("#sexoFunc").val(funcionario.Sexo);
	$("#cpfFunc").val(funcionario.Cpf);

	editaFuncionario();
}

function limpaCampos() {
	$("#nomeFunc").val("");
	$("#emailFunc").val("");
	$("#celFunc").val("");
	$("#dataNascFunc").val("");
	$("#endFunc").val("");
	$("#sexoFunc").val("");
	$("#cpfFunc").val("");

	$("#nomeFunc").attr("disabled");
	$("#emailFunc").attr("disabled");
	$("#celFunc").attr("disabled");
	$("#dataNascFunc").attr("disabled");
	$("#endFunc").attr("disabled");
	$("#sexoFunc").attr("disabled");
	$("#cpfFunc").attr("disabled");
}

function salvaFuncionario() {
	let id = $("#idFunc").val();

	if (id == -1) {
		alert("Selecione um Funcionario ou clique em Novo!");
	}

	let funcionario = {
		id: Number(id),
		nome: $("#nomeFunc").val(),
		email: $("#emailFunc").val(),
		telefone: $("#celFunc").val(),
		dataNascimento: $("#dataNascFunc").val(),
		endereco: $("#endFunc").val(),
		sexo: $("#sexoFunc").val(),
		cpf: $("#cpfFunc").val()
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

	funcionario = JSON.stringify(funcionario);

	$.ajax({
		url: "Funcionario/" + (id == 0 ? "Novo" : "Edita") + "/",
		data: funcionario,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function () { successSalvaFuncionario(); },
		error: function (data) { failSalvaFuncionario(data); }
	});
}

function successSalvaFuncionario (data) {
	res = alert("Funcionario salvo com sucesso!");
	location.reload();
}

function failSalvaFuncionario(data) {
	parsed = data.responseJSON;
	alert(parsed);
}


function criaFuncionario() {
	limpaCampos();
	$("#idFunc").val("0");

	$(":input").removeAttr("disabled");
}

function editaFuncionario() {
	$(":input").removeAttr("disabled");
}

function excluiFuncionario() {
	let id = $("#idFunc").val();
	if (id <= 0) return;

	if (confirm("Você realmente deseja excluir este funcionario?")) {
		$.ajax({
			url: "Funcionario/Remove/" + id,
			type: "DELETE",
			success: function (data) { successExcluiFuncionario(data); }
		});
	}
}

function successExcluiFuncionario(data) {
	location.reload();
}


function cancela() {
	limpaCampos();
}