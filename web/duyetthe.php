<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
    '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Trao thẻ</h5>
                ';
                if (isset($_SESSION['thong_bao'])) {
                    if ($_SESSION['thong_bao'][0] === 'success') {
                        $content .= '<p class="success" style="color: blue">' . $_SESSION['thong_bao'][1] . '</p>';
                    } elseif ($_SESSION['thong_bao'][0] === 'error') {
                        $content .= '<p class="error" style="color: red">' . $_SESSION['thong_bao'][1] . '</p>';
                    }
                    unset($_SESSION['thong_bao']);
                }
                $content .= '
                <div class="card">
                    <div class="card-body">
                        <form action="action/capnhat_the_seri.php" method="post">
                            <table class="table">
                                <thead style="background-color: #E9ECEF">
                                    <tr>
                                        <th>Tên</th>
                                        <th>Nhà mạng</th>
                                        <th>Mệnh giá</th>
                                        <th>Thời gian</th>
                                        <th>Mã Thẻ</th>
                                        <th>Seri</th>
                                        <th>Cập nhật</th>
                                    </tr>
                                </thead>
                                <tbody>';

                                include "db_conn.php";

                                // Truy vấn dữ liệu từ bảng doithe với điều kiện mã hoặc seri bằng NULL hoặc rỗng
                                $query = "SELECT d.id, d.character_ten, d.nhamang, d.menhgia, d.mathe, d.seri, d.thoigiantaodonhang
                                            FROM doithe d WHERE trangthai = 0";

                                // Phân trang
                                $rowsPerPage = 10;
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
                                        $content .= "<td>" . $row["character_ten"] . "</td>";
                                        $content .= "<td>" . $row["nhamang"] . "</td>";
                                        $content .= "<td>" . number_format($row["menhgia"], 0, '.', ',') . "</td>";
                                        $content .= "<td>" . $row["thoigiantaodonhang"] . "</td>";
                                        
                                        $content .= '<form action="action/capnhat_the_seri.php" method="post">
                                                        <input type="text" name="id" hidden value="' . $row["id"] . '">
                                                        <td>
                                                            <input type="text" name="mathe" class="form-control">
                                                        </td>
                                                        <td>
                                                            <input type="text" name="seri" class="form-control">
                                                        </td>
                                                        <td>
                                                            <button type="submit" class="btn btn-success" onclick="return confirmChange()">
                                                                <i class="fa fa-refresh"></i> Cập nhật
                                                            </button>
                                                        </td>                                       
                                                    </form>';                        
                                        $content .= "</tr>";
                                        $count++;
                                    }
                                } else {
                                    $content .= "<tr><td colspan='6'>Không có người đổi thẻ .</td></tr>";
                                }
                                mysqli_close($conn);

                                $content .= '
                                </tbody>
                            </table>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    ';

    // Bên dưới là mã JavaScript để xác nhận cập nhật và chuyển trang sau khi cập nhật
    $content .= '
    <script>
        function confirmChange() {
            var result = confirm("Bạn có chắc chắn muốn đổi thẻ?");
            if (result === true) {
                // Nếu người dùng chọn "OK," tiếp tục gửi biểu mẫu để thực hiện đổi thẻ
                return true;
            } else {
                // Nếu người dùng chọn "Cancel," ngăn chặn biểu mẫu gửi đi
                return false;
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            const updateButtons = document.querySelectorAll("button[name=\'capnhat\']");
            updateButtons.forEach(function (button) {
                button.addEventListener("click", function (event) {
                    const confirmation = confirm("Bạn có muốn cập nhật không?");
                    if (!confirmation) {
                        event.preventDefault(); // Ngăn chặn sự kiện mặc định
                    }
                });
            });
        });
    </script>
    ';
    
    // Tạo các liên kết phân trang
    $conn = mysqli_connect("localhost", "root", "", "vps_acc");
    $query = "SELECT COUNT(*) AS totalRows FROM doithe WHERE trangthai = 0";
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
