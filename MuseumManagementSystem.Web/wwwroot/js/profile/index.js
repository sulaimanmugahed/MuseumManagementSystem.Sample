const inputFile = document.getElementById('profileInput');
const inputArea = document.getElementById('profile-upload');


inputArea.addEventListener('click', () => {
    inputFile.click();
});
$(document).ready(function () {
    


        $('#profileInput').on('change', function (event) {
            // Retrieve the selected file
            const selectedFile = event.target.files[0];

            const reader = new FileReader();

            reader.onload = function (event) {
                const imageElement = document.getElementById('profilePicture')

                imageElement.src = event.target.result;
            };

            reader.readAsDataURL(selectedFile);
        });

    

    ///////////////////////////////////////////
    $('#form').submit(function (event) {
        event.preventDefault(); // Prevent the form from submitting normally



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


        // Serialize the form data
                var formData = new FormData(this);

        // Send the updated profile data to the server using AJAX
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            processData: false,
            contentType:false,
            success: function (response) {
            
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

        })

    });

    //$(document).on('click', '.submitBtn', function (e) {
    //    e.preventDefault();
    //    var row = $(this).closest('tr');
    //    var rowId = $(this).data('id');
    //    Swal.fire({
    //        title: AlertText.confirmQ,
    //        icon: "warning",
    //        cancelButtonText: AlertText.cancel,
    //        confirmButtonText: AlertText.ok,
    //        showCancelButton: true,
    //        cancelButtonColor: Colors.danger,
    //        confirmButtonColor: Colors.success,
    //    }).then((result) => {
    //        if (result.isConfirmed) {
    //            $.ajax({
    //                url: '/users/delete/' + rowId,
    //                type: 'POST',
    //                success: function (response) {
    //                    row.fadeOut(400, function () {
    //                        row.remove();
    //                    });
    //                    Swal.fire({
    //                        showConfirmButton: false,
    //                        title: AlertText.successtitle,
    //                        text: response.message,
    //                        icon: "success",
    //                        timer: 2000
    //                    });

    //                },

    //                error: function (xhr, textStatus, errorThrown) {
    //                    Swal.fire({
    //                        confirmButtonText: AlertText.ok,
    //                        text: xhr.responseText,
    //                        title: AlertText.dangertitle,
    //                        icon: "error",

    //                    });

    //                }



    //            });

    //        }

    //    })
    
    //});
});