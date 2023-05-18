(function($) {

	"use strict";

	var fullHeight = function() {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$('#sidebarCollapse').on('click', function () {
      $('#sidebar').toggleClass('active');
	});
	

	var updatePage = function () {
		$('#sidebar ul li.active').click();
    }

	$('#sidebar ul li').on('click', function (event) {		
		$('#sidebar ul li.active').removeClass('active');
		$(this).addClass('active');

		event.preventDefault();
		let href = $(this).children('a').attr('href');
		
		if($(this).children('a.logout').length){
			// fetch(href, {
			// 	method: 'POST'
			// }).then(function(){
			// 	window.location.replace('/User/Login');
			// })
			// return;
			let form = document.createElement("form");
			form.method = "POST";
			form.action = href;

			document.body.appendChild(form);
			form.submit();
		}

		fetchWithUpdateContent('#content', href);
	});

	$('#createChildBtn').on('click', function () {		
		fetchWithUpdateContent('#content', '/Child/Create');
	});	

	updatePage();

})(jQuery);
