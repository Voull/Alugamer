$(document).ready(function () {
    //Para Texto Geral
    setInputFilter(document.getElementById("campoUsuario"), function (value) {
        return !/["']/.test(value);
    });

    setInputFilter(document.getElementById("campoSenha"), function (value) {
        return !/["']/.test(value);
    });
});

$('#modalLogin').on('shown.bs.modal', function (e) {
    Login();
});

$('#formLogin').on('submit', function () {
    $("#modalLogin").modal({
        backdrop: "static"
    });
    return false;
});

function Login() {
    var url = "/Login/Login";

    var usuario = $("#campoUsuario").val();
    var senha = SHA256($("#campoSenha").val());

    $.ajax({
        url: url,
        type: "POST",
        data: { nomeUsuario: usuario, senha: senha},

        error: function (data) {
            document.getElementById("modalErroMensagem").innerHTML = data.responseText;
            $("#modalLogin").modal('hide');
            $("#modalErro").modal();
        }
    });
}