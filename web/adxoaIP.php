<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {

    $content =
        '  
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Xóa người dùng theo địa chỉ IP</h5>
                <div class="card">
                    <div class="card-body">
                        <form action="" method="post" class="mb-4">
                            <div class="mb-3">
                                <label for="ip" class="form-label">Nhập địa chỉ IP:</label>
                                <input type="text" class="form-control" id="ip" name="ip" placeholder="Ví dụ: 168.9.0.0">
                            </div>
                            <button type="submit" class="btn btn-danger">Xóa</button>
                        </form>';

                        if ($_SERVER["REQUEST_METHOD"] == "POST") {
                            $ip = $_POST["ip"];

                            if (empty($ip)) {
                                $content .= '<div class="alert alert-danger">Vui lòng nhập địa chỉ IP trước khi xóa.</div>';
                            } else {
                                // Sử dụng prepared statement để xóa người dùng
                                $stmt = $conn->prepare("DELETE FROM `user` WHERE `last_ip` = ?");
                                $stmt->bind_param("s", $ip);

                                if ($stmt->execute()) {
                                    $content .= '<div class="alert alert-success">Đã xóa các người dùng có địa chỉ IP là ' . $ip . '</div>';
                                } else {
                                    $content .= '<div class="alert alert-danger">Xảy ra lỗi khi xóa dữ liệu</div>';
                                }

                                // Đóng prepared statement
                                $stmt->close();
                            }
                        }

                        $content .= '
                    </div>
                </div>
            </div>
        </div>
    </div>
    ';

    mysqli_close($conn);
    include "layout.php";
} else {
    header("Location: login.php");
    exit();
}
?>
