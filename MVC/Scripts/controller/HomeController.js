var homeController = {
    init: function() {
        homeController.registerEvent();
        homeController.searchEmployee();
    },
    registerEvent: function() {
        homeController.getAllEmployees();
        homeController.createEmployee(); 
    },

    //Create new Employee
    createEmployee: function() {
        $('#btnSave').off('click').on('click',
            function() {
                var id = $('#ID').val();
                var name = $('#name').val();
                var salary = $('#salary').val();
                var status = $('#status').prop('checked');
                var data = JSON.stringify({
                    Id: id,
                    Name: name,
                    Salary: salary,
                    Status:status
                });
                $.ajax({
                    url: '/Employee/CreateEmployee',
                    dataType: 'JSON',
                    data: { employee: data },
                    type: 'POST',
                    success: function(response) {
                        if (response.status === true) {
                            $('#addEmployee').modal('hide');
                            alert("Success!");
                            homeController.getAllEmployees();
                        }
                    },
                    error: function() {
                        console.log("Err!");
                    }
                });
            });
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
                    homeController.binData(data);
                }
            }
        });
    },

    //Ấn Enter thì update
    eventUpdate: function() {
        $('.btnUpdate').off("click").on("click",
            function () {
                var id = $(this).val();
                homeController.getEmployeeById(id);
                $('#addEmployee').modal('show');
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
                    alert("Update Success!");
                    homeController.getAllEmployees();
                }
            },
            error: function() {
                alert("Error!");
            }
        });
    },

    //Get Employee By Id
    getEmployeeById: function(id) {
        $.ajax({
            url: '/Employee/GetEmployeeById/',
            type: 'POST',
            dataType: 'JSON',
            data: { id: id },
            success: function(response) {
                var data = response.data;
                $('#ID').val(data.Id);
                $('#name').val(data.Name);
                $('#salary').val(data.Salary);
                $('#status').prop("checked",data.Status);
            },
            error: function() {
                console.log("Err!");
            }
        });
    },

    //Search Employee 
    searchEmployee() {
        $('#btnSearch').off('click').on('click', function () {
        var keyword = $('#keyword').val();
        if (keyword === null || keyword === "") {
            homeController.getAllEmployees();
        } else {
            
                $.ajax({
                    url: '/Employee/SearchEmployee',
                    type: 'Get',
                    dataType: 'JSON',
                    data: { keyword: keyword },
                    success: function (responce) {
                        homeController.binData(responce.data);
                    },
                    error: function () {
                        console.log("Error!");
                    }
                });
            }
        });
    },

    //Bindding Data to template
    binData: function(data) {
        var html = '';
        var template = $('#data-template').html();
        $.each(data,
            function (i, item) {
                html += window.Mustache.render(template,
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
};

homeController.init();