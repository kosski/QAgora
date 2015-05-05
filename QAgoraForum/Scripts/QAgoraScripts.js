function start() {

    $('#registerLink').click(function () {
        $.ajax({
            type: "GET",
            url: '/Account/_LoginPartialView'
        }).success(function (data) {
            $('#modalPlace').html(data);
            $("#modal").modal("show");
        });
    });

    $(".jqte").jqte();





    $('#mailNotify').click(function () {
        var nfo = { messageId: $(this).attr("messageId") };
        AjaxRequest("GET", '/Messages/_ReadFromNavbar', nfo, showModalmessage);
    });

}
function showModalmessage(data) {
    $("#modalPlace").html(data);
    $('#ButtonMAnswer').click(function () {
        var nfo = { messageId: $(this).attr("messageId") };
        AjaxRequest("GET", "/Messages/_AnswerForm", nfo, showAnswerForm);
    })
    $("#modal").modal("show");
}

function showAnswerForm(data) {
    $("#WriteAnswer").hide();
    $("#WriteAnswer").html(data);
    $(".jqte").jqte();
    $("#WriteAnswer").show(400);
}

function AjaxRequest(type, url, data, onSuccess) {
    $.ajax({
        type: type,
        url: url,
        data: data
    }).success(function (data) {
        onSuccess(data);
    });
}

