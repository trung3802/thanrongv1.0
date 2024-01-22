<?php
session_start();
// include "check_role.php";
if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Thông tin người dùng</h5>
                <table class="table table-bordered">
                    <tbody>';

                    include "db_conn.php";
    
            
            
    // Truy vấn dữ liệu từ bảng users
    $query = "SELECT sdt, email, created_at FROM user WHERE username = ?";
    $stmt = $conn->prepare($query);
    $stmt->bind_param("s", $_SESSION['username']);
    $stmt->execute();
    $stmt->bind_result($sdt, $email, $created_at);
    $stmt->fetch();
    $stmt->close();
    
    
    
    // Hiển thị thông tin trong bảng
    $content .= '
                        
                        <tr>
                            <td style="width: 150px"><strong>Tài khoản:</strong></td>
                            <td><input type="text" id="username" value="' . $_SESSION['username'] . '" name="username" class="form-control" readonly></td>
                        </tr>
                        <tr>
                            <td style="width: 150px"><strong>Số điện thoại:</strong></td>
                            <td><input type="text" id="sdt" value="' . $sdt . '" name="sdt" class="form-control"readonly></td>
                        </tr>
                        <tr>
                            <td style="width: 150px"><strong>Email:</strong></td>
                            <td><input type="text" id="email" value="' . $email . '" name="email" class="form-control"readonly></td>
                        </tr>
                        <tr>
                            <td style="width: 150px"><strong>Ngày tạo:</strong></td>
                            <td><input type="text" id="created_at" value="' . $created_at . '" name="created_at" class="form-control" readonly></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    ';
    include "layout.php";
} else {
    header("Location: login");
    exit();
}

