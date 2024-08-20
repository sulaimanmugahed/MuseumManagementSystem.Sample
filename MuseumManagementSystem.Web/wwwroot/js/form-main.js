// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
    $(".upload-area").click(function () {
        $('#upload-input').trigger('click');
    });

    $('#upload-input').change(event => {
        if (event.target.files) {
            let filesAmount = event.target.files.length;
            $('.upload-img').html("");

            for (let i = 0; i < filesAmount; i++) {
                let reader = new FileReader();
                reader.onload = function (event) {
                    let html = `
                        <div class = "uploaded-img">
                            <img src = "${event.target.result}">
                            <button type = "button" class = "remove-btn">
                                <i class = "mdi mdi-close"></i>
                            </button>
                        </div>
                    `;
                    $(".upload-img").append(html);
                }
                reader.readAsDataURL(event.target.files[i]);
            }

            $('.upload-info-value').text(filesAmount);
            $('.upload-img').css('padding', "20px");
        }
    });

    $(window).click(function (event) {
        if ($(event.target).hasClass('remove-btn')) {
            $(event.target).parent().remove();
        } else if ($(event.target).parent().hasClass('remove-btn')) {
            $(event.target).parent().parent().remove();
        }
    })
});



//form
const selectBtn = document.querySelector(".select-btn"),
    items = document.querySelectorAll(".item");

selectBtn.addEventListener("click", () => {
    selectBtn.classList.toggle("open");
});

items.forEach(item => {
    item.addEventListener("click", () => {
        item.classList.toggle("checked");

        let checked = document.querySelectorAll(".checked"),
            btnText = document.querySelector(".btn-text");

        if (checked && checked.length > 0) {
            btnText.innerText = `${checked.length} Selected`;
        } else {
            btnText.innerText = "Select Language";
        }
    });
})
