<!DOCTYPE html>
<html>
<head>
<meta name="google-adsense-account" content="ca-pub-1313339641046545">
<script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-1313339641046545"
     crossorigin="anonymous"></script>
    <title>Lịch Sử Nạp Thẻ</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <style>
        body {
            padding: 20px;
        }

        h2 {
            margin-bottom: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <h2>Lịch Sử Nạp Thẻ</h2>
    <table class="table table-bordered">
        <tr>
            <th>Nhà mạng</th>
            <th>Mệnh giá</th>
            <th>Seri</th>
            <th>Mã Thẻ</th>
            <th>Mã GD</th>
            <th>Trạng thái</th>
        </tr>
        <tbody id="table-body"></tbody>

        <script>
            // Gửi yêu cầu AJAX để lấy dữ liệu từ tệp xử lý
            var xhr = new XMLHttpRequest();
            xhr.open("GET", "ttcard_p.php", true);
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    var data = JSON.parse(xhr.responseText);
                    var tableBody = document.getElementById("table-body");

                    if (data.length > 0) {
                        // Hiển thị dữ liệu trong bảng
                        for (var i = 0; i < data.length; i++) {
                            var row = document.createElement("tr");

                            var telcoCell = document.createElement("td");
                            telcoCell.textContent = data[i].telco;
                            row.appendChild(telcoCell);

                            var menhgiaCell = document.createElement("td");
                            menhgiaCell.textContent = data[i].menhgia;
                            row.appendChild(menhgiaCell);

                            var seriCell = document.createElement("td");
                            seriCell.textContent = data[i].seri;
                            row.appendChild(seriCell);

                            var codeCell = document.createElement("td");
                            codeCell.textContent = data[i].code;
                            row.appendChild(codeCell);

                            var magdCell = document.createElement("td");
                            magdCell.textContent = data[i].magd;
                            row.appendChild(magdCell);

                            var statusCell = document.createElement("td");
                            statusCell.textContent = data[i].status;
                            row.appendChild(statusCell);

                            tableBody.appendChild(row);
                        }
                    } else {
                        // Hiển thị thông báo khi không có dữ liệu
                        var noDataCell = document.createElement("td");
                        noDataCell.setAttribute("colspan", "6");
                        noDataCell.textContent = "Không có dữ liệu";
                        var noDataRow = document.createElement("tr");
                        noDataRow.appendChild(noDataCell);
                        tableBody.appendChild(noDataRow);
                    }
                }
            };
            xhr.send();
        </script>
    </table>
</body>
</html>
