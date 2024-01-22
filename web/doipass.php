<?php
session_start();
if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Đổi mật khẩu</h5>
                <div class="card">
                    <div class="card-body">
                        <form action="action/doipass_post" method="post">';
                        if (isset($_GET["error"])) {
                            $content .= '<p class="error" style="color: red">' . $_GET["error"] . '</p>';
                        }
                        if (isset($_GET["success"])) {
                            $content .= '<p class="success" style="color: blue">' . $_GET["success"] . '</p>';
                        }
                        $content .= '
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Mật khẩu cũ</label>
                                <input type="password" class="form-control" name="oldpass" id="exampleInputEmail1" aria-describedby="emailHelp">
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Mật khẩu mới</label>
                                <input type="password" class="form-control" name="newpass" id="exampleInputPassword1">
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Xác nhận mật khẩu mới</label>
                                <input type="password" class="form-control" name="repass" id="exampleInputPassword1">
                            </div>
                            <button type="submit" class="btn btn-primary">Đổi</button>
                            <a href="home" class="btn btn-primary">Quay lại</a>
                        </form>
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