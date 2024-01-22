<?php 
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {

    include "db_conn.php";

if (isset($_POST['oldpass']) && isset($_POST['newpass'])
    && isset($_POST['repass'])) {

	function validate($data){
       $data = trim($data);
	   $data = stripslashes($data);
	   $data = htmlspecialchars($data);
	   return $data;
	}

	$oldpass = validate($_POST['oldpass']);
	$newpass = validate($_POST['newpass']);
	$repass = validate($_POST['repass']);
    
    if(empty($oldpass)){
      header("Location: ../doipass.php?error=Vui lòng nhập mật khẩu cũ");
	  exit();
    }else if(empty($newpass)){
      header("Location: ../doipass.php?error=Vui lòng nhập mật khẩu mới");
	  exit();
    }else if($newpass !== $repass){
      header("Location: ../doipass.php?error=Xác nhận lại mật khẩu không trùng khớp với mật khẩu mới");
	  exit();
    }else {
    	// hashing the password
    	// $oldpass = $oldpass;
    	// $newpass = $newpass;
        $id = $_SESSION['id'];

        $sql = "SELECT password
                FROM user WHERE 
                id='$id' AND password='$oldpass'";
        $result = mysqli_query($conn, $sql);
        if(mysqli_num_rows($result) === 1){
        	
        	$sql_2 = "UPDATE user
        	          SET password='$newpass'
        	          WHERE id='$id'";
        	mysqli_query($conn, $sql_2);
        	header("Location: ../doipass.php?success=Đổi mật khẩu thành công");
	        exit();

        }else {
        	header("Location: ../doipass.php?error=Mật khẩu không đúng");
	        exit();
        }

    }

    
}else{
	header("Location: ../doipass.php");
	exit();
}

}
else{
     header("Location: ../login.php");
     exit();
}