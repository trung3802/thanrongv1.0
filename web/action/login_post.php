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
	$pass = validate($_POST['password']);

	if (empty($uname)) {
		header("Location: ../login.php?error=Vui lòng nhập tài khoản");
	    exit();
	}else if(empty($pass)){
        header("Location: ../login.php?error=Vui lòng nhập mật khẩu");
	    exit();
	}else{
		// hashing the password
        // $pass = md5($pass);

        
		$sql = "SELECT * FROM user WHERE username='$uname' AND password='$pass'";

		$result = mysqli_query($conn, $sql);

		if (mysqli_num_rows($result) === 1) {
			$row = mysqli_fetch_assoc($result);
            if ($row['username'] === $uname && $row['password'] === $pass) {
            	$_SESSION['username'] = $row['username'];
            	
            	$_SESSION['id'] = $row['id'];
            	header("Location: ../index.php");
		        exit();
            }else{
				header("Location: ../login.php?error=Tài khoản hoặc mật khẩu sai");
		        exit();
			}
		}else{
			header("Location: ../login.php?error=Tài khoản hoặc mật khẩu sai");
	        exit();
		}
	}
	
}else{
	header("Location: ../login.php");
	exit();
}