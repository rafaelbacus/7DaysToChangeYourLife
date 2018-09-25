tinymce.init({
    selector: '.post-editor',
    browser_spellcheck: true,
    plugins: [
        'advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker',
        'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
        'save table contextmenu directionality emoticons template paste textcolor'
    ],
    statusbar: false,
    toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons'
});

tinymce.init({
    selector: '.comment-editor',
    browser_spellcheck: true,
    max_chars: 1024,
    menubar: false,
    plugins: [
        'advlist autolink link image imagetools lists charmap hr anchor pagebreak spellchecker',
        'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
        'save table directionality emoticons template paste textcolor'
    ],
    paste_preprocess: function (plugin, args) {
        var editor = tinymce.get("CommentEditor");
        var currentText = $.trim(editor.getContent({format: 'text'}));
        var currentLength = currentText.length;
        var text = args.content;
        var textLength = text.length;
    
        if (currentLength + textLength > editor.settings.max_chars) {
            var diffLength = editor.settings.max_chars - currentLength;
            var diffText = text.substring(0, diffLength);
            args.content = diffText;
        }
    },
    setup: function (editor) {
        var allowedKeys = [8, 37, 38, 39, 40, 46]; // backspace, delete and cursor keys

        editor.on('keydown', function(e){
            if (allowedKeys.indexOf(e.keyCode) != -1) { return true };
            if (editor.getContent({format: 'text'}).length >= editor.settings.max_chars){
                e.preventDefault();
                e.stopPropagation();

                return false;
              }
        });
        
        editor.on('keyup', function (e) {
            SetCharacterCount(editor, ".post-detail .character-count");
        });
    },    
    statusbar: false,
    toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | forecolor backcolor emoticons'
});

function SetCharacterCount(editor, target) {
    var text = $.trim(editor.getContent({format: 'text'}));
    var count = text.length;
    var maxChars = "1024";
    $(target).html(count + "/" + maxChars);
}