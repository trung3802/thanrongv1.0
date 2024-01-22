<?php 
session_start(); 
include "db_conn.php";

if (isset($_POST['uname']) && isset($_POST['email']) && isset($_POST['sdt']) && isset($_POST['password']) && isset($_POST['re_password'])) {

	function validate($data){
       $data = trim($data);
	   $data = stripslashes($data);
	   $data = htmlspecialchars($data);
	   return $data;
	}

	$uname = validate($_POST['uname']);
	$uname = strtolower($uname);
	$pass = validate($_POST['password']);
	$pass = strtolower($pass);

	$email = validate($_POST['email']);
	$email = strtolower($email);
	$sdt = validate($_POST['sdt']);

	$re_pass = validate($_POST['re_password']);
	$re_pass = strtolower($re_pass);

	$user_data = 'uname=' . $uname;
	$email_data = 'email=' . $email;
	$sdt_data = 'sdt=' . $sdt;

	if (empty($uname)) {
		header("Location: dangky?error=Vui lòng nhập tài khoản&$user_data&$email_data&$sdt_data");
		exit();
	}else if(empty($email)){
        header("Location: dangky?error=Vui lòng nhập email&$user_data&$email_data&$sdt_data");
	    exit();
	}else if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
		header("Location: dangky?error=Định dạng email không hợp lệ (@gmail.com)&$user_data&$email_data&$sdt_data");
		exit();
	}else if(empty($sdt)){
        header("Location: dangky?error=Vui lòng nhập số điện thoại&$user_data&$email_data&$sdt_data");
	    exit();
	}else if (!preg_match("/^0\d{9}$/", $sdt)) {
		header("Location: dangky?error=Số điện thoại không hợp lệ&$user_data&$email_data&$sdt_data");
		exit();
	}else if(empty($pass)){
        header("Location: dangky?error=Vui lòng nhập mật khẩu&$user_data&$email_data&$sdt_data");
	    exit();
	}else if (strlen($pass) < 6) {
		header("Location: dangky?error=Mật khẩu phải có ít nhất 6 ký tự&$user_data&$email_data&$sdt_data");
		exit();
	}else if(empty($re_pass)){
        header("Location: dangky?error=Chưa nhập lại mật khẩu&$user_data&$email_data&$sdt_data");
	    exit();
	}else if($pass !== $re_pass){
        header("Location: dangky?error=Mật khẩu xác nhận không khớp&$user_data&$email_data&$sdt_data");
	    exit();
	}

	else{

		// hashing the password
        // $pass = md5($pass);

	    $sql = "SELECT * FROM user WHERE username='$uname' ";

		$result = mysqli_query($conn, $sql);

		if (mysqli_num_rows($result) > 0) {
			header("Location: dangky?error=Tài khoản đã tồn tại&$user_data&$email_data&$sdt_data");
	        exit();
		}else {
			
			//Kiểm tra email trùng
			$kiemtraemail = "SELECT * FROM user WHERE email='$email' ";

			$result3 = mysqli_query($conn, $kiemtraemail);

			if(mysqli_num_rows($result3) > 0)
			{
				header("Location: dangky?error=Email đã tồn tại&$user_data&$email_data&$sdt_data");
	        	exit();
			}
			else{
				$sql2 = "INSERT INTO user(username, password, email, sdt, created_at, trangthai, vip) VALUES('$uname', '$pass', '$email', '$sdt', NOW(), 0, 0)";
				$result2 = mysqli_query($conn, $sql2);
				if ($result2) {
					header("Location: dangky?success=Đăng ký thành công");
					exit();
				}else {
					header("Location: dangky?error=unknown error occurred&$user_data&$email_data&$sdt_data");
					exit();
				}
			}
		}
	}
	
}else{
	header("Location: dangky");
	exit();
}