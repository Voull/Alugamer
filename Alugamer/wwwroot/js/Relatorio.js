$(document).ready(function () {
    $('#tbCategoria').DataTable({
        dom: 'Bfrtip',
        select: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.22/i18n/Portuguese-Brasil.json"
        }
    });

});


function buscar() {

    limpaTabela()

    var id = Number($('#cliente').val());

    if (id == 0) {
        alert("Favor escolher um Cliente ou a opção Todos");
    }
    else {
        if (id == -1) {
            // busca todos no período fornecido

            var dataIni = $("#dataIni").val();
            var dataFim = $("#dataFim").val();

            $.ajax({
                url: "Relatorio/BuscaTodos?DataInicial=" + dataIni + "&DataFinal=" + dataFim,
                type: "GET",
                success: function (data) { successBusca(data); },
                error: function (data) { failBusca(data); }
            });

        }
        else {
            // busca os alugueis de um usuário especifico no período fornecido

            var dataIni = $("#dataIni").val();
            var dataFim = $("#dataFim").val();

                $.ajax({
                    url: "Relatorio/BuscaCliente?DataInicial=" + dataIni + "&DataFinal=" + dataFim +"&Id=" + id,
                    type: "GET",
                    success: function (data) { successBusca(data); },
                    error: function (data) { failBusca(data); }
                });
        }
    }
}

function successBusca(data) {

    var Alugueis = JSON.parse(data);

    var vlrTotal = 0;

    var table = document.getElementById('tbCategoria');

    for (var i = 0; i < Alugueis.length; i++) {
        var rowCount = table.rows.length;
        var row = table.insertRow(rowCount);

        var cell1 = row.insertCell(0);
        cell1.innerHTML = Alugueis[i].Locatario;

        var cell2 = row.insertCell(1);
        cell2.innerHTML = Alugueis[i].Vendedor;

        var cell3 = row.insertCell(2);
        cell3.innerHTML = (Alugueis[i].DataDevolucao).split('T')[0];

        var cell4 = row.insertCell(3);
        cell4.innerHTML = Alugueis[i].Valor_total;

        var cell5 = row.insertCell(4);
        cell5.innerHTML = Alugueis[i].Valor_desconto;

        var cell6 = row.insertCell(5);
        cell6.innerHTML = Alugueis[i].Valor_multa;

        var vlr = Alugueis[i].Valor_total - Alugueis[i].Valor_desconto + Alugueis[i].Valor_multa;

        vlrTotal += vlr;

    }

    $("#valor").val(vlrTotal);

}

function failBusca(data) {
    parsed = data.responseJSON;
    alert(parsed);
}

function limpaTabela() {

    var table = document.getElementById('tbCategoria');
    table.innerHTML = "<thead><tr><th><label>Cliente</label></th><th><label>Vendedor</label></th><th><label>Data de Devolução</label></th><th><label>Valor Total</label></th><th><label>Valor do Desconto</label></th><th><label>Valor da Multa</label></th></tr></thead ><tbody></tbody >";

}
