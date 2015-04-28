function start() {
    
    $('#registerLink').click(function() {
        $.ajax({
            type: "GET",
            url: '/Account/_LoginPartialView'
        }).success(function(data) {
            $('#modalPlace').html(data);
            $("#modal").modal("show");
        });
    });

    $(".jqte").jqte();
}

