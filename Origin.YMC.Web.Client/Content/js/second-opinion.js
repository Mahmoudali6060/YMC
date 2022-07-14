$(document).ready(function(){
	
//calculate height of positioned absolute element and apply it as a margin for another element
    function calculatedHeight () {
	var margin = $('.bottom-art').innerHeight();
    $('.range-slider-status').css('margin-bottom',margin);
	}
	
	calculatedHeight ();
	
	$(window).resize(function() {
		calculatedHeight ();
	});
 $('.wizard .close-btn-cir').click(function(e){
        e.preventDefault();
        $(this).parents("li").css( "visibility", "hidden" );
        });
		
});

