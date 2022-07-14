$(document).ready(function () {

    $(".discussion-messages , .discussion-more , .discussion-message-input").show();
 
 //  show the content related

	$('.report-btn').click(function(e) {
			e.preventDefault()
			$(this).parent().siblings("div").slideToggle();
			$(this).parents(".discussion-content").toggleClass("active-content")
			 
	});




});

(function($){
        $(window).on("load",function(){
            $(".replies-media").mCustomScrollbar();
        });
 })(jQuery);



