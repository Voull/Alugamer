$(document).ready(function () {
    //Para Texto Geral
    setInputFilter(document.getElementById("NomeUsuario"), function (value) {
        return !/["']/.test(value);
    });
    setInputFilter(document.getElementById("SenhaOld"), function (value) {
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
    };

    let senhaAtual = SHA256($("#SenhaOld").val());
    let senhaNova = SHA256($("#SenhaNew").val());

    $.ajax({
        url: "/Usuario/Salva",
        type: "POST",
        data: {perfil: perfil, senhaAtual: senhaAtual, senhaNova: senhaNova},
        dataType: "json",
        success: function (data) { successSalvaItem(data); },
        error: function (data) { errorGenerico(data); }
    });
}

function validaCampos() {
    let usuario = $("#NomeUsuario").val();
    let senhaOld = $("#SenhaOld").val();
    let senhaNew = $("#SenhaNew").val();
    let senhaNewRepeat = $("#SenhaNewRepeat").val();

    erros = "";

    if (usuario == "")
        erros += "Insira um nome de usuário!\n";
    if (senhaOld != "") {
        if (senhaOld.length < 6)
            erros += "Senha Atual deve conter ao menos 6 caracteres!\n";
        if (senhaNew.length < 6)
            erros += "Senha Nova deve conter ao menos 6 caracteres!\n";
        else if (senhaNewRepeat != senhaNew)
            erros += "Os campos com a Senha Nova não coincidem!\n";
    }

    if (erros != "") {
        alert(erros);
        return false;
    }

    return true;
}