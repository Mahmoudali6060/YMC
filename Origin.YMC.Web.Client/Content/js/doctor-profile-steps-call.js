
function checkValidationElementindiv(divId) {
    var isValid = true;
    var container = $("#" + divId);
    $.each(container.find('input, textarea, select'), function () {
        // $('form').validate().element($(this));
        if (!$(this).valid()) {
            isValid = false;
            return false;
        }
    });
    if ($("#imgs").find("img").length == 0) {
        isValid = false;
        $("#lblErrUploadCerts").show();
    } else {
        $("#lblErrUploadCerts").hide();
    }
    return isValid;

}

function onFailure(msg) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": 0,
        "extendedTimeOut": 0,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "tapToDismiss": false
    };
    toastr.error(msg);
}

$(document).ready(function () {
    // steps
    $('#wizard').steps({
        showBackButton: false,
        onChange: function (currentIndex, newIndex, stepDirection) {
            var divName = "step" + newIndex;
            if (checkValidationElementindiv(divName)) {
                return true;
            } else {
                // onFailure("Please enter your personal information. All fields are required.");
                return false;
            }
        },
        onFinish: function () {
            if (checkValidationElementindiv("form-doctor-step")) {
                $('#wizard .to-be-hidden').hide();
                $('#wizard .to-be-shown').show();
                $('#wizard .step-footer').hide();
                $('#wizard .step-steps li:last-child ').removeClass('active').addClass('done');
                $('#wizard .step-steps li ').unbind('click');
            }
        }
    });



    $('#wizard .close-btn-cir').click(function (e) {
        e.preventDefault();
        $(this).parents("li").css("visibility", "hidden");
    });

    // step1 text for next buton
    $('#wizard .nxt-stp ,#wizard .decline-form-btn , .step-steps li ').click(function () {

        if (checkValidationElementindiv("step1")) {
            var str = 'next : Account  Information';
            if ($("#wizard #step1").hasClass("active")) {
                $('#wizard .nxt-stp').html(str);
                $('#wizard .to-be-hidden').hide();
            }
        }

    });

    // step2 text for next buton
    $('#wizard .nxt-stp  , .step-steps li ').click(function () {
        if (checkValidationElementindiv("step2")) {
            var str = 'Accept';
            if ($("#wizard #step2").hasClass("active")) {
                $('#wizard .nxt-stp').html(str);
            }
        }
    });
});

