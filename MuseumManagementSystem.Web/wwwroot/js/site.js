

window.setTimeout(function () {
    $(".alert").fadeOut(500, function () {
        $(this).remove();
    }
    );
},3000);



$(".nav-link[data-bs-toggle='collapse']").click(function () {
    var $submenu = $($(this).data('bs-target'));
    $(".collapse.show").not($submenu).collapse("hide");
    $submenu.collapse("show");

});


$(document).ready(function () {
    $("#back-button").click(function () {
        window.history.go(-1);
    });

    $(".upload-area").click(function () {
        $('#upload-input').trigger('click');
    });

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

    $(document).on('click', '.dropdown', function (e) {
        e.stopPropagation()
        $(".dropdown-content").fadeOut(300);

        $(this).children(".dropdown-content").fadeToggle(200);
    });

    $(document).click(function (event) {

        $(".dropdown-content").fadeOut(300);
        //const sidebar = $("#sidebar");
        //if (sidebar.hasClass('active') && !sidebar.has(event.target).length) {
        //    sidebar.removeClass('active');
        //}




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

        if (checked && checked.length > 0) {``
            btnText.innerText = `${checked.length} Selected`;
        } else {
            btnText.innerText = "Select Language";
        }
    });
})



//datatable



