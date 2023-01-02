// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
	$(".btn").click(function () {
		$(".form-signin").toggleClass("form-signin-left");
		$(".form-signup").toggleClass("form-signup-left");
		$(".frame").toggleClass("frame-long");
		$(".signup-inactive").toggleClass("signup-active");
		$(".signin-active").toggleClass("signin-inactive");
		$(".forgot").toggleClass("forgot-left");
		$(this).removeClass("idle").addClass("active");
	});
});

$(function () {
	$(".btn-signup").click(function () {
		$(".nav").toggleClass("nav-up");
		$(".form-signup-left").toggleClass("form-signup-down");
		$(".success").toggleClass("success-left");
		$(".frame").toggleClass("frame-short");
	});
});

$(function () {
	$(".btn-signin").click(function () {
		$(".btn-animate").toggleClass("btn-animate-grow");
		$(".welcome").toggleClass("welcome-left");
		$(".cover-photo").toggleClass("cover-photo-down");
		$(".frame").toggleClass("frame-short");
		$(".profile-photo").toggleClass("profile-photo-down");
		$(".btn-goback").toggleClass("btn-goback-up");
		$(".forgot").toggleClass("forgot-fade");
	});
});



$(document).on('click', '.fa-thumbs-up', function () {
	
	let idPost = $(this).attr("id")


	$.ajax({
		type: "POST",
		url: `/posts/${idPost}/like`,
		success: function (response) {
			$(`#post-${idPost}`).text(response);
		},
		failure: function (response) {
			alert(response.responseText);
		},
		error: function (response) {
			alert(response.responseText);
		}
	});
});






$(function () {
	$(".fa-user-tie").click(function () {
		$.ajax({
			type: "GET",
			url: "/blogs/admins",
			success: function (response) {
				document.getElementById("select-admins").innerHTML = "";
				response.forEach((admin) => {
					//var option = `<option value="${admin.id}">${admin.userName}</option>`;
					let element = document.createElement("option");
					element.setAttribute("value", admin.id)
					element.innerHTML = admin.userName;
					document.getElementById("select-admins").appendChild(element);
				});
			},
			failure: function (response) {
				alert(response.responseText);
			},
			error: function (response) {
				alert(response.responseText);
			}
		});
	});

	$("#assigner-btn").click(function () {
		let idBlog = localStorage.getItem('id_blog');
		let idAdmin = $('#select-admins option:selected').attr("value");
		let url = `/blogs/${idBlog}/admins/${idAdmin}/assignate`;
		
		$.ajax({
			type: "POST",
			url: url,
			success: function (response) {
				//document.location.reload();
				//console.log(response);
				console.log(response);
			},
			failure: function (response) {
				alert(response.responseText);
			},
			error: function (response) {
				alert(response.responseText);
			}
		});
	});

	$(".fa-pen-to-square").click(function () {
		let idBlog = localStorage.getItem('blog_id');

		$.ajax({
			type: "GET",
			url: `/blogs/details/${idBlog}`,
			success: function (response) {
				$("#nom_modifier").val(response.name);
				$("#prive_modifier").prop('checked', response.prive);
				$("#form_modifier").attr("action", `/blogs/edit/${response.id}`);
			},
			failure: function (response) {
				alert(response.responseText);
			},
			error: function (response) {
				alert(response.responseText);
			}
		});
	});



	
});




$(document).ready(function () {

	$.ajax({
		type: "GET",
		url: `/user/role`,
		success: function (response) {
			/*if (response[0].roleId == "id_admin_blog") {
				let blogs = [1]
				document.querySelectorAll(".interaction").forEach(e => {
					if (blogs.includes(e.id)) {
						e.remove();
					}  
					
				});
			} */
			$.ajax({
				type: "GET",
				url: `/admin/blogs`,
				success: function (responseLast) {
					console.log(responseLast);
				}
				
			});
			console.log(response);
		},
		failure: function (response) {
			alert(response.responseText);
		},
		error: function (response) {
			alert(response.responseText);
		}
	});



	//like
	

});


