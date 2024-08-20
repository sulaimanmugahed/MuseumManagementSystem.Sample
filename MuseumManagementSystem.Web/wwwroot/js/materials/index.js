

$(document).ready(function () {


   var table= $('#table_materials').DataTable({
        "ajax": {
            "url": "/api/materials",
            "type": "Post",
            data: function (data) {
                data.searchValue = data.search.value;
                data.skip = data.start;
                data.pageSize = data.length;
            },
            dataType: 'json',
        },

        "processing": true,
        "serverSide": true,
        "filter": true,
        "ordering": false,
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },

            {
                className: "dt-center", targets: [1, 2, 3]
            },
        ],

        "columns": [
            { "data": "id", "name": "Id", "autowidth": true },
            { "data": "name", "name": "Name", "autowidth": true },
            { "data": "artifactsCount", "name": "ArtifactsCount", "autowidth": true },
            {
                data: null,
                render: function (data, type, row) {
                  
                    return `

 <div class="dropdown">
  <a class=" dropdown-toggle p-1 m-1"></a>
  <div class="dropdown-content">
 
          
          <a class="dropdown-item" href="materials/edit/${row.id}">
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
                text: Buttons.export,
                className: "page-lenght-list",
                action: function (e, dt, button, config) {
                    var form = document.createElement('form');
                    form.method = 'POST';
                    form.action = '/materials/export';

                    var idInput = document.createElement('input');
                    idInput.type = 'hidden';
                    idInput.name = 'type';
                    idInput.value = "excel";

                    form.appendChild(idInput);
                    document.body.appendChild(form);

                    form.submit();
                }
            },
           
            {
                text: Buttons.add,
                className: "page-lenght-list",
                action: function (e, dt, button, config) {
                    window.location.href = '/materials/create';
                }
            },

         


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
        showConfirmMessage(AlertMessages.delete_material_confirm_msg)
            .then(result => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/materials/delete/' + rowId,
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