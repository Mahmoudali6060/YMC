$(document).ready(function () {

	  
    // steps
    $('#wizard').steps({
       
        onFinish: function () {
          $('#wizard .to-be-hidden').hide();
          $('#wizard .to-be-shown').show();
          $('#wizard .step-footer').hide();
          $('#wizard .step-steps li:last-child ').removeClass('active').addClass('done');
          $('#wizard .step-steps li ').unbind('click');
        }   
      }); 
        
     
      

 // step1 text for next buton
      $('#wizard .nxt-stp ,#wizard .decline-form-btn , .step-steps li ').click(function(){
        var str = 'next : Terms & Conditions';
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
       
        
// step3 text for next buton
      $('#wizard .nxt-stp  , .step-steps li ').click(function(){
        var str = 'Next: Contact Information';
        if ($("#wizard #step3").hasClass("active")) {
          $('#wizard .nxt-stp').html( str );
          $('#wizard .decline-form-btn').hide();
        }
        });

// step4 text for next buton
      $('#wizard .nxt-stp , .step-steps li ').click(function(){
        var str = 'Next: Payment  Information';
        if ($("#wizard #step4").hasClass("active")) {
          $('#wizard .nxt-stp').html( str );
          $('#wizard .decline-form-btn').hide();
        }
        });

 // step5 text for next buton
      $('#wizard .nxt-stp , .step-steps li ').click(function(){
        var str = 'fINISH';
        if ($("#wizard #step5").hasClass("active")) {
          $('#wizard .nxt-stp').html( str );
          $('#wizard .decline-form-btn').hide();
        }
        });

        
    });
    
    