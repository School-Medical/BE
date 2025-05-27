# 🏥 School Medical Management System – Back-end (.NET)

Hệ thống quản lý y tế học đường, được phát triển với kiến trúc Clean Architecture sử dụng nền tảng .NET. Dự án hỗ trợ việc quản lý học sinh, hồ sơ sức khỏe, thông báo và xử lý y tế trong môi trường giáo dục.

---

## 📁 Project Structure

```
/SchoolMedicalSystem
│
├── SchoolMedicalSystem.Application     # Application layer (services, use cases, DTOs)
├── SchoolMedicalSystem.Domain          # Domain layer (entities, value objects, interfaces)
├── SchoolMedicalSystem.Infrastructure  # Infrastructure (EF Core, database, external services)
├── SchoolMedicalSystem                 # Web API project (Controllers, Middleware, DI setup)
├── SchoolMedicalSystem.sln             # Solution file
├── .gitignore
└── README.md
```

---

## 🚧 Branch Naming Convention

```
type/scope_author
```

- `feature`: Phát triển tính năng  
- `bugfix`: Sửa lỗi  
- `hotfix`: Sửa lỗi khẩn cấp  
- `refactor`: Tối ưu code, không thay đổi logic  
- `chore`: Công việc phụ trợ (cấu hình, script...)

📌 **scope**: Chức năng cụ thể (auth, student, health-record...)  
📌 **author**: Tên viết thường, không dấu (ví dụ: `trungnt`, `duytv`)

🔹 **Ví dụ**:
```bash
feature/auth_trungnt
bugfix/health-record_duytv
```

---

## 📝 Commit Message Convention

```
<type>(<scope>): <short-description>
```

### ✅ type gồm:
- `Add`: Thêm mới tính năng  
- `Update`: Cập nhật tính năng hoặc logic  
- `Fix`: Sửa lỗi  
- `Refactor`: Tối ưu code (không thay đổi logic)  
- `Delete`: Xóa bỏ chức năng / đoạn code

🔹 **Ví dụ**:
```bash
Add(auth): implement jwt authentication
Fix(student): validate student age input
Refactor(health-record): clean up controller logic
```

---

## 🌐 API Naming Convention

- Dùng **plural nouns**: `/students`, `/health-records`
- Dùng **lowercase** và **dấu gạch ngang (-)** trong URL
- Tối đa **2 cấp nested resource**
- Hành động đặc biệt đặt rõ ở cuối endpoint

🔹 **Ví dụ**:
```http
GET    /students
POST   /students
GET    /students/{id}/health-records
PATCH  /health-records/{id}/approve
```

---

## 📦 API Response Format

- Trả về theo định dạng **PascalCase** (chuẩn C#)
- Phía client (React, Angular, ...) có thể convert sang `camelCase` nếu cần

🔹 **Mẫu Response**:
```json
{
  "StatusCode": 200,
  "Message": "Success",
  "Data": {
    "StudentId": 123,
    "FullName": "Nguyen Van A",
    "HealthRecords": [...]
  }
}
```

---

## 🛠️ Tech Stack

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Swagger** (API documentation)
- **Clean Architecture** Pattern

---

## 👨‍💻 Development Guide

1. Clone project:
   ```bash
   git clone https://github.com/<your-org>/school-medical-management-system.git
   ```

2. Cấu hình `appsettings.Development.json` tại dự án Web API:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=SchoolMedicalDb;Trusted_Connection=True;"
     },
     "Jwt": {
       "Key": "your-secret-key",
       "Issuer": "your-app",
       "Audience": "your-app"
     }
   }
   ```

3. Run migration & database:
   ```bash
   dotnet ef database update --project SchoolMedicalSystem.Infrastructure
   ```

4. Run project:
   ```bash
   dotnet run --project SchoolMedicalSystem
   ```

5. Mở Swagger UI:
   ```
   http://localhost:<port>/swagger
   ```

---

## 📬 Contact

Mọi thắc mắc vui lòng liên hệ team backend qua Slack hoặc Email nội bộ.

---
