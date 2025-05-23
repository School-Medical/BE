# BE
School Medical Management System: Back-end, write by .NET
1. 🚧 Quy Ước Đặt Tên Nhánh (Branch Naming Convention)
	<type>/<scope>_<author>
 
Trong đó: 
- type: Loại công việc (feature, bugfix, hotfix, refactor, chore). Nhiều như vậy nhưng mình xài feature (để phát triển tính năng là chủ yếu).
- scope: Phạm vi chức năng (ví dụ: auth, order, product, dashboard...)
- author: Tên người thực hiện viết thường, không dấu, có thể dùng dấu gạch ngang (trungnt, duytv...)
  
  Ví dụ: feature/auth_trungnt

2. 📝 Quy Ước Commit Message (Commit Message Convention)
	<type>(<scope>): <short-description>
 
 Trong đó:
- type:
  + Add: Thêm mới tính năng
  + Update: Cập nhật logic/tính năng
  + Fix: Sửa lỗi
  + Refactor: Tối ưu, làm sạch code không đổi logic
  + Delete: Xóa bỏ tính năng
- scope: Phạm vi ảnh hưởng
- short-description: Mô tả ngắn, tiếng Anh
  
  Ví dụ: Add(auth): implement jwt authentication
  
3. 🌐 Quy Ước Đặt Tên API (API Naming Convention)
Quy tắc chung:
- Plural nouns cho resource
- Lowercase + dấu gạch ngang (-) 
- Tối đa 2 cấp nested resource
- Các hành động đặc biệt được tách rõ (nếu cần): /orders/{id}/cancel

 4. 📦 Quy Ước Response Object (API Response Format)
Quy tắc:
- API trả về dữ liệu dạng PascalCase để đồng bộ với C# convention.
- Phía client (React/Angular) có thể map sang camelCase nếu cần.
