
function checkValidationElementindiv(divId) {
    var isValid = true;
    var container = $("#" + divId);
    $.each(container.find('input, textarea, select'), function () {
        //$('form').validate().element($(this));
        if (!$(this).valid()) {
            isValid = false;
            return false;
        }
    });
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
        onChange: function (currentIndex, newIndex, stepDirection) {

            for (var i = 1; i < 6; i++) {
                var divName = "step" + i;
                if (checkValidationElementindiv(divName)) {
                    if (newIndex == i) {
                        patientCurrentIndex = newIndex;
                        return true;
                    }
                } else {
                    //  onFailure("Please enter your personal information. All fields are required.");
                    return false;
                }
            }

        },
        onFinish: function () {
            if (checkValidationElementindiv("form-patient-step")) {
                $('#wizard .to-be-hidden').hide();
                $('#wizard .to-be-shown').show();
                $('#wizard .step-footer').hide();
                $('#wizard .step-steps li:last-child ').removeClass('active').addClass('done');
                $('#wizard .step-steps li ').unbind('click');
            }
        }
    });




    // step1 text for next buton
    $('#wizard .nxt-stp ,#wizard .decline-form-btn , .step-steps li ').click(function () {

        // if (checkValidationElementindiv("step1")) {
        var str = window.Next;
        if ($("#wizard #step1").hasClass("active")) {
            $('#wizard .nxt-stp').html(str);
            $('#wizard .to-be-hidden').hide();
        }
        //  }


    });

    // step2 text for next buton
    $('#wizard .nxt-stp  , .step-steps li ').click(function () {
        // if (checkValidationElementindiv("step2")) {
        var str = window.Next;
        if ($("#wizard #step2").hasClass("active")) {
            $('#wizard .nxt-stp').html(str);
        }
        // }
    });


    // step3 text for next buton
    $('#wizard .nxt-stp  , .step-steps li ').click(function () {
        // if (checkValidationElementindiv("step3")) {
        var str = window.Next;
        if ($("#wizard #step3").hasClass("active")) {
            $('#wizard .nxt-stp').html(str);
            $('#wizard .decline-form-btn').hide();
        }
        // }
    });

    // step4 text for next buton
    $('#wizard .nxt-stp , .step-steps li ').click(function () {
        // if (checkValidationElementindiv("step4")) {
        var str = window.Next;
        if ($("#wizard #step4").hasClass("active")) {
            $('#wizard .nxt-stp').html(str);
            $('#wizard .decline-form-btn').hide();
        }
        // }
    });

    // step5 text for next buton
    $('#wizard .nxt-stp , .step-steps li ').click(function () {
        //  if (checkValidationElementindiv("step5")) {
        var str = window.FINISH;
        if ($("#wizard #step5").hasClass("active")) {
            $('#wizard .nxt-stp').html(str);
            $('#wizard .decline-form-btn').hide();
        }
        //  }
    });


});

