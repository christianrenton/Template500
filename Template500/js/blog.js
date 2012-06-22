var blogintro, blogbody, introtext, bodytext;

$(function () {

    $(document).on('mouseleave', '#blogintro', function () {
        if (introtext != null && blogintro != null) introtext.value = blogintro.innerHTML;
    });
    $(document).on('mouseleave', '#blogbody', function () {
        if (bodytext != null && blogbody != null) bodytext.value = blogbody.innerHTML;
    });
    $(document).on('submit', 'form', function () {
        if (introtext != null && blogintro != null) introtext.value = blogintro.innerHTML;
    });
    $(document).on('submit', 'form', function () {
        if (bodytext != null && blogbody != null) bodytext.value = blogbody.innerHTML;
    });

    $(document).on('click', '.delete-blog a', function (e) {
        if (confirm("Are you sure you want to delete this blog entry?")) {
            window.location.href = $(this).attr('href');
        }
        e.preventDefault();
    });

    blogintro = document.getElementById('blogintro');
    blogbody = document.getElementById('blogbody');
    introtext = document.getElementById('Introduction');
    bodytext = document.getElementById('Body');

    if (introtext != null && bodytext != null && blogbody != null && blogintro != null) {
        if (self.initButtons) initButtons();
        blogintro.designMode = 'On'; 		// Moz, Op, Saf
        blogintro.contentEditable = true; // Op, IE, Saf
        blogbody.designMode = 'On'; 		// Moz, Op, Saf
        blogbody.contentEditable = true; // Op, IE, Saf

        var buttons = document.getElementById('wysiwyg-controls-intro').getElementsByTagName('input');
        for (var i = 0; i < buttons.length; i++) {
            if (buttons[i].type != 'button') continue;
            buttons[i].onclick = command;
        }

        var buttons = document.getElementById('wysiwyg-controls-body').getElementsByTagName('input');
        for (var i = 0; i < buttons.length; i++) {
            if (buttons[i].type != 'button') continue;
            buttons[i].onclick = command;
        }

        $(document).on('keyup', '#blogintro', function () {
            if (introtext != null && blogintro != null) introtext.value = blogintro.innerHTML;
        });
        $(document).on('keyup', '#blogbody', function () {
            if (bodytext != null && blogbody != null) bodytext.value = blogbody.innerHTML;
        });

        blogintro.onpaste = function () {
            // The paste event is fired before the actual content is inserted
            // Therefore we give the browser some microseconds to insert the new HTML code
            window.setTimeout(sanitize_it, 5);
        };
        blogbody.onpaste = function () {
            // The paste event is fired before the actual content is inserted
            // Therefore we give the browser some microseconds to insert the new HTML code
            window.setTimeout(sanitize_it, 5);
        };

        blogintro.innerHTML = introtext.value;
        blogbody.innerHTML = bodytext.value;
        if (blogbody.innerHTML.length == 0) {
            var randomnumber = Math.floor(Math.random() * 5);
            if (randomnumber == 0) blogbody.innerHTML = "Today I saw what can only be described as . . . .";
            if (randomnumber == 1) blogbody.innerHTML = "Speaking to my friend the other day . . . .";
            if (randomnumber == 2) blogbody.innerHTML = "Whilst reading my book, it dawned on me that . . . .";
            if (randomnumber == 3) blogbody.innerHTML = "I was watching the match last night and thought. . . .";
            if (randomnumber == 4) blogbody.innerHTML = "After listening to their entire album. . . .";
        }

        sanitize_it();
    }
});

function command() {
    var cmd = this.getAttribute('command');
    var bool = false;
    var value = this.getAttribute('cmdValue') || null;
    if (value == 'promptUser') value = prompt(this.getAttribute('promptText'));
    inBetween(cmd, bool, value);
    if (introtext != null && blogintro != null) introtext.value = blogintro.innerHTML;
    if (bodytext != null && blogbody != null) bodytext.value = blogbody.innerHTML;
}

function inBetween(command, bool, value) {
    var returnValue = document.execCommand(command, bool, value);
}

function sanitize_it() {
    // Create new Sanitize object
    var s = new Sanitize(Sanitize.Config.BASIC);
    var cleaned_html_intro = s.clean_node(blogintro);
    var cleaned_html_body = s.clean_node(blogbody);
    // Show source of sanitized HTML in textarea
    blogintro.innerHTML = "";
    blogintro.appendChild(cleaned_html_intro);
    blogbody.innerHTML = "";
    blogbody.appendChild(cleaned_html_body);

    if (introtext != null && blogintro != null) introtext.value = blogintro.innerHTML;
    if (bodytext != null && blogbody != null) bodytext.value = blogbody.innerHTML;
}