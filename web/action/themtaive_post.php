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

// Lấy dữ liệu từ form
$phienban = $_POST['phienban'];
$link = $_POST['link'];
$kieu = $_POST['kieu'];
$trangthai = $_POST['trangthai'];
// Kiểm tra dữ liệu không được để trống
if (empty($phienban) || empty($link)) {
    $_SESSION['thong_bao'] = array('error', "Phiên bản và link không được để trống");
    header("Location: ../adtaive.php");
    exit();
}
// Thêm dữ liệu vào bảng
$sql = "INSERT INTO taive (phienban, link, kieu, trangthai) VALUES ('$phienban', '$link', '$kieu', '$trangthai')";

if ($conn->query($sql) === TRUE) {
    $_SESSION['thong_bao'] = array('success', "Thêm dữ liệu thành công");
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . $sql . "<br>" . $conn->error);
}

$conn->close();

header("Location: ../adtaive.php");
exit();
?>
