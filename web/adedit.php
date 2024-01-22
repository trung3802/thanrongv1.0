<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    
    // Xử lý khi form được submit
    if (isset($_POST['submit'])) {
        $username = $_POST['username'];
        $role = $_POST['role'];
        $vnd = $_POST['vnd'];
        $tongnap = $_POST['tongnap'];
        $lock = $_POST['lock'];
        
        // Cập nhật giá trị mới vào cơ sở dữ liệu
        $query = "UPDATE `user` SET `role` = '$role', `vnd` = '$vnd', `tongnap` = '$tongnap', `lock` = '$lock' WHERE `username` = '$username'";
        $result = mysqli_query($conn, $query);
        
        // Kiểm tra và hiển thị kết quả
        if ($result) {
            $message = "Thông tin đã được cập nhật thành công.";
        } else {
            $message = "Có lỗi xảy ra khi cập nhật thông tin: " . mysqli_error($conn);
        }
    }
    
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Chỉnh sửa thông tin người dùng</h5>
                <div class="card">
                    <div class="card-body">
                        <form method="POST">
                            <div class="mb-3">
                                <label for="username" class="form-label">Username:</label>
                                <input type="text" class="form-control" id="username" name="username" placeholder="Nhập username">
                            </div>
                            <div class="mb-3">
                                <label for="role" class="form-label">Role:</label>
                                <input type="text" class="form-control" id="role" name="role" placeholder="Nhập role">
                            </div>
                            <div class="mb-3">
                                <label for="vnd" class="form-label">VND:</label>
                                <input type="text" class="form-control" id="vnd" name="vnd" placeholder="Nhập VND">
                            </div>
                            <div class="mb-3">
                                <label for="tongnap" class="form-label">Tongnap:</label>
                                <input type="text" class="form-control" id="tongnap" name="tongnap" placeholder="Nhập tongnap">
                            </div>
                            <div class="mb-3">
                                <label for="lock" class="form-label">Lock:</label>
                                <input type="text" class="form-control" id="lock" name="lock" placeholder="Nhập lock">
                            </div>
                            <input type="submit" name="submit" class="btn btn-primary" value="Cập nhật">
                            <a href="index.php" class="btn btn-primary">Quay lại</a>
                        </form>';
    
                    if (isset($_POST['username'])) {
                        $username = $_POST['username'];
                        
                        // Truy vấn dữ liệu từ bảng user cho username được nhập vào
                        $query = "SELECT `role`, `vnd`, `tongnap`, `lock` FROM `user` WHERE `username` = '$username'";
                        $result = mysqli_query($conn, $query);
                        
                        // Kiểm tra và hiển thị dữ liệu
                        if (mysqli_num_rows($result) > 0) {
                            $row = mysqli_fetch_assoc($result);
                            $role = $row['role'];
                            $vnd = $row['vnd'];
                            $tongnap = $row['tongnap'];
                            $lock = $row['lock'];
                            
                            $content .= '
                            <div class="mt-4">
                                <h5 class="card-title fw-semibold mb-4">Thông tin người dùng</h5>
                                <table class="table">
                                    <tr>
                                    
                                        <th>Username</th>
                                        <th>Lock</th>
                                        <th>Role</th>
                                        <th>VND</th>
                                        <th>Tongnap</th>
                                        
                                    </tr>
                                    <tr>
                                    
                                        <td>' . $username . '</td>
                                        <td>' . $lock . '</td>
                                        <td>' . $role . '</td>
                                        <td>' . $vnd . '</td>
                                        <td>' . $tongnap . '</td>
                                        
                                    </tr>
                                </table>
                            </div>';
                        } else {
                            $content .= '<p class="text-danger">Không tìm thấy người dùng có username: ' . $username . '</p>';
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
