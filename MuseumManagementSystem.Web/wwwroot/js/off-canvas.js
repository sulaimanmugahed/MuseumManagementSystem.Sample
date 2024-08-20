//(function($) {
//  'use strict';
//  $(function() {
//    $('[data-toggle="offcanvas"]').on("click", function() {
//      $('.sidebar-offcanvas').toggleClass('active')
//    });
//      $(document).click(function (event) {
//          if (!$(event.target).closest('.sidebar-offcanvas').length &&
//              !$(event.target).is('[data-toggle="offcanvas"]')) {
//              $('.sidebar-offcanvas').removeClass('active');
//          }
//      });
//  });




//})(jQuery);

(function ($) {
    'use strict';
    $(function () {
        // Use event delegation for better performance and flexibility
        $('body').on('click', '[data-toggle="offcanvas"]', function (event) {
            event.stopPropagation(); // Prevent event bubbling to document
            $('.sidebar-offcanvas').toggleClass('active');
        });

        // Close sidebar on click outside
        $(document).click(function (event) {
            if (!$(event.target).closest('.sidebar-offcanvas').length) {
                $('.sidebar-offcanvas').removeClass('active');
            }
        });
    });
})(jQuery);