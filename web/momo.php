<head>
<meta name="google-adsense-account" content="ca-pub-1313339641046545">
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>
    <title>MOMO</title>
    <style>
        img {
            display: block;
            margin: 0 auto;
        }
        .center-text {
            text-align: center;
        }
    </style>
    <!-- Add other CSS links if needed -->
</head>
<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $name = $_SESSION['username'];

    // Đường dẫn tới file ảnh
    $ten_anh = "momo1.jpg";
    $anh = "anh/" . $ten_anh;

    $content = '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Bước 1: Quét Mã QR</h5>
                <div class="center-content">
                    <img class="anhmomo" src="' . $anh . '" style="max-width: 36%; height: auto; margin-bottom: 36px;" alt="MOMO Image">
                    
                    <div>

                        
                        <h5 class="card-title fw-semibold mb-4">Bước 2: Nhập số tiền</h5>
                        <table>
                            <tr>
                            <h5 class="card-title fw-semibold mb-4">Bước 3: Nhập đúng nội dung bên dưới</h5>
                              
                                <td><input style="border: 1px solid black; color: red" type="text" id="username" value="donate ' . $_SESSION['username'] . '" name="username" class="form-control" readonly></td>                                
                            </tr>                            
                        </table>
                        <br>
                        <h5 class="card-title fw-semibold mb-4">Bước 4: Đợi giao dịch cộng tiền tự động</h5>
                        <table>
                            <tr>
                            <td style="width: 595px"><strong>📞 Phản hồi Admin khi chưa được cộng tiền qua Page or Zalo !</strong></td>
                            </tr>
                        </table>
						 <br>
						<table>
                            <tr>
                           
							<td style="width: 675px; color:red"><strong>* Lưu ý : Min chuyển khoản >= 10.000 VNĐ !</strong></td>
                            </tr>
                        </table>
                    </div>

                    
                </div>
            </div>
        </div>
    </div>

    <style>
        @media (min-width: 936px) {
            /* Apply styles for screens with a width of 936px or larger (desktop) */
            .anhmomo {
                max-width: 60%; /* You can adjust this value for your desired image size */
            }
        }
    </style>
    ';
    include "layout.php";
} else {
    header("Location: login");
    exit();
}
?>
