$(function () {

    // Blog gallery file uploader
    $('input[type=file]').change(function (e) {
        $('#customfileupload').html($(this).val());
    });

    $(document).on('click', 'a.blog-gallery-action', function (e) {
        if (confirm("Do you want to delete this image?")) {
            window.location.href = $(this).attr('href');
        }
        e.preventDefault();
    });

    $(document).on('click', '.blog-gallerythumb img', function () {
        var elem = $(this);
        var blogbody = parent.top.document.getElementById("blogbody");
        if (blogbody != null) {
            blogbody.innerHTML = blogbody.innerHTML + elem.clone().wrap('<div></div>').parent().html();
            parent.top.sanitize_it();
        }
    });
});

$(window).load(function () {
    $('#pleasewait').fadeOut();
});