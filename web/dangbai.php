<?php 
session_start(); 
include "db_conn.php";

if (isset($_POST['uname']) && isset($_POST['noidung']) && isset($_POST['tieude'])) {

    $uname = $_SESSION['username'];
    $noidung = $_POST['noidung'];
    $tieude = $_POST['tieude'];
    $avatar = $_POST['avatar'];

    if (empty($avatar)) {
        $avatar = '1.jpg';
    }

	$noidung_data = 'noidung=' . $noidung;
	$tieude_data = 'tieude=' . $tieude;

    //truy vấn tiền trong cơ sở dữ liệu
    $sqlvnd = "SELECT vnd FROM user WHERE id = '{$_SESSION['id']}'";
    $kiemtravnd = mysqli_query($conn, $sqlvnd);

    $row = mysqli_fetch_assoc($kiemtravnd);
    $vnd = $row['vnd'];

    if (empty($uname)) {
        header("Location: nhapdiendan?error=Vui lòng đăng nhập&$noidung_data&$tieude_data");
        exit();
    }else if(empty($tieude) || $tieude == "") {
        header("Location: nhapdiendan?error=Vui lòng nhập tiêu đề&$noidung_data&$tieude_data");
        exit();
    }else if (strlen($tieude) > 50) {
        header("Location: nhapdiendan?error=Tiêu đề chỉ được tối đa 50 ký tự&$noidung_data&$tieude_data");
        exit();
    }else if(empty($noidung) || $noidung == ""){
        header("Location: nhapdiendan?error=Vui lòng nhập nội dung&$noidung_data&$tieude_data");
        exit();
    }else if (strlen($noidung) > 255) {
        header("Location: nhapdiendan?error=Nội dung chỉ được tối đa 255 ký tự&$noidung_data&$tieude_data");
        exit();
    }else if ($vnd < 2000) {
        header("Location: nhapdiendan?error=Không đủ tiền để đăng bài, vui lòng nạp thêm tiền&$noidung_data&$tieude_data");
        exit();
    }
    else {
        // Xử lý lưu dữ liệu vào cơ sở dữ liệu
        $sql = "INSERT INTO diendan (tieude, noidung, userid, created_at, diendancha, avatar, admin) VALUES('$tieude', '$noidung', '{$_SESSION['id']}', NOW(), 0, '$avatar', 0)";
        $sql1 = "UPDATE user SET vnd = vnd - 2000 WHERE id = {$_SESSION['id']}";
        $result = mysqli_query($conn, $sql);
        $result = mysqli_query($conn, $sql1);
        if ($result) {
            header("Location: diendan?success=Đăng bài thành công");
            exit();
        } else {
            header("Location: nhapdiendan?error=Có lỗi xảy ra khi đăng bài&$noidung_data&$tieude_data");
            exit();
        }
    }
    
} else {
    header("Location: dangnhap");
    exit();
}
?>
