$(document).ready(function () {
    $('#tbCategoria').DataTable({
        dom: 'Bfrtip',
        select: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.22/i18n/Portuguese-Brasil.json"
        },
        buttons: [{
            text: "Novo",
            action: function (e, dt, button, config) {
                document.location.href = '/Categoria/Cadastro';
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

function removeCategorias() {
    if (!confirm("Realmente desejar remover os Itens selecionados?"))
        return;

    let categorias = []

    $(".selected td:nth-child(1)").each(function () {
        categorias.push(Number($(this).html()));
    });

    if (categorias.length == 0) {
        alert("Não há itens selecionados!");
        return;
    }

    categorias = JSON.stringify(categorias);

    $.ajax({
        url: "/Categoria/DeleteGrupo",
        data: categorias,
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { successDeleteMultiploItem(data); },
        error: function (data) { errorGenerico(data); }
    });

}

function successRemove(data) {
    let msg = "";
    if (data == "")
        msg = 'Os Itens foram removidos com sucesso!';
    else
        msg = data;

    let result = alert(msg);

    location.reload();
}

function failRemove(data) {
    let msg = JSON.parse(data);

    alert(msg);
}