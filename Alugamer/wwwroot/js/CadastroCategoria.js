function salvaCategoria(e) {
    e.preventDefault();

    let id = Number($("#Id").val());
    let nome = $("#Nome").val();
    let descricao = $("#Descricao").val();
	let categoria = {
		Id: id,
		Nome: nome,
		Descricao: descricao
	};

	categoria = JSON.stringify(categoria);

	$.ajax({
		url: "/Categoria/" + (id == 0 ? "Novo" : "Edita") + "/",
		data: categoria,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) { successSalvaItem(data); },
		error: function (data) { errorGenerico(data); }
	})
}