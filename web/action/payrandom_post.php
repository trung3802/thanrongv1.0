<?php

// Lấy dữ liệu từ form
$username = $_POST['username'];
$amount = $_POST['amount'];

// Kết nối database
$conn = mysqli_connect("localhost", "root", "", "vps_acc");

// Kiểm tra xem tài khoản có tồn tại không
$query = "SELECT * FROM user WHERE username = '$username'";
$result = mysqli_query($conn, $query);

if (mysqli_num_rows($result) > 0) {
  // Nếu tài khoản tồn tại, cập nhật số tiền trong bảng user
  $row = mysqli_fetch_assoc($result);
  $vnd = $row['vnd'];
  $tongnap = $row['tongnap'];
  if ($tongnap == 0) {
    $new_balance = $vnd + $amount *3;
    $new_balance1 = $tongnap + $amount *3;
  }else{
    $new_balance = $vnd + $amount;
    $new_balance1 = $tongnap + $amount;
  }
  // thêm tiền vào bảng
  $update_query = "UPDATE user SET vnd = '$new_balance' , tongnap='$new_balance1' WHERE username = '$username'";
  // thêm dữ liệu vào bảng lịch sử nạp tiền
  $sql = "INSERT INTO naptien (username, amount, time)VALUES ('$username', '$amount', NOW())";
  
  mysqli_query($conn, $update_query);
    mysqli_query($conn, $sql);

  // Hiển thị thông báo nạp tiền thành công và nút quay lại trang chủ
  echo "<script>alert('Nạp tiền thành công!'); window.location = '../adnaptienbatki.php';</script>";
} else {
  // Nếu tài khoản không tồn tại, hiển thị thông báo lỗi và nút quay lại trang chủ
  echo "<script>alert('Tài khoản không tồn tại!'); window.location = '../adnaptienbatki.php';</script>";
}

mysqli_close($conn);
?>