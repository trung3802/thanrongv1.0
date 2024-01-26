<!DOCTYPE html>
<html>
<head>
<meta name="google-adsense-account" content="ca-pub-1313339641046545">
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>
    <title>Danh sách last_ip trùng nhau nhiều nhất</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }
        h1 {
            background-color: #333;
            color: white;
            text-align: center;
            padding: 10px 0;
        }
        table {
            width: 60%;
            margin: 20px auto;
            border-collapse: collapse;
        }
        table, th, td {
            border: 1px solid #333;
        }
        th, td {
            padding: 8px;
            text-align: center;
        }
        th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>

<h1>Danh sách last_ip trùng nhau nhiều nhất</h1>

<?php
include "db_conn.php";

// Truy vấn dữ liệu và thực hiện thống kê
$sql = "SELECT last_ip, COUNT(*) AS user_count FROM `user` GROUP BY last_ip HAVING user_count > 1 ORDER BY user_count DESC";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "<table>";
    echo "<tr><th>Địa chỉ IP</th><th>Số lượng người dùng</th></tr>";
    
    while ($row = $result->fetch_assoc()) {
        $lastIp = $row["last_ip"];
        $userCount = $row["user_count"];
        echo "<tr><td>$lastIp</td><td>$userCount</td></tr>";
    }
    
    echo "</table>";
} else {
    echo "<p>Không có dữ liệu thống kê nào</p>";
}

// Đóng kết nối
$conn->close();
?>

</body>
</html>
