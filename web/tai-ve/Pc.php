<?php
    // Kết nối đến cơ sở dữ liệu
    include "../db_conn.php";

    // Lấy dữ liệu từ bảng taive kieu 2 là pc
    $taive_query = "SELECT * FROM taive WHERE kieu = 2 AND trangthai = 1";
    $taive_result = $conn->query($taive_query);


    session_start();
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
    <link href="../assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="../assets/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <script src="../assets/vendor/jquery/jquery.min.js"></script>
    <script src="../assets/vendor/notify/notify.js"></script>
    <link rel="icon" href="../assets/images/icon.png?v=99">
    <link href="../assets/css/main.css" rel="stylesheet" type="text/css">
    <style>
    .btn-primary {
        border-color: #f44336 !important;
        color: #fff !important;
    }

    .border-primary {
        border-color: #f44336 !important;
    }

    .bg-primary,
    .btn-primary {
        background-color: #f44336 !important;
    }

    .btn-outline-primary:hover {
        background-color: #f44336;
        border-color: #f44336;
    }

    .btn-outline-primary {
        color: #f44336;
        border-color: #f44336;
    }

    .feature-box {
        padding: 38px 30px;
        position: relative;
        overflow: hidden;
        background: #fff;
        box-shadow: 0 0 29px 0 rgb(18 66 101 / 8%);
        transition: all 0.3s ease-in-out;
        border-radius: 8px;
        z-index: 1;
        width: 100%;
    }

    .feature-icon {
        font-size: 1.8em;
        margin-bottom: 1rem;
    }

    .feature-title {
        font-size: 1.2em;
        font-weight: 500;
        padding-bottom: 0.25rem;
        text-decoration: none;
        color: #212529;
    }

    .list-group-item.active {
        background-color: #f44336;
        border-color: #f44336;
    }

    a {
        text-decoration: none;
    }

    .nav-pills .nav-link.active,
    .nav-pills .show>.nav-link {
        background-color: #f44336;
    }

    .nav-link {
        color: #f44336;
    }

    .nav-link:focus,
    .nav-link:hover {
        color: #cd3a2f;
    }

    .copy-text {
        cursor: pointer;
    }
    </style>
    <style>
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        padding: 0;
        display: flex;
        justify-content: center;
        align-items: center;
        min-height: 100vh;
    }

    .game-icon {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 25px;
        height: 25px;
        background-color: #007bff;
        /* Màu xanh */
        border-radius: 50%;
        /* Bo tròn hình ảnh */
        color: white;
        text-decoration: none;
        font-size: 13px;
    }

    .color-changing-text {
        display: flex;
        justify-content: center;
        align-items: center;
    }
    </style>


<style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h4 {
            color: #333;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            border: 1px solid #ddd;
        }

        th, td {
            padding: 15px;
            text-align: center;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f2f2f2;
            font-weight: bold;
            text-transform: uppercase;
        }

        td {
            font-size: 14px;
        }

        a {
            color: #007bff;
            text-decoration: none;
        }

        a:hover {
            text-decoration: underline;
        }
        #snow {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            pointer-events: none;
            z-index: -100;
        }
    </style>

</head>

<body style="    background: linear-gradient(to right, rgb(199 95 95), rgb(124 183 124), rgb(62 62 189));">
<div id="snow"></div>
    <script>
    document.addEventListener('DOMContentLoaded', function() {
        var script = document.createElement('script');
        script.src = 'https://cdn.jsdelivr.net/particles.js/2.0.0/particles.min.js';
        script.onload = function() {
            particlesJS("snow", {
                "particles": {
                    "number": {
                        "value": 75,
                        "density": {
                            "enable": true,
                            "value_area": 400
                        }
                    },
                    "color": {
                        "value": "#63B8FF"
                    },
                    "opacity": {
                        "value": 1,
                        "random": true,
                        "anim": {
                            "enable": false
                        }
                    },
                    "size": {
                        "value": 3,
                        "random": true,
                        "anim": {
                            "enable": true
                        }
                    },
                    "line_linked": {
                        "enable": true
                    },
                    "move": {
                        "enable": true,
                        "speed": 1,
                        "direction": "top",
                        "random": true,
                        "straight": false,
                        "out_mode": "out",
                        "bounce": false,
                        "attract": {
                            "enable": true,
                            "rotateX": 300,
                            "rotateY": 1200
                        }
                    }
                },
                "interactivity": {
                    "events": {
                        "onhover": {
                            "enable": false
                        },
                        "onclick": {
                            "enable": false
                        },
                        "resize": false
                    }
                },
                "retina_detect": true
            });
        }
        document.head.append(script);
    });
    </script>
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

                                setTimeout("farbschrift2()", 40); // Đặt khoảng thời gian đổi màu chậm hơn
                            }

                            text2 = "THANRONG.XYZ";
                            string2array2(text2);
                            divserzeugen2();
                            </script>

                        </h4>&nbsp;&nbsp;<a class="game-icon"><i class="fas fa-check"></i></a>
                    </div></br>
                    <div class="text-center pt-2">
                        <div style="display: inline-block;">
                            <a href="Android">
                                <img class="icon-download" src="../assets/images/android.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="Pc"><img class="icon-download" src="../assets/images/pc.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="Ios"><img class="icon-download" src="../assets/images/ip.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div style="display: inline-block;">
                            <a href="../dangnhap"><img class="icon-download" src="../assets/images/napngoc.png"></a>
                            </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div>
                            <img height="12" src="../assets/images/12.png" style="vertical-align: middle;">
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
                        <a href="../index" class="btn p-1 btn-header-active">Trang chủ</a>
                    </div>
                    <div class="col">
                        <a href="../diendan" class="btn p-1 btn-header">Diễn Đàn</a>
                    </div>
                    <?php
                if (!isset($_SESSION['id']) || !isset($_SESSION['username'])) {
                    echo '
                    <div class="col">
                        <a href="../dangky" class="btn p-1 btn-header">Đăng Ký</a>
                    </div>
                    <div class="col">
                        <a href="../dangnhap" class="btn p-1 btn-header">Đăng Nhập</a>
                    </div>
                    ';
                }
                ?>
                    <?php
                if (isset($_SESSION['id']) || isset($_SESSION['username'])) {
                    echo '
                    <div class="col">
                        <a href="../home" class="btn p-1 btn-header">Người Dùng</a>
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
        <div class="container pt-5 pb-5">
            <div class="col">
                <h4>Phiên bản Pc</h4>
                <?php
                    if ($taive_result->num_rows > 0) {
                        // Tạo bảng HTML để hiển thị thông tin
                        $content = '
                            <table class="table table-bordered">
                                <thead>
                                    <th>Phiên bản</th>
                                    <th>Link tải về</th>
                                </thead>
                                <tbody>';
                
                        // Lặp qua kết quả từ cơ sở dữ liệu và thêm dữ liệu vào bảng HTML nếu kieu = 1
                        while ($row = $taive_result->fetch_assoc()) {
                            $content .= '
                                <tr>
                                    <td><b>' . $row['phienban'] . '</b></td>
                                    <td><a href="' . $row['link'] . '"> Download </a></td>
                                </tr>';
                        }
                
                        $content .= '
                                </tbody>
                            </table>';
                    } else {
                        // Hiển thị thông báo nếu không có phiên bản nào thỏa mãn điều kiện
                        $content = '<p class="text-center">Không có phiên bản nào</p>';
                    }
                
                    // Hiển thị nội dung
                    echo $content;
                
                    // Đóng kết nối cơ sở dữ liệu
                    $conn->close();
                ?>
            </div>
        </div>
        <div class="border-secondary border-top"></div>
        <div class="container pt-4 pb-4 text-white" style="background-color: #BFEFFF">
            <div class="row">
                <div class="col">
                    <div class="text-center">
                        <div style="font-size: 13px" class="text-dark">
                            <small>2023©THANRONG.XYZ</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>