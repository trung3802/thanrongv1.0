<?php


// Kết nối đến cơ sở dữ liệu
include "db_conn.php";

// Lấy dữ liệu từ bảng tanthu
$tanthu_query = "SELECT * FROM tanthu";
$tanthu_result = $conn->query($tanthu_query);


//Lấy dữ liệu từ bảng nạp card
$napcard_query = "SELECT n.id, n.uid, n.menhgia, n.created_at, u.username, n.telco
                    FROM napcard n 
                    INNER JOIN user u ON u.id = n.uid  
                    ORDER BY n.created_at DESC LIMIT 10";
$napcard_result = $conn->query($napcard_query);


//Lấy dữ liệu từ bảng nạp tiền
$naptien_query = "SELECT n.id, n.amount, n.time, n.username 
                    FROM naptien n
                    ORDER BY n.time DESC LIMIT 10";
$naptien_result = $conn->query($naptien_query);

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

//Chuyển ký tự thành ...
function shortenString($text, $maxLength = 3) {
    if (strlen($text) > $maxLength) {
        return substr($text, 0, $maxLength) . '******';
    } else {
        return $text;
    }
}

    $content =
        '      
        
        <!--Chạy những lần nạp gần nhât-->
        <p style="font-size: 16px" >
            <font>
                <marquee  onmouseover="if (!window.__cfRLUnblockHandlers) return false; stop()" onmouseout="if (!window.__cfRLUnblockHandlers) return false; start()" direction="left" style="background:white" data-cf-modified-24203b09d1209b35c347da69->
                    ';
                        while ($row = $napcard_result->fetch_assoc()) {
                            $content .= '
                                <b><span class="text-success"> Người chơi ' . shortenString($row['username']) . '</span></b> vừa ủng hộ thẻ ' . $row['telco'] . ' mệnh giá <b> ' . number_format($row['menhgia']) . ' VND</b> vào ' . formatTimeAgo($row['created_at']) . ' <span style="margin-right: 40px"></span>
                            ';
                        }
                    $content .= '
                </marquee>
            </font>
        </p>

        <div class="mt-3 text-center">
                <img src="assets/images/hot.gif"> <a href="gitcode"
                    class="text-dark"><b>Đăng ký ngay để nhận GiftCode Quà Tặng Tân Thủ</b></a> <img src="assets/images/hot.gif">
        </div>
        <div class="mt-3 text-center">
                <img src="assets/images/hot.gif"> <a href="napthe"
                    class="text-dark"><b>Khuyến mãi nạp lần đầu x2</b></a> <img src="assets/images/hot.gif">
        </div></br>
        
        
        
        <p style="font-size: 16px">
            <font>
                <marquee  onmouseover="if (!window.__cfRLUnblockHandlers) return false; stop()" onmouseout="if (!window.__cfRLUnblockHandlers) return false; start()" direction="left" style="background:white" data-cf-modified-24203b09d1209b35c347da69->
                    ';
                        while ($row = $naptien_result->fetch_assoc()) {
                            $content .= '
                                <b><span class="text-success"> Người chơi ' . shortenString($row['username']) . '</span></b> donate <b> ' . number_format($row['amount']) . ' VND</b> vào ' . formatTimeAgo($row['time']) . ' <span style="margin-right: 40px"></span>
                            ';
                        }
                    $content .= '
                </marquee>
            </font>
        </p>


        <div>
            <h5 class="text-danger pt-4">Hướng Dẫn Tân Thủ</h5>
            <div class="table-responsive">
                <table class="table">
                    <tbody>';
                        while ($row = $tanthu_result->fetch_assoc()) {
                            $content .= '
                            <tr>
                                <td><b>' . $row['tieude'] . '</b></td>
                                <td>' . $row['noidung'] . '</td>
                            </tr>';
                        }
                    $content .= '
                    </tbody>

                </table>
            </div>
        </div>


        <div class="border-danger border-top mt-4"></div>
        <div>
            <h5 class="text-danger pt-4" >Update Khung Giờ 1 số boss</h5>
            <div>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Boss</th>
                            <th>Thông Tin</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Gohan Bư</td>
                            <td>Rơi đồ thần linh</td>
                        </tr>
                        <tr>
                            <td>Siêu Cumber</td>
                            <td>Rơi ngọc rồng</td>
                        </tr>
                        <tr>
                            <td>Kingkong Hè</td>
                            <td>Rơi cải trang</td>
                        </tr>
                        <tr>
                            <td>Pic hè</td>
                            <td>Rơi cải trang</td>
                        </tr>
                        <tr>
                            <td>Xên Bọ Hung</td>
                            <td>Rơi ngọc rồng + vàng</td>
                        </tr>
                        <tr>
                            <td>Ma Bư</td>
                            <td>Rơi item x2</td>
                        </tr>
                        <tr>
                            <td>Tàu Pảy Pảy</td>
                            <td>Rơi trứng</td>
                        </tr>
                        <tr>
                            <td>Drabula</td>
                            <td>Rơi đồ thần linh</td>
                        </tr>
                        <tr>
                            <td>Yacon</td>
                            <td>Rơi đồ thần linh</td>
                        </tr>
                        <tr>
                            <td>Dr.lyChee</td>
                            <td>Rơi cải trang</td>
                        </tr>
                        <tr>
                            <td>Super Black Goku</td>
                            <td>Rơi đồ thần linh</td>
                        </tr>
                        <tr>
                            <td>Siêu bọ hung</td>
                            <td>Rơi đồ thần linh</td>
                        </tr>
                        <tr>
                            <td>Fide</td>
                            <td>Rơi ngọc rồng + vàng</td>
                        </tr>
                        <tr>
                            <td>Cooler</td>
                            <td>Rơi đồ thần linh</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        
    ';
    include "layouttrangchu.php";

?>