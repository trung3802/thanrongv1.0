<?php
session_start();
if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
        <style>
            .form-check-input[type="radio"] {
                display: none;
                }
        </style>
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title fw-semibold mb-4">Nạp tiền SV1</h5>
                    <div class="card">
                        <div class="card-body">
                            <form action="action/naptien_post.php" method="post">
                                <label class="form-label">Mệnh giá</label>
                                <div class="card-body p-2 mb-3">
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-5000" value="5000">
                                        <label class="form-check-label" for="amount-5000"> 5,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-10000" value="10000">
                                        <label class="form-check-label" for="amount-10000">10,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-15000" value="15000">
                                        <label class="form-check-label" for="amount-15000">15,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-20000" value="20000">
                                        <label class="form-check-label" for="amount-20000">20,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-30000" value="30000">
                                        <label class="form-check-label" for="amount-30000">30,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-40000" value="40000">
                                        <label class="form-check-label" for="amount-40000">40,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-50000" value="50000">
                                        <label class="form-check-label" for="amount-50000">50,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-100000" value="100000">
                                        <label class="form-check-label" for="amount-100000">100,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-150000" value="150000">
                                        <label class="form-check-label" for="amount-150000">150,000 VNĐ</label>
                                    </div>
                                    <div class="form-check btn btn-outline-primary m-1">
                                        <input class="form-check-input" type="radio" name="amount" id="amount-200000" value="200000">
                                        <label class="form-check-label" for="amount-200000">200,000 VNĐ</label>
                                    </div>
                                </div>
                                <div class="mb-3">
                                    <label for="exampleInputEmail1" class="form-label">Tài khoản</label>
                                    <input type="text" class="form-control" id="exampleInputEmail1" name="username" aria-describedby="emailHelp">
                                </div>
                                <button type="submit" class="btn btn-primary">Thực thi</button>
                                <a href="index.php" class="btn btn-primary">Quay lại</a>
                            </form>
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
            formChecks.forEach(formCheck => {
            formCheck.classList.remove("active");
            });
    
            // Thêm class "active" cho div form-check được bấm
            formCheck.classList.add("active");
        });
        });
        formChecks.forEach(formCheck => {
            formCheck.addEventListener("click", () => {
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
    </script>
    ';
    include "layout.php";
} else
    header("Location:dangnhap.php");
exit();
?>