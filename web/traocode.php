<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $trangthai = isset($_POST['trangthai']) ? $_POST['trangthai'] : 0;

    $content =
    '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Trao code cho mấy con vợ</h5>
                <form method="post" action="' . htmlspecialchars($_SERVER["PHP_SELF"]) . '">
                    <table class="table">
                        <thead>
                            <td>
                                <select class="form-control" name="trangthai" id="hien_an">
                                    <option value="0">Chưa Nhận</option>
                                    <option value="1">Đã Nhận</option>
                                </select>
                            </td>
                            <td>
                                <input type="submit" class="btn btn-primary" value="Hiển thị">  
                            </td>
                        </thead>
                    </table>
                </form>';
                if (isset($_SESSION['thong_bao'])) {
                    if ($_SESSION['thong_bao'][0] === 'success') {
                        $content .= '<p class="success" style="color: blue">' . $_SESSION['thong_bao'][1] . '</p>';
                    } elseif ($_SESSION['thong_bao'][0] === 'error') {
                        $content .= '<p class="error" style="color: red">' . $_SESSION['thong_bao'][1] . '</p>';
                    }
                    unset($_SESSION['thong_bao']);
                }
                $content .= '
                <div class="card">
                    <div class="card-body">
                        <table class="table">
                            <thead style="background-color: #E9ECEF">
                                <tr>
                                    <th>Username</th>
                                    <th>Tổng nạp</th>
                                    <th>Trạng thái</th>
                                    <th>Giftcode</th>
                                    <th>Trao</th>
                                </tr>
                            </thead>
                            <tbody>';
    
                            include "db_conn.php";
                            
                            $id = $_SESSION['id'];
                            // Truy vấn dữ liệu từ bảng napcard
                            $query = "SELECT u.id, u.username, u.trangthai, u.gitcodetrao, u.tongnap
                                        FROM user u 
                                        WHERE u.tongnap > 59000 and u.trangthai = $trangthai";

                            $result = mysqli_query($conn, $query);
                            // Hiển thị dữ liệu trên bảng
                            if (!$result) {
                                die('Query error: ' . mysqli_error($conn));
                            }
                            if (mysqli_num_rows($result) > 0) {
                                $count = 1;
                                while ($row = mysqli_fetch_assoc($result)) {
                                    $content .= "<tr>";
                                    $content .= "<td>" . $row["username"] . "</td>";
                                    $content .= "<td>" . number_format($row["tongnap"], 0, '.', ',') . " VNĐ</td>";
                                    if($row["trangthai"] == 0)
                                    {
                                        $content .= '<td><a style="color: red"><i class="fas fa-check-circle fa-lg fa-spin"></i> Chưa nhận</a></td>';
                                        $content .= '<form action="action/traogiftcode_post.php" method="post">
                                                        <input type="text" name="id" hidden value="' . $row["id"] . '">
                                                        <td>
                                                            <select class="form-control" name="gitcodetrao">';    

                                                            $giftcode_query = "SELECT code FROM giftcodett WHERE trangthai = 0 AND count = 1 ORDER BY code ASC LIMIT 2";
                                                            $giftcode_result = mysqli_query($conn, $giftcode_query);
                                                            
                                                            if ($giftcode_result && mysqli_num_rows($giftcode_result) > 0) {
                                                                while ($giftcode_row = mysqli_fetch_assoc($giftcode_result)) {
                                                                    $content .= '<option value="' . $giftcode_row["code"] . '">' . $giftcode_row["code"] . '</option>';
                                                                }
                                                            } else {
                                                                $content .= '<option value="">Không còn giftcode</option>';
                                                            }
                                        
                                        $content .= '       </select>
                                                        </td>
                                                        <td><button type="submit" class="btn btn-primary">Trao</button></td>                                       
                                                    </form>';
                                    }
                                    else
                                    {
                                        $content .= '<td><a style="color: green"><i class="fas fa-check-circle fa-lg fa-spin"></i> Đã nhận</a></td>';
                                        $content .= "<td>" . $row["gitcodetrao"] . "</td>";
                                    }
                                    $content .= "</tr>";
                                    $count++;
                                }                                
                            } else {
                                $content .= "<tr><td colspan='6'>Chưa có dữ liệu.</td></tr>";
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