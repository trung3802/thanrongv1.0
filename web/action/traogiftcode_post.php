<?php
session_start();

// Lấy dữ liệu từ form
$gitcodetrao = $_POST['gitcodetrao'];
$id = $_POST['id'];

// Kết nối database
$conn = mysqli_connect("localhost", "root", "", "vps_acc");

if (!$conn) {
    die("Kết nối database thất bại: " . mysqli_connect_error());
}

$demslgiftcode_query = "SELECT COUNT(*) FROM giftcodett WHERE trangthai = 0";
$result = mysqli_query($conn, $demslgiftcode_query);

if ($result) {
    $row = mysqli_fetch_array($result);
    $count = $row[0]; // Lấy số lượng giftcode
    if ($count == 0) {
        $_SESSION['thong_bao'] = array('error', "Không còn giftcode, vui lòng nhập thêm giftcode");
    } else {
        // Sửa trạng thái giftcode
        $updategiftcodett_query = "UPDATE giftcodett SET trangthai = 1 WHERE code = '$gitcodetrao'";
        if (mysqli_query($conn, $updategiftcodett_query)) {
            // Sửa vào bảng user
            $update_query = "UPDATE user SET trangthai = 1, gitcodetrao = '$gitcodetrao' WHERE id = '$id'";
            if (mysqli_query($conn, $update_query)) {
                $_SESSION['thong_bao'] = array('success', "Thêm dữ liệu thành công");
            } else {
                $_SESSION['thong_bao'] = array('error', "Lỗi: " . $update_query . "<br>" . mysqli_error($conn));
            }
        } else {
            $_SESSION['thong_bao'] = array('error', "Lỗi: " . $updategiftcodett_query . "<br>" . mysqli_error($conn));
        }
    }
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . mysqli_error($conn));
}

mysqli_close($conn);
header("Location: ../traocode.php");
?>
