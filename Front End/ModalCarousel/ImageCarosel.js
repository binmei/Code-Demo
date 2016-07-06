  $(document).ready(function() {
		//$('#carousel_ul li:first').show(); 
		$('#carousel_ul li:first').before($('#carousel_ul li:last')); 
		
        // going right        
        $('#right_scroll img').click(function(){
        
            var item_width = $('#carousel_ul li').outerWidth();
            
			
            var left_indent = parseInt($('#carousel_ul').css('left')) - item_width;
            
            $('#carousel_ul:not(:animated)').animate({'left' : left_indent},500,function(){    
                
                $('#carousel_ul li:last').after($('#carousel_ul li:first')); 
				$('#carousel_ul li:first').hide();
				$('#carousel_ul li:first').next().show();
                $('#carousel_ul').css({'left' : '0px'});
				
            }); 
        });
        
        // going left
        $('#left_scroll img').click(function(){
			
            var item_width = $('#carousel_ul li').outerWidth();
  
            var left_indent = parseInt($('#carousel_ul').css('left')) + item_width;
            
            $('#carousel_ul:not(:animated)').animate({'left' : left_indent},500,function(){    
           
				$('#carousel_ul li:first').before($('#carousel_ul li:last'));
				$('#carousel_ul li:last').hide();
				$('#carousel_ul li:first').next().show();
				$('#carousel_ul li:nth-child(3)').hide();
				$('#carousel_ul').css({'left' : '0px'});
            });
        });
		
/* 		        //going right        
        $('#right_scroll img').click(function(){
        
            var item_width = $('#carousel_ul li').outerWidth();
            
            var left_indent = parseInt($('#carousel_ul').css('left')) - item_width;
            
            $('#carousel_ul:not(:animated)').animate({'left' : left_indent},500,function(){    
                
                $('#carousel_ul li:last').after($('#carousel_ul li:first')); 

                $('#carousel_ul').css({'left' : '0px'});
            }); 
        });
        
        //going left
        $('#left_scroll img').click(function(){
            
            var item_width = $('#carousel_ul li').outerWidth();
  
            var left_indent = parseInt($('#carousel_ul').css('left')) + item_width;
            
            $('#carousel_ul:not(:animated)').animate({'left' : left_indent},500,function(){    
            
            
            $('#carousel_ul li:first').before($('#carousel_ul li:last')); 
           
            $('#carousel_ul').css({'left' : '0px'});
            });
            
            
        }); */

  });