<?php
include "db_conn.php";

// Truy vấn cơ sở dữ liệu để lấy thông tin về top 10 người nạp nhiều nhất
$sql = "SELECT id, username, tongnap FROM user ORDER BY tongnap DESC LIMIT 10";
$result = mysqli_query($conn, $sql);
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <link rel="shortcut icon" href="//theme.hstatic.net/1000271846/1001087843/14/favicon.png?v=86" type="image/png">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Top Nạp</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #E066FF;
            margin: 0;
            padding: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
        }
        tr:hover {
            background-color: #e0e0e0;
            cursor: pointer;
        }
        .enlarged-row {
            font-weight: bold;
            font-size: 18px;
        }
        .container {
            background-color: #ffffff;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2);
            border-radius: 8px;
            padding: 20px;
            width: 80%;
            max-width: 600px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        th, td {
            padding: 12px 15px;
            text-align: left;
        }
        th {
            background-color: #e0e0e0;
            font-weight: bold;
        }
        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        tr:hover {
            background-color: #e0e0e0;
        }
        h1 {
            margin-top: 0;
            color: #333;
        }
        h2 {
            color: #555;
        }
        .highlight {
            font-weight: bold;
            color: #ff5722;
        }
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            overflow: hidden;
            background-image: url('anh.jpg');
            background-size: cover;
        }
        #snow {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            pointer-events: none;
            z-index: -100;
        }

    </style>
    
</head>
<body>


    <div class="container">
        <h1>Danh Sách Đua Top Nạp</h1>
        <table>
            <tr>
                <th>TOP</th>
                <th>Tên Người Dùng</th>
                <th>Tổng Tiền Nạp</th>
            </tr>
            <?php
            $rank = 1;
            while ($row = mysqli_fetch_assoc($result)) {
                echo "<tr>";
                echo "<td>" . $rank . "</td>";
                echo "<td>" . $row['username'] . "</td>";
				
                echo "<td><span class='highlight'>$" . number_format($row['tongnap'] / 23000, 2, '.', ',') . "</span></td>";

                echo "</tr>";
                $rank++;
            }
            ?>
        </table>
        <p><em>Dữ liệu được cập nhật 24/7.</em></p></br>
        <h4><a  href="home">Quay Lại</a></h4>
    </div>
    <div id="snow"></div>
    <script>
        
    document.addEventListener('DOMContentLoaded', function () {
        var script = document.createElement('script');
        script.src = 'https://cdn.jsdelivr.net/particles.js/2.0.0/particles.min.js';
        script.onload = function () {
            particlesJS("snow", {
                "particles": {
                    "number": {
                        "value": 90,
                        "density": {
                            "enable": true,
                            "value_area": 500
                        }
                    },
                    "color": {
                        "value": "#FFC0CB"
                    },
                    "opacity": {
                        "value": 3,
                        "random": true,
                        "anim": {
                            "enable": false
                        }
                    },
                    "size": {
                        "value": 5,
                        "random": true,
                        "anim": {
                            "enable": true
                        }
                    },
                    "line_linked": {
                        "enable": true
                    },
                    "move": {
                        "enable": true,
                        "speed": 1,
                        "direction": "top",
                        "random": true,
                        "straight": false,
                        "out_mode": "out",
                        "bounce": false,
                        "attract": {
                            "enable": true,
                            "rotateX": 400,
                            "rotateY": 1400
                        }
                    }
                },
                "interactivity": {
                    "events": {
                        "onhover": {
                            "enable": false
                        },
                        "onclick": {
                            "enable": false
                        },
                        "resize": false
                    }
                },
                "retina_detect": true
            });
        }
        document.head.append(script);
    });

</script>
</body>
</html>

