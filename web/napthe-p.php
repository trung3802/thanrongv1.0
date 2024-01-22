<?php 
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
$id = $_SESSION['id'];
    include "db_conn.php";

if (isset($_POST['mathe']) && isset($_POST['seri']) && isset($_POST['loaithe'])) {

	function validate($data){
       $data = trim($data);
	   $data = stripslashes($data);
	   $data = htmlspecialchars($data);
	   return $data;
	}

	$mathe      = validate($_POST['mathe']);
	$seri       = validate($_POST['seri']);
	$loaithe    = validate($_POST['loaithe']);
	$menhgia    = validate($_POST['menhgia']);
    
    if(empty($loaithe)){
      header("Location: napthe?error=Vui lòng chọn loại thẻ");
	  exit();
    } else  if(empty($menhgia)){
      header("Location: napthe?error=Vui lòng chọn mệnh giá");
	  exit();
    } else if(empty($mathe)){
      header("Location: napthe?error=Vui lòng nhập mã thẻ");
	  exit();
    }else if(empty($seri)){
      header("Location: napthe?error=Vui lòng nhập mã seri");
	  exit();
    }else {
        $code = rand(11111111,99999999999);
        $partner_id = '38014300306';
    	$partner_key = '481963ff9224a826412b11a835f87ce4';
        $curl = curl_init();
        curl_setopt_array($curl, array(
            CURLOPT_RETURNTRANSFER => 1,
            CURLOPT_CONNECTTIMEOUT => 0,
            CURLOPT_TIMEOUT => 16,
          CURLOPT_URL => 'https://thesieure.com/chargingws/v2',
            CURLOPT_USERAGENT => 'TUANORI CURL',
            CURLOPT_POST => 1,
            CURLOPT_SSL_VERIFYPEER => false, //Bỏ kiểm SSL
            CURLOPT_POSTFIELDS => http_build_query(array(
                'sign' => md5($partner_key.$mathe.$seri),
                'telco' => $loaithe,
                'code' => $mathe,
                'serial' => $seri,
                'amount' => $menhgia,
                'request_id' => $code,
                'partner_id' => $partner_id,
                'command'   => 'charging'
            ))
        ));
        $resp = curl_exec($curl);
        curl_close($curl);
        $obj = json_decode($resp, true);
        if($obj['status'] == 99) {
            $sql2 = "INSERT INTO napcard(uid, telco, menhgia, seri, code, magd, status,created_at) VALUES('$id', '$loaithe', '$menhgia', '$seri', '$mathe', '$code', '0',NOW())";
            $result2 = mysqli_query($conn, $sql2);
           if ($result2) {
           	 header("Location: napthe.php?success=Gửi thẻ thành công! Vui lòng chờ xử lý");
	         exit();
           }else {
	           	header("Location: napthe.php?error=unknown error occurred&$user_data");
		        exit();
           }
        } else {
            header("Location: napthe.php?error=".$obj['status']);
        	exit();
        }
    }

    
}else{
	header("Location: napthe");
	exit();
}

}else{
     header("Location: dangnhap");
     exit();
}