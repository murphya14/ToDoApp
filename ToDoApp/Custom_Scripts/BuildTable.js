$(document).ready(function () {
    $.ajax({
        url: '/ToDoes/BuildToDoTable', ///Shows the AJAX call we want to call a request to the action BuildToDoTable in the Controller.
                                       ///This builds the partial view _ToDoTable
        success: function (result) {
            $('#tableDiv').html(result) ///Table div is replaced by the result of running the BuildToDoTable action 
        }
    });

});