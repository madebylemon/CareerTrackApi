import json
import urllib.request
import urllib.error
import random

BASE_URL = "http://localhost:5145"

# Ham gui HTTP Request dung thu vien built-in urllib
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
            print(f"Error HTTP {e.code}: {err_json}")
        except:
            print(f"Error HTTP {e.code}: {body}")
        raise e

# Kich ban chay demo
def run_demo():
    print("=== Bat dau chuong trinh DEMO CareerTrackApi ===\n")
    
    # 1. Dang ky tai khoan moi
    email = f"demo_user_{random.randint(1000, 9999)}@example.com"
    password = "password123"
    print(f"1. Dang ky tai khoan moi: {email}")
    register_res = send_request("/api/auth/register", "POST", {"email": email, "password": password})
    print(f"Success! User ID: {register_res['userId']}\n")
    
    # 2. Dang nhap he thong
    print(f"2. Dang nhap he thong...")
    login_res = send_request("/api/auth/login", "POST", {"email": email, "password": password})
    token = login_res["token"]
    print(f"Success! JWT Token nhan duoc (rut gon): {token[:40]}...\n")
    
    # 3. Lay thong tin profile nguoi dung hien tai
    print(f"3. Lay thong tin tai khoan hien tai (/api/users/me)...")
    profile = send_request("/api/users/me", "GET", token=token)
    print(f"Success! Thong tin ho so: ID={profile['id']}, Email={profile['email']}, Tao luc={profile['createdAt']}\n")
    
    # 4. Tao don ung tuyen moi
    print(f"4. Tao don ung tuyen moi (Microsoft - Software Engineer)...")
    app_data = {
        "companyName": "Microsoft",
        "roleTitle": "Software Engineer II",
        "status": "Applied",
        "dateApplied": "2026-06-12T08:00:00Z",
        "notes": "Ung tuyen qua cong thong tin Microsoft Careers."
    }
    app = send_request("/api/applications", "POST", app_data, token=token)
    app_id = app["id"]
    print(f"Success! Tao thanh cong don ung tuyen! ID don: {app_id}, Trang thai: {app['status']}\n")
    
    # 5. Cap nhat trang thai don ung tuyen (PATCH)
    print(f"5. Cap nhat trang thai don ung tuyen sang PhoneScreen (PATCH)...")
    patched = send_request(f"/api/applications/{app_id}/status", "PATCH", {"status": "PhoneScreen"}, token=token)
    print(f"Success! Cap nhat trang thai thanh cong! Trang thai moi: {patched['status']}\n")
    
    # 6. Them cuoc phong van long trong don ung tuyen
    print(f"6. Len lich phong van HR (vong PhoneScreen)...")
    interview_data = {
        "interviewType": "HR",
        "scheduledAt": "2026-06-16T15:00:00Z",
        "notes": "Trao doi voi HR ve muc luong va kinh nghiem lam viec."
    }
    interview = send_request(f"/api/applications/{app_id}/interviews", "POST", interview_data, token=token)
    print(f"Success! Len lich phong van thanh cong! Vong: {interview['interviewType']}, Lich hen: {interview['scheduledAt']}\n")
    
    # 7. Xem danh sach don ung tuyen
    print(f"7. Xem danh sach tat ca don ung tuyen cua User...")
    apps = send_request("/api/applications", "GET", token=token)
    for a in apps:
        print(f"   - [{a['status']}] {a['companyName']} - {a['roleTitle']} (Nop ngay: {a['dateApplied']})")
    print()
    
    # 8. Xem danh sach cac cuoc phong van cua don nay
    print(f"8. Xem danh sach cac cuoc phong van cho don ung tuyen ID {app_id}...")
    interviews = send_request(f"/api/applications/{app_id}/interviews", "GET", token=token)
    for i in interviews:
        print(f"   - Vong: {i['interviewType']} | Hen luc: {i['scheduledAt']} | Ghi chu: {i['notes']}")
    print()
    
    print("*** DEMO HOAN THANH THANH CONG! ***")

if __name__ == "__main__":
    run_demo()
