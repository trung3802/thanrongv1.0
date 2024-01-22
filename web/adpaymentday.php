<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
$content =
    '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Lịch sử nạp hôm nay</h5>
                <div class="card">
                    <div class="card-body">
                        <table class="table">
                            <thead style="background-color: #E9ECEF">
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Tài khoản</th>
                                <th scope="col">Số tiền</th>
                                <th scope="col">Thời gian</th>
                            </tr>
                            </thead>
                            <tbody>';
                                // Truy vấn dữ liệu từ bảng naptien
                                $query = "SELECT * FROM naptien WHERE DATE(time) = CURDATE()";
                                $result = mysqli_query($conn, $query);
                                // Hiển thị dữ liệu trên form HTML
                                $total = 0;
                                if (mysqli_num_rows($result) > 0) {
                                    $count = 1;
                                    while ($row = mysqli_fetch_assoc($result)) {
                                        $total += $row['amount'];
                                        $content .= "<tr>";
                                        $content .= "<td>" . $count . "</td>";
                                        $content .= "<td>" . $row["username"] . "</td>";
                                        $content .= "<td>" . $row["amount"] . " " . "VND" . "</td>";
                                        $content .= "<td>" . $row["time"] . "</td>";
                                        $content .= "</tr>";
                                        $count++;
                                    }
                                } else {
                                    $content .= "<tr><td colspan='4'>Không có dữ liệu nạp tiền.</td></tr>";
                                }
                                mysqli_close($conn);
                                $content .= "<tr>";
                                $content .= '<td colspan="2"></td>';
                                $content .= "<td><strong>Tổng tiền:</strong></td>";
                                $content .= "<td><strong>" . number_format($total) . " VND</strong></td>";

                                $content .= "</tr>";
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
} else
header("Location: login.php");
exit();
?>