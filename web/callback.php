<?php 
session_start();
if (isset($_GET['request_id']) && isset($_GET['status'])) {
    include "db_conn.php";
    function validate($data){
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }

    $status = validate($_GET['status']);
    $request_id = validate($_GET['request_id']);
    $sql = "SELECT * FROM napcard WHERE magd='$request_id' AND status='0'";
    $result = mysqli_query($conn, $sql);
    
    if (mysqli_num_rows($result) === 1) {
        $row = mysqli_fetch_assoc($result);
        $tien = $row['menhgia'];

        // Truy vấn để kiểm tra giá trị 'tongnap' trong bảng 'user'
        $uid = $row['uid'];
        $sql_tongnap = "SELECT tongnap FROM user WHERE id='$uid'";
        $result_tongnap = mysqli_query($conn, $sql_tongnap);
        $row_tongnap = mysqli_fetch_assoc($result_tongnap);
        $tongnap_user = $row_tongnap['tongnap'];

        // // Kiểm tra và xử lý
        if ($status == 1 && $tongnap_user == 0) {
            $tien *= 2; // Nhân 3 lần đầu
         }

if($status == 1) {
        $sql_2 = "UPDATE napcard
                  SET status='1'
                  WHERE magd='$request_id'";

        $sql_3 = "UPDATE user
                  SET vnd = vnd + '$tien', tongnap = tongnap + '$tien'
                  WHERE id='$uid'";
				  mysqli_query($conn, $sql_3); // Cập nhật user trước
} else {
            $sql_2 = "UPDATE napcard
        	          SET status='2'
        	          WHERE magd='$request_id'";
        	  
        }
        
        mysqli_query($conn, $sql_2); // Cập nhật napcard sau

    } else {
        exit();
    }
}
?>
