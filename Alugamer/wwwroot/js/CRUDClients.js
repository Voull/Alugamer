$(document).ready(function () {
	$('#tbCliente').DataTable({
		dom: 'Bfrtip',
		select: true,
		language: {
			url: "//cdn.datatables.net/plug-ins/1.10.22/i18n/Portuguese-Brasil.json"
		},
		buttons: [{
			text: "Novo",
			action: function (e, dt, button, config) {
				document.location.href += '/cadastro';
			},
			className: 'btn-primary'
		},
		{
			text: "Remover",
			action: function (e, dt, button, config) {
				removeClientes();
			},
			className: 'btn-danger'

		}
		]
	});
});


function removeClientes() {
	if (!confirm("Realmente desejar remover os Itens selecionados?"))
		return;

	let clientes = []

	$(".selected td:nth-child(1)").each(function () {
		clientes.push(Number($(this).html()));
	});

	if (clientes.length == 0) {
		alert("Não há itens selecionados!");
		return;
	}

	clientes = JSON.stringify(clientes);

	$.ajax({
		url: "/Cliente/DeleteGrupo",
		data: clientes,
		type: "DELETE",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) { successDeleteMultiploItem(data); },
		error: function (data) { errorGenerico(data); }
	});

}