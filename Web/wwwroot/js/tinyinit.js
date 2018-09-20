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
    menubar: false,
    plugins: [
        'advlist autolink link image imagetools lists charmap hr anchor pagebreak spellchecker',
        'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
        'save table contextmenu directionality emoticons template paste textcolor'
    ],
    setup: function (editor) {
        editor.on('keyup', function (e) {
            SetCharacterCount(editor, ".post-detail .character-count");
        });
    },    
    statusbar: false,
    toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | forecolor backcolor emoticons'
});

function SetCharacterCount(editor, target) {
    var text = editor.getContent({format: 'text'});
    var count = text.length;
    var maxCount = "300";
    $(target).html(count + "/" + maxCount);
}