$(function(){
	var url = window.location.href;
	if (url.indexOf("Post/Detail") !== -1) {
		SetCommentAuthor();
	}
});

/* Add Comment */
$(document).on("click", ".comment-add", function(btn){
	var editor = tinymce.get("CommentEditor");
	var comment = $.trim(editor.getContent({format: 'text'}));
	var author = $(".comment-author").first().val();
	var msg = $(".comments .comment-msg");

	SetLocalStorageAuthor(author);

	if (comment.length < 1) {
		$(msg).addClass("text-danger");
		$(msg).text("Comment is required.");

		return false;
	}

	$.ajax({
		method: 'POST',
		url:'/Comment/Add' ,
		data: { 
			PostId: $(this).data('id'), 
			Author: author,
			Content: comment
		},
		dataType: 'json',
		beforeSend: function(xhr, settings){
			var msg = $(".comments .comment-msg");
			$(msg).text("");
			$(msg).removeClass("text-success");
			$(msg).removeClass("text-danger");

			var loader = $(".comments .loader");
			$(loader).removeClass("d-none");			
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


// Local Storage
var authorKey = "comment-author";
function SetLocalStorageAuthor(author){
	if (typeof(Storage) !== "undefined" && author !== "") {
		localStorage.setItem(authorKey, author);
	} 
}

function SetCommentAuthor(){
	if (typeof(Storage) !== "undefined") {
		var author = localStorage.getItem(authorKey);
		if (author !== undefined || author !== "") {
			$(".comment-author").val(author);
		}
	} 
}