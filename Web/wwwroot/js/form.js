// Checks if inputs on form have entry
// Prompts admin if they want to continue cancellation
$(document).on("click", ".btn-cancel", function(btn){
	var hasEntry = false;
	var inputs = $("form").find("input[type!='hidden'], textarea");

	$.each(inputs, function(index, input){
		if($.trim($(input).val()).length !== 0) {
			hasEntry = true;

			return;
		}
	});

	if(hasEntry){
		$(".btn-grp-submission").addClass("d-none");
		$(".btn-grp-cancellation").removeClass("d-none");
	}
	else{
		window.location.href = $(this).result("indexurl");
	}
});

// Removes cancellation prompt
$(document).on("click", ".btn-cancel-no", function(btn){
	$(".btn-grp-cancellation").addClass("d-none");
	$(".btn-grp-submission").removeClass("d-none");
});

// Delete button click
$(document).on("click", ".btn-delete-yes", function() {
	$(".post-delete .msg").removeClass("text-success");
	$(".post-delete .msg").removeClass("text-danger");

	let promise = $.ajax({
		method: 'POST',
		url:'/Post/Delete' ,
		data: { Id: $(this).data('id') },
		dataType: 'json'
	});

	promise.then(deleteSuccess, deleteError);
});

function deleteSuccess(result, status, xhr){
	var msg = $(".post-delete .msg");

	if (result.succeeded) {
		$(msg).addClass("text-success");
		$(".post-delete .btn-delete-back").removeClass("d-none");
		$(".post-delete .btn-delete-cancel").addClass("d-none");
		$(".post-delete .btn-delete-yes").addClass("d-none");
	}
	else {
		$(msg).addClass("text-danger");
	}

	$(msg).html(result.message);
}

function deleteError(xhr, status, error){
	var msg = $(".post-delete .msg");
	$(".post-delete .msg").addClass("text-danger");
}