<?php
session_start();

// Kết nối đến cơ sở dữ liệu
include "db_conn.php";

// Kiểm tra kết nối
if ($conn->connect_error) {
    die("Kết nối thất bại: " . $conn->connect_error);
}

// Lấy dữ liệu từ form
$tieude = $_POST['tieude'];
$avatar = $_POST['avatar'];
$noidung = $_POST['noidung'];
$anh = $_POST['anh'];

// Kiểm tra dữ liệu không được để trống
if (empty($tieude) || empty($noidung)) {
    $_SESSION['thong_bao'] = array('error', "Tiêu đề và nội dung không được để trống");
    header("Location: ../tanthu.php");
    exit();
}

if (empty($avatar)) {
    $avatar = 'avatar6.png';
}


// Thêm dữ liệu vào bảng
$sql = "INSERT INTO diendan (tieude, noidung, userid, created_at, diendancha, avatar, anh, admin) VALUES ('$tieude', '$noidung', '{$_SESSION['id']}', now(), 0, '$avatar', '$anh', 1)";

if ($conn->query($sql) === TRUE) {
    $_SESSION['thong_bao'] = array('success', "Thêm dữ liệu thành công");
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . $sql . "<br>" . $conn->error);
}

$conn->close();

header("Location: ../tanthu.php");
exit();
?>
