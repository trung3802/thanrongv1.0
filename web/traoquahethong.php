<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Trao quà hệ thống</h5>
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
                        <form action="action/traoquahethong_post.php" method="post">
                            <div class="mb-3">
                                <label for="username" class="form-label">Tài khoản</label>
                                <input type="text" class="form-control" id="username" placeholder="Nhập username" name="username">
                            </div>
                            <div class="mb-3">
                                <label for="code" class="form-label">Giftcode</label>
                                ';    
                                    include "db_conn.php";

                                    $giftcode_query = "SELECT code FROM giftcodeht WHERE trangthai = 0 AND count = 1 ORDER BY code ASC LIMIT 1";
                                    $giftcode_result = mysqli_query($conn, $giftcode_query);
                                    
                                    if ($giftcode_result && mysqli_num_rows($giftcode_result) > 0) {
                                        while ($giftcode_row = mysqli_fetch_assoc($giftcode_result)) {
                                            $content .= '<input type="text" class="form-control" id="code" name="code" value="'.$giftcode_row["code"].'"/ readonly>';
                                        }
                                    } else {
                                        $content .= '<input type="text" class="form-control" id="code" name="code" value="Không còn giftcode hệ thống"/ readonly>';
                                    }
                                        
                                $content .= '
                            </div>
                            <button type="submit" class="btn btn-primary">Trao quà hệ thống</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    ';
    include "layout.php";
} else
    header("Location: login");
exit();
?>