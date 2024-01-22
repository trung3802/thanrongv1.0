<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title fw-semibold mb-4">Danh Sách Nạp Card</h5>
                    <div class="card">
                        <div class="card-body">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>UID</th>
                                        <th>Telco</th>
                                        <th>Menhgia</th>
                                        <th>Seri</th>
                                        <th>Code</th>
                                        <th>Magd</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>';                             
                                    
                                    $sql = "SELECT * FROM napcard ORDER BY id DESC LIMIT 15";
                                    $result = $conn->query($sql);
                                    if ($result->num_rows > 0) {
                                        while ($row = $result->fetch_assoc()) {
                                            $content .= "<tr>";
                                            $content .= "<td>" . $row["id"] . "</td>";
                                            $content .= "<td>" . $row["uid"] . "</td>";
                                            $content .= "<td>" . $row["telco"] . "</td>";
                                            $content .= "<td>" . $row["menhgia"] . "</td>";
                                            $content .= "<td>" . $row["seri"] . "</td>";
                                            $content .= "<td>" . $row["code"] . "</td>";
                                            $content .= "<td>" . $row["magd"] . "</td>";
                                            $content .= "<td>" . $row["status"] . "</td>";
                                            $content .= "</tr>";
                                        }
                                    } else {
                                        $content .= "<tr><td colspan='8'>Không có dữ liệu.</td></tr>";
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