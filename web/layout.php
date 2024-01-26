<!doctype html>

<html lang="en">
<?php
// Assuming you have established a database connection
// and retrieved the user ID
$userID = $_SESSION['id'];


// Perform a database query to get the user's role based on the ID
// Here is a basic example assuming you are using MySQL and mysqli
$connection = mysqli_connect("localhost", "root", "", "vps_acc");
$query = "SELECT role FROM user WHERE id = $userID";
$result = mysqli_query($connection, $query);

if ($result) {
    $row = mysqli_fetch_assoc($result);
    $userRole = $row['role'];

    // ... (rest of your code)
} else {
    echo "Error querying database: " . mysqli_error($connection);
}

mysqli_close($connection);
?>

<head>
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>
<meta name="google-adsense-account" content="ca-pub-1313339641046545">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Người Dùng</title>
    
    <link rel="shortcut icon" href="//theme.hstatic.net/1000271846/1001087843/14/favicon.png?v=86" type="image/png">

    <link rel="shortcut icon" type="image/png" href="assets/images/logos/favicon.png" />
    <link rel="stylesheet" href="assets/css/styles.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
        integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
        integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />

    <style>
    body::before {
        content: "";
        /* background-image: url('anh/1.png'); */

        background-size: cover;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        filter: blur(5px);
        /* Điều chỉnh độ mờ tùy ý */
        z-index: -1;
    }

    /* Hiển thị header khi màn hình dưới 1198px */
    @media (max-width: 1198px) {
        .app-header {
            display: block;
        }
    }

    /* Ẩn header khi màn hình trên 1198px */
    @media (min-width: 1199px) {
        .app-header {
            display: none;
        }
    }
    #myVideo {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover; /* Thay thế background-size: cover */
  filter: blur(5px);
  /* Điều chỉnh độ mờ tùy ý */
  z-index: -1;
}

    </style>



</head>

<body>
<video autoplay loop muted playsinline  id="myVideo">
  <source src="assets/images/logo.mp4" type="video/mp4">
  
</video>


    <!--  Body Wrapper -->
    <div class="page-wrapper" id="main-wrapper" data-layout="vertical" data-navbarbg="skin6" data-sidebartype="full"
        data-sidebar-position="fixed" data-header-position="fixed">
        <!-- Sidebar Start -->
        <aside class="left-sidebar" style="    background: linear-gradient(45deg, rgb(231 136 136), rgb(146 225 146), rgb(156 156 233));">
            <!-- Sidebar scroll-->
            <div>
                <div class="brand-logo d-flex align-items-center justify-content-between">
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
                                    document.all["b" + i].style.fontSize = "30px"; // Đặt kích thước chữ to hơn
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

                                setTimeout("farbschrift2()", 40); // Đặt khoảng thời gian đổi màu chậm hơn
                            }

                            text2 = "THANRONG";
                            string2array2(text2);
                            divserzeugen2();
                            </script>
                        </h4>
                    </div></br>
                    <div class="close-btn d-xl-none d-block sidebartoggler cursor-pointer" id="sidebarCollapse">
                        <i class="ti ti-x fs-8"></i>
                    </div>
                </div>
                <!-- Sidebar navigation-->
                <nav class="sidebar-nav scroll-sidebar" data-simplebar="">
                    <ul id="sidebarnav">
                        <!-- Menu cấp 2 cho Trang Chủ -->
                        <li class="nav-small-cap">
                            <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                            <a href="javascript:void(0)" aria-expanded="false" id="trangchuLink">
                                <span class="hide-menu">Thông Tin Chung</span>
                                <i class="fas fa-chevron-down" style="margin-left: 15px;"></i>
                            </a>
                            <ul id="trangchuSubmenu" style="display: none;">
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="home" aria-expanded="false">
                                        <span>
                                            <i class="fas fa-home"></i>
                                        </span>
                                        <span class="hide-menu">Thông Tin</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <span class="sidebar-link">
                                        <span>
                                            <i class="fas fa-credit-card"></i>
                                        </span>
                                        <span class="hide-menu">
                                            Số Dư: <?php include 'get_user_vnd.php'; echo number_format($userVnd); ?>
                                        </span>
                                    </span>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="gitcode" aria-expanded="false">
                                        <span>
                                            <i class="fas fa-bookmark"></i>
                                        </span>
                                        <span class="hide-menu">Gitcode</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="lichsugiftcode" aria-expanded="false">
                                        <span>
                                            <i class="fa-solid fa-money-bill-trend-up"></i>
                                        </span>
                                        <span class="hide-menu">Gitcode nhập</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="quahethong" aria-expanded="false">
                                        <span>
                                            <i class="fa-solid fa-money-bill-trend-up"></i>
                                        </span>
                                        <span class="hide-menu">Quà hệ thống</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="lichsutraocode" aria-expanded="false">
                                        <span>
                                            <i class="fas fa-credit-card"></i>
                                        </span>
                                        <span class="hide-menu">Lịch sử nhận gitcode</span>
                                    </a>
                                </li>
                                <!-- Các phần tử menu cấp 2 cho Trang Chủ -->
                            </ul>
                        </li>

                        <!-- Các phần tử menu cấp 2 cho Trang Chủ -->
                        <li class="nav-small-cap">
                            <i class="ti ti-dots nav-small-cap-icon fs-4"></i>

                            <a href="javascript:void(0)" aria-expanded="false" id="chucnangLink">
                                <span class="hide-menu">Chức Năng</span>
                                <i class="fas fa-chevron-down" style="margin-left: 14px;"></i>
                            </a>
                            <ul id="chucnangSubmenu" style="display: none;">

                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="doipass" aria-expanded="false">
                                        <span>
                                            <i class="fas fa-lock"></i>
                                        </span>
                                        <span class="hide-menu">Đổi Mật Khẩu</span>
                                    </a>
                                </li>
                                
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="napthe" aria-expanded="false">
                                        <span>
                                            <i class="fas fa-credit-card"></i>
                                        </span>
                                        <span class="hide-menu">Nạp Thẻ</span>
                                    </a>
                                </li>


                        </li>






                        <!--<li class="sidebar-item">
                            <a class="sidebar-link" href="mb" aria-expanded="false">
                                <span>
                                    <i class="fas fa-credit-card"></i>
                                </span>
                                <span class="hide-menu">Nạp Ngân Hàng</span>
                            </a>
                        </li>-->
                        <li class="sidebar-item">
                            <a class="sidebar-link" href="momo" aria-expanded="false">
                                <span>
                                    <i class="fas fa-credit-card"></i>
                                </span>
                                <span class="hide-menu">Nạp MOMO</span>
                            </a>
                        </li>

                        <li class="sidebar-item">
                            <a class="sidebar-link" href="rechargehistory" aria-expanded="false">
                                <span>
                                    <i class="fa-solid fa-money-bill-trend-up"></i>
                                </span>
                                <span class="hide-menu">Lịch sử ủng hộ</span>
                            </a>
                        </li>

                        <li class="sidebar-item">
                            <a class="sidebar-link" href="lichsuchuyenkhoan" aria-expanded="false">
                                <span>
                                    <i class="fa-solid fa-money-bill-trend-up"></i>
                                </span>
                                <span class="hide-menu">Lịch sử donate</span>
                            </a>
                        </li>
                        <!-- Thêm các phần tử menu cấp 2 khác tại đây -->
                    </ul>
                    </li>
                    <li class="nav-small-cap">
                        <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                        <a href="javascript:void(0)" aria-expanded="false" id="tienichLink">
                            <span class="hide-menu">Tiện Ích</span>
                            <i class="fas fa-chevron-down" style="margin-left: 35px;"></i>
                        </a>
                        <ul id="tienichSubmenu" style="display: none;">
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="diendan" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Đăng Bài</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="index" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-home"></i>
                                    </span>
                                    <span class="hide-menu">Trang Chủ</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="paytop" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-credit-card"></i>
                                    </span>
                                    <span class="hide-menu">Đua TOP Nạp</span>
                                </a>
                            </li>

                            <li class="sidebar-item">
                                <a class="sidebar-link" href="doithe" aria-expanded="false">
                                    <span>
                                        <i class="fa-solid fa-money-bill-trend-up"></i>
                                    </span>
                                    <span class="hide-menu">Đổi thẻ</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="lichsudoithe" aria-expanded="false">
                                    <span>
                                        <i class="fa-solid fa-money-bill-trend-up"></i>
                                    </span>
                                    <span class="hide-menu">Lịch sử đổi thẻ</span>
                                </a>
                            </li>
                        </ul>
                    </li>



                    <!-- tiện ích admin--><?php if ($userRole == 1): ?>
                    <li class="nav-small-cap">
                        <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                        <a href="javascript:void(0)" aria-expanded="false" id="adminLink">
                            <span class="hide-menu">Admin</span>
                            <i class="fas fa-chevron-down" style="margin-left: 35px;"></i>
                        </a>
                        <ul id="adminSubmenu" style="display: none;">
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="tanthu" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Đăng bài</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="traocode" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Trao code</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="traoquahethong" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Trao quà hệ thống</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="adtaive" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Quản lý tải về</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="adtuyetchieu" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Sửa tuyệt chiêu</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="adcard" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">DS Nạp Card</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="adedit" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Sửa Admin</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="adhienthi" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Show Admin</span>
                                </a>
                            </li>
                            <li class="sidebar-item">
                                <a class="sidebar-link" href="addiendan" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Đăng diễn đàn</span>
                                </a>
                            </li>


                            <li class="sidebar-item">
                                <a class="sidebar-link" href="adlayID" aria-expanded="false">
                                    <span>
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="hide-menu">Lấy ID</span>
                                </a>
                            </li>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adsuaID" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Sửa ID</span>
                        </a>
                    </li>
                    </ul>
                    </li>
                    <li class="nav-small-cap">
                        <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                        <a href="javascript:void(0)" aria-expanded="false" id="tienich1Link">
                            <span class="hide-menu">Admin Nạp</span>
                            <i class="fas fa-chevron-down" style="margin-left: 35px;"></i>
                        </a>
                        <ul id="tienich1Submenu" style="display: none;">
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adnaptien" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Nạp Tiền</span>
                        </a>
                    </li>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adnaptienbatki" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Nạp SLL</span>
                        </a>
                    </li>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adpaymentday" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Nạp DAY</span>
                        </a>
                    </li>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adlock" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Khóa TK</span>
                        </a>
                    </li>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adip" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Check IP</span>
                        </a>
                    </li>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="adxoaIP" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Xóa IP</span>
                        </a>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="duyetthe" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Duyệt thẻ</span>
                        </a>
                    </li>
                    <li class="sidebar-item">
                        <a class="sidebar-link" href="lichsutraothe" aria-expanded="false">
                            <span>
                                <i class="fas fa-pencil-alt"></i>
                            </span>
                            <span class="hide-menu">Lịch sử trao thẻ</span>
                        </a>
                    </li>
                    </ul>
                    </li><?php endif; ?>
                    <!-- end tiện ích admin -->

                    <!-- đăng xuất -->
                    </br>
                    <li class="sidebar-item">
                        <a href="action/logout" class="btn btn-outline-primary mx-3 mt-2 d-block">Đăng Xuất</a>
                    </li>
                    </ul>
                </nav>
                <script>
                $(document).ready(function() {
                    $("#chucnangLink").click(function() {
                        $("#chucnangSubmenu").slideToggle();
                        $("#trangchuSubmenu, #tienichSubmenu, #adminSubmenu,#tienich1Submenu")
                            .slideUp();
                    });

                    $("#trangchuLink").click(function() {
                        $("#trangchuSubmenu").slideToggle();
                        $("#chucnangSubmenu, #tienichSubmenu, #adminSubmenu,#tienich1Submenu")
                            .slideUp();
                    });

                    $("#tienichLink").click(function() {
                        $("#tienichSubmenu").slideToggle();
                        $("#chucnangSubmenu, #trangchuSubmenu, #adminSubmenu,#tienich1Submenu")
                            .slideUp();
                    });

                    $("#adminLink").click(function() {
                        $("#adminSubmenu").slideToggle();
                        $("#chucnangSubmenu, #tienichSubmenu, #trangchuSubmenu,#tienich1Submenu")
                            .slideUp();
                    });
                    $("#tienich1Link").click(function() {
                        $("#tienich1Submenu").slideToggle();
                        $("#chucnangSubmenu, #trangchuSubmenu, #adminSubmenu,#tienichSubmenu")
                            .slideUp();
                    });
                });
                </script>

                <!-- End Sidebar navigation -->
            </div>
            <!-- End Sidebar scroll-->
        </aside>
        <!--  Sidebar End -->
        <!--  Main wrapper -->
        <div class="body-wrapper">
            <!--  Header Start -->
            <header class="app-header">
                <nav class="navbar navbar-expand-lg navbar-light">
                    <ul class="navbar-nav">
                        <li class="nav-item d-block d-xl-none">
                            <a class="nav-link sidebartoggler nav-icon-hover" id="headerCollapse"
                                href="javascript:void(0)">
                                <i class="ti ti-menu-2"></i>
                            </a>
                        </li>
                    </ul>
                </nav>
            </header>
            <!--  Header End -->
            <?php if (isset($content)) echo $content; ?>
            <!-- <?php echo $content ?> -->
        </div>

    </div>

    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="assets/libs/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="assets/js/sidebarmenu.js"></script>
    <script src="assets/js/app.min.js"></script>
    <script src="assets/libs/apexcharts/dist/apexcharts.min.js"></script>
    <script src="assets/libs/simplebar/dist/simplebar.js"></script>
    <script src="assets/js/dashboard.js"></script>
</body>

</html>