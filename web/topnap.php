<!DOCTYPE html>
<html>
<head>
<meta name="google-adsense-account" content="ca-pub-1313339641046545">
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>
	<title>ĐUA TOP NẠP</title>
	<!-- Load Bootstrap CSS -->
	<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
	<style>
		table {
			margin: 50px auto;
			width: 80%;
			background-color: #f9f9f9;
			border: 1px solid #ccc;
			border-radius: 5px;
			text-align: center;
		}

		h1 {
			text-align: center;
			margin-bottom: 20px;
		}

		th {
			background-color: #4CAF50;
			color: white;
			padding: 10px;
			font-weight: bold;
		}

		td {
			padding: 10px;
			border-bottom: 1px solid #ccc;
		}
	</style>
</head>
<body>
	
	<h1>TOP 20</h1>
	<table>
		<thead>
			<tr>
				<th>TOP</th>
				<th>ID</th>
				<th>Số tiền nạp</th>

			</tr>
		</thead>
		<tbody>
			<?php

// Kết nối database
include "db_conn.php";

// Truy vấn dữ liệu từ bảng người dùng
$query = "SELECT * FROM user ORDER BY tongnap DESC LIMIT 10";

$result = mysqli_query($conn, $query);

// Hiển thị dữ liệu trên form HTML
if (mysqli_num_rows($result) > 0) {
  $count = 1;
  while ($row = mysqli_fetch_assoc($result)) {
    echo "<tr>";
    echo "<td>".$count."</td>";
    echo "<td>".$row['username']."</td>";
    echo "<td>".$row['tongnap']."</td>";
    echo "</tr>";
    $count++;
  }
} else {
  echo "<tr><td colspan='4'>Không có dữ liệu nạp tiền.</td></tr>";
}

mysqli_close($conn);

?>
		</tbody>
	</table>
	
	<!-- Load Bootstrap JS -->
	<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>
