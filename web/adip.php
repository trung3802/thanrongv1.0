<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title fw-semibold mb-4">Danh Sách IP</h5>
                    <div class="card">
                        <div class="card-body">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Địa chỉ IP</th>
                                        <th>Số lượng người dùng</th>
                                    </tr>
                                </thead>
                                <tbody>';                                 

                                    // Truy vấn dữ liệu và thực hiện thống kê
                                    $sql = "SELECT last_ip, COUNT(*) AS user_count FROM `user` GROUP BY last_ip HAVING user_count > 1 ORDER BY user_count DESC";
                                    $result = $conn->query($sql);

                                    if ($result->num_rows > 0) {
                                        while ($row = $result->fetch_assoc()) {
                                            $lastIp = $row["last_ip"];
                                            $userCount = $row["user_count"];
                                            $content .= "<tr><td>$lastIp</td><td>$userCount</td></tr>";
                                        }
                                    } else {
                                        $content .= "<tr><td colspan='2'>Không có dữ liệu thống kê nào</td></tr>";
                                    }

                                    $conn->close();

                                    $content .= '
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        ';

    include "layout.php";
} else {
    header("Location: dangnhap.php");
    exit();
}
?>
