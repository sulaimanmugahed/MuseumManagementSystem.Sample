// =============  Data Table - (Start) ================= //
$(document).ready(function(){
    var excelbtntext = $('.data_table').data('exportexcelbtntext');
    var printbtntext = $('.data_table').data('printbtntext');
    var table = $('#example').DataTable({
        

        "lengthMenu": [10, 20, 50, 100,200],
        
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
           
            
            
            
        }],

       

       

        "aaSorting":[[1,'asc']],
        /* buttons: ['copy', 'csv', 'excel', 'pdf', 'print']*/

       
        
        buttons: [
            {
                extend: 'excel',
                text:'<i class="mdi mdi-export" style="color:green; width:100%;"></i>',
                exportOptions: {
                    columns:':not(.notexport)'
                } 
            },

            "colvis",
            
               

            {
                extend: 'print',
                text: '<i class="mdi mdi-cloud-print-outline" style="color:green; width:100%;"></i>',
                title: "",
                messageTop:"",
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10pt')
                        .prepend('<h1 class="pb-4">Museum Of Taiz</h1>')
                        .css('direction', 'rtl');
                        
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit')
                        .css('direction','rtl');
                },
            
                exportOptions: {
                    columns: ':not(.notexport)'
                } 
            },
          

        ],


        //language
        'language': {
            "decimal": "",

            "emptyTable": "",

            "info": " _START_ - _END_  --->  _TOTAL_ ",

            "infoEmpty": "",

            "infoFiltered": "",

            "searchPlaceholder": "Search",

            "search":"_INPUT_",

            "infoPostFix": "",

            "thousands": ",",

            "lengthMenu": " _MENU_ ",

            "loadingRecords": "Loading...",

            "processing": "",

            
            "zeroRecords": "",

            "paginate": {

                "first": "First",

                "last": "Last",

                "next": ">",

                "previous": "<"

            },

            "aria": {

                "sortAscending": ": activate to sort column ascending",

                "sortDescending": ": activate to sort column descending"

            }
        }

    });
    
    
    table.buttons().container()
    .appendTo('#example_wrapper .col-md-6:eq(0)');

});


// =============  Data Table - (End) ================= //
