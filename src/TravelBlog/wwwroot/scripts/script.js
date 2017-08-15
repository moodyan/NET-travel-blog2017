$('.hello-ajax').click(function () {
            console.log("hello!!!");
            $.ajax({
                type: 'GET',
                url: '@Url.Action("HelloAjax")',
                success: function (result) {
                    $('#result1').html(result);
                }
            });
        });
$('.register').click(function () {
    $.ajax({
        type: 'GET',
        dataType: 'html',
        url: '@Url.Action("Register", "Account")',
        success: function (result) {
            $('#register-form').html(result);
        }
    });
});
