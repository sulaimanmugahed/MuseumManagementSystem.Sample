/// <reference path="../shared/ar-YE.js" />

$(document).ready(function () {
    var id = $('#safe').data('safeid');
  
   var table = $('#table_safe_artifacts').DataTable({

        "ajax": {
            "url": "/api/artifacts/getartifactsbysafeid/"+id,
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
                className: "dt-center", targets: [1, 2, 3, 4, 5, 6]
            },
        ],

        "columns": [
            { "data": "id", "name": "Id", "autowidth": true },
            { "data": "name", "name": "Name", "autowidth": true },
            { "data": "serialNumber", "name": "SerialNumber", "autowidth": true },
            { "data": "oldMuseumNumber", "name": "OldMuseumNumber", "autowidth": true },
            { "data": "newMuseumNumber", "name": "NewMuseumNumber", "autowidth": true },
            { "data": "count", "name": "Count", "autowidth": true },
            {
                data: null,
                render: function (data, type, row) {
                    return `

 <div class="dropdown">
  <a class=" dropdown-toggle p-1 m-1"></a>
  <div class="dropdown-content">
 
          <a class="dropdown-item" href="../../artifacts/details/${row.id}">
            ${Buttons.details}</a>
          <div class="dropdown-divider"></div>
          <a class="dropdown-item" href="../../artifacts/edit/${row.id}">
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
                    form.action = `/safes/exporttoexcelartifactsbysafeid`;

                    var idInput = document.createElement('input');
                    idInput.type = 'hidden';
                    idInput.name = 'id';
                    idInput.value = id;

                    form.appendChild(idInput);
                    document.body.appendChild(form);

                    form.submit();
                }
            },
           


       ],
       initComplete: () => {
           $(".dataTables_paginate").appendTo($('.my_pagination'));
           $(".dataTables_info").appendTo($('.dt_info'));
       },
        stateSave: true,
        'language':DataTable.language

    });


    $(document).on('click', '.deleteButton', function (e) {
        e.preventDefault();
        var row = $(this).closest('tr');
        var rowId = $(this).data('id');
        showConfirmMessage(AlertMessages.delete_artifact_confirm_msg)
            .then(result => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/artifacts/delete/' + rowId,
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