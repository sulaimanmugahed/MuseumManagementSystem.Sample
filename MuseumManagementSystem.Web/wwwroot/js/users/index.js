

$(document).ready(function () {


    var table =  $('#table_users').DataTable({
        "ajax": {
            "url": "/api/users",
            "type": "Post",
            data: function (data) {
                data.searchValue = data.search.value;
                data.sortColumn = data.columns[data.order[0].column].name;
                data.sortColumnDirection = data.order[0].dir;
                data.skip = data.start;
                data.pageSize = data.length;
            },
            dataType: 'json',
        },
   
        "processing": true,
        "serverSide": true,
        "filter": true,
       
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
          
            {
                className: "dt-center", targets: [1, 2, 3, 4,5,6,7]
            },
        ],

        "columns": [
            { "data": "id", "name": "Id", "autowidth": true },
            { "data": "firstName", "name": "FirstName", "autowidth": true },
            { "data": "lastName", "name": "LastName", "autowidth": true },
            { "data": "userName", "name": "UserName", "autowidth": true },
            { "data": "email", "name": "Email", "autowidth": true },
            { "data": "phoneNumber", "name": "PhoneNumber", "autowidth": true },
            { "data": "role", "name": "Role", "autowidth": true, "orderable": false },
           
            {
                data: null,
                render: function (data, type, row) {

                    return `

 <div class="dropdown">
  <a class=" dropdown-toggle p-1 m-1"></a>
  <div class="dropdown-content">
 
          <a class="dropdown-item" href="users/edit/${row.id}">
            ${Buttons.edit}</a>
            <div class="dropdown-divider"></div>
          <a class="dropdown-item deleteButton" href="javascript:;" data-id="${row.id}">
            ${Buttons.delete}</a>
       
  </div>
</div>
                    `;
                },
                "orderable": false
            },

        ],
        dom: "Bfrtip",
        buttons: [
            {
                extend: "pageLength",
                className: "page-lenght-list",
            },
            {
                text: Buttons.add,
                className: "page-lenght-list",
                action: function (e, dt, button, config) {
                    window.location.href = '/users/create';
                }
            }


        ],
        initComplete: () => {
            $(".dataTables_paginate").appendTo($('.my_pagination'));
            $(".dataTables_info").appendTo($('.dt_info'));
        },
        stateSave: true,
        'language': DataTable.language

    });


    $(document).on('click', '.deleteButton', function (e) {
        e.preventDefault();
        var row = $(this).closest('tr');
        var rowId = $(this).data('id');
        showConfirmMessage(AlertMessages.delete_user_confirm_msg)
            .then(result => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/users/delete/' + rowId,
                        type: 'POST',
                        success: function (response) {
                            row.fadeOut(400, function () {
                                row.remove();
                            });
                            showSuccessMessage(response.message);
                        },
                        error: function (xhr) {
                            showErrorMessage(xhr.responseText)
                        }
                    });
                }
            });
    
    });
    table.buttons().container()
        .appendTo($('.myButtons'));

    $('#tableSearch').on('keyup', function () {
        table.search(this.value).draw();
    });
});
