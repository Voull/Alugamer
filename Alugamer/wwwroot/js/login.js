$(document).ready(function () {
    //Para Texto Geral
    setInputFilter(document.getElementById("campoUsuario"), function (value) {
        return !/["']/.test(value);
    });

    setInputFilter(document.getElementById("campoSenha"), function (value) {
        return !/["']/.test(value);
    });
});

function login(e) {
    e.preventDefault();

    var url = "/Login/Login";

    var usuario = $("#campoUsuario").val();
    var senha = SHA256($("#campoSenha").val());

    $.ajax({
        url: url,
        type: "POST",
        data: { nomeUsuario: usuario, senha: senha},
        dataType: "json",
        success: function () { successLogin();},
        error: function (data) {erroLogin(data);}
    });
}

function successLogin() {
    location.href = "/";
}

function erroLogin(data) {
    errorGenerico(data);
    $("#campoSenha").val("");
}