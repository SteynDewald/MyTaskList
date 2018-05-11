$(document).ready(function () {

    $('.TaskState').change(function () {
        var self = $(this);
        var id = self.attr('id');
        var value = self.prop('checked');

        $.ajax({
            url: '/TaskItems/AJAXEdit',
            data: {
                id: id,
                value: value
            },
            type: "POST",
            success: function (result) {
                $(TaskList).html(result);
            }

        });
    });

});