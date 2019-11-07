$(document).ready(function () {

    $('.ActiveCheck').change(function () {     ///So if there is a change in the checkbox this function is executed 
        var self = $(this);      /// this is the checkbox
        var id = self.attr('id'); //attr is attribute
        var value = self.prop('checked'); //So this is getting all these variables and now it can execute the Ajax

        $.ajax({
            url: '/ToDoes/AJAXEdit',
            data: {
                id: id,
                value: value              ////This matches the arguements in the controller action 
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result) ///Table div is replaced by the result of running the BuildToDoTable action 
            }
        });
    });



});