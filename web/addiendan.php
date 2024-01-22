<?php
session_start();

if (isset($_SESSION['id']) && isset($_SESSION['username'])) {
    $content =
        '
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title fw-semibold mb-4">Thêm dữ liệu</h5>
                <div class="card">
                    <div class="card-body">
                        <form action="action/adthemdiendan_post.php" method="post">';
                        if (isset($_SESSION['thong_bao'])) {
                            if ($_SESSION['thong_bao'][0] === 'success') {
                                $content .= '<p class="success" style="color: blue">' . $_SESSION['thong_bao'][1] . '</p>';
                            } elseif ($_SESSION['thong_bao'][0] === 'error') {
                                $content .= '<p class="error" style="color: red">' . $_SESSION['thong_bao'][1] . '</p>';
                            }
                            unset($_SESSION['thong_bao']);
                        }
                        $content .= '
                            <div class="mb-3">
                                <label for="exampleInputTitle" class="form-label">Tiêu đề</label>
                                <input type="text" class="form-control" name="tieude" id="exampleInputTitle">
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputContent" class="form-label">Avatar</label>
                                <select class="form-control mt-2" name="avatar" id="avatar">
                                <option value="">Chọn avatar</option>
                                    <option value="avatar3.png">Avatar quy lão</option>
                                    <option value="avatar6.png">Avatar 6</option>
                                    <option value="avatar10.png">Avatar zamazu</option>
                                    <option value="avatar13.png">Avatar whis</option>
                                    <option value="6.png">Avatar Trái đất</option>
                                    <option value="7.png">Avatar xayda</option>
                                    <option value="8.png">Avatar zeno</option>
                            </select>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputContent" class="form-label">Nội dung</label>
                                <textarea class="form-control" name="noidung" id="exampleInputContent"></textarea>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputTitle" class="form-label">Ảnh nội dung</label>
                                <input type="text" class="form-control" name="anh" id="exampleInputTitle">
                            </div>
                            <button type="submit" class="btn btn-primary">Thêm</button>
                            <a href="home.php" class="btn btn-primary">Quay lại</a>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    ';
    include "layout.php";
} else {
    header("Location: login.php");
    exit();
}
?>
