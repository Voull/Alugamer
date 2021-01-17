function salvaAlugavel(e) {
	e.preventDefault();

	let id = Number($("#Id").val());
	let nome = $("#Nome").val();
	let descricao = $("#Descricao").val();
	let quantidade = Number($("#Quantidade").val());
	let valorCompra = Number($("#Valor_compra").val().replace(",", "."));
	let valorAluguel = Number($("#Valor_aluguel").val().replace(",", "."));
	let categoria = Number($("#categoria").val());

	let alugavel = {
		Id: id,
		Nome: nome,
		Descricao: descricao,
		Quantidade: quantidade,
		Valor_compra: valorCompra,
		Valor_aluguel: valorAluguel,
		IdCategoria: categoria,
	};

	alugavel = JSON.stringify(alugavel);

	$.ajax({
		url: "/Alugavel/" + (id == 0 ? "Novo" : "Edita") + "/",
		data: alugavel,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) { successSalvaItem(data); },
		error: function (data) { errorGenerico(data); }
	})
}