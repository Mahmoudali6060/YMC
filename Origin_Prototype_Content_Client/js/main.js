$(document).ready(function () {

	  
// loader  
    $('#loader').fadeOut('slow');


	
// top links icon on mobile only
$('.offers-toggle').click(function(){
	$(this).toggleClass('clicked');
		$('.top_header').toggleClass('open');
		$('.middle_header').toggleClass('margin-open');
		
		return false;
	});

	


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


// WOW 
 new WOW().init();


	
});

