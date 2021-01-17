function successSalvaItem(data){
    let msg = "O Item foi salvo com sucesso!";

    if (data != undefined) {
        if (data.responseText != undefined && data.responseText != "")
            msg = data.responseText;
        else if (data != "")
            msg = data;
    }


    let result = alert(msg);

    window.history.back();
}

function successDeleteItem(data) {
    let msg = "O Item foi removido com sucesso!";

    if (data != undefined) {
        if (data.responseText != undefined && data.responseText != "")
            msg = data.responseText;
        else if (data != "")
            msg = data;
    }

    let result = alert(msg);

    location.reload();
}

function successDeleteMultiploItem(data) {
    let msg = "Os Itens foi removidos com sucesso!";

    if (data != undefined) {
        if (data.responseText != undefined && data.responseText != "")
            msg = data.responseText;
        else if (data != "")
            msg = data;
    }

    let result = alert(msg);

    location.reload();
}

function errorGenerico(data) {
    let msg = "Um erro ocorreu, atualize a página!\nCaso o erro persista, notifique um Administrador.";

    if (data != undefined) {
        if (data.responseText != undefined && data.responseText != "")
            msg = data.responseText;
        else if (data != "")
            msg = data;
    }

    let result = alert(msg);
}