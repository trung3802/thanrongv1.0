<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Nạp tiền (Bất kỳ)</h5>
                <div class="card">
                    <div class="card-body">
                        <form action="action/payrandom_post.php" method="post">
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Tài khoản</label>
                                <input type="text" class="form-control" id="exampleInputEmail1" name="username" aria-describedby="emailHelp">
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Số tiền</label>
                                <input type="number" min="0" placeholder="0" name="amount" class="form-control" id="exampleInputPassword1">
                            </div>
                            <button type="submit" class="btn btn-primary">Thực thi</button>
                            <a href="index.php" class="btn btn-primary">Quay lại</a>
                        </form>
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