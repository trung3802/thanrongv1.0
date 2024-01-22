<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
    '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Lịch sử nhập giftcode</h5>
                <div class="card">
                    <div class="card-body">
                        <table class="table">
                            <thead style="background-color: #E9ECEF">
                                <tr>
                                    <th>Code</th>
                                    <th>Thời gian nhập</th>
                                    <th>Trạng thái</th>
                                </tr>
                            </thead>
                            <tbody>';

                            include "db_conn.php";

                            $id = $_SESSION['id'];
                            // Truy vấn dữ liệu từ bảng napcard
                            $query = "SELECT g.code, g.character, g.time_used
                                        FROM user u INNER JOIN `character` c ON u.character = c.id
                                                    INNER JOIN giftcode_used g ON c.name = g.character
                                        WHERE u.id = $id";

                            $result = mysqli_query($conn, $query);
                            // Hiển thị dữ liệu trên bảng
                            if (!$result) {
                                die('Query error: ' . mysqli_error($conn));
                            }
                            if (mysqli_num_rows($result) > 0) {
                                $count = 1;
                                while ($row = mysqli_fetch_assoc($result)) {
                                    $content .= "<tr>";
                                    $content .= "<td>" . $row["code"] . "</td>";
                                    $content .= "<td>" . $row["time_used"] . "</td>";
                                    $content .= '<td><a style="color: green"><i class="fas fa-check-circle fa-lg fa-spin"></i> Đã sử dụng</a></td>';
                                    $content .= "</tr>";
                                    $count++;
                                }                                
                            } else {
                                $content .= "<tr><td colspan='6'>Không có dữ liệu nhập giftcode.</td></tr>";
                            }
                            mysqli_close($conn);
                            
                            $content .= '
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Lịch sử nhập giftcode VIP</h5>
                <div class="card">
                    <div class="card-body">
                        <table class="table">
                            <thead style="background-color: #E9ECEF">
                                <tr>
                                    <th>Code</th>
                                    <th>Thời gian nhập</th>
                                    <th>Trạng thái</th>
                                </tr>
                            </thead>
                            <tbody>';

                            include "db_conn.php";
                            $id = $_SESSION['id'];
                            // Truy vấn dữ liệu từ bảng napcard
                            $query = "SELECT g.code, g.character, g.time_used
                                        FROM user u INNER JOIN `character` c ON u.character = c.id
                                                    INNER JOIN giftcodett_used g ON c.name = g.character
                                        WHERE u.id = $id";

                            $result = mysqli_query($conn, $query);
                            // Hiển thị dữ liệu trên bảng
                            if (!$result) {
                                die('Query error: ' . mysqli_error($conn));
                            }
                            if (mysqli_num_rows($result) > 0) {
                                $count = 1;
                                while ($row = mysqli_fetch_assoc($result)) {
                                    $content .= "<tr>";
                                    $content .= "<td>" . $row["code"] . "</td>";
                                    $content .= "<td>" . $row["time_used"] . "</td>";
                                    $content .= '<td><a style="color: green"><i class="fas fa-check-circle fa-lg fa-spin"></i> Đã sử dụng</a></td>';
                                    $content .= "</tr>";
                                    $count++;
                                }                                
                            } else {
                                $content .= "<tr><td colspan='6'>Không có dữ liệu nhập giftcode vip.</td></tr>";
                            }
                            mysqli_close($conn);
                            
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
    header("Location: login");
    exit();
}
?>
