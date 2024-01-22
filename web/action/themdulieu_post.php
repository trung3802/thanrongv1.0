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
$tieude = $_POST['tieude'];
$noidung = $_POST['noidung'];
// Kiểm tra dữ liệu không được để trống
if (empty($tieude) || empty($noidung)) {
    $_SESSION['thong_bao'] = array('error', "Tiêu đề và nội dung không được để trống");
    header("Location: ../tanthu.php");
    exit();
}
// Thêm dữ liệu vào bảng
$sql = "INSERT INTO tanthu (tieude, noidung) VALUES ('$tieude', '$noidung')";

if ($conn->query($sql) === TRUE) {
    $_SESSION['thong_bao'] = array('success', "Thêm dữ liệu thành công");
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . $sql . "<br>" . $conn->error);
}

$conn->close();

header("Location: ../tanthu.php");
exit();
?>
