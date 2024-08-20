
jQuery(function ($) {


    $('#exportDate').datepicker({
        language: "ar",
        format: 'dd-mm-yyyy'


    });

});

$(document).ready(function () {

    $('#tb_artifacts thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#tb_artifacts thead');


    var table = $('#tb_artifacts').DataTable({
        "ajax": {
            "url": "/api/reports",
            "type": "GET",
            "datatype": "json",
        },

        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "serialNumber" },
            { "data": "oldMuseumNumber" },
            { "data": "newMuseumNumber" },
            { "data": "artifactType" },
            { "data": "importantMaterial" },
            { "data": "artifactCondition" },
            { "data": "safe" },
            { "data": "stowage" },
            { "data": "count" },

            {
                "data": "imageLink",

            }
       


        ],

        columnDefs: [
            {
                targets: 0,
                visible: false,
                searchable: false
            },
          
            {
                className: "dt-center", targets: [1, 2, 3, 4, 5, 6, 7, 8, 9,10,11]
            },
         

            {

                targets: 'no-sort',
                orderable: false
            },
          


        ],

        "aaSorting": [[1, 'asc']],

        dom: "Bfrtip",
        "stateLoadParams": function (settings, data) {
            for (var i = 0; i < data.columns.length; i++) {
                data.columns[i].search.search = "";
                data.columns[i].search.smart = true;
            }
        },
      

        buttons: [
            {
                extend: "excelHtml5",
                text: Buttons.export,
                title: function () {
                    return `${$('#exportTitle').val()} (${$('#exportNumber').val()})`;
                },

                exportOptions: {
                    columns: ':visible:not(.actions)'
                },

                className: "btn-export-excel",
            },
            {
                extend: "print",
                text: Buttons.print,
                title: () => $('#exportTitle').val(),
                exportOptions: {
                    columns: ':visible:not(.actions)'
                },

                className: "btn-export-print",
              

                customize: function (win) {
                    $(win.document).find('head title').text('');
                    $(win.document.body)
                        .css('font-size', '12pt')
                        .css('text-align', 'center')
                        .prepend(`

                        
      <div class=" text-dark text-center mb-2"><br>بسم الله الرحمن الرحيم</div>
       
        
        <div class="row align-items-center">
          
            
            <div class="col-4 text-dark text-center "><br>${Report.no} : ${$('#exportNumber').val()}<br>${Report.date} : ${$('#exportDate').val()} </div>
            <div class="col-4 text-dark text-center "><img src="/images/yemen.png" alt="" style="width: 50%; "></div>
            <div class="col-4 text-dark text-center "><br>الجمهورية اليمنية<br>محافظة تعز<br>فرع الهيئه العامة للأثار والمتاحف</div>
            
        
          </div>
          <hr width="100%" style="border: solid 1px black;" >
    </div> 

                        `)
                        .css('direction', Report.direction);

                    $(win.document.body).find('img').css('width', '150px')

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit')
                        .css('direction', Report.direction);

                    var header = $(win.document.body).find('table thead tr');
                    header.prepend('<th></th>');
                    var index = 1;
                    $(win.document.body).find('table tbody tr').each(function () {
                        $(this).prepend('<td>' + index + '</td>');
                        index++;

                    });

                },

            },
            {
                extend: "pageLength",
                className: "page-lenght-list",
            }
            ,
            {
                extend: "colvis",
                columns: ":not(.noVis)",
                collectionLayout: "fixed columns",
                className: "cilvis-list",

            }
        ],

        //forsearch input
        orderCellsTop: true,
        fixedHeader: true,
        stateSave: true,

        initComplete: function () {
            $(".dataTables_paginate").appendTo($('.my_pagination'));
            $(".dataTables_info").appendTo($('.dt_info'));
            var api = this.api();

            // For each column
            api
                .columns(':visible')
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );

                    console.log("colIdx ", colIdx)
                    var title = $(cell).text();

                    $(cell).html('<input type="text" style="border:0;" />');


                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('change', function (e) {
                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();
                        })
                        .on('keyup', function (e) {
                            e.stopPropagation();

                            $(this).trigger('change');
                            //$(this)
                            //    .focus()[0]
                            //    .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },


'language':DataTable.language,


    });


    table.buttons().container()
        .appendTo($('.myButtons'));

    $('#tableSearch').on('keyup', function () {
        table.search(this.value).draw();
    });



});
