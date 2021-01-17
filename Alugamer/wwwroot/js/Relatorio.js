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

    var id = Number($('#cliente').val());

    if (id == 0) {
        alert("Favor escolher um Cliente ou a opção Todos");
    }
    else {
        if (id == -1) {
            // busca todos no período fornecido

            var dataIni = $("#dataIni").val();
            var dataFim = $("#dataFim").val();

            let aluguel = {};

            aluguel.DataInicial = dataIni;
            aluguel.DataFinal = dataFim;

            aluguel = JSON.stringify(aluguel);


            $.ajax({
                url: "Relatorio/BuscaTodos",
                data: aluguel,
                type: "application/json",
                success: function (data) { successBuscaTodos(data); },
                error: function (data) { failBuscaTodos(data); }
            });

        }
        else {
            // busca os alugueis de um usuário especifico no período fornecido

            var dataIni = $("#dataIni").val();
            var dataFim = $("#dataFim").val();

                $.ajax({
                    url: "Relatorio/BuscaCliente",
                    data: { DataInicial: dataIni, DataFim: dataFim , Id: id},
                    type: "application/json",
                    success: function (data) { successBuscaCliente(data); },
                    error: function (data) { failBuscaCliente(data); }
                });

        }
    }
}

function successBuscaTodos(data) {


    var x = 1;
}

function failBuscaTodos(data) {
    parsed = data.responseJSON;
    alert(parsed);
}

function successBuscaCliente(data) {
    var cx = 1;
}

function failBuscaCliente(data) {
    parsed = data.responseJSON;
    alert(parsed);
}