$(document).ready(function () {

// slick plugin

	//students slider
	$('.slider-for').slick({
	  rtl: true,
	  slidesToShow: 1,
	  slidesToScroll: 1,
	  arrows: false,
	  fade: true,
	  asNavFor: '.slider-nav'
	});
	$('.slider-nav').slick({
		rtl: true,
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
	 rtl: true,
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
		rtl: true,
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
			duration: 10000,
			easing: 'swing',
			step: function (now) {
				$(this).text(Math.ceil(now));
			}
		});
	});
	



	
  
     
	
});



