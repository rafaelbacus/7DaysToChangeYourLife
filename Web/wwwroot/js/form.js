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
		window.location.href = $(this).data("indexurl");
	}
});

// Removes cancellation prompt
$(document).on("click", ".btn-cancel-no", function(btn){
	$(".btn-grp-cancellation").addClass("d-none");
	$(".btn-grp-submission").removeClass("d-none");
});