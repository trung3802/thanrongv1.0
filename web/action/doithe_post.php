<?php
// Hàm để tạo mã đơn hàng có 6 ký tự chữ và 2 chữ số
function generateOrderCode() {
    $characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'; // Chuỗi chữ cái
    $digits = '0123456789'; // Chuỗi số

    $random_chars = '';
    for ($i = 0; $i < 9; $i++) {
        $random_chars .= $characters[rand(0, strlen($characters) - 1)];
    }

    $random_digits = '';
    for ($i = 0; $i < 3; $i++) {
        $random_digits .= $digits[rand(0, strlen($digits) - 1)];
    }

    return $random_chars . $random_digits;
}

session_start();

// Lấy dữ liệu từ form
$nhamang = $_POST['nhamang'];
$menhgia = $_POST['menhgia'];
$id = $_SESSION['id'];

// Chêch lệch giữa điểm sự kiện so với mệnh giá tiền. VD 20 điểm = 20.000 VND chêch lệch 1000 lần
$chechlechtien = 100;

// Kiểm tra xem menhgia và nhamang có giá trị và không rỗng
if (empty($menhgia) || empty($nhamang)) {
    $_SESSION['thong_bao'] = array('error', "Vui lòng chọn nhà mạng và mệnh giá.");
    header("Location: ../doithe.php");
    exit();
}

// Tiếp tục với xử lý dữ liệu khác
$conn = mysqli_connect("localhost", "root", "", "vps_acc");

if (!$conn) {
    die("Kết nối database thất bại: " . mysqli_connect_error());
}

// $check_query = "SELECT u.id, u.username, u.character, c.name, c.dataevent, u.vip
//                 FROM user u INNER JOIN `character` c ON u.character = c.id
//                 WHERE u.id = $id";
$check_query = "SELECT u.id, u.username, u.character, c.name, c.dataevent, JSON_UNQUOTE(JSON_EXTRACT(c.InfoChar, '$.VIP')) AS vip
                FROM user u INNER JOIN `character` c ON u.character = c.id
                WHERE u.id = $id";
$result = mysqli_query($conn, $check_query);

if ($result) {
    $row = mysqli_fetch_array($result);

    // Kiểm tra xem đã tạo nhân vật chưa
    if ($row['character'] == 0) {
        $_SESSION['thong_bao'] = array('error', "Chưa tạo nhân vật");
    } else {
        $vipValue = intval($row['vip']); // Chuyển đổi giá trị VIP thành số nguyên
        if ($vipValue < 2) {
            $_SESSION['thong_bao'] = array('error', "Yêu cầu cấp VIP phải là 2");
            header("Location: ../doithe.php");
            exit();
        }
        else{
            // Kiểm tra điều kiện 'dataevent' có đủ so với mệnh giá chọn không
            if ($row['dataevent'] * $chechlechtien < $menhgia) {
                $_SESSION['thong_bao'] = array('error', "Không đủ điểm sự kiện để đổi");
            } else {
                $madonhang = generateOrderCode(); // Tạo mã đơn hàng mới
                $update_query = "UPDATE `character` SET dataevent = dataevent - ('$menhgia' / $chechlechtien) WHERE id = '{$row['character']}'";
                if (mysqli_query($conn, $update_query)) {
                    $insert_query = "INSERT INTO doithe (username, character_id, character_ten, madonhang, menhgia, nhamang, soluong, thoigiantaodonhang, trangthai) 
                                    VALUES ('{$row['username']}', '{$row['character']}', '{$row['name']}', '$madonhang', '$menhgia', '$nhamang', 1, NOW(), 0)";
                    if (mysqli_query($conn, $insert_query)) {
                        $_SESSION['thong_bao'] = array('success', "Đổi thẻ thành công. Xem thẻ tại lịch sử đổi thẻ.");
                    } else {
                        $_SESSION['thong_bao'] = array('error', "Lỗi: " . $update_query . "<br>" . mysqli_error($conn));
                    }
                } else {
                    $_SESSION['thong_bao'] = array('error', "Lỗi: " . $update_query . "<br>" . mysqli_error($conn));
                }
            }
        }
    }
} else {
    $_SESSION['thong_bao'] = array('error', "Lỗi: " . mysqli_error($conn));
}

mysqli_close($conn);
header("Location: ../doithe.php");
?>