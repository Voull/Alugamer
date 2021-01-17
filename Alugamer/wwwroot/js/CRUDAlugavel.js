$(document).ready(function () {
	$('#tbAlugavel').DataTable({
		dom: 'Bfrtip',
		select: true,
		language: {
			url: "//cdn.datatables.net/plug-ins/1.10.22/i18n/Portuguese-Brasil.json"
		},
		buttons: [{
			text: "Novo",
			action: function (e, dt, button, config) {
				document.location.href = '/Alugavel/Cadastro';
			},
			className: 'btn-primary'
		},
		{
			text: "Remover",
			action: function (e, dt, button, config) {
				removeAlugaveis();
			},
			className: 'btn-danger'

		}
		]
	});

});

function removeAlugaveis() {
	if (!confirm("Realmente desejar remover os Itens selecionados?"))
		return;

	let alugaveis = []

	$(".selected td:nth-child(1)").each(function () {
		alugaveis.push(Number($(this).html()));
	});

	if (alugaveis.length == 0) {
		alert("Não há itens selecionados!");
		return;
	}

	alugaveis = JSON.stringify(alugaveis);

	$.ajax({
		url: "/Alugavel/RemoveGrupo",
		data: alugaveis,
		type: "DELETE",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) { successDeleteMultiploItem(data); },
		error: function (data) { errorGenerico(data); }
	});

}