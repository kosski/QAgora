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
        var url = $(this).val("messageId");
        $.ajax({
            type: "GET",
            url: '/Messages/_ReadFromNavbar',
            data: {messageId: url}
        }).success(function (data) {
            $("#modalPlace").html(data);
        });


    });
}

