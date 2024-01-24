<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content = '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Quản lý tải về</h5>
                <form action="action/themtaive_post.php" method="post">';
    
    if (isset($_SESSION['thong_bao'])) {
        if ($_SESSION['thong_bao'][0] === 'success') {
            $content .= '<p class="success" style="color: blue">' . $_SESSION['thong_bao'][1] . '</p>';
        } elseif ($_SESSION['thong_bao'][0] === 'error') {
            $content .= '<p class="error" style="color: red">' . $_SESSION['thong_bao'][1] . '</p>';
        }
        unset($_SESSION['thong_bao']);
    }

    $content .= '
        <div class="row">
            <div class="col-sm-3">
                <label for="phienban" class="form-label">Phiên bản</label>
                <input type="text" class="form-control" name="phienban" id="phienban">
            </div>
            <div class="col-sm-3">
                <label for="link" class="form-label">Link</label>
                <input type="text" class="form-control" name="link" id="link">
            </div>
            <div class="col-sm-3">
                <label for="kieu" class="form-label">Kiểu</label>
                <select class="form-select" name="kieu" id="kieu">
                    <option value="1">Android</option>
                    <option value="2">Pc</option>
                    <option value="3">Ios</option>
                </select>
            </div>
            <div class="col-sm-3">
                <label for="trangthai" class="form-label">Trạng thái</label>
                <select class="form-select" name="trangthai" id="trangthai">
                    <option value="1">Hiển thị</option>
                    <option value="0">Khoá</option>
                </select>
            </div>
        </div>
        <br/>
        <button type="submit" class="btn btn-primary">Thêm</button>
    </form>
    <br/>
    <div class="card">
        <div class="card-body">
            <table class="table">
                <thead style="background-color: #E9ECEF">
                    <tr>
                        <th>Phiên bản</th>
                        <th>Link</th>
                        <th>Kiểu</th>
                        <th>Trạng thái</th>
                        <th>Sửa</th>
                        <th>Xoá</th>
                    </tr>
                </thead>
                <tbody>';

                    include "db_conn.php";

                    $query = "SELECT * FROM taive";
                    $rowsPerPage = 10;
                    $currentPage = isset($_GET['page']) ? $_GET['page'] : 1;
                    $startIndex = ($currentPage - 1) * $rowsPerPage;
                    $query .= " LIMIT $startIndex, $rowsPerPage";

                    $result = mysqli_query($conn, $query);

                    if (!$result) {
                        die('Query error: ' . mysqli_error($conn));
                    }

                    if ($result->num_rows > 0) {
                        while ($row = $result->fetch_assoc()) {
                            $content .= "<tr>";
                            $content .= "<td>" . $row["phienban"] . "</td>";
                            $content .= "<td><a href='" . $row["link"] . "'>" . $row["link"] . "</a></td>";
                            if ($row["kieu"] == 1) {
                                $content .= '<td style="color: blue;">Android</td>';
                            } else if ($row["kieu"] == 2) {
                                $content .= '<td style="color: green;">Pc</td>';
                            } else if ($row["kieu"] == 3) {
                                $content .= '<td style="color: red;">Ios</td>';
                            }

                            if ($row["trangthai"] == 0) {
                                $content .= '<td><a style="color: red"><i class="fas fa-check-circle fa-lg fa-spin"></i> Khoá</a></td>';
                            } else {
                                $content .= '<td><a style="color: green"><i class="fas fa-check-circle fa-lg fa-spin"></i> Hiển thị</a></td>';
                            }

                            $content .= '<td><a class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#editModal" 
                                        data-id="' . $row["id"] . '" 
                                        data-phienban="' . $row["phienban"] . '" 
                                        data-link="' . $row["link"] . '" 
                                        data-kieu="' . $row["kieu"] . '" 
                                        data-trangthai="' . $row["trangthai"] . '" 
                                        onclick="openEditModal(this)">Sửa</a></td>';

                            $content .= '<td><a href="action/xoataive_post.php?id=' . $row["id"] . '" onclick="return confirm(\'Bạn có chắc chắn muốn xoá bản tải về này?\');" class="btn btn-danger">Xoá</a></td>';
                            $content .= "</tr>";
                        }
                    } else {
                        $content .= "<tr><td colspan='8'>Không có dữ liệu.</td></tr>";
                    }

                    mysqli_close($conn);

                    $content .= '

                </tbody>
            </table>
        </div>
    </div>
</div>
</div>
</div>';

    $content .= '
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Sửa Thông Tin</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form action="action/suataive_post.php" method="post">
                        <input type="hidden" name="editId" id="editId">
                        <div class="mb-3">
                            <label for="editPhienBan" class="form-label">Phiên bản</label>
                            <input type="text" class="form-control" name="editPhienBan" id="editPhienBan">
                        </div>
                        <div class="mb-3">
                            <label for="editLink" class="form-label">Link</label>
                            <input type="text" class="form-control" name="editLink" id="editLink">
                        </div>
                        <div class="mb-3">
                            <label for="editKieu" class="form-label">Kiểu</label>
                            <select class="form-select" name="editKieu" id="editKieu">
                                <option value="1">Android</option>
                                <option value="2">Pc</option>
                                <option value="3">Ios</option>
                            </select>
                        </div>
                        <div class="mb-3">
                            <label for="editTrangThai" class="form-label">Trạng thái</label>
                            <select class="form-select" name="editTrangThai" id="editTrangThai">
                                <option value="1">Hiển thị</option>
                                <option value="0">Khoá</option>
                            </select>
                        </div>
                        <button type="submit" class="btn btn-warning">Cập nhật</button>
                    </form>
                </div>
            </div>
        </div>
    </div>';

    $content .= '
    <script>
        function openEditModal(button) {
            var id = button.getAttribute("data-id");
            var phienban = button.getAttribute("data-phienban");
            var link = button.getAttribute("data-link");
            var kieu = button.getAttribute("data-kieu");
            var trangthai = button.getAttribute("data-trangthai");

            // You can now use these variables to populate your modal fields
            document.getElementById("editId").value = id;
            document.getElementById("editPhienBan").value = phienban;
            document.getElementById("editLink").value = link;
            document.getElementById("editKieu").value = kieu;
            document.getElementById("editTrangThai").value = trangthai;
        }
    </script>';


    include "layout.php";
} else {
    header("Location: login");
    exit();
}
?>
