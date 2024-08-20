
$(document).ready(function () {


   var table = $('#table_recyclebin').DataTable({
        "ajax": {
            "url": "/api/recyclebin",
            "type": "Post",
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
 
          <a class="dropdown-item recoveryButton" href="javascript:;" data-id="${row.id}">
            ${Buttons.recovery}</a>
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
                className: "page-lenght-list",
                text: Buttons.clear,
                
                action: (e, dt, button, config) => {
                    showConfirmMessage(AlertMessages.clear_recyclebin_confirm_msg)
                        .then((result) => {
                        if (result.isConfirmed) {
                            fetch('/api/recyclebin/Clear', { method: "post" })
                                .then(res => {
                                    dt.clear().draw();
                                    return showSuccessMessage();
                                })
                                .catch(_ => {
                                    return showErrorMessage();
                                });
                        }
                    })
                    
                }
            }
          


       ],
       initComplete: function () {
           $(".dataTables_paginate").appendTo($('.my_pagination'));
           $(".dataTables_info").appendTo($('.dt_info'));
       },
        'language': DataTable.language,
      
   });

    table.on('draw.dt', () => {

        if (!$("#tableSearch")[0].value && table.rows().count() === 0) {
          
            $('.myButtons').hide();
            $("#tableSearch").hide();
        } else {
            $('.myButtons').show();
            $('#tableSearch').show();


        }
    });



    $(document).on('click', '.deleteButton', function (e) {
        e.preventDefault();
        var row = $(this).closest('tr');
        var rowId = $(this).data('id');
        showConfirmMessage(AlertMessages.kill_artifact_confirm_msg)
            .then(result => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/recyclebin/delete/' + rowId,
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


    $(document).on('click', '.recoveryButton', function (e) {
        e.preventDefault();
        var row = $(this).closest('tr');
        var rowId = $(this).data('id');
        showConfirmMessage(AlertMessages.recovery_artifact_confirm_msg)
            .then(result => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/recyclebin/recovery/' + rowId,
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

    //if (table.rows().count() === 0) {
    //    $('.myButtons').hide();
    //} else {
    //    $('.myButtons').show();

    //}

    $('#tableSearch').on('keyup', function () {
        table.search(this.value).draw();
    });


});
