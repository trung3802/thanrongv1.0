<?php 
session_start(); 
include "db_conn.php";

if (isset($_POST['uname']) && isset($_POST['password']) && isset($_POST['re_password'])) {

	function validate($data){
       $data = trim($data);
	   $data = stripslashes($data);
	   $data = htmlspecialchars($data);
	   return $data;
	}

	$uname = validate($_POST['uname']);
	$pass = validate($_POST['password']);

	$re_pass = validate($_POST['re_password']);

	$user_data = 'uname='. $uname;


	if (empty($uname)) {
		header("Location: ../register.php?error=Vui lòng nhập tài khoản&$user_data");
	    exit();
	}else if(empty($pass)){
        header("Location: ../register.php?error=Vui lòng nhập mật khẩu&$user_data");
	    exit();
	}
	else if(empty($re_pass)){
        header("Location: ../register.php?error=Vui lòng nhập xác nhận mật khẩu&$user_data");
	    exit();
	}
	else if($pass !== $re_pass){
        header("Location: ../register.php?error=Mật khẩu xác nhận không khớp&$user_data");
	    exit();
	}

	else{

		// hashing the password
        // $pass = md5($pass);

	    $sql = "SELECT * FROM user WHERE username='$uname' ";
		$result = mysqli_query($conn, $sql);

		if (mysqli_num_rows($result) > 0) {
			header("Location: ../register.php?error=Tài khoản đã tồn tại&$user_data");
	        exit();
		}else {
           $sql2 = "INSERT INTO user(username, password) VALUES('$uname', '$pass')";
           $result2 = mysqli_query($conn, $sql2);
           if ($result2) {
           	 header("Location: ../register.php?success=Đăng ký thành công");
	         exit();
           }else {
	           	header("Location: ../register.php?error=Xảy ra lỗi không xác định được&$user_data");
		        exit();
           }
		}
	}
}else{
	header("Location: ../register.php");
	exit();
}