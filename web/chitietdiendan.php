<?php
    session_start();
?>
<!doctype html>
<html lang="en">

<head>
    <link rel="shortcut icon" href="//theme.hstatic.net/1000271846/1001087843/14/favicon.png?v=86" type="image/png">
    <meta name="google-adsense-account" content="ca-pub-1313339641046545">
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Chi Tiết</title>
    
    <link href="./assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="./assets/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <script src="./assets/vendor/jquery/jquery.min.js"></script>
    <script src="./assets/vendor/notify/notify.js"></script>
    <link rel="icon" href="./assets/images/icon.png?v=99">
    <link href="./assets/css/main.css" rel="stylesheet" type="text/css">
    
</head>

<body style="    background: linear-gradient(to right, rgb(199 95 95), rgb(124 183 124), rgb(62 62 189));">
    <div class="container" style="border-radius: 15px; background: #BFEFFF; padding: 0px">
        <div class="container" style="background-color: #BFEFFF">
            <div class="row bg pb-3 pt-2">
                <div class="col">
                    </br>
                    <div class="color-changing-text">
                        <h4 class="shop text-center">
                            <script type="text/javascript" data-cfasync="false">
                            farbbibliothek = new Array();

                            farbbibliothek[2] = new Array("#824800", "#824800", "#824800", "#824800", "#824800",
                                "#925100",
                                "#a05900", "#b56500", "#c36d00", "#d27703", "#e48000", "#ff8f00", "#ff9209",
                                "#ffb900",
                                "#ffdc00", "#ffb900", "#ff9209", "#e48000", "#d27703", "#c36d00", "#b56500",
                                "#a05900",
                                "#925100", "#824800", "#824800", "#824800", "#824800", "#824800");
                            farben2 = farbbibliothek[2];

                            function farbschrift2() {
                                for (var i = 0; i < Buchstabe2.length; i++) {
                                    document.all["b" + i].style.color = farben2[i];
                                    document.all["b" + i].style.fontSize = "54px"; // Đặt kích thước chữ to hơn
                                }
                                farbverlauf2();
                            }

                            function string2array2(text) {
                                Buchstabe2 = new Array();
                                while (farben2.length < text.length) {
                                    farben2 = farben2.concat(farben2);
                                }
                                k = 0;
                                while (k <= text.length) {
                                    Buchstabe2[k] = text.charAt(k);
                                    k++;
                                }
                            }

                            function divserzeugen2() {
                                for (var i = 0; i < Buchstabe2.length; i++) {
                                    document.write("<span id='b" + i + "' class='b" + i + "'>" + Buchstabe2[i] +
                                        "<\/span>");
                                }
                                farbschrift2();
                            }

                            function farbverlauf2() {
                                for (var i = 0; i < farben2.length; i++) {
                                    farben2[i - 1] = farben2[i];
                                }
                                farben2[farben2.length - 1] = farben2[-1];

                                setTimeout("farbschrift2()", 70); // Đặt khoảng thời gian đổi màu chậm hơn
                            }

                            text2 = "THANRONG";
                            string2array2(text2);
                            divserzeugen2();
                            </script>
                        </h4>
                    </div></br>
                    <div class="text-center pt-2">
                        <div style="display: inline-block;">
                            <a href="tai-ve/Android">
                                <img class="icon-download" src="./assets/images/android.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="tai-ve/Pc"><img class="icon-download" src="./assets/images/pc.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="tai-ve/Ios"><img class="icon-download" src="./assets/images/ip.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="dangnhap"><img class="icon-download" src="./assets/images/napngoc.png"></a>
                            </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div>
                            <img height="12" src="./assets/images/12.png" style="vertical-align: middle;">
                            <small style="font-size: 10px" id="hour3">Dành cho người chơi trên 12 tuổi. Chơi quá 180
                                phút mỗi ngày sẽ hại sức khỏe.</small>
                        </div>
                        <video autoplay muted loop style="object-fit: cover; /* Cover the entire viewport */
            width: 100vw; /* Make the video take the full width of the viewport */
            height: 100vh; /* Make the video take the full height of the viewport */
            position: fixed;
            top: 0;
            left: 0;
            z-index: -1; /* Place the video behind other content */">
        <source src="assets/images/logo.mp4" type="video/mp4">
    </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="container color-main2 pb-2" style="background-color: #BFEFFF">
            <div class="text-center">
                <div class="row">
                    <div class="col pr-0">
                        <a href="./index" class="btn p-1 btn-header-active">Trang chủ</a>
                    </div>
                    <div class="col">
                        <a href="./diendan" class="btn p-1 btn-header">Diễn Đàn</a>
                    </div>
                    <?php
                    if (!isset($_SESSION['id']) || !isset($_SESSION['username'])) {
                        echo '
                        <div class="col">
                            <a href="./dangky" class="btn p-1 btn-header">Đăng Ký</a>
                        </div>
                        <div class="col">
                            <a href="./dangnhap" class="btn p-1 btn-header">Đăng Nhập</a>
                        </div>
                        ';
                    }
                    ?>
                    <?php
                    if (isset($_SESSION['id']) || isset($_SESSION['username'])) {
                        echo '
                        <div class="col">
                            <a href="./home" class="btn p-1 btn-header">Người Dùng</a>
                        </div>
                        <div class="col">
                            <a href="./logout" class="btn p-1 btn-header">Đăng Xuất</a>
                        </div>
                        ';
                    }
                    ?>
                </div>
            </div>
        </div>
        <?php
            //Tính thời gian
            function formatTimeAgo($timestamp) {
                date_default_timezone_set('Asia/Ho_Chi_Minh'); // Đặt múi giờ cho múi giờ Đông Dương
                $now = time();
                $timeDiff = $now - strtotime($timestamp);

                $seconds = floor($timeDiff);
                if ($seconds < 60) {
                    return $seconds . " giây trước";
                }

                $minutes = floor($seconds / 60);
                if ($minutes < 60) {
                    return $minutes . " phút trước";
                }

                $hours = floor($minutes / 60);
                if ($hours < 24) {
                    return $hours . " giờ trước";
                }

                $days = floor($hours / 24);
                return $days . " ngày trước";
            }

            include "db_conn.php";

            // Truy vấn dữ liệu từ bảng diendan và sắp xếp theo created_at giảm dần
            $diendan_admin_query = "SELECT d.id, d.tieude, d.noidung, d.userid, u.username, d.created_at, d.diendancha, d.avatar
                                    FROM diendan d 
                                    INNER JOIN user u ON u.id = d.userid 
                                    WHERE d.diendancha = 0 and d.admin = 1
                                    ORDER BY d.created_at DESC";

            $diendan_admin_result = $conn->query($diendan_admin_query);

            $content = ""; // Khởi tạo biến để chứa nội dung HTML

            if (mysqli_num_rows($diendan_admin_result) > 0) {
                while ($row = mysqli_fetch_assoc($diendan_admin_result)) {
                    $content .= '<div class="box-stt">';
                    $content .= '<div style="width: 40px; float:left; margin-right: 25px;">';
                    $content .= '<a href="chitietdiendan?id='. $row["id"] .'"><img src="assets/images/avatar/' . $row["avatar"] . '" style="width: 50px ; height:50px"></a>';
                    $content .= '</div>';
                    $content .= '<div class="box-right">';
                    $content .= '<b><a href="chitietdiendan?id='. $row["id"] .'">' . $row["tieude"] . ' <img src="assets/images/hot.gif"></a></b>';
                    $content .= '<div class="box-name" style="font-size: 14px;">';
                    $content .= 'bởi ' . $row["username"] . ' <a style="float: right"><i>' . formatTimeAgo($row["created_at"]) . '</i></a></div>';
                    $content .= '</div>';
                    $content .= '</div>';
                }
            } else {
                $content .= "<div class='box-stt'><div class='box-right'><a href='#'>Không có bài viết nào.</a></div></div>";
            }
            mysqli_close($conn);
        ?>
        <div class="container color-forum pt-2" s style="background-color: #87CEFA">
            <div class="row">
                <div class="col">
                    <?php echo $content; ?>
                </div>
            </div>
        </div>

        <?php if (isset($_GET['error'])) { ?>
        <div style="display: block; padding-left: 50px; height: 30px; padding-top: 10px;">
            <p style="color: red; font-weight: bold;" class="error">
                <?php echo $_GET['error']; ?>
            </p>
        </div>
        <?php } ?>
        <?php if (isset($_GET['success'])) { ?>
        <div style="display: block; padding-left: 50px; height: 30px; padding-top: 10px;">
            <p style="color: blue; font-weight: bold;" class="success">
                <?php echo $_GET['success']; ?>
            </p>
        </div>
        <?php } ?>


        <?php
            
            //Chuyển ký tự thành ...
            function shortenString($text, $maxLength = 3) {
                if (strlen($text) > $maxLength) {
                    return substr($text, 0, $maxLength) . '...';
                } else {
                    return $text;
                }
            }

            //Truy vấn diễn đàn theo id
            include "db_conn.php";

            if (isset($_GET['id'])) {
                $id = $_GET['id']; // Lấy giá trị ID từ tham số 'id' trong URL
                $id = mysqli_real_escape_string($conn, $id); // Bảo vệ dữ liệu nhập từ URL

                $query = "SELECT d.id, d.diendancha, d.tieude, d.noidung, d.userid, u.username, d.created_at, d.avatar, d.anh
                        FROM diendan d 
                        INNER JOIN user u ON u.id = d.userid 
                        WHERE d.id = '$id' 
                        ORDER BY d.created_at DESC";

                $result = mysqli_query($conn, $query);

                if ($result) {
                    if (mysqli_num_rows($result) > 0) {
                        $row = mysqli_fetch_assoc($result);
                    } else {
                        echo "Không có dữ liệu với ID cụ thể.";
                    }
                } else {
                    echo "Có lỗi xảy ra trong truy vấn: " . mysqli_error($conn);
                }
            } else {
                echo "Không có tham số ID trong URL.";
            }

            mysqli_close($conn);
            ?>

        <div class="container color-forum2 pt-2" style="background-color: #BFEFFF">
            <div class="row">
                <div class="col">
                    <div class="box-stt">
                        <table>
                            <thead>
                                <th style="text-align: center;">
                                    <img src="./assets/images/avatar/<?php echo $row["avatar"] ?>" style="width: 70px">
                                    <?php echo $row["username"] ?>
                                </th>
                                <th style="width: 100%">
                                    <div class="form-control">
                                        <div class="form-group">
                                            <div>
                                                <label class="mt-2">
                                                    <a style="font-size: 12px"
                                                        id="timeago"><i><?php echo formatTimeAgo($row["created_at"]); ?></i></a>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div>
                                                <label class="mt-2">
                                                    <h6><b><?php echo $row["tieude"] ?> </b></h6>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div style="border-top: 1px solid pink">
                                                <label class="mt-2"><?php echo $row["noidung"] ?> </label>
                                            </div>
                                            <?php if (!empty($row["anh"])) : ?>
                                                <div style="display: flex; justify-content: center; align-items: center; height: 200px;">
                                                    <img src="./assets/images/admindiendan/<?php echo $row["anh"] ?>" style="width: auto; height: 100%;">
                                                </div>
                                            <?php endif; ?>
                                        </div>
                                    </div>
                                </th>
                            </thead>
                        </table>
                        <div class="mt-2 me-2"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container pb-2">
            <div class="row mt-3">
                <div class="col-3">
                    <?php 
                    // Kết nối đến cơ sở dữ liệu
                    include "db_conn.php";

                    // Kiểm tra kết nối
                    if ($conn->connect_error) {
                        die("Kết nối thất bại: " . $conn->connect_error);
                    }

                    $id = $_GET['id'];

                    //Đếm số bình luận trên 1 diễn đàn
                    $dembinhluan_query = "SELECT COUNT(id) 
                                        FROM diendan
                                        WHERE diendancha = $id";
                    $dembinhluan_result = $conn->query($dembinhluan_query);

                    $row = $dembinhluan_result->fetch_assoc();

                    //Kiểm tra số lượng bình luận nhỏ hơn 30
                    if ($row['COUNT(id)'] <= 90) {
                        if(isset($_SESSION['username'])) {
                            echo '<a class="btn btn-main btn-sm" id="toggleCommentForm">Bình luận</a>';
                        } else {
                            echo '<a class="btn btn-main btn-sm" href="dangnhap">Đăng nhập để bình luận</a>';
                        }
                    } else {
                        echo '<a class="btn btn-main btn-sm" href="">Đã vượt giới hạn bình luận trên mỗi bài</a>';
                    }

                ?>
                </div>
                <div id="commentForm" style="display: none;">
                    <form id="form" action="binhluan" method="post" autocomplete="off" style="margin-bottom: 10px;">
                        <input class="form-control mt-2 me-2" type="text" name="id" id="id" value="<?php echo $id;?>"
                            hidden>
                        <div class="form-group">
                            <label class="mt-2"> Người đăng: </label>
                            <div class="d-flex">
                                <input class="form-control mt-2 me-2" type="text" name="uname" id="username"
                                    placeholder="<?php echo $_SESSION['username'];?>" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="mt-4"><span class="text-danger">*</span> Nội dung:</label>
                            <?php if (isset($_GET['noidung'])) { ?>
                            <textarea class="form-control mt-2" name="noidung" id="noidung"
                                maxlength="255"><?php echo $_GET['noidung']; ?></textarea>
                            <?php } else { ?>
                            <textarea class="form-control mt-2" name="noidung" id="noidung" maxlength="255"></textarea>
                            <?php } ?>
                        </div>
                        <div class="mt-3" id="notify" class="text-danger pb-2 font-weight-bold"></div>
                        <button class="btn btn-header form-control" id="btn" type="submit">Đăng bài</button>
                    </form>
                </div>
            </div>
        </div>

        <!--Danh sách bình luận của diễn đàn-->
        <div class="container color-forum2 pt-2" style="background-color: #BFEFFF">
            <div class="row">
                <div class="col">
                    <?php 
                        include "db_conn.php";

                        if (isset($_GET['id'])) {
                            $id = $_GET['id']; // Lấy giá trị ID từ tham số 'id' trong URL
                            $id = mysqli_real_escape_string($conn, $id); // Bảo vệ dữ liệu nhập từ URL
                            
                            // Truy vấn lấy danh sách các bình luận trong diễn đàn dựa trên diendancha
                            $query = "SELECT d.id, d.diendancha, d.tieude, d.noidung, d.userid, u.username, d.created_at 
                                    FROM diendan d 
                                    INNER JOIN user u ON u.id = d.userid 
                                    WHERE d.diendancha = '$id' 
                                    ORDER BY d.created_at DESC";
                            
                            $postsPerPage = 5; // Số bài viết trên mỗi trang
                            $totalPosts = mysqli_num_rows(mysqli_query($conn, $query));
                            $totalPages = ceil($totalPosts / $postsPerPage);

                            $currentUrl = $_SERVER['PHP_SELF']; // URL của trang hiện tại

                            $page = isset($_GET['page']) ? intval($_GET['page']) : 1;
                            $start = ($page - 1) * $postsPerPage;
                            $queryWithLimit = $query . " LIMIT $start, $postsPerPage";
                            $resultWithLimit = mysqli_query($conn, $queryWithLimit);

                            if ($result) {
                                if (mysqli_num_rows($resultWithLimit) > 0) {
                                    while ($row = mysqli_fetch_assoc($resultWithLimit)) {
                                        ?>
                                <div class="box-stt">
                                    <table>
                                        <thead>
                                            <th style="text-align: center;">
                                                <img src="./assets/images/avatar/macdinh.jpg" style="width: 70px">
                                                <?php echo shortenString($row["username"]) ?>
                                            </th>
                                            <th style="width: 100%">
                                                <div class="form-control">
                                                    <div class="form-group">
                                                        <div>
                                                            <label class="mt-2">
                                                                <a style="font-size: 12px"
                                                                    id="timeago"><i><?php echo formatTimeAgo($row["created_at"]); ?></i></a>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div style="border-top: 1px solid pink">
                                                            <label class="mt-2"><?php echo $row["noidung"] ?> </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </th>
                                        </thead>
                                    </table>
                                    <div class="mt-2 me-2"></div>
                                </div>
                                <?php
                                    }
                                } else {
                                    echo "Không có bình luận nào";
                                }
                            } else {
                                echo "Có lỗi xảy ra trong truy vấn: " . mysqli_error($conn);
                            }
                        } else {
                            echo "Không có tham số ID trong URL.";
                        }
                    
                        mysqli_close($conn);
                    ?>
                </div>
            </div>
        </div>

        <div class="container pb-2" style="background-color: #BFEFFF">
            <div class="row mt-3">
                <div class="col-9 text-right">
                    <?php if ($totalPages > 1) : ?>
                    <?php if ($page > 1) : ?>
                    <a href="<?php echo $currentUrl . '?id=' . $id . '&page=1'; ?>" class="btn btn-sm btn-light">
                        <<< /a>
                            <?php endif; ?>

                            <?php for ($i = 1; $i <= $totalPages; $i++) : ?>
                            <?php if ($i === 1 || $i === $totalPages || ($i === $page - 1) || ($i === $page) || ($i === $page + 1)) : ?>
                            <a href="<?php echo $currentUrl . '?id=' . $id . '&page=' . $i; ?>"
                                class="btn btn-sm <?php echo $page === $i ? 'page-active' : 'btn-light'; ?>"><?php echo $i; ?></a>
                            <?php elseif (($i === $page - 2) || ($i === $page + 2)) : ?>
                            <span class="btn btn-sm btn-light">...</span>
                            <?php endif; ?>
                            <?php endfor; ?>

                            <?php if ($page < $totalPages) : ?>
                            <a href="<?php echo $currentUrl . '?id=' . $id . '&page=' . $totalPages; ?>"
                                class="btn btn-sm btn-light">>></a>
                            <?php endif; ?>
                        <?php endif; ?>
                </div>
            </div>
        </div>

        <div class="border-secondary border-top"></div>
        <div class="container pt-4 pb-4 text-white" style="background-color: #BFEFFF">
            <div class="row">
                <div class="col">
                    <div class="text-center">
                        <div style="font-size: 13px" class="text-dark">
                            <small>2023©THANRONG</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>

<script>
//Thu gọn, hiển thị form bình luận
document.addEventListener("DOMContentLoaded", function() {
    var toggleButton = document.getElementById("toggleCommentForm");
    var commentForm = document.getElementById("commentForm");

    toggleButton.addEventListener("click", function() {
        if (commentForm.style.display === "none") {
            commentForm.style.display = "block";
            toggleButton.innerText = "Thu gọn";
        } else {
            commentForm.style.display = "none";
            toggleButton.innerText = "Bình luận";
        }
    });
});
</script>