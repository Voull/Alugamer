function buscaAlugavel() {
	let id = $("#AlugavelAlu").val();
	if (id == 0) return;

	$.ajax({
		url: "Alugavel/Busca/" + id,
		type: "GET",
		success: function (data) { successBuscaAlugavel(data); }
	});
}

function successBuscaAlugavel(data) {
	alugavel = JSON.parse(data);

	$("#idAluga").val(alugavel.Id);
	$("#nomeAluga").val(alugavel.Nome);
	$("#descricaoAluga").val(alugavel.Descricao);
	$("#categoriaAluga").val(alugavel.Categoria);
	$("#valorCompraAluga").val(alugavel.Valor_compra);
	$("#valorAluga").val(alugavel.Valor_aluguel);
	$("#qtdAluga").val(alugavel.Quantidade);

	editaAlugavel();
}

function limpaCampos() {
	$("#idAluga").val("");
	$("#nomeAluga").val("");
	$("#descricaoAluga").val("");
	$("#categoriaAluga").val("");
	$("#valorCompraAluga").val("");
	$("#valorAluga").val("");
	$("#qtdAluga").val("");

	$("#idAluga").attr("disabled");
	$("#nomeAluga").attr("disabled");
	$("#descricaoAluga").attr("disabled");
	$("#categoriaAluga").attr("disabled");
	$("#valorCompraAluga").attr("disabled");
	$("#valorAluga").attr("disabled");
	$("#qtdAluga").attr("disabled");
}

function salvaAlugavel() {
	let id = $("#idAluga").val();

	if (id == -1) {
		alert("Selecione um Item ou clique em Novo!");
	}

	//let cliente = {
	//	id = id,
	//	nome = $("#nomeCli").val(),
	//	email = $("#emailCli").val(),
	//	telefone = $("#celCli").val(),
	//	dataNascimento = $("#dataNascCli").val(),
	//	endereco = $("#endCli").val(),
	//	sexo = $("#sexoCli").val(),
	//	cpf = $("#cpfCli").val()
	//};

	let alugavel = {};
	alugavel.Id = Number(id);
	alugavel.Nome = $("#nomeAluga").val();
	alugavel.Descricao = $("#descricaoAluga").val();
	alugavel.Categoria = $("#categoriaAluga").val();
	alugavel.Valor_compra = Number($("#valorCompraAluga").val());
	alugavel.Valor_aluguel = Number($("#valorAluga").val());
	alugavel.Quantidade = Number($("#qtdAluga").val());

	alugavel = JSON.stringify(alugavel);

	$.ajax({
		url: "Alugavel/" + (id == 0 ? "Novo" : "Edita") +"/",
		data: alugavel,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function () { successSalvaAlugavel(); },
		error: function (data) { failSalvaAlugavel(data); }
	});
}

function successSalvaAlugavel(data) {
	res = alert("Item salvo com sucesso!");
	location.reload();
}

function failSalvaAlugavel(data) {
	parsed = data.responseJSON;
	alert(parsed);
}


function criaAlugavel() {
	limpaCampos();
	$("#idAluga").val("0");

	$(":input").removeAttr("disabled");
}

function editaAlugavel(){
	$(":input").removeAttr("disabled");
}

function excluiAlugavel() {
	let id = $("#idAluga").val();
	if (id <= 0) return;

	if (confirm("Você realmente deseja excluir este item?")) {
		$.ajax({
			url: "Alugavel/Remove/" + id,
			type: "DELETE",
			success: function (data) { successExcluiAlugavel(data); }
		});
	}
}

function successExcluiAlugavel(data) {
	location.reload();
}


function cancela() {
	limpaCampos();
}

