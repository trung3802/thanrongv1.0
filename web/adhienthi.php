<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    
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
                            <input type="submit" name="submit" class="btn btn-primary" value="Tìm kiếm">
                        </form>';
    
                        if (isset($_POST['username'])) {
                            $username = $_POST['username'];
                            
                            // Truy vấn dữ liệu từ bảng user cho username được nhập vào
                            $query = "SELECT `role`, `vnd`, `tongnap` FROM `user` WHERE `username` = '$username'";
                            $result = mysqli_query($conn, $query);
                            
                            // Kiểm tra và hiển thị dữ liệu
                            if (mysqli_num_rows($result) > 0) {
                                $row = mysqli_fetch_assoc($result);
                                $role = $row['role'];
                                $vnd = $row['vnd'];
                                $tongnap = $row['tongnap'];
                                
                                $content .= '
                                <div class="mt-4">
                                    <h5 class="card-title fw-semibold mb-4">Thông tin người dùng</h5>
                                    <table class="table">
                                        <tr>
                                            <th>Username</th>
                                            <th>Role</th>
                                            <th>VND</th>
                                            <th>Tongnap</th>
                                        </tr>
                                        <tr>
                                            <td>' . $username . '</td>
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
