let verifiedIdentifier = "";

function showAlert(msg, isSuccess = false) {
    const box = document.getElementById("alertBox");
    box.textContent = msg;
    box.className = "alert-box " + (isSuccess ? "alert-success-custom" : "alert-danger-custom");
    box.style.display = "block";
}

function hideAlert() {
    const box = document.getElementById("alertBox");
    box.style.display = "none";
}

window.handleVerifyAccount = async function (e) {
    e.preventDefault();
    hideAlert();

    const input = document.getElementById("identifierInput");
    const val = input.value.trim();
    if (!val) {
        showAlert("Vui lòng nhập thông tin tài khoản");
        return;
    }

    const btn = document.getElementById("verifyBtn");
    btn.disabled = true;
    btn.innerHTML = "<span>Đang kiểm tra...</span>";

    try {
        const res = await fetch("/api/auth/forgot/verify", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ identifier: val })
        });

        const data = await res.json();
        if (!res.ok || !data.success) {
            showAlert(data.message || "Không tìm thấy tài khoản phù hợp");
            return;
        }

        verifiedIdentifier = val;

        // Cập nhật Stepper sang Bước 2
        document.getElementById("stepNode1").classList.remove("active");
        document.getElementById("stepLine").classList.add("passed");
        document.getElementById("stepNode2").classList.add("active");

        // Cập nhật nội dung tiêu đề sang Bước 2
        document.getElementById("stepTitle").textContent = "Đổi Mật Khẩu";
        document.getElementById("stepSubtitle").textContent = "Thiết lập mật khẩu mới cho tài khoản của bạn";

        // Chuyển Form
        document.getElementById("step1Form").style.display = "none";
        document.getElementById("step2Form").style.display = "flex";

        document.getElementById("badgeDisplayName").textContent = data.data.displayName || data.data.username;
        const info = (data.data.email ? " • " + data.data.email : "");
        document.getElementById("badgeUsername").textContent = "@" + data.data.username + info;
        document.getElementById("newPasswordInput").focus();
    } catch (err) {
        showAlert("Không thể kết nối đến máy chủ. Vui lòng thử lại!");
    } finally {
        btn.disabled = false;
        btn.innerHTML = "<span>Tiếp tục</span>";
    }
};

window.backToStep1 = function () {
    hideAlert();

    // Khôi phục Stepper về Bước 1
    document.getElementById("stepNode2").classList.remove("active");
    document.getElementById("stepLine").classList.remove("passed");
    document.getElementById("stepNode1").classList.add("active");

    // Khôi phục tiêu đề Bước 1
    document.getElementById("stepTitle").textContent = "Tìm Tài Khoản";
    document.getElementById("stepSubtitle").textContent = "Nhập Username, Email hoặc SĐT đã đăng ký để kiểm tra";

    // Chuyển Form
    document.getElementById("step2Form").style.display = "none";
    document.getElementById("step1Form").style.display = "flex";
    document.getElementById("identifierInput").focus();
};

window.handleResetPassword = async function (e) {
    e.preventDefault();
    hideAlert();

    const pass = document.getElementById("newPasswordInput").value;
    const confirm = document.getElementById("confirmPasswordInput").value;

    if (pass.length < 6) {
        showAlert("Mật khẩu mới phải có ít nhất 6 ký tự");
        return;
    }

    if (pass !== confirm) {
        showAlert("Mật khẩu xác nhận không khớp!");
        return;
    }

    const btn = document.getElementById("resetBtn");
    btn.disabled = true;
    btn.innerHTML = "<span>Đang cập nhật...</span>";

    try {
        const res = await fetch("/api/auth/forgot/reset-password", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                identifier: verifiedIdentifier,
                newPassword: pass
            })
        });

        const data = await res.json();
        if (!res.ok || !data.success) {
            showAlert(data.message || "Đặt lại mật khẩu thất bại");
            return;
        }

        // Ẩn Stepper và tiêu đề khi thành công
        document.querySelector(".stepper-bar").style.display = "none";
        document.getElementById("stepTitle").style.display = "none";
        document.getElementById("stepSubtitle").style.display = "none";
        document.getElementById("step2Form").style.display = "none";
        document.getElementById("stepSuccess").style.display = "block";
    } catch (err) {
        showAlert("Không thể kết nối máy chủ. Vui lòng thử lại!");
    } finally {
        btn.disabled = false;
        btn.innerHTML = "<span>Xác nhận đổi mật khẩu</span>";
    }
};
