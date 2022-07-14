$(document).ready(function () {

// slick plugin

	//students slider
	$('.slider-for').slick({
	  slidesToShow: 1,
	  slidesToScroll: 1,
	  arrows: false,
	  fade: true,
	  asNavFor: '.slider-nav'
	});
	$('.slider-nav').slick({
	  slidesToShow: 5,
	  slidesToScroll: 1,
	  asNavFor: '.slider-for',
	  dots: false,
	  centerMode: false,
	  focusOnSelect: true,
	  	  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
    ]
	});
	 
	 
	 //groups-slider
	$('.groups-slider').slick({
	  slidesToShow: 3,
	  slidesToScroll: 1,
	  autoplay: false,
	  autoplaySpeed: 2000,
	  responsive: [
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 1
      }
    }
    ]

	});
	
	 //events-slider
	$('.events-slider').slick({
	  slidesToShow: 2,
	  slidesToScroll: 1,
	  autoplay: false,
	  centerMode: true,
	  dots: true,
	  autoplaySpeed: 2000,
	  responsive: [
     {
      breakpoint: 1200,
      settings: {
        slidesToShow: 2,
		centerMode: false
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 1,
		centerMode: false
      }
    }
    ]

	});
			 

// animate numbers
	$('.count').each(function () {
		$(this).prop('Counter',0).animate({
			Counter: $(this).text()
		}, {
			duration: 12000,
			easing: 'swing',
			step: function (now) {
				$(this).text(Math.ceil(now));
			}
		});
	});


// close current modal
	$( ".close-1" ).click(function( event ) {
       event.preventDefault();
	   	$('#SignInPatientModal').modal('hide')

	});
	
	$( ".close-2" ).click(function( event ) {
       event.preventDefault();
	   	$('#SignInDocModal').modal('hide')

	});
	
// because this is inside modal content, so no need to hide any thing
	$( ".close-3 , .close-4" ).click(function( event ) {
       event.preventDefault();
	});
     
	 
	  $('#SignUpModal , #ForgotPasswordModal').on('shown.bs.modal', function () {
	  $(".actions-list-reg").addClass("static")
	})
	  $('#SignUpModal , #ForgotPasswordModal').on('hidden.bs.modal', function () {
	  $(".actions-list-reg").removeClass("static")
	})
	

// send-step

 $(".send-step" ).click(function( event ) {
       event.preventDefault();
	   	$('#first-step-forget').hide();
		$('#second-step-forget').show();
		$('li.first-step').addClass("steped-1")
		$('li.second-step').addClass("steped-2")
		
	});
	
	$(".save-step" ).click(function( event ) {
       event.preventDefault();
	   	$('#second-step-forget').hide();
		$('#third-step-forget').show();
		$('li.first-step').addClass("steped-3")
		$('li.second-step').addClass("steped-4")
		
	});
	
	$(".patient-btn" ).click(function( event ) {
       event.preventDefault();
	   $(this).addClass("active-sec")
	   $('.doctor-btn').removeClass("active-sec")
	   	$('#form-doctor').hide();
		$('#form-patient').show();
		
	});
	
	$(".doctor-btn" ).click(function( event ) {
       event.preventDefault();
	    $(this).addClass("active-sec")
	    $('.patient-btn').removeClass("active-sec")
	   	$('#form-patient').hide();
		$('#form-doctor').show();
		
	});
	
	
	
});



