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
    $ten_anh = "momo.jpg";
    $anh = "anh/" . $ten_anh;

    $content = '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Nạp momo</h5>
                <div class="center-content">
                    <img class="anhmomo" src="' . $anh . '" style="max-width: 36%; height: auto; margin-bottom: 36px;" alt="MOMO Image">
                    
                    <div>
                        <table>
                            <tr>
                                <td style="width: 120px"><strong>📝Nội Dung CK:</strong></td>
                                <td><input style="border: 1px solid black; color: red" type="text" id="username" value="donate ' . $_SESSION['username'] . '" name="username" class="form-control" readonly></td>                                
                            </tr>                            
                        </table>
                        <br>
                        <table>
                            <tr>
                            <td style="width: 595px"><strong>📞 Phản hồi Admin khi chưa được cộng tiền qua Page or Zalo !</strong></td>
                            </tr>
                        </table>
						 <br>
						 <table>
                            <tr>
                           
							<td style="width: 595px"><strong>* Lưu ý : Min chuyển khoản >= 10.000 VNĐ. Nạp dưới 10.000 VNĐ Ko hoàn trả !</strong></td>
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
