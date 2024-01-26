<?php
    session_start();
?>
<!doctype html>
<html lang="en">

<head>

<meta name="google-adsense-account" content="ca-pub-1313339641046545">
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>

<link rel="shortcut icon" href="//theme.hstatic.net/1000271846/1001087843/14/favicon.png?v=86" type="image/png">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">
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
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Trang Chủ Chính Thức</title>
    <meta name="description" content="">
    <meta name="author" content="">

    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <script src="assets/vendor/jquery/jquery.min.js"></script>
    <script src="assets/vendor/notify/notify.js"></script>
    <link rel="icon" href="assets/images/icon.png?v=99">
    <link href="assets/css/main.css" rel="stylesheet" type="text/css">
    <style>
    @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');

    .banner {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 9999;
        opacity: 0;
        animation: fade-in 0.5s forwards;
    }

    .card {
        max-width: 400px;
        background-color: #fff;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        opacity: 0;
        animation: fade-in 0.5s forwards;
    }

    .card-body {
        padding: 20px;
        font-family: 'Roboto', sans-serif;
    }

    h3 {
        font-size: 24px;
        margin-bottom: 15px;
        color: #333;
    }

    p {
        margin-bottom: 10px;
        color: #555;
    }

    .hide-button {
        text-align: center;
    }

    .btn {
        display: inline-block;
        padding: 10px 20px;
        background-color: #007bff;
        color: #fff;
        border: none;
        border-radius: 5px;
        font-size: 16px;
        cursor: pointer;
        opacity: 0;
        animation: fade-in 0.5s forwards;
    }

    .btn:hover {
        background-color: #0069d9;
    }

    @keyframes fade-in {
        0% {
            opacity: 0;
        }

        100% {
            opacity: 1;
        }
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

    #thongbao p {
        color: black;
    }

    h6.shop.text-center {
        text-transform: uppercase;
        letter-spacing: 0.3em;
        border-bottom: 1px solid #fd7e14;
        margin-bottom: 20px;
    }

    #nutthongbao:hover {
        color: black;
        opacity: 0.8;
    }
    </style>

    <script>
    document.addEventListener("DOMContentLoaded", function() {
        var banner = document.querySelector(".banner");
        var hideButton = document.querySelector(".hide-button");

        // Hiển thị banner khi trang web tải xong
        banner.style.visibility = "visible";

        // Ẩn banner khi nhấp vào nút ẩn
        hideButton.addEventListener("click", function() {
            banner.style.display = "none";
        });
    });
    </script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

    <style>
    /* Your CSS styles go here */
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        padding: 0;
        background-color: #f2f2f2;
        border-radius: 15px;
        /* Rounded corners for the body */
    }

    .global-fixed-group {
        position: fixed;
        bottom: 0;
        right: 0;
        padding: 10px;
        background-color: #E8E8E8;
        display: flex;
        flex-direction: column;
        align-items: flex-end;
        transform: translateX(100%);
        /* Initial hide */
        transition: transform 0.3s ease-in-out;
        border-radius: 15px 0 0 0;
        /* Rounded corners for the top-left corner */
    }

    .show-group {
        transform: translateX(0);
        /* Show when scrolling down */
    }

    .contact-box-wrapper {
        display: flex;
        align-items: center;
        margin-bottom: 15px;
        font-size: 18px;
        color: #0000FF;
        text-decoration: none;
    }

    .contact-icon-box {
        font-size: 24px;
        margin-right: 10px;
    }

    .contact-info {
        font-size: 14px;
    }



    /* Chat Box styles */
    .chat-box {
        position: fixed;
        bottom: 0;
        right: 0;
        padding: 10px;
        background-color: #fff;
        border: 1px solid #ccc;
        border-radius: 15px 0 0 0;
        /* Rounded corners for the top-left corner */
        display: none;
        z-index: 9999;
    }

    .close-button {
        font-size: 20px;
        cursor: pointer;
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

        .verification-text {
            color: #007bff; /* Màu xanh */
            font-size: 18px;
            opacity: 0;
            position: absolute;
            top: 0;
            left: calc(100% + 10px);
            transition: opacity 0.3s;
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

    <!--Hiệu ứng chữ -->
    <script type="24203b09d1209b35c347da69-text/javascript">
    $(document).ready(function() {
        $(".form-control").tooltip({
            placement: 'right'
        });
    });
    </script>

    <div class="banner">
        <div class="card" style="background-color: pink">
            <div class="card-body">

                <!--Bắt đầu Hiệu ứng chữ đổi màu liên tục----------------------------------------------->
                <h6 class="shop text-center">
                    <script type="text/javascript" data-cfasync="false">
                    farbbibliothek = new Array();
                    farbbibliothek[0] = new Array("#824800", "#824800", "#824800", "#824800", "#824800", "#925100",
                        "#a05900", "#b56500", "#c36d00", "#d27703", "#e48000", "#ff8f00", "#ff9209", "#ffb900",
                        "#ffdc00", "#ffb900", "#ff9209", "#e48000", "#d27703", "#c36d00", "#b56500", "#a05900",
                        "#925100", "#824800", "#824800", "#824800", "#824800", "#824800");
                    farbbibliothek[1] = new Array("#824800", "#824800", "#824800", "#824800", "#824800", "#925100",
                        "#a05900", "#b56500", "#c36d00", "#d27703", "#e48000", "#ff8f00", "#ff9209", "#ffb900",
                        "#ffdc00", "#ffb900", "#ff9209", "#e48000", "#d27703", "#c36d00", "#b56500", "#a05900",
                        "#925100", "#824800", "#824800", "#824800", "#824800", "#824800");

                    farben = farbbibliothek[0];

                    function farbschrift() {
                        for (var i = 0; i < Buchstabe.length; i++) {
                            document.all["a" + i].style.color = farben[i];
                        }
                        farbverlauf();
                    }

                    function string2array(text) {
                        Buchstabe = new Array();
                        while (farben.length < text.length) {
                            farben = farben.concat(farben);
                        }
                        k = 0;
                        while (k <= text.length) {
                            Buchstabe[k] = text.charAt(k);
                            k++;
                        }
                    }

                    function divserzeugen() {
                        for (var i = 0; i < Buchstabe.length; i++) {
                            document.write("<span id='a" + i + "' class='a" + i + "'>" + Buchstabe[i] + "<\/span>");
                        }
                        farbschrift();
                    }
                    var a = 1;

                    function farbverlauf() {
                        for (var i = 0; i < farben.length; i++) {
                            farben[i - 1] = farben[i];
                        }
                        farben[farben.length - 1] = farben[-1];

                        setTimeout("farbschrift()", 60);
                    }
                    var farbsatz = 1;

                    function farbtauscher() {
                        farben = farbbibliothek[farbsatz];
                        while (farben.length < text.length) {
                            farben = farben.concat(farben);
                        }
                        farbsatz = Math.floor(Math.random() * (farbbibliothek.length - 0.0001));
                    }
                    setInterval("farbtauscher()", 60000);

                    text = "THANRONG - THÔNG BÁO";
                    string2array(text);
                    divserzeugen();
                    </script>
                    </h4>
                    <!--Kết thúc ứng chữ đổi màu liên tục----------------------------------------------->

                    <div class="mb-3" id="thongbao">
                        <p><b>Wedsite giao diện thân thiện dễ sử dụng!</b></p>
                        <p><b>Game có độ trải nghiệm cao</b></p>
                        <p><b>Bấm đồng ý và chấp nhận các điều khoản!</b></p>
                    </div>
                    <div class="hide-button">
                        <button id="nutthongbao" class="btn btn-primary" style="background: #924c31">Đồng ý</button>
                    </div>
            </div>
        </div>
    </div>

    <div class="container" style="border-radius: 15px; background: #BFEFFF; padding: 0px">
        <div class="container" style="background-color: #BFEFFF">
            <div class="row bg pb-3 pt-2">
                <div class="col">


                    <!-- // code mới -->

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

                            text2 = "THANRONG";
                            string2array2(text2);
                            divserzeugen2();
                            </script>

                        </h4>&nbsp;&nbsp;
                        <a class="game-icon"><i class="fas fa-check"></i></a>
                        
                    </div></br>
                    




                    <!-- kết thúc -->
                    <div class="text-center pt-4 ">
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
                            <a href="home"><img class="icon-download" src="assets/images/napngoc.png"></a> </br>
                            <small class="text-dark">0.0.1</small>
                        </div>
                        <div>
                            <img height="12" src="assets/images/12.png" style="vertical-align: middle;">
                            <small style="font-size: 15px" id="hour3">Dành cho người chơi trên 12 tuổi. Chơi quá 180
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
        <div class="container">
            <?php echo $content ?>
        </div>
    </div>




    <div class="global-fixed-group" id="globalGroup">
        <a href="javascript:void(0)" id="js-close" class="global-close" onclick="toggleGlobalGroup()"><i
                class='	far fa-times-circle' style='font-size:30px'></i></a>
        
        <a href="#" target="_blank"
            class="contact-box-wrapper animate__backInRight">
            <span class="contact-icon-box icons icon-youtube"><i class="fab fa-youtube"></i></span>
            <div class="contact-info">
                <b>Xem trên Youtube</b>
            </div>
        </a>
        <a href="https://t.me/+o5_G2k2QNpZkOGU1" target="_blank" class="contact-box-wrapper animate__backInRight">
            <span class="contact-icon-box icons icon-facebook"><i class="far fa-comment-dots"></i></span>
            <div class="contact-info">
                <b>Box TELE</b>
            </div>
        </a>
        <a class="contact-box-wrapper nut-chat-facebook" href="#" rel="nofollow"
            target="_blank">
            <span class="contact-icon-box icons icon-facebook"><i class="fab fa-facebook"></i></span>
            <div class="contact-info">
                <b>Chat Fb (8h00 - 21h00)</b>
            </div>
        </a>
        <a class="contact-box-wrapper nut-chat-zalo" href="#" rel="nofollow"
            target="_blank">
            <span class="far fa-comment-dots"></span> <!-- Sử dụng class icon-zalo ở đây -->
            <div class="contact-info">
                &nbsp;&nbsp;<b>Zalo (8h00 - 21h00)</b>
            </div>
        </a>
        <a class="contact-box-wrapper nut-chat-facebook" href="#" rel="nofollow"
            target="_blank">
            <span class="contact-icon-box icons icon-facebook"><i class="fab fa-facebook"></i></span>
            <div class="contact-info">
                <b>Page FB </b>
            </div>
        </a>
        
        <!-- Your contact links here -->
        <a href="javascript:void(0)" id="js-goTop" class="global-goTop"
            onclick="$('body,html').animate({scrollTop:0},800);"><i class='fas fa-arrow-circle-up'
                style='font-size:30px'></i></a>

    </div>





    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script>
    let globalGroup = document.getElementById("globalGroup");
    let toggleGlobalGroup = () => {
        if (globalGroup.style.display === "none") {
            globalGroup.style.display = "block";
        } else {
            globalGroup.style.display = "none";
        }
    };

    let scrollThreshold = 100;

    window.addEventListener("scroll", function() {
        if (window.scrollY > scrollThreshold) {
            globalGroup.classList.add("show-group");
        } else {
            globalGroup.classList.remove("show-group");
        }
    });

    // Toggle chat box visibility
    let chatBox = document.getElementById("chatBox");
    let closeChat = document.getElementById("closeChat");

    closeChat.addEventListener("click", function() {
        chatBox.style.display = "none";
        document.body.style.overflow = "auto"; // Allow scrolling
    });

    // Toggle chat box visibility and disable scrolling
    let toggleChat = document.getElementById("toggleChat");

    toggleChat.addEventListener("click", function() {
        chatBox.style.display = "block";
        document.body.style.overflow = "hidden"; // Prevent scrolling
    });
    </script>



    <div class="border-secondary border-top"></div>
    <div class="container pt-4 pb-4 text-white" style="background-color: #BFEFFF">
        <div class="row">
            <div class="col">
                <div class="text-center">
                    <div style="font-size: 13px" class="text-dark">
                        <small>2024©THANRONG</small>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>

</body>
<!-- Bootstrap core JavaScript -->
<script src="asset/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="asset/main.js"></script>