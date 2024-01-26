<?php
    session_start();
    include "db_conn.php";

    // Khởi tạo biến thông báo rỗng
    $message = '';

    // Kiểm tra nếu có ID nhân vật được truyền qua tham số GET
    if (isset($_GET['id'])) {
        $character_id = $_GET['id'];
        $id = $character_id; // Gán giá trị cho biến $id

        // Sử dụng Prepared Statement để bảo vệ khỏi SQL Injection
        $query = "SELECT * FROM `character` WHERE id = ?";
        $stmt = mysqli_prepare($conn, $query);
        mysqli_stmt_bind_param($stmt, "i", $character_id);
        mysqli_stmt_execute($stmt);
        $result = mysqli_stmt_get_result($stmt);

        // Kiểm tra và hiển thị dữ liệu
        if (mysqli_num_rows($result) > 0) {
            $row = mysqli_fetch_assoc($result);
            $character_id = $row['id'];
            $name = $row['Name'];
            $itemBody = $row['ItemBody'];
            $itemBag = $row['ItemBag'];
            $itemBox = $row['ItemBox'];
            $infoChar = $row['InfoChar'];
        } else {
            $message = "Không tìm thấy thông tin nhân vật với ID nhân vật đã cho.";
        }

        mysqli_stmt_close($stmt);
    }

    // Kiểm tra nếu có dữ liệu được gửi từ form
    if (isset($_POST['submit'])) {
        // Lấy dữ liệu từ form
        $character_id = $_POST['character_id'];
        $column = $_POST['column'];
        $newValue = $_POST['newValue'];

        // Kiểm tra xem cả cột và giá trị mới có được cung cấp hay không
        if (!empty($column) && !empty($newValue)) {
            // Sử dụng Prepared Statement để bảo vệ khỏi SQL Injection
            if ($column === 'name') {
                $query = "UPDATE `character` SET `Name` = ? WHERE id = ?";
            } else {
                $query = "UPDATE `character` SET $column = ? WHERE id = ?";
            }
            $stmt = mysqli_prepare($conn, $query);
            mysqli_stmt_bind_param($stmt, "si", $newValue, $character_id);
            $result = mysqli_stmt_execute($stmt);

            // Kiểm tra và hiển thị kết quả
            if ($result) {
                $message = "Thông tin nhân vật đã được cập nhật thành công.";
            } else {
                $message = "Có lỗi xảy ra khi cập nhật thông tin nhân vật: " . mysqli_error($conn);
            }

            mysqli_stmt_close($stmt);
        } else {
            $message = "Vui lòng nhập cả cột và giá trị mới.";
        }
    }
    // Đóng kết nối
    mysqli_close($conn);

    // Tiếp tục xử lý giao diện và thêm vào trang layout
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Chỉnh sửa ID ' . $id .' </h5>
                <div class="card">
                    <div class="card-body">
                    <form method="POST">
                        <div class="mb-3">
                            <input type="hidden" name="character_id" value="' . $id .'">
                            <label for="column">Chọn cột: </label>
                            <select id="column" name="column" class="form-select" onchange="updateInputType()">
                                <option value="name">Name</option>
                                <option value="itemBody">ItemBody</option>
                                <option value="itemBag">ItemBag</option>
                                <option value="itemBox">ItemBox</option>
                                <option value="infoChar">InfoChar</option>
                            </select>
                        </div>
                        <div class="mb-3">
                            <label for="newValue" class="form-label">Giá trị mới</label>
                            <textarea name="newValue" class="form-control" id="newValue" rows="4">' . $name . '</textarea>
                        </div>
                        
                        <!-- Add similar textarea fields for other values -->
                        <input type="submit" name="submit" class="btn btn-primary" value="Cập nhật">
                        <a href="index.php" class="btn btn-primary">Quay lại</a>
                    </form>
                        <div class="mt-4">';
                        $content .=  "<p> $message </p>";
                        $content .=  '
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    ';
    include "layout.php";
    
?>


<script>
    // Auto-refresh the page after a specified time (in milliseconds)

    function updateInputType() {
        var column = document.getElementById("column").value;
        var textareaValue = document.getElementById("newValue");
        textareaValue.rows = 4; // Set rows based on your preference

        // Set initial values based on the selected column
        <?php
            echo 'var nameValue = ' . json_encode($name) . ";\n";
            echo 'var itemBodyValue = ' . json_encode($itemBody) . ";\n";
            echo 'var itemBagValue = ' . json_encode($itemBag) . ";\n";
            echo 'var itemBoxValue = ' . json_encode($itemBox) . ";\n";
            echo 'var infoCharValue = ' . json_encode($infoChar) . ";\n";
        ?>

        if (column === "name") {
            textareaValue.value = nameValue;
        } else if (column === "itemBody") {
            textareaValue.value = itemBodyValue;
        } else if (column === "itemBag") {
            textareaValue.value = itemBagValue;
        } else if (column === "itemBox") {
            textareaValue.value = itemBoxValue;
        } else if (column === "infoChar") {
            textareaValue.value = infoCharValue;
        }
        
    }
    
</script>
