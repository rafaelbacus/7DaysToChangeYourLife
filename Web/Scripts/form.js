/* Add or Edit Post */
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

/* Delete Post */
$(document).on("click", ".btn-delete-yes", function() {
	$.ajax({
		method: 'POST',
		url:'/Post/Delete' ,
		data: { Id: $(this).data('id') },
		dataType: 'json',
		beforeSend: function(xhr, settings){
			var msg = $(".post-delete .msg");
			$(msg).text("");
			$(msg).removeClass("text-success");
			$(msg).removeClass("text-danger");
		}
	})
	.then(postDeleteSuccess, postDeleteError);
});

function postDeleteSuccess(result, status, xhr){
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

function postDeleteError(xhr, status, error){
	var msg = $(".post-delete .msg");
	$(msg).addClass("text-danger");
	$(msg).text("Unable to delete post at this time.");
}

/* Add Comment */
$(document).on("click", ".comment-add", function(btn){
	var editor = tinymce.get("CommentEditor");
	var comment = $.trim(editor.getContent({format: 'text'}));

	$.ajax({
		method: 'POST',
		url:'/Comment/Add' ,
		data: { postId: $(this).data('id') },
		dataType: 'json',
		beforeSend: function(xhr, settings){
			var msg = $(".comments .comment-msg");
			$(msg).text("");
			$(msg).removeClass("text-success");
			$(msg).removeClass("text-danger");

			var loader = $(".comments .loader");
			$(loader).removeClass("d-none");

			if (comment.length < 1) {
				$(msg).addClass("text-danger");
				$(msg).text("Comment is required.");
				$(loader).addClass("d-none");

				return false;
			}
		}
	})
	.then(commentAddSuccess, commentAddError)
	.always(function(){
		$(".comments .loader").addClass("d-none");
	});
});

function commentAddSuccess(result, stauts, xhr) {
	var msg = $(".comments .comment-msg");

	if (result.succeeded) {
		$(msg).addClass("text-success");
	}
	else {
		$(msg).addClass("text-danger");
	}

	$(msg).html(result.message);
}

function commentAddError(xhr, status, error) {
	var msg = $(".comments .comment-msg");
	$(msg).addClass("text-danger");
	$(msg).text("Unable to add comment at this time.");
}