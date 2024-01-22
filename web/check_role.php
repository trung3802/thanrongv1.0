<?php
session_start();

// Kiểm tra xem người dùng đã đăng nhập hay chưa
if (!isset($_SESSION['id']) || !isset($_SESSION['username'])) {
    header("Location: login");
    exit();
}

// Kiểm tra vai trò của người dùng (1 là admin)
if ($_SESSION['role'] !== 1) {
    header("Location: unauthorized.php"); // Trang thông báo truy cập không được phép
    exit();
}
?>
