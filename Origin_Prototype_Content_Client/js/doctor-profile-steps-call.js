$(document).ready(function () {

	  
    // steps
    $('#wizard').steps({
		showBackButton: false,
       
        onFinish: function () {
          $('#wizard .to-be-hidden').hide();
          $('#wizard .to-be-shown').show();
          $('#wizard .step-footer').hide();
          $('#wizard .step-steps li:last-child ').removeClass('active').addClass('done');
          $('#wizard .step-steps li ').unbind('click');
        }   
      }); 
        
     
      
      $('#wizard .close-btn-cir').click(function(e){
        e.preventDefault();
        $(this).parents("li").css( "visibility", "hidden" );
        });

 // step1 text for next buton
      $('#wizard .nxt-stp ,#wizard .decline-form-btn , .step-steps li ').click(function(){
        var str = 'next : Account  Information';
        if ($("#wizard #step1").hasClass("active")) {
		 $('#wizard .nxt-stp').html( str );
         $('#wizard .to-be-hidden').hide();
        }
        });

 // step2 text for next buton
      $('#wizard .nxt-stp  , .step-steps li ').click(function(){
        var str = 'Accept';
        if ($("#wizard #step2").hasClass("active")) {
          $('#wizard .nxt-stp').html( str );
        }
        });
       
        
        
    });
    
    