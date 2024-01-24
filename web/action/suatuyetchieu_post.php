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

// Lấy dữ liệu từ form sửa
$id = $_POST['id'];
$skills = $_POST['skills'];
$boughtskill = $_POST['boughtskill'];

// Kiểm tra dữ liệu không được để trống
if (empty($skills) || empty($boughtskill)) {
    $_SESSION['thong_bao'] = array('error', "skill và bought skill không được để trống");
    header("Location: ../adtuyetchieu.php");
    exit();
}
// Thêm dữ liệu vào bảng
$sql = "UPDATE `character` SET skills = '$skills', boughtskill = '$boughtskill' WHERE id = '$id'";

if ($conn->query($sql) === TRUE) {
    $_SESSION['thong_bao'] = array('success', "Sửa dữ liệu thành công");
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . $sql . "<br>" . $conn->error);
}

$conn->close();

header("Location: ../adtuyetchieu.php");
exit();
?>
