$(document).ready(function () {
    $.ajax({
        url: '/TaskItems/ReloadTasks',
        success: function (result) {
            $('#TaskList').html(result);
        }
    });
});