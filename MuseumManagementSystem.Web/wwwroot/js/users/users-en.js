/// <reference path="../shared/en-US.js" />


$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        Swal.fire({
            title: AlertText.confirmQ,
            icon: "warning",
            cancelButtonText: AlertText.cancel,
            confirmButtonText: AlertText.ok,
            showCancelButton: true,
            cancelButtonColor: Colors.danger,
            confirmButtonColor: Colors.success,
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/users/delete/' + btn.data('id'),
                    type: 'POST',
                    success: function (response) {


                        btn.parents('.todelete').fadeOut();
                        Swal.fire({
                            showConfirmButton: false,
                            title: AlertText.successtitle,
                            text: response.message,
                            icon: "success",
                            timer: 2000
                        });

                    },

                    error: function (xhr, textStatus, errorThrown) {
                        Swal.fire({
                            confirmButtonText: AlertText.ok,
                            text: xhr.responseText,
                            title: AlertText.dangertitle,
                            icon: "error",

                        });
                    }
                });
            }


        });


    });
});
