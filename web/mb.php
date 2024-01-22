<head>
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
    $ten_anh = "mb.jpg";
    $anh = "anh/" . $ten_anh;

    $content = '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Nạp ngân hàng</h5>
                <div class="center-content">
                    <img class="anhmomo" src="' . $anh . '" style="max-width: 50%; height: auto; margin-bottom: 50px;" alt="MOMO Image">
                    
                    <div>
                        <table>
                            <tr>
                                <td style="width: 195px"><strong>☑️ Nội dung chuyển khoản:</strong></td>
                                <td><input type="text" id="username" value="naptien ' . $_SESSION['username'] . '" name="username" class="form-control" readonly></td>                                
                            </tr>                            
                        </table>
                        <br>
                        <table>
                            <tr>
                            <td style="width: 595px"><strong>☑️ Liên hệ key vàng trong box chat zalo để xử lý giao dịch sau 30 phút chuyển khoản!</strong></td>
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
    header("Location: login.php");
    exit();
}
?>
