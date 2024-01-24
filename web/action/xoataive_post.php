<?php
session_start();

// Kết nối đến cơ sở dữ liệu
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "vps_acc";

$conn = new mysqli($servername, $username, $password, $dbname);

// Kiểm tra kết nối
if ($conn->connect_error) {
    die("Kết nối thất bại: " . $conn->connect_error);
}

// Kiểm tra xem có ID được gửi từ URL không
if (isset($_GET['id'])) {
    $id = $_GET['id'];

    // Thực hiện truy vấn xóa dữ liệu theo ID
    $sql = "DELETE FROM taive WHERE id = '$id'";

    if ($conn->query($sql) === TRUE) {
        $_SESSION['thong_bao'] = array('success', "Xoá dữ liệu thành công");
    } else {
        $_SESSION['thong_bao'] = array('error', "Lỗi khi xoá dữ liệu: " . $conn->error);
    }

} else {
    $_SESSION['thong_bao'] = array('error', "ID không được xác định");
}

$conn->close();

header("Location: ../adtaive.php");
exit();
?>
