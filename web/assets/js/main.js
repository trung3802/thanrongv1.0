var btnHtml;

function setBtnWait(b) {
    btnHtml = b.html();
    b.prop('disabled', true);
    b.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
}

function setBtnOk(b) {
    b.prop('disabled', false);
    b.html(btnHtml);
}