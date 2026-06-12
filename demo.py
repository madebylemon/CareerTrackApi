import json
import urllib.request
import urllib.error
import random

BASE_URL = "http://localhost:5145"

# Hàm gửi HTTP Request dùng thư viện built-in urllib
def send_request(url, method="GET", data=None, token=None):
    headers = {"Content-Type": "application/json"}
    if token:
        headers["Authorization"] = f"Bearer {token}"
    
    req_data = json.dumps(data).encode("utf-8") if data else None
    req = urllib.request.Request(f"{BASE_URL}{url}", data=req_data, headers=headers, method=method)
    
    try:
        with urllib.request.urlopen(req) as res:
            if res.status == 204:
                return None
            body = res.read().decode("utf-8")
            return json.loads(body) if body else None
    except urllib.error.HTTPError as e:
        body = e.read().decode("utf-8")
        try:
            err_json = json.loads(body)
            print(f"❌ HTTP Error {e.code}: {err_json}")
        except:
            print(f"❌ HTTP Error {e.code}: {body}")
        raise e

# Kịch bản chạy demo
def run_demo():
    print("🚀 Bắt đầu chương trình DEMO CareerTrackApi...\n")
    
    # 1. Đăng ký tài khoản mới
    email = f"demo_user_{random.randint(1000, 9999)}@example.com"
    password = "password123"
    print(f"👤 1. Đăng ký tài khoản mới: {email}")
    register_res = send_request("/api/auth/register", "POST", {"email": email, "password": password})
    print(f"✅ Đăng ký thành công! User ID: {register_res['userId']}\n")
    
    # 2. Đăng nhập hệ thống
    print(f"🔑 2. Đăng nhập hệ thống...")
    login_res = send_request("/api/auth/login", "POST", {"email": email, "password": password})
    token = login_res["token"]
    print(f"✅ Đăng nhập thành công! JWT Token nhận được (rút gọn): {token[:40]}...\n")
    
    # 3. Lấy thông tin profile người dùng hiện tại
    print(f"🔍 3. Lấy thông tin tài khoản hiện tại (/api/users/me)...")
    profile = send_request("/api/users/me", "GET", token=token)
    print(f"✅ Thông tin hồ sơ: ID={profile['id']}, Email={profile['email']}, Tạo lúc={profile['createdAt']}\n")
    
    # 4. Tạo đơn ứng tuyển mới
    print(f"💼 4. Tạo đơn ứng tuyển mới (Microsoft - Software Engineer)...")
    app_data = {
        "companyName": "Microsoft",
        "roleTitle": "Software Engineer II",
        "status": "Applied",
        "dateApplied": "2026-06-12T08:00:00Z",
        "notes": "Ứng tuyển qua cổng thông tin Microsoft Careers."
    }
    app = send_request("/api/applications", "POST", app_data, token=token)
    app_id = app["id"]
    print(f"✅ Tạo thành công đơn ứng tuyển! ID đơn: {app_id}, Trạng thái: {app['status']}\n")
    
    # 5. Cập nhật trạng thái đơn ứng tuyển (PATCH)
    print(f"🔄 5. Cập nhật trạng thái đơn ứng tuyển sang PhoneScreen (PATCH)...")
    patched = send_request(f"/api/applications/{app_id}/status", "PATCH", {"status": "PhoneScreen"}, token=token)
    print(f"✅ Cập nhật trạng thái thành công! Trạng thái mới: {patched['status']}\n")
    
    # 6. Thêm cuộc phỏng vấn lồng trong đơn ứng tuyển
    print(f"📅 6. Lên lịch phỏng vấn HR (vòng PhoneScreen)...")
    interview_data = {
        "interviewType": "HR",
        "scheduledAt": "2026-06-16T15:00:00Z",
        "notes": "Trao đổi với HR về mức lương và kinh nghiệm làm việc."
    }
    interview = send_request(f"/api/applications/{app_id}/interviews", "POST", interview_data, token=token)
    print(f"✅ Lên lịch phỏng vấn thành công! Vòng: {interview['interviewType']}, Lịch hẹn: {interview['scheduledAt']}\n")
    
    # 7. Xem danh sách đơn ứng tuyển
    print(f"📋 7. Xem danh sách tất cả đơn ứng tuyển của User...")
    apps = send_request("/api/applications", "GET", token=token)
    for a in apps:
        print(f"   - [{a['status']}] {a['companyName']} - {a['roleTitle']} (Nộp ngày: {a['dateApplied']})")
    print()
    
    # 8. Xem danh sách các cuộc phỏng vấn của đơn này
    print(f"📋 8. Xem danh sách các cuộc phỏng vấn cho đơn ứng tuyển ID {app_id}...")
    interviews = send_request(f"/api/applications/{app_id}/interviews", "GET", token=token)
    for i in interviews:
        print(f"   - Vòng: {i['interviewType']} | Hẹn lúc: {i['scheduledAt']} | Ghi chú: {i['notes']}")
    print()
    
    print("🎯 DEMO HOÀN THÀNH THÀNH CÔNG! Dự án hoạt động hoàn hảo! 🎯")

if __name__ == "__main__":
    run_demo()
