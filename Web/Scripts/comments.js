$(function(){
	var url = window.location.href;
	if (url.indexOf("Post/Detail") !== -1) {
		SetCommentAuthor();
	}
});

/* Add Comment */
$(document).on("click", ".comment-add", function(btn){
	var editor = tinymce.get("CommentEditor");
	var author = $(".comment-author").first().val();
	var comment = $.trim(editor.getContent({format: 'text'}));
	var msg = $(".comments .comment-msg");
	var token = $(".comments #RequestVerificationToken").val();

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
			xhr.setRequestHeader("RequestVerificationToken", token);

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

function commentAddSuccess(data, stauts, xhr) {
	var msg = $(".comments .comment-msg");

	if (data.result.succeeded) {
		$(msg).addClass("text-success");

		var editor = tinymce.get("CommentEditor");
		editor.setContent("");
		SetCharacterCount(editor, ".post-detail .character-count");

		var author = $(".comment-author").first().val();
		var comment = $.trim(editor.getContent({format: 'text'}));

		CreateCommentBlock(author, data.date, comment);
	}
	else {
		$(msg).addClass("text-danger");
	}

	$(msg).html(data.result.message);
	$(".comments #RequestVerificationToken").val(data.token);
}

function commentAddError(xhr, status, error) {
	var msg = $(".comments .comment-msg");
	$(msg).addClass("text-danger");
	$(msg).text("Unable to add comment at this time.");
}

function CreateCommentBlock(authorName, dateString, contentText){
	// Create new DOM elements for comment
	var comment = $("<div></div>", { class: "comment" });
	var author = $("<h4></h4>").text(authorName);
	var date = $("<p></p>").text(dateString);
	var content = $("<span></span>").text(contentText);
	
	// Order elements
	$(comment).append(author);
	$(comment).append(date);
	$(comment).append(content);
	
	// Add new comment block
	var commentsList = $(".comments-list");
	if ($(commentsList).children(".first-to-comment").length != 0) {
		$(commentsList).empty();
	}
	$(commentsList).append(comment);
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