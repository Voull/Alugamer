var Itens = "";
var vlrTotalAluguel = 0;

function carregaItens() {

	var categoria = $("#categoriaAluga").val();

	$.ajax({
		url: "/Aluguel/CarregaItens",
		data: {categoria: categoria},
		type: "GET",
		success: function (data) { successCarregaItens(data); },
		error: function (data) { failCarregaItens(data); }
	});
}

function successCarregaItens(data) {

	limpaItens();

	Itens = JSON.parse(data);
	var select = document.getElementById("itensAluga"); 

	for (var i = 0; i < Itens.length; i++) {
		var el = document.createElement("option");
			var item = Itens[i].Nome + " - Em estoque: " + Itens[i].Quantidade + " - Valor: " + Itens[i].Valor_aluguel;

		el.textContent = item;
		el.value = i;

		select.appendChild(el);
    }
}

function failCarregaItens(data) {

	parsed = data.responseJSON;
	alert(parsed);
}

function escolheItem() {

	var index = $("#itensAluga").val();
	var descricaoItem = Itens[index].Descricao;

	document.getElementById('itemDescricao').innerHTML = descricaoItem;
}

function limpaItens() {

	var select = document.getElementById("itensAluga");
	var length = select.options.length;
	for (i = length - 1; i > 0; i--) {
		select.options[i] = null;
	}
}

function adicionaItem() {

	var index = $("#itensAluga").val();
	var quantidade = Number($("#itenQtd").val())

	if (quantidade <= 0) {
		alert("Quantidade escolhida inválida");
	}
	else {
		if (Itens[index].Quantidade < quantidade) {
			alert("Quantidade escolhida é maior que o disponível em estoque");
		}
		else {
			var table = document.getElementById('dataTable');

			var rowCount = table.rows.length;
			var row = table.insertRow(rowCount);

			var cell1 = row.insertCell(0);
			var element1 = document.createElement("input");
			element1.type = "checkbox";
			element1.name = "chkbox[]";
			cell1.appendChild(element1);

			var cell2 = row.insertCell(1);
			cell2.innerHTML = Itens[index].Id;

			var cell3 = row.insertCell(2);
			cell3.innerHTML = Itens[index].Nome;

			var cell4 = row.insertCell(3);
			var qtd = $("#itenQtd").val();
			cell4.innerHTML = qtd;

			var cell5 = row.insertCell(4);
			var vlrAluga = Itens[index].Valor_aluguel;
			cell5.innerHTML = vlrAluga;

			vlrTotalAluguel += qtd * vlrAluga;

			calculaVlrTotal();

			$("#itenQtd").val("");
		}
    }
}

function removeItem() {
	try {
		var table = document.getElementById('dataTable');
		var rowCount = table.rows.length;

		for (var i = 0; i < rowCount; i++) {
			var row = table.rows[i];
			var chkbox = row.cells[0].childNodes[0];
			if (null != chkbox && true == chkbox.checked) {
				table.deleteRow(i);
				rowCount--;
				i--;

				var qtd = Number(row.cells[3].childNodes[0].data);
				var vlr = Number(row.cells[4].childNodes[0].data);

				vlrTotalAluguel -= (qtd * vlr);

				calculaVlrTotal()
			}
		}
	} catch (e) {
		alert(e);
	}
}

function finalizar() {

	var table = document.getElementById('dataTable');
	var rowCount = table.rows.length;
	var ListItensAluguel = [];

	for (var i = 1; i < rowCount; i++) {
		var row = table.rows[i];

		let itemAluguel = { };
		itemAluguel.IdAluguel = -1;
		itemAluguel.IdItem = Number(row.cells[1].childNodes[0].data);
		itemAluguel.Quantidade = Number(row.cells[3].childNodes[0].data);
		itemAluguel.Valor = Number(row.cells[4].childNodes[0].data);

		ListItensAluguel[i-1] = itemAluguel;
	}

	var data = new Date();
	var dateInicio = data.getFullYear() + "-0" + (data.getMonth() + 1) + "-" + data.getDate();
	var tempoAluguel = Number($("#tempoAluguel").val())
	data.setUTCMonth(data.getUTCMonth() + tempoAluguel);
	var dateFim = data.getUTCFullYear() + "-0" + (data.getUTCMonth() + 1) + "-" + data.getUTCDate();
	let desconto = $("#taxaDesconto").val() == "" ? 0 : $("#taxaDesconto").val();
	let valorDesconto = vlrTotalAluguel * (desconto / 100) * tempoAluguel;

	let aluguel = {};
	aluguel.Id = -1;
	aluguel.Locatario = Number($("#nomeCliente").val());
	aluguel.Vendedor = 1;
	aluguel.Valor_total = $("#total").val();
	aluguel.DataInicial = dateInicio;
	aluguel.DataFinal = dateFim;
	aluguel.Itens = ListItensAluguel;
	aluguel.Valor_desconto = valorDesconto;

	aluguel = JSON.stringify(aluguel);

	$.ajax({
		url: "/Aluguel/Novo/",
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
	let tempoAluguel = $("#tempoAluguel").val() == "" ? 1 : $("#tempoAluguel").val();
	let desconto = $("#taxaDesconto").val() == "" ? 0 : $("#taxaDesconto").val();
	let valorFinal = vlrTotalAluguel * (1 - (desconto / 100)) * tempoAluguel;

	$("#total").val(valorFinal)
}