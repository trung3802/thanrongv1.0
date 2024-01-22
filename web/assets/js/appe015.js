const url = window.location.pathname;
const clipboard = new ClipboardJS('.copy-text');
const swal = (...props) => Swal.fire(...props);
clipboard.on('success', function (e) {
    console.log(e);
});

clipboard.on('error', function (e) {
    console.log(e);
});

$("#navigation").children("a").each(function () {
    var link = $(this).attr("href");
    if (link.indexOf(url) > -1) {
        $(this).addClass("active");
        return;
    }
});
$('.auth').ajaxForm({
    url: '../ajax.php',
    method: 'POST',
    dataType: 'json',
    success: (res) => {
        !res.success ? $('#message').html(`<div class="alert alert-danger">${res.error}</div>`) && grecaptcha.reset() : $('#message').html(`<div class="alert alert-success">${res.success}</div>`) && setTimeout(() => window.location.reload(), 1500);
    }
})
$('.ajaxForm').ajaxForm({
    url: '../ajax.php',
    method: 'POST',
    dataType: 'json',
    success: (res) => {
        if (res.success) {
            $('#message').length ? $('#message').html(`<div class="alert alert-success">${res.success}</div>`) : swal('Thông báo', res.success, 'success');
            setTimeout(() => window.location.reload(), 1500);
        } else {
            $('#message').length ? $('#message').html(`<div class="alert alert-danger">${res.error}</div>`) : swal('Thông báo', res.error, 'error');
        }
    }
})