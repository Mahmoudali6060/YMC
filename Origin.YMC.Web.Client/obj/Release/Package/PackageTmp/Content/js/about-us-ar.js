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
	 
	 
	
});



