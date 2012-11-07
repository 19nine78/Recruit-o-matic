//Setup based on the assumption we're identifying all editors with the .mdd_editor class

$(document).ready(function () {
    $("textarea.mdd_editor").MarkdownDeep({
           help_location: "/Scripts/mdd_help.htm",
           disableTabHandling: true
       });
});



