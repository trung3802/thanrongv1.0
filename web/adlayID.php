<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    
    $content =
        '
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title fw-semibold mb-4">Lấy thông tin id</h5>
                    <div class="card">
                        <div class="card-body">
                            <form method="post">
                                <div class="mb-3">
                                    <label for="exampleInputEmail1" class="form-label">Nhập username</label>
                                    <input type="text" class="form-control" id="exampleInputEmail1" name="username" placeholder="Nhập username muốn lấy ID" aria-describedby="emailHelp">
                                </div>
                                <button type="submit" class="btn btn-primary">Thực thi</button>
                                <a href="adsuaID.php" class="btn btn-primary">Sửa</a>
                                <a href="index.php" class="btn btn-primary">Quay lại</a>';
                        
                                // Kiểm tra nếu có dữ liệu được gửi từ form
                                if (isset($_POST['username'])) {
                                    $username = $_POST['username'];
                        
                                    // Truy vấn dữ liệu từ bảng user cho username nhập vào
                                    $query = "SELECT `character`,`password`,`last_ip`, `vnd` FROM `user` WHERE `username` = '$username'";
                                    $result = mysqli_query($conn, $query);
                        
                                    // Kiểm tra và hiển thị dữ liệu
                                    if (mysqli_num_rows($result) > 0) {
                                        $row = mysqli_fetch_assoc($result);
                                        $character = $row['character'];
                                        $password = $row['password'];
                                        $vnd = $row['vnd'];
                                        $last_ip = $row['last_ip'];
                                        $content .= "<p class ='mt-5'>Character: $character</p>";
                                        $content .= "<p class ='mt-5'>Password: $password</p>";
                                        $content .= "<p class ='mt-5'>VND: $vnd</p>";
                                        $content .= "<p class ='mt-5'>IP: $last_ip</p>";
                                    } else {
                                        $content .= "<p class ='mt-5'>Không tìm thấy thông tin cho username '$username'.</p>";
                                    }
                                }
                        
                                // Đóng kết nối
                                mysqli_close($conn);
                                $content .=
                                '
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    ';
    include "layout.php";
} else
    header("Location: login.php");
exit();

?>