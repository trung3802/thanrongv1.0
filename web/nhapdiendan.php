<?php
session_start();
if (!isset($_SESSION['id']) || !isset($_SESSION['username'])) {
    header("Location: dangnhap");
    exit();
}
?>

<!doctype html>
<html lang="en">

<head>
    <link rel="shortcut icon" href="//theme.hstatic.net/1000271846/1001087843/14/favicon.png?v=86" type="image/png">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Trang Chủ Chính Thức</title>
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="description"
        content="Website chính thức của Chú Bé Rồng Online – Game Bay Vien Ngoc Rong Mobile nhập vai trực tuyến trên máy tính và điện thoại về Game 7 Viên Ngọc Rồng hấp dẫn nhất hiện nay!" />
    <meta name="keywords"
        content="Chú Bé Rồng Online,ngoc rong mobile, game ngoc rong, game 7 vien ngoc rong, game bay vien ngoc rong" />
    <meta name="twitter:card" content="summary" />
    <meta name="twitter:title"
        content="Website chính thức của Chú Bé Rồng Online – Game Bay Vien Ngoc Rong Mobile nhập vai trực tuyến trên máy tính và điện thoại về Game 7 Viên Ngọc Rồng hấp dẫn nhất hiện nay!" />
    <meta name="twitter:description"
        content="Website chính thức của Chú Bé Rồng Online – Game Bay Vien Ngoc Rong Mobile nhập vai trực tuyến trên máy tính và điện thoại về Game 7 Viên Ngọc Rồng hấp dẫn nhất hiện nay!"
        content="assets/images/nrogreen.png" content="200" content="200" />
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <script src="assets/vendor/jquery/jquery.min.js"></script>
    <script src="assets/vendor/notify/notify.js"></script>
    <link rel="icon" href="assets/images/icon.png?v=99">
    <link href="assets/css/main.css" rel="stylesheet" type="text/css">
</head>

<body>
    <div class="container" style="border-radius: 15px; background: #ffaf4c; padding: 0px">
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

                            text2 = "THANRONG2017";
                            string2array2(text2);
                            divserzeugen2();
                            </script>
                        </h4>
                    </div></br>
                    <div class="text-center pt-2">
                        <div style="display: inline-block;">
                            <a href="tai-ve/Android">
                                <img class="icon-download" src="assets/images/android.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="tai-ve/Pc"><img class="icon-download" src="assets/images/pc.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="tai-ve/Ios"><img class="icon-download" src="assets/images/ip.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="dangnhap"><img class="icon-download" src="assets/images/napngoc.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div>
                            <img height="12" src="assets/images/12.png" style="vertical-align: middle;">
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
                        <a href="index" class="btn p-1 btn-header-active">Trang chủ</a>
                    </div>
                    <div class="col">
                        <a href="diendan" class="btn p-1 btn-header">Diễn Đàn</a>
                    </div>
                    <?php
                    if (!isset($_SESSION['id']) || !isset($_SESSION['username'])) {
                        echo '
                        <div class="col">
                            <a href="dangky" class="btn p-1 btn-header">Đăng Ký</a>
                        </div>
                        <div class="col">
                            <a href="dangnhap" class="btn p-1 btn-header">Đăng Nhập</a>
                        </div>
                        ';
                    }
                    ?>
                    <?php
                    if (isset($_SESSION['id']) || isset($_SESSION['username'])) {
                        echo '
                        <div class="col">
                            <a href="home" class="btn p-1 btn-header">Người Dùng</a>
                        </div>
                        <div class="col">
                            <a href="logout" class="btn p-1 btn-header">Đăng Xuất</a>
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

        <div class="container color-forum2 pt-2" style="background-color: #BFEFFF">
            <div class="row">
                <div class="col">
                    <h4>Đăng bài</h4>
                    <?php if (isset($_GET['error'])) { ?>
                    <p style="color: red; font-weight: bold;" class="error">
                        <?php echo $_GET['error']; ?>
                    </p>
                    <?php } ?>

                    <?php if (isset($_GET['success'])) { ?>
                    <p style="color: blue; font-weight: bold;" class="success">
                        <?php echo $_GET['success']; ?>
                    </p>
                    <?php } ?>

                    
                    

                    <form id="form" action="dangbai" method="post" autocomplete="off" style="margin-bottom: 10px;">
                        <div class="form-group">
                            <label class="mt-2"> Người đăng:</label>
                            <div class="d-flex">
                                <input class="form-control mt-2 me-2" type="text" name="uname" id="username"
                                    placeholder="<?php echo $_SESSION['username']; ?>" readonly>
                                <input class="form-control mt-2" type="text" name="vnd" id="vnd"
                                    placeholder="<?php include 'get_user_vnd.php'; echo number_format($userVnd); ?> VND"
                                    readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="mt-2"><span class="text-danger">*</span> Tiêu đề:</label>
                            <?php if (isset($_GET['tieude'])) { ?>
                            <input class="form-control mt-2" type="text" name="tieude" id="tieude" maxlength="50"
                                placeholder="Nhập tiêu đề" value="<?php echo $_GET['tieude']; ?>" />
                            <?php } else { ?>
                            <input class="form-control mt-2" type="text" name="tieude" id="tieude" maxlength="50"
                                placeholder="Nhập tiêu đề">
                            <?php } ?>
                        </div>
                        <div class="form-group">
                            <label class="mt-2"><span class="text-danger">*</span> Chọn Avatar:</label>
                            <select class="form-control mt-2" name="avatar" id="avatar">
                                <option value="">Chọn avatar</option>
                                    <option value="1.jpg">Avatar 1</option>
                                    <option value="2.jpg">Avatar 2</option>
                                    <option value="3.jpg">Avatar 3</option>
                                    <option value="4.jpg">Avatar 4</option>
                            </select>
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
                        <div class="form-group">
                            <label class="mt-4"><span class="text-danger">* <i>Lưu ý: cần tối thiểu 2k để đăng
                                        bài!</i></span></label>
                        </div>
                        <div class="mt-2" id="notify" class="text-danger pb-2 font-weight-bold"></div>
                        <button class="btn btn-header form-control" id="btn" type="submit">Đăng bài</button>
                    </form>
                </div>
            </div>
        </div>
        <div class="border-secondary border-top"></div>
        <div class="container pt-4 pb-4 text-white" style="background-color: #BFEFFF">
            <div class="row">
                <div class="col">
                    <div class="text-center">
                        <div style="font-size: 13px" class="text-dark">
                            
                            <small>2023©THANRONG2017</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>