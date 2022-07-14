$(document).ready(function () {


// loader  
    $('#loader').fadeOut('slow');

// close-survey
    $('.close-survey , .send-survey').click(function (e) {
        e.preventDefault();
        //$('.user-survey').hide();
    });

	
// top links icon on mobile only
$('.offers-toggle').click(function(){
	$(this).toggleClass('clicked');
		$('.top_header').toggleClass('open');
		$('.middle_header').toggleClass('margin-open');
		
		return false;
	});

	
// Custom Scrollbar
   // $("#form-doctor").mCustomScrollbar();
 //   $("#form-interpreter").mCustomScrollbar();
    
 // smooth-scrollbar
    var Scrollbar = window.Scrollbar;
    if (document.querySelector('#form-doctor')) {

    Scrollbar.init(document.querySelector('#form-doctor'));
    }
    if (document.querySelector('#form-interpreter')) {
        Scrollbar.init(document.querySelector('#form-interpreter'));
    }
    



// scroller
	$(window).scroll(function () {
        if ($(this).scrollTop() > 800) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });

    $('.scrollup').click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 600);
        return false;
    });

 // create account btn on click
    $(".btn-create-doctor-account").click(function (event) {
        event.preventDefault();
        $('.doctor-btn').click();
        $('#form-doctor').show();
        $('#form-patient').hide();

    });

    $(".btn-create-patient-account").click(function (event) {
        event.preventDefault();
        $('.patient-btn').click();
        $('#form-doctor').hide();
        $('#form-patient').show();

    });

// close current modal
    $(".close-1").click(function (event) {
        event.preventDefault();
        $('#SignInPatientModal').modal('hide');

    });

    $(".close-2").click(function (event) {
        event.preventDefault();
        $('#SignInDocModal').modal('hide');

    });

// because this is inside modal content, so no need to hide any thing
    $(".close-3 , .close-4").click(function (event) {
        event.preventDefault();
    });


    $('#ForgotPasswordModal, #SignUpModal, #TCWindow').on('shown.bs.modal', function () {
        $(".actions-list-reg").addClass("static");
    });
    $('#ForgotPasswordModal, #SignUpModal, #TCWindow').on('hidden.bs.modal', function () {
        $(".actions-list-reg").removeClass("static");
    });

// refrance list
    if ($('.refrance-list label').children('input').length > 0) {
        $('.refrance-list li:first-child label').addClass("label-has-child");
    }

    if ($('.refrance-list span').children('input').length > 0) {
        $('.refrance-list li:first-child span').addClass("span-has-child");
    }


// for just demo purpose Replace empty src with image defualt avatar in doctors list
    $('.doctors-list-section .doctors-item img[src="/Content/User_Files/748a96bc-1102-44da-a0fd-f24cec556564_150413-doctor-stock.jpg').attr('src', '/Content/img/avatar-default.jpg');
   // $('.doctors-list-section .doctors-item img').attr('src', '/Content/img/featuredoc3.jpg');

 // send-step
    $(".steped-1").click(function (event) {
        event.preventDefault();
        $('#first-step-forget').show();
        $('#second-step-forget').hide();

    });
    $(".send-step").click(function (event) {
        event.preventDefault();
        $('#first-step-forget').hide();
        $('#second-step-forget').show();
        $('li.first-step').addClass("steped-1");
        $('li.second-step').addClass("steped-2");

    });

    $(".save-step").click(function (event) {
        event.preventDefault();
        $('#second-step-forget').hide();
        $('#third-step-forget').show();
        $('li.first-step').addClass("steped-3");
        $('li.second-step').addClass("steped-4");

    });

    $(".patient-btn").click(function (event) {
        event.preventDefault();
        $(this).addClass("active-sec");
        $('.doctor-btn').removeClass("active-sec");
        $('#form-doctor').hide();
        $('#form-patient').show();

    });

    $(".doctor-btn").click(function (event) {
        event.preventDefault();
        $(this).addClass("active-sec");
        $('.patient-btn').removeClass("active-sec");
        $('#form-patient').hide();
        $('#form-doctor').show();

    });

// Custom Scrollbar
   // $("#form-doctor").mCustomScrollbar();

   // $(".footer-links a").click(function () {
    //    $("#form-doctor").mCustomScrollbar();
  //  });

// WOW 
 new WOW().init();


	
});

