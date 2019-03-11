var homeController = {
    init: function() {
        homeController.registerEvent();
    },
    registerEvent: function() {
        homeController.getAllEmployees();
    },

    //Load data from Controller using Ajax
    getAllEmployees: function() {
        $.ajax({
            url: '/Employee/GetListEmployees',
            type: 'GET',
            dataType: 'JSON',
            success: function(response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data,
                        function(i, item) {
                            html += Mustache.render(template,
                                {
                                    ID: item.Id,
                                    Name: item.Name,
                                    Salary: item.Salary,
                                    Status: item.Status === true
                                        ? '<span class="label label-success">Active</span>'
                                        : '<span class="label label-danger">Locked</span>'
                                });
                        });
                    $('#tblData').html(html);
                    homeController.eventUpdate();
                }
            }
        });
    },

    //Ấn Enter thì update
    eventUpdate: function() {
        $('.salary').off("keypress").on("keypress",
            function(e) {
                if (e.which === 13) {
                    var id = $(this).data("id");
                    var salary = $(this).val();
                    homeController.updateEmployee(id, salary);
                }
            });
    },

    //Update Data using Ajax
    updateEmployee(id, salary) {
        var data = { ID: id, Salary: salary };
        $.ajax({
            url: '/Employee/UpdateEmployee',
            type: 'POST',
            dataType: 'JSON',
            data: { model: JSON.stringify(data) },
            success: function (response) {
                if (response.status === "OK") {
                    console.log(response.data);
                    alert("Update Success!");
                    homeController.getAllEmployees();
                }
            },
            error: function() {
                alert("Error!");
            }
        });
    }
};

homeController.init();