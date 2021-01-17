$(document).ready(function () {
	$('#tbAluguel').DataTable({
		dom: 'Bfrtip',
		select: true,
		language: {
			url: "//cdn.datatables.net/plug-ins/1.10.22/i18n/Portuguese-Brasil.json"
		},
		buttons: [{
			text: "Novo",
			action: function (e, dt, button, config) {
				document.location.href = '/Aluguel/Cadastro';
			},
			className: 'btn-primary'
		},
		{
			text: "Remover",
			action: function (e, dt, button, config) {
				removeCategorias();
			},
			className: 'btn-danger'

		}
		]
	});

});
