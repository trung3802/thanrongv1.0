<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Chỉnh sửa tuyệt chiêu</h5>
                <div class="card">
                    <div class="card-body">
                        <form method="POST">
                        ';
                            if (isset($_SESSION['thong_bao'])) {
                                if ($_SESSION['thong_bao'][0] === 'success') {
                                    $content .= '<p class="success" style="color: blue">' . $_SESSION['thong_bao'][1] . '</p>';
                                } elseif ($_SESSION['thong_bao'][0] === 'error') {
                                    $content .= '<p class="error" style="color: red">' . $_SESSION['thong_bao'][1] . '</p>';
                                }
                                unset($_SESSION['thong_bao']);
                            }

                            $content .= '
                            <div class="mb-3">
                                <label for="username" class="form-label">Username:</label>
                                <input type="text" class="form-control" id="username" name="username" placeholder="Nhập username">
                            </div>
                            <input type="submit" name="submit" class="btn btn-primary" value="Tìm kiếm">
                        </form>';
    
                        if (isset($_POST['username'])) {
                            $username = $_POST['username'];
                            
                            // Truy vấn dữ liệu từ bảng user cho username được nhập vào
                            $query = "SELECT `id`, `name`, `skills`, `boughtskill`, `infochar` FROM `character` WHERE `name` = '$username'";
                            $result = mysqli_query($conn, $query);
                            
                            // Kiểm tra và hiển thị dữ liệu
                            if (mysqli_num_rows($result) > 0) {
                                $row = mysqli_fetch_assoc($result);
                                $id = $row['id'];
                                $name = $row['name'];
                                $skills = $row['skills'];
                                $boughtskill = $row['boughtskill'];
                                $infochar = $row['infochar'];
                                
                                //Tách chuỗi json lấy ra nclass
                                $data = json_decode($infochar, true);
                                $NClass = $data['NClass'];

                                $content .= '
                                    <div class="mt-4">
                                        <h5 class="card-title fw-semibold mb-4">Thông tin người dùng</h5>
                                        <table class="table">
                                            <tr>
                                                <th>Name</th>
                                                <th>Hành tinh</th>
                                            </tr>
                                            <tr>
                                                <td>' . $name . '</td>';
                                                
                                                if ($NClass == 0) {
                                                    $content .= '<td>Trái dất</td>';
                                                } else if ($NClass == 1) {
                                                    $content .= '<td>Namec</td>';
                                                } else if ($NClass == 2) {
                                                    $content .= '<td>Xayda</td>';
                                                }

                                $content .= '
                                            </tr>
                                        </table>                       
                                    </div>
                                        
                                    <form action="action/suatuyetchieu_post.php" method="post">
                                        <div class="row">                                   
                                            <div class="col-sm-8">
                                                <input type="hidden" name="id" id="id" value="' . $id . '">
                                                <label for="link" class="form-label">Skills</label>';
                                                
                                                if ($NClass == 0) {
                                                    $content .= '<textarea class="form-control" name="skills" id="skills">[{"Id":0,"SkillId":0,"CoolDown":0,"Point":1,"CurrExp":0}]</textarea>';
                                                } else if ($NClass == 1) {
                                                    $content .= '<textarea class="form-control" name="skills" id="skills">[{"Id":2,"SkillId":14,"CoolDown":0,"Point":1,"CurrExp":0}]</textarea>';
                                                } else if ($NClass == 2) {
                                                    $content .= '<textarea class="form-control" name="skills" id="skills">[{"Id":4,"SkillId":28,"CoolDown":0,"Point":1,"CurrExp":0}]</textarea>';
                                                }

                                $content .= '
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="link" class="form-label">Bought skill</label>';
                                                
                                                if ($NClass == 0) {
                                                    $content .= '<textarea class="form-control" name="boughtskill" id="boughtskill">[66]</textarea>';
                                                } else if ($NClass == 1) {
                                                    $content .= '<textarea class="form-control" name="boughtskill" id="boughtskill">[79]</textarea>';
                                                } else if ($NClass == 2) {
                                                    $content .= '<textarea class="form-control" name="boughtskill" id="boughtskill">[87]</textarea>';
                                                }

                                $content .= '
                                            </div>
                                            <div class="col-sm-2">
                                                <button type="submit" onclick="return confirm(\'Bạn có chắc chắn muốn sửa skill?\');" class="btn btn-primary">Sửa</button>
                                            </div>
                                        </div>
                                    </form>';

                                            
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
