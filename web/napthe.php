<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $name = $_SESSION['username'];

    $error = isset($_GET['error']) ? $_GET['error'] : '';
    $success = isset($_GET['success']) ? $_GET['success'] : '';

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
                <h5 class="card-title fw-semibold mb-4">Nạp thẻ</h5>
                <div class="card">
                    <div class="card-body">
                        <form action="napthe-p.php" method="post">
                            ' . ($error ? '<p class="error">' . $error . '</p>' : '') . '
                            ' . ($success ? '<p class="success">' . $success . '</p>' : '') . '
                            <table class="table table-borderless">
                                <tbody>
                                    <tr>
                                        <th colspan="2"><h3>Nạp thẻ cào</h3></th>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Loại thẻ </td>
                                        <td>
                                            <div class="card-body p-2 mb-3">
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="loaithe" id="amount-viettel" value="VIETTEL">
                                                    <img style="width: 165px; height: 86px" src="anh/viettel.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="loaithe" id="amount-vinaphone" value="VINAPHONE">
                                                    <img style="width: 165px; height: 86px" src="anh/vinaphone.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="loaithe" id="amount-mobifone" value="MOBIFONE">
                                                    <img style="width: 165px; height: 86px" src="anh/mobifone.png"/>
                                                </div>
                                                <div class="form-check btn btn-outline-primary m-1">
                                                    <input class="form-check-input" type="radio" name="loaithe" id="amount-vietnamobile" value="VIETNAMOBILE">
                                                    <img style="width: 165px; height: 86px" src="anh/vietnamobile.png"/>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Mệnh giá </td>
                                        <td>
                                            <div class="card-body p-2 mb-3">
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-10000" value="10000">
                                                    <label class="form-check-label" for="amount-10000">10,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-20000" value="20000">
                                                    <label class="form-check-label" for="amount-20000">20,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-30000" value="30000">
                                                    <label class="form-check-label" for="amount-30000">30,000 VNĐ</label>
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
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-300000" value="300000">
                                                    <label class="form-check-label" for="amount-300000">300,000 VNĐ</label>
                                                </div>
                                                <div class="form-check1 btn btn-outline-primary m-1">
                                                    <input class="form-check-input1" type="radio" name="menhgia" id="amount-500000" value="500000">
                                                    <label class="form-check-label" for="amount-500000">500,000 VNĐ</label>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Mã thẻ </td>
                                        <td><input class="form-control" type="text" name="mathe" placeholder="Mã thẻ"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Mã seri </td>
                                        <td><input class="form-control" type="text" name="seri" placeholder="Mã seri"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td><input type="submit" class="btn btn-primary" value="Nạp thẻ"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </form>
                        <div class="center-content">
                            <strong>Chú ý:</strong><br>
                            - Ghi đúng mã thẻ + seri thẻ nạp.<br>
                            - Nạp sai mệnh giá trừ 50%.<br>
                            - Kiểm tra tiền tại NPC SanTa!<br>
							- Nạp sai nhiều lần liên tục khoá vĩnh viễn!!
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
           
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
    header("Location: login.php");
    exit();
}
?>
