$(document).ready(function () {

 // highlight the selected item and show the content related
	$('#btn-chabters').click(function(e) {
			e.preventDefault()
			$(this).addClass("selected-link").parent().siblings().children('a').removeClass("selected-link");
			$(".chapters-section").fadeIn();
			$(".organizers-section").fadeIn();
			$(".discussion-section").hide();
			
	});
	
	$('#btn-discussion').click(function(e) {
			e.preventDefault()
			$(this).addClass("selected-link").parent().siblings().children('a').removeClass("selected-link");
			$(".discussion-section").fadeIn();
			$(".chapters-section").hide();
			$(".organizers-section").hide();
	});
	
	$('.report-btn').click(function(e) {
			e.preventDefault()
			$(this).parent().siblings("div").slideToggle();
			$(this).parents(".discussion-content").toggleClass("active-content")
			 
	});




});





