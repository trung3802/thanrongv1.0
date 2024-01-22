<?php
session_start();

// Kết nối đến cơ sở dữ liệu
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "vps_acc";

$conn = new mysqli($servername, $username, $password, $dbname);

// Kiểm tra kết nối
if ($conn->connect_error) {
    die("Kết nối thất bại: " . $conn->connect_error);
}

// Lấy dữ liệu từ form
$username = $_POST['username'];
$code = $_POST['code'];

// Kiểm tra dữ liệu không được để trống
if (empty($username) || empty($username)) {
    $_SESSION['thong_bao'] = array('error', "Tài khoản và giftcode hệ thống không được để trống");
    header("Location: ../traoquahethong.php");
    exit();
}
// Kiểm tra xem tài khoản có tồn tại không
$query = "SELECT * FROM user WHERE username = '$username'";
$result = mysqli_query($conn, $query);

if (mysqli_num_rows($result) > 0) {
  $row = mysqli_fetch_assoc($result); // Lấy dòng dữ liệu đầu tiên từ kết quả truy vấn
  $uid = $row['id']; // Lấy giá trị uid từ dòng dữ liệu
  $query1 = "SELECT * FROM giftcodeht WHERE code = '$code' AND count = 1";
  $result1 = mysqli_query($conn, $query1);

  if (mysqli_num_rows($result1) > 0) {
    $sql = "INSERT INTO quahethong (uid, code, thoigiannhan) VALUES ('$uid', '$code', NOW())";
    if (mysqli_query($conn, $sql)) {
      $query2 = "UPDATE giftcodeht SET trangthai = 1 WHERE code = '$code'";
      $result2 = mysqli_query($conn, $query2);
      $_SESSION['thong_bao'] = array('success', "Trao quà thành công");
    } else {
        $_SESSION['thong_bao'] = array('error', "Lỗi: " . $updategiftcodett_query . "<br>" . mysqli_error($conn));
    }
  } else {
    $_SESSION['thong_bao'] = array('error', "giftcode hệ thống không tồn tại hoặc đã hết lượt sử dụng");
  }
} else {
  $_SESSION['thong_bao'] = array('error', "Tài khoản không tồn tại");
}

$conn->close();

header("Location: ../traoquahethong.php");
exit();

?>
