<?php
session_start();

// Kiểm tra xem người dùng đã đăng nhập chưa
if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $id = $_SESSION['id'];

    // Thay đổi thông tin kết nối cơ sở dữ liệu tại đây
    $servername = "localhost";
    $username = "root";
    $password = "";
    $dbname = "vps_acc";

    // Tạo kết nối
    $conn = new mysqli($servername, $username, $password, $dbname);

    // Kiểm tra kết nối
    if ($conn->connect_error) {
        die("Kết nối thất bại: " . $conn->connect_error);
    }

    // Truy vấn dữ liệu theo UID
    $sql = "SELECT telco, menhgia, seri, code, magd, status FROM napcard WHERE uid = $id";
    $result = $conn->query($sql);

    if ($result) {
        $data = array();

        // Lấy dữ liệu từ kết quả truy vấn
        while ($row = $result->fetch_assoc()) {
            $data[] = $row;
        }

        // Trả về dữ liệu dưới dạng JSON
        echo json_encode($data);
    } else {
        echo "Không có dữ liệu";
    }

    // Đóng kết nối
    $conn->close();
} else {
    echo "Vui lòng đăng nhập.";
}
?>
