<?php
session_start(); // Bắt đầu phiên làm việc

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "vps_acc";

// Kết nối đến cơ sở dữ liệu
$conn = new mysqli($servername, $username, $password, $dbname);

// Kiểm tra kết nối
if ($conn->connect_error) {
    die("Kết nối thất bại: " . $conn->connect_error);
}

if (isset($_SESSION['user_id'])) {
    $userId = $_SESSION['user_id'];

    // Truy vấn dữ liệu VND của người dùng dựa trên ID đăng nhập
    $sql = "SELECT `vnd` FROM `user` WHERE `id` = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("i", $userId);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        $userVnd = $row["vnd"];
    } else {
        $userVnd = 0;
    }

    $stmt->close();
} else {
    $userVnd = 0;
}

// Đóng kết nối
$conn->close();
?>
