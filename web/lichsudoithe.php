<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Lịch sử đổi thẻ</h5>
                <div class="card">
                    <div class="card-body">
                        <table class="table">
                            <thead style="background-color: #E9ECEF">
                                <tr>
                                    <th>Nhà mạng</th>
                                    <th>Mệnh giá</th>
                                    <th>Số lượng</th>
                                    <th>Mã Thẻ</th>
                                    <th>Seri</th>
                                    <th>Thời gian</th>
                                    <th>Trao thẻ</th>
                                    <th>Trạng thái</th>
                                </tr>
                            </thead>
                            <tbody>';

                            include "db_conn.php";

                            $id = $_SESSION['id'];

                            // Truy vấn dữ liệu từ bảng doithe
                            $query = "SELECT d.nhamang, d.menhgia, d.soluong, d.mathe, d.seri, d.thoigiantaodonhang, d.thoigiantraothe, d.trangthai 
                                        FROM doithe d INNER JOIN `character` c ON d.character_id = c.id
                                        INNER JOIN user u  ON c.id = u.character
                                        WHERE u.id = '$id' ORDER BY d.id DESC";
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
                                    $statusText = $row["trangthai"] == 0 ?
                                        '<span class="badge badge-warning">Đang xử lý</span>' :
                                        '<span class="badge badge-success">Thành công</span>'; // Sử dụng badge CSS để tạo biểu tượng màu xanh vàng
                                    $content .= "<tr>";
                                    $content .= "<td>" . $row["nhamang"] . "</td>";
                                    $content .= "<td>" . number_format($row["menhgia"], 0, '.', ',') . "</td>";
                                    $content .= "<td>" . $row["soluong"] . "</td>";
                                    $content .= "<td><input type='text' name='mathe' value='" . $row["mathe"] . "' disabled></td>"; // Thêm thuộc tính disabled
                                    $content .= "<td><input type='text' name='seri' value='" . $row["seri"] . "' disabled></td>"; // Thêm thuộc tính disabled
                                    $content .= "<td>" . $row["thoigiantaodonhang"] . "</td>";
                                    $content .= "<td>" . $row["thoigiantraothe"] . "</td>";
                                    $content .= "<td>" . $statusText . "</td>"; // Hiển thị trạng thái dưới dạng văn bản
                                    $content .= "</tr>";
                                    $count++;
                                }
                            } else {
                                $content .= "<tr><td colspan='7'>Bạn chưa đổi thẻ cào.</td></tr>";
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
    
    $query = "SELECT COUNT(*) AS totalRows 
                FROM doithe d INNER JOIN `character` c ON d.character_id = c.id
                INNER JOIN user u  ON c.id = u.character
                WHERE u.id = '$id' ORDER BY d.id DESC";
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
