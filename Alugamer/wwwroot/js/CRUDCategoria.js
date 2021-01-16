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
                document.location.href += '/cadastro';
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
        categorias.push($(this).val());
    });

    if (categorias.length == 0) {
        alert("Não há itens selecionados!");
        return;
    }

    categorias = JSON.stringify(categorias);

    $.ajax({
        url: "Categoria/" + Remove + "/",
        data: categorias,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () { successRemove(); },
        error: function (data) { failRemove(data); }
    });

}

function successRemove() {
    location.reload();
}

function failRemove(data) {
    let msg = JSON.parse(data);

    alert(msg);
}