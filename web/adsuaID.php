<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Chỉnh sửa</h5>
                <div class="card">
                    <div class="card-body">
                        <form method="GET">
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Nhập ID</label>
                                <input type="text" class="form-control" id="exampleInputEmail1" name="character_id" placeholder="Nhập ID" aria-describedby="emailHelp">
                            </div>
                            <button type="submit" class="btn btn-primary">Thực thi</button>
                            <a href="index.php" class="btn btn-primary">Quay lại</a>
                        </form>';
                        // Kiểm tra nếu có ID được nhập vào
                        if (isset($_GET['character_id'])) {
                            $character_id = $_GET['character_id'];
                
                            // Truy vấn dữ liệu từ bảng character cho ID nhập vào
                            $query = "SELECT id, Name, ItemBody, ItemBag, ItemBox, InfoChar FROM `character` WHERE id = $character_id";
                            $result = mysqli_query($conn, $query);
                
                            // Kiểm tra và hiển thị dữ liệu
                            if (mysqli_num_rows($result) > 0) {
                                $content .= "<table class='table'>
                                <thead style='background-color: #E9ECEF'>
                                        <tr>
                                            <th scope='col'>Sửa</th>
                                            <th scope='col'>ID</th>
                                            <th scope='col'>Name</th>
                                            <th scope='col'>ItemBody</th>
                                            <th scope='col'>ItemBag</th>
                                            <th scope='col'>ItemBox</th>
                                            <th scope='col'>InfoChar</th>
                                            
                                        </tr>
                                    </thead>
                                    <tbody>";
                                while ($row = mysqli_fetch_assoc($result)) {
                                    $content .= "<tr>";
                                    $content .= "<td><a href='edit.php?id=" . $row['id'] . "' style='font-size: 20px; color: #000'><i class='ti ti-edit'></i></a></td>";
                                    $content .= "<td>" . $row['id']  . "</td>";
                                    $content .= "<td>" . $row['Name'] . "</td>";
                                    $content .= "<td>" . $row['ItemBody'] .  "</td>";
                                    $content .= "<td>" . $row['ItemBag'] . "</td>";
                                    $content .= "<td>" . $row['ItemBox'] . "</td>";
                                    $content .= "<td>" . $row['InfoChar'] . "</td>";
                                    
                                    $content .= "</tr>";
                                }
                            } else {
                                $content .= "<tr><td colspan='4'>Không có dữ liệu người chơi.</td></tr>";
                            }
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
} else
    header("Location: login.php");
exit();
?>