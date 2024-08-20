
const showConfirmMessage = (message) => {
    return Swal.fire({
        title: AlertText.confirmQ,
        icon: "warning",
        text: message,
        cancelButtonText: AlertText.cancel,
        confirmButtonText: AlertText.ok,
        showCancelButton: true,
        cancelButtonColor: Colors.danger,
        confirmButtonColor: Colors.success,
    })
};

const showSuccessMessage = (message) => {
   return Swal.fire({
        showConfirmButton: false,
        title: AlertText.successtitle,
        text: message,
        icon: "success",
        timer: 2000
    });
};

const showErrorMessage = (message) => {

    return Swal.fire({
        confirmButtonText: AlertText.ok,
        text: message,
        title: AlertText.dangertitle,
        icon: "error",
    });
}