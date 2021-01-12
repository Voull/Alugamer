$(document).ready(function () {
    //Para Texto Geral
    setInputFilter(document.getElementById("campoUsuario"), function (value) {
        return !/["']/.test(value);
    });

    setInputFilter(document.getElementById("campoSenha"), function (value) {
        return !/["']/.test(value);
    });
};

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
    var senha = $("#campoSenha").val();

    $.ajax({
        url: url,
        type: "POST",
        data: { Usuario: usuario, Senha: senha, Captcha: captcha },

        success: function (data) {
            data = JSON.parse(data);

            if (!data.Item1) {
                document.getElementById("modalErroMensagem").innerHTML = data.Item2;

                $("#modalLogin").modal('hide');
                $("#modalErro").modal();
                grecaptcha.reset();
            }
            else {
                document.location.href = data.Item2;
            }

        },

        error: function () {
            document.getElementById("modalErroMensagem").innerHTML = "Erro na Validação do Login. Tente Novamente mais tarde.";
            $("#modalLogin").modal('hide');
            $("#modalErro").modal();
            grecaptcha.reset();
        }
    });
}