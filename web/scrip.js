<script>
function confirmTopUp() {
  var r = confirm("Bạn có muốn nạp tiền cho tài khoản này?");
  if (r == true) {
    // Nếu người dùng đồng ý, submit form nạp tiền
    document.getElementById("topUpForm").submit();
  }
}
</script>