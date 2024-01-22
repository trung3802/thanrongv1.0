<?php
session_start();
include "db_conn.php";

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $name = $_SESSION['username'];

    $content = '
    <style>
        .form-check-input[type="radio"] {
            display: none;
            }
        .form-check-input1[type="radio"] {
            display: none;
            }
    </style>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Đổi thẻ</h5>
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
                <div class="card">
                    <div class="card-body">
                        <form action="action/doithe_post.php" method="post">                           
                            <table class="table table-borderless">
                                <tbody>
                                    <tr>
                                        ';

                                        $id = $_SESSION['id'];
                                        // Truy vấn dữ liệu từ bảng napcard
                                        $query = "SELECT u.id, u.character, c.dataevent, u.vip, u.vip, c.infochar
                                                    FROM user u INNER JOIN `character` c ON u.character = c.id
                                                    WHERE u.id = $id";
                                        
                                        $result = mysqli_query($conn, $query);
                                        // Hiển thị dữ liệu trên bảng
                                        if (!$result) {
                                            die('Query error: ' . mysqli_error($conn));
                                        }
                                        if (mysqli_num_rows($result) > 0) {
                                            $count = 1;
                                            while ($row = mysqli_fetch_assoc($result)) {

                                                $data = json_decode($row["infochar"]);

                                                // Lấy giá trị của thuộc tính "VIP"
                                                $vipValue = $data->VIP;

                                                $content .= "<td>Cấp vip: " . $vipValue . "</td>";       
                                                $content .= "<td>Điểm sự kiện: " . $row["dataevent"] . " điểm</td>";                      
                                                $count++;
                                            }                                
                                        } else {
                                            $content .= "<td colspan='6'>Không có dữ liệu.</td>";
                                        }
                                        mysqli_close($conn);
                                        
                                        $content .= '
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Loại thẻ </td>
                                        <td>
                                            <div class="card-body p-2 mb-3">
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="nhamang" id="amount-viettel" value="VIETTEL">
                                                    <img style="width: 165px; height: 86px" src="anh/viettel.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="nhamang" id="amount-vinaphone" value="VINAPHONE">
                                                    <img style="width: 165px; height: 86px" src="anh/vinaphone.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="nhamang" id="amount-mobifone" value="MOBIFONE">
                                                    <img style="width: 165px; height: 86px" src="anh/mobifone.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="nhamang" id="amount-vietnamobile" value="VIETNAMOBILE">
                                                    <img style="width: 165px; height: 86px" src="anh/vietnamobile.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="nhamang" id="amount-garena" value="GARENA">
                                                    <img style="width: 165px; height: 86px" src="anh/garena.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="nhamang" id="amount-zing" value="ZING">
                                                    <img style="width: 165px; height: 86px" src="anh/zing.png"/>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Mệnh giá </td>
                                        <td>
                                            <div class="card-body p-2 mb-3">
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-20000" value="20000">
                                                    <label class="form-check-label" for="amount-20000">20,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-50000" value="50000">
                                                    <label class="form-check-label" for="amount-50000">50,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-100000" value="100000">
                                                    <label class="form-check-label" for="amount-100000">100,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-200000" value="200000">
                                                    <label class="form-check-label" for="amount-200000">200,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-500000" value="500000">
                                                    <label class="form-check-label" for="amount-500000">500,000 VNĐ</label>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>                                   
                                    <tr>
                                        <td></td>
                                        <td>
                                            <input type="submit" class="btn btn-primary" value="Đổi thẻ" onclick="return confirmChange()">                                                                            
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </form>
                        <div class="center-content">
                            <strong>Chú ý:</strong><br>
                            - Chọn đúng loại thẻ + mệnh giá.<br>
                            - 1 điểm = 100đ.<br>
                            - Khi đổi thẻ không hoàn trả
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function confirmChange() {
            var result = confirm("Bạn có chắc chắn muốn đổi thẻ?");
            if (result === true) {
                // Nếu người dùng chọn "OK," tiếp tục gửi biểu mẫu để thực hiện đổi thẻ
                return true;
            } else {
                // Nếu người dùng chọn "Cancel," ngăn chặn biểu mẫu gửi đi
                return false;
            }
        }
        
        const formChecks = document.querySelectorAll(".form-check");

        // Gắn sự kiện "click" vào từng form-check
        formChecks.forEach(formCheck => {
            formCheck.addEventListener("click", () => {
                // Loại bỏ class "active" khỏi tất cả các div form-check
                formChecks.forEach(otherFormCheck => {
                    otherFormCheck.classList.remove("active");
                });

                // Thêm class "active" cho div form-check được bấm
                formCheck.classList.add("active");

                // Lấy radio button bên trong form-check được bấm
                const radioButton = formCheck.querySelector(".form-check-input");

                // Đặt giá trị "true" cho radio button đó
                radioButton.checked = true;

                // Loại bỏ thuộc tính "checked" cho các radio button khác
                formChecks.forEach(otherFormCheck => {
                    if (otherFormCheck !== formCheck) {
                        const otherRadioButton = otherFormCheck.querySelector(".form-check-input");
                        otherRadioButton.checked = false;
                    }
                });
            });
        });

        const formChecks1 = document.querySelectorAll(".form-check1");

        // Gắn sự kiện "click" vào từng form-check1
        formChecks1.forEach(formCheck1 => {
            formCheck1.addEventListener("click", () => {
                // Loại bỏ class "active" khỏi tất cả các div form-check1
                formChecks1.forEach(otherFormCheck1 => {
                    otherFormCheck1.classList.remove("active");
                });

                // Thêm class "active" cho div form-check1 được bấm
                formCheck1.classList.add("active");

                // Lấy radio button bên trong form-check1 được bấm
                const radioButton1 = formCheck1.querySelector(".form-check-input1");

                // Đặt giá trị "true" cho radio button đó
                radioButton1.checked = true;

                // Loại bỏ thuộc tính "checked" cho các radio button khác
                formChecks1.forEach(otherFormCheck1 => {
                    if (otherFormCheck1 !== formCheck1) {
                        const otherRadioButton1 = otherFormCheck1.querySelector(".form-check-input1");
                        otherRadioButton1.checked = false;
                    }
                });
            });
        });    
    </script>

    ';

    include "layout.php";
} else {
    header("Location: login");
    exit();
}
?>
