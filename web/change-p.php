<?php 
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {

    include "db_conn.php";

if (isset($_POST['op']) && isset($_POST['np'])
    && isset($_POST['c_np'])) {

	function validate($data){
       $data = trim($data);
	   $data = stripslashes($data);
	   $data = htmlspecialchars($data);
	   return $data;
	}

	$op = validate($_POST['op']);
	$np = validate($_POST['np']);
	$c_np = validate($_POST['c_np']);
    
    if(empty($op)){
      header("Location: change-password?error=Old Password is required");
	  exit();
    }else if(empty($np)){
      header("Location: change-password?error=New Password is required");
	  exit();
    }else if($np !== $c_np){
      header("Location: change-password?error=The confirmation password  does not match");
	  exit();
    }else {
    	// hashing the password
    	$op = $op;
    	$np = $np;
        $id = $_SESSION['id'];

        $sql = "SELECT password
                FROM user WHERE 
                id='$id' AND password='$op'";
        $result = mysqli_query($conn, $sql);
        if(mysqli_num_rows($result) === 1){
        	
        	$sql_2 = "UPDATE user
        	          SET password='$np'
        	          WHERE id='$id'";
        	mysqli_query($conn, $sql_2);
        	header("Location: change-password?success=Your password has been changed successfully");
	        exit();

        }else {
        	header("Location: change-password?error=Incorrect password");
	        exit();
        }

    }

    
}else{
	header("Location: change-password.php");
	exit();
}

}else{
     header("Location: dangnhap");
     exit();
}