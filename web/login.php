<?php 
session_start(); 
include "db_conn.php";

if (isset($_POST['uname']) && isset($_POST['password'])) {

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

	if (empty($uname)) {
		header("Location: dangnhap?error= Vui lòng nhập tài khoản");
	    exit();
	}else if(empty($pass)){
        header("Location: dangnhap?error= Vui lòng nhập mật khẩu");
	    exit();
	}else{
		// hashing the password
        // $pass = md5($pass);

        
		$sql = "SELECT u.id, u.username, u.password, u.character as character_id FROM user u WHERE username='$uname' AND password='$pass'";

		$result = mysqli_query($conn, $sql);

		if (mysqli_num_rows($result) === 1) {
			$row = mysqli_fetch_assoc($result);
            if ($row['username'] === $uname && $row['password'] === $pass) {
				if ($row['character_id'] == 0) {
					header("Location: dangnhap?error=Tài khoản chưa tạo nhân vật.");
					exit();
				} else {
					$_SESSION['username'] = $row['username'];

					$_SESSION['id'] = $row['id'];
					header("Location: home");
					exit();
				}          	
            }else{
				header("Location: dangnhap?error= Tài khoản hoặc mật khẩu sai! Vui lòng nhập lại.");
		        exit();
			}
		}else{
			header("Location: dangnhap?error= Tài khoản hoặc mật khẩu sai! Vui lòng nhập lại.");
	        exit();
		}
	}
	
}else{
	header("Location: dangnhap");
	exit();
}