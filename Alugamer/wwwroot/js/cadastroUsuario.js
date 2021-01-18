$(document).ready(function () {
    //Para Texto Geral
    setInputFilter(document.getElementById("NomeUsuario"), function (value) {
        return !/["']/.test(value);
    });

    setInputFilter(document.getElementById("SenhaNew"), function (value) {
        return !/["']/.test(value);
    });
    setInputFilter(document.getElementById("SenhaNewRepeat"), function (value) {
        return !/["']/.test(value);
    });
});

function salvaPerfil(e) {
    e.preventDefault();

    if (!validaCampos())
        return;

    let perfil = {
        CodFuncionario: $("#CodFuncionario").val(),
        NomeUsuario: $("#NomeUsuario").val(),
        Admin: $("#Admin").val()
    };

    let senhaNova = SHA256($("#SenhaNew").val());

    $.ajax({
        url: "/Funcionario/SalvaUsuario",
        type: "POST",
        data: {perfil: perfil, senhaNova: senhaNova},
        dataType: "json",
        success: function (data) { successSalvaItem(data); },
        error: function (data) { errorGenerico(data); }
    });
}

function validaCampos() {
    let usuario = $("#NomeUsuario").val();
    let senhaNew = $("#SenhaNew").val();
    let senhaNewRepeat = $("#SenhaNewRepeat").val();

    erros = "";

    if (usuario == "")
        erros += "Insira um nome de usuário!\n";
    if (senhaNew.length < 6)
        erros += "Senha Nova deve conter ao menos 6 caracteres!\n";
    else if (senhaNewRepeat != senhaNew)
        erros += "Os campos com a Senha Nova não coincidem!\n";

    if (erros != "") {
        alert(erros);
        return false;
    }

    return true;
}