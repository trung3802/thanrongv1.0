<?php
session_start();

// Lấy dữ liệu từ form
$mathe = $_POST['mathe'];
$seri = $_POST['seri'];
$id = $_POST['id'];

// Kiểm tra dữ liệu không được để trống
if (empty($mathe) || empty($seri)) {
    $_SESSION['thong_bao'] = array('error', "Mã thẻ và số seri không được để trống");
    header("Location: ../duyetthe.php");
    exit();
}

// Kết nối database
$conn = mysqli_connect("localhost", "root", "", "vps_acc");

if (!$conn) {
    die("Kết nối database thất bại: " . mysqli_connect_error());
}

$demslgiftcode_query = "SELECT COUNT(*) FROM doithe WHERE id = '$id'";
$result = mysqli_query($conn, $demslgiftcode_query);

if ($result) {
    $row = mysqli_fetch_array($result);
    $count = $row[0]; 
    if ($count == 0) {
        $_SESSION['thong_bao'] = array('error', "Không tồn tại giao dịch này");
    } else {
        // Sửa trạng thái giftcode
        $updategiftcodett_query = "UPDATE doithe SET mathe = '$mathe', seri = '$seri', trangthai = 1, thoigiantraothe = NOW() WHERE id = '$id'";
        if (mysqli_query($conn, $updategiftcodett_query)) {
            $_SESSION['thong_bao'] = array('success', "Trao thẻ thành công");
        } else {
            $_SESSION['thong_bao'] = array('error', "Lỗi: " . $updategiftcodett_query . "<br>" . mysqli_error($conn));
        }
    }
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . mysqli_error($conn));
}

mysqli_close($conn);
header("Location: ../duyetthe.php");
?>
