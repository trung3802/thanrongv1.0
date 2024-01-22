<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Lịch sử nạp card</h5>
                <div class="card">
                    <div class="card-body">
                        <table class="table">
                            <thead style="background-color: #E9ECEF">
                                <tr>
                                    <th>Nhà mạng</th>
                                    <th>Mệnh giá</th>
                                    <th>Seri</th>
                                    <th>Mã Thẻ</th>
                                    <th>Mã GD</th>
                                    <th>Trạng thái</th>
                                </tr>
                            </thead>
                            <tbody>';

                            include "db_conn.php";

                            $id = $_SESSION['id'];
                            // Truy vấn dữ liệu từ bảng napcard
                            $query = "SELECT telco, menhgia, seri, code, magd, status FROM napcard WHERE uid = $id ORDER BY id DESC";

                            // Phân trang
                            $rowsPerPage = 5;
                            $currentPage = isset($_GET['page']) ? $_GET['page'] : 1;
                            $startIndex = ($currentPage - 1) * $rowsPerPage;

                            // Thêm LIMIT vào truy vấn để chỉ lấy số lượng dòng dữ liệu cho trang hiện tại
                            $query .= " LIMIT $startIndex, $rowsPerPage";

                            $result = mysqli_query($conn, $query);

							// Hiển thị dữ liệu trên bảng
                            if (mysqli_num_rows($result) > 0) {
                                $count = 1;
                                while ($row = mysqli_fetch_assoc($result)) {
                                    $content .= "<tr>";
                                    $content .= "<td>" . $row["telco"] . "</td>";
                                    $content .= "<td>" . number_format($row["menhgia"], 0, '.', ',') . "</td>";
                                    $content .= "<td>" . $row["seri"] . "</td>";
                                    $content .= "<td>" . $row["code"] . "</td>";
                                    $content .= "<td>" . $row["magd"] . "</td>";
									// Kiểm tra giá trị của status
									if ($row["status"] == 0) {
										// Nếu status bằng 0, hiển thị "Đang xử lý" với màu đỏ
										$content .= "<td style='color: yellow;'><b>Đang xử lý</b></td>";
									} elseif ($row["status"] == 1) {
										// Nếu status bằng 1, hiển thị "Thành công" với màu xanh
										$content .= "<td style='color: green;'><b>Thành công</b></td>";
										}elseif ($row["status"] == 2) {
										// Nếu status bằng 1, hiển thị "Thành công" với màu xanh
										$content .= "<td style='color: red;'><b>Thẻ Lỗi</b></td>";
									} else {
										// Nếu status không phải 0 hoặc 1, hiển thị giá trị status
										$content .= "<td>" . $row["status"] . "</td>";
									}


                                   // $content .= "<td>" . $row["status"] . "</td>";

                                    $content .= "</tr>";
                                    $count++;
                                }
                            } else {
                                $content .= "<tr><td colspan='6'>Bạn Chưa Nạp.</td></tr>";
                            }
                            mysqli_close($conn);
                            
                            $content .= '
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    ';

    // Tạo các liên kết phân trang
    include "db_conn.php";
    
    $query = "SELECT COUNT(*) AS totalRows FROM napcard WHERE uid = $id ORDER BY id DESC";
    $result = mysqli_query($conn, $query);
    $row = mysqli_fetch_assoc($result);
    $totalRows = $row['totalRows'];
    $totalPages = ceil($totalRows / $rowsPerPage);

    $content .= '<ul class="pagination justify-content-center">';

    // Hiển thị nút Previous
    if ($currentPage > 1) {
        $prevPage = $currentPage - 1;
        $content .= '<li class="page-item"><a class="page-link" href="?page=' . $prevPage . '">&lt;&lt;</a></li>';
    }
    
    // Hiển thị các trang ở giữa
    $maxPages = 3; // Number of middle links to display
    $halfMaxPages = floor($maxPages / 2); // Half of the middle links
    
    $startPage = max(1, min($currentPage - $halfMaxPages, $totalPages - $maxPages + 1));
    $endPage = min($startPage + $maxPages - 1, $totalPages);
    
    for ($i = $startPage; $i <= $endPage; $i++) {
        $activeClass = $i == $currentPage ? 'active' : '';
        $content .= '<li class="page-item ' . $activeClass . '"><a class="page-link" href="?page=' . $i . '">' . $i . '</a></li>';
    }
    
    // Hiển thị nút Next
    if ($currentPage < $totalPages) {
        $nextPage = $currentPage + 1;
        $content .= '<li class="page-item"><a class="page-link" href="?page=' . $nextPage . '">&gt;&gt;</a></li>';
    }

    $content .= '</ul>';

    mysqli_close($conn);

    include "layout.php";
} else {
    header("Location: login");
    exit();
}
?>
