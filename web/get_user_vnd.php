<?php


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

$userVnd = 0; // Giá trị mặc định

if (isset($_SESSION['username'])) {
    $username = $_SESSION['username'];

    // Truy vấn dữ liệu VND của người dùng dựa trên tên người dùng
    $sql = "SELECT `vnd` FROM `user` WHERE `username` = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        $userVnd = $row["vnd"];
    }

    $stmt->close();
}

// Đóng kết nối
$conn->close();
?>
