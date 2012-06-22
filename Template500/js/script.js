/* Author: Gregory Pratt, Web Developer */

$(function () {
    $(document).on('click', '#closeModal', function (e) {
        CloseModal();
    });

    $(document).on('click', '#modalbg', function (e) {
        CloseModal();
    });

    // Enhance tables
    $('table').dataTable();

});

function ShowModal(elem) {
    $('#modalbg').fadeIn(function () {
        $(elem).fadeIn();
    });
}

function CloseModal() {
    $('.modal').fadeOut(function () {
        $('#modalbg').fadeOut();
    });
}

function ResetValidation() {
    // Add all forms back into the validation plugin.
    // Useful for dynamically added forms.
    $.validator.unobtrusive.parse('form');
    $('form').validate();
    // Remove validation messages.
    $('.validation-summary-errors').attr('class', 'validation-summary-valid');
    $('.input-validation-error').removeClass('input-validation-error');
}

//function ApplyCasesTableSort() {
//    $('#case-table').dataTable({
//        "iDisplayLength": 5,
//        "aoColumnDefs": [{
//            "bSortable": false, "aTargets": [0, 5],
//            "bSearchable": false, "aTargets": [0]
//        }],
//        "oLanguage": {
//            "sInfo": "Total of <em>_TOTAL_</em> cases",
//            "sInfoEmpty": "<em>0</em> cases"
//        }
//    });
//}

var support = "Sorry that didn't work, please try again later.\nIf you continue to experience problems contact support.";


/* Touch events
-------------------------------------------------------*/
document.addEventListener("touchstart", function () { }, true);

/* Extensions
-------------------------------------------------------*/

(function ($) {
    $.fn.outerHTML = function () {
        return $(this).clone().wrap('<div></div>').parent().html();
    }
})(jQuery);