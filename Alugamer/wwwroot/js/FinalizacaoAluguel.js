var Itens = "";
var vlrTotalAluguel = 0;

$(document).ready(function () {
	vlrTotalAluguel = Number($("#total").val());
});

function finalizar() {

	var dateFim = $("#DataFinal").val();
	var dateDevolucao = $("#DataDevolucao").val();
	let multa = Number($("#taxaMulta").val() == "" ? 0 : $("#taxaMulta").val());
	let valorMulta = Number(vlrTotalAluguel) * (multa / 100);

	if (new Date(dateFim).setHours(0, 0, 0, 0) >= new Date(dateDevolucao).setHours(0, 0, 0, 0))
		valorMulta = 0;

	let aluguel = {};
	aluguel.Id = Number($("#idAluguel").val());
	aluguel.DataDevolucao = dateDevolucao;
	aluguel.Valor_multa = valorMulta;

	aluguel = JSON.stringify(aluguel);

	$.ajax({
		url: "/Aluguel/FinalizaAluguel/",
		data: aluguel,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function () { successSalvaItem(); },
		error: function (data) { errorGenerico(data); }
	});

}

function successSalvaAluguel() {
	res = alert("Item salvo com sucesso!");
	location.reload();
}

function failSalvaAluguel(data) {
	parsed = data.responseJSON;
	alert(parsed);
}

function calculaVlrTotal() {
	if ($("#DataDevolucao").val() == "" || new Date($("#DataFinal").val()).setHours(0, 0, 0, 0) >= new Date($("#DataDevolucao").val()).setHours(0, 0, 0, 0)) {
		$("#total").val(vlrTotalAluguel);
		return;
	}

	let multa = Number($("#taxaMulta").val() == "" ? 0 : $("#taxaMulta").val());
	let valorFinal = vlrTotalAluguel * (1 + (multa / 100));

	$("#total").val(valorFinal)
}