<?php

session_start();
// kết nối tới database
$conn = mysqli_connect("localhost", "root", "", "vps_acc");

// lấy thông tin từ form
$ban = $_POST["ban"];
$online = $_POST["online"];
$is_login = $_POST["is_login"];
$username = $_POST["username"];
$id = $_SESSION['id'];
// kiểm tra xem ID sản phẩm có tồn tại trong database hay không
$sql = "SELECT * FROM user WHERE id = $id";

$result = mysqli_query($conn, $sql);

// cập nhật thông tin mới cho sản phẩm có ID tương ứng

$sql = "UPDATE user SET online = $online, is_login = $is_login ,ban = $ban ";
if (mysqli_query($conn, $sql)) {
    $_SESSION['message'] = "Cập nhật thành công.";
} else {
    $_SESSION['message'] = "Lỗi: " . mysqli_error($conn);
}
mysqli_close($conn);
?>
<script>
    // Lấy giá trị thông báo từ session và gán cho biến message
    var message = "<?php echo isset($_SESSION['message']) ? $_SESSION['message'] : ''; ?>";
    window.location.href = "unlock"
    // Kiểm tra nếu message không rỗng thì hiển thị alert
    if (message !== "") {
       
        alert(message);
    }
</script>