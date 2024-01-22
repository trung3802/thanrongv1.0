<?php 
session_start(); 
include "db_conn.php";

if (isset($_POST['uname']) && isset($_POST['noidung']) && isset($_POST['id'])) {

    $uname = $_SESSION['username'];
    $noidung = $_POST['noidung'];
    $id = $_POST['id'];

	$noidung_data = 'noidung=' . $noidung;
    $id_data = 'id=' . $id;
    
    if (empty($uname)) {
        header("Location: chitietdiendan?error=Vui lòng đăng nhập&$id_data&$noidung_data");
        exit();
    }else if(empty($noidung) || $noidung == ""){
        header("Location: chitietdiendan?error=Vui lòng nhập nội dung&$id_data&$noidung_data");
        exit();
    }else if (strlen($noidung) > 255) {
        header("Location: chitietdiendan?error=Nội dung chỉ được tối đa 255 ký tự&$id_data&$noidung_data");
        exit();
    }
    else {

        //Đếm số bình luận trên 1 diễn đàn
        $dembinhluan_query = "SELECT COUNT(id) 
                                FROM diendan
                                WHERE diendancha = $id";
        $dembinhluan_result = $conn->query($dembinhluan_query);

        $row = $dembinhluan_result->fetch_assoc();

        //Kiểm tra số lượng bình luận
        if ($row['COUNT(id)'] <= 30) {

            // Xử lý lưu dữ liệu vào cơ sở dữ liệu
            $sql = "INSERT INTO diendan (noidung, userid, created_at, diendancha) VALUES('$noidung', '{$_SESSION['id']}', NOW(), '$id')";
            $result = mysqli_query($conn, $sql);

            if ($result) {
                header("Location: chitietdiendan?success=Bình luận thành công&$id_data");
                exit();
            } else {
                header("Location: chitietdiendan?error=Có lỗi xảy ra khi bình luận&$id_data");
                exit();
            }
        } else {
            header("Location: chitietdiendan?error=Đã vượt quá số lượng bình luận&$id_data");
            exit();
        }
    }
    
} else {
    header("Location: dangnhap");
    exit();
}
?>
