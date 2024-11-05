# MedSy - Đồ án môn học Lập Trình Windows
## Mục lục
- [Thông tin nhóm](#thông-tin-nhóm)
- [Các kỹ thuật (công nghệ) sử dụng trong đồ án](#các-kỹ-thuật-công-nghệ-sử-dụng-trong-đồ-án)
- [Milestone 1](#milestone-1)
    - [Làm việc nhóm](#làm-việc-nhóm)
    - [Các chức năng đã làm](#các-chức-năng-đã-làm)
    - [Hướng dẫn chạy ứng dụng](#hướng-dẫn-chạy-ứng-dụng)
    - [Số giờ làm việc](#số-giờ-làm-việc)
    - [Điểm tự đánh giá](#điểm-tự-đánh-giá)
## Thông tin nhóm
- 22120174 - Phạm Quốc Kiệt
- 22120353 - Nguyễn Quang Thông
## Các kỹ thuật (công nghệ) sử dụng trong đồ án
- Sử dụng mô hình MVVM
- WinUI 3
- Xây dựng server expressJS phục vụ cho tính năng chat realtime thông qua phương thức websocket
## Milestone 1
### Làm việc nhóm
#### Phân công công việc
![alt text](./report_resource/{A9505644-AF5A-4751-9C7D-D7A4944BB2A6}.png)
![alt text](./report_resource/{1F3AC46A-7E9B-40AA-B5D2-401BB7F22F3F}.png)
#### Git flow
![alt text](./report_resource/{2CC9D224-49CB-46A2-9E92-230F10C33BD7}.png)
### Các chức năng đã làm
#### Đăng nhập
##### Kiểm thử
##### Demo
#### Tư vấn trực tuyến thông qua nhắn tin
##### Kiểm thử
##### Demo
#### Xem danh sách các bác sĩ. Cho feedback về chất lượng của bác sĩ
#### Xem danh sách bác sĩ
##### Kiểm thử
- Đảm bảo danh sách bác sĩ được hiển thị chính xác trên giao diện theo đúng bố cục đề ra, bao gồm các thông tin về tên, id, chuyên môn, giới tính, số năm kinh nghiệm

##### Điều kiện tiên quyết
Ứng dụng đã khởi động và đăng nhập thành công.
Dữ liệu bác sĩ có sẵn trong danh sách giả lập (sử dụng mock data).

#### Các bước kiểm thử

| Bước | Mô tả | Kết quả mong đợi |
|------|-------|------------------|
| 1    | Tạo một folder Doctor trong Services chứa DoctorMockDao nhằm tạo một danh sách giả lập chứa các bác sĩ với các thuộc tính liên quan (ID, tên, chuyên môn, giới tính, số năm kinh nghiệm,...) | Danh sách được khởi tạo thành công. |
| 2    | Tạo trang Doctor_Infor để xem danh sách bác sĩ, sử dụng Gridview để tổ chức các bác sĩ trên giao diện, hiển thị danh sách các bác sĩ. | Trang được tạo thành công, danh sách bác sĩ được hiển thị trên giao diện theo đúng format. |
| 3    | Kiểm tra số lượng bác sĩ hiển thị trong giao diện có khớp với danh sách giả lập. | Số lượng bác sĩ hiển thị đúng với số lượng trong danh sách giả lập. |
| 4    | Xác nhận thông tin từng bác sĩ hiển thị trên danh sách có đúng với các thông tin từ Mock data không | Thông tin từng bác sĩ hiển thị chính xác. |


#### Cho feedback về chất lượng bác sĩ
##### Kiểm thử
- Cho phép bệnh nhân đưa feedback và đánh giá sao cho bác sĩ

##### Điều kiện tiên quyết
- Ứng dụng đã khởi động và đăng nhập thành công.
- Dữ liệu bác sĩ có sẵn trong danh sách giả lập (sử dụng Mock Data).
- Dữ liệu các feedback và đánh giá của một bác sĩ có sẵn trong danh sách giả lập (sử dụng Mock Data)

#### Các bước kiểm thử

| Bước | Mô tả | Kết quả mong đợi |
|------|-------|------------------|
| 1    | Tạo một folder Feedback trong Services chứa FeedbackMockDao nhằm tạo một danh sách giả lập lưu trữ các Feedback từ một bệnh nhân đến một bác sĩ với các thuộc tính như nội dung feedback, đánh giá sao | Danh sách được khởi tạo thành công. |
| 2    | Tạo một trang DoctorDetail chứa thông tin chi tiết bác sĩ, hiển thị phần comment giúp xem và thực hiện các feedback cho bác sĩ| Trang được tạo thành công, giao diện tổ chức theo đúng format|
| 3    | Dựa trên trang Doctor_infor, khi Ckick chọn một bác sĩ thì navigate tới trang DoctorDetail để thực hiện việc xem thông tin chi tiết, thực hiện feedback và đánh giá sao | điều hướng đến được trang DoctorDetail với thông tin chi tiết đúng với thông tin của bác sĩ đã chọn ở DoctorInfor |
| 4    | Trên trang DoctorDetail, Kiểm tra thông tin phần comment (ID bệnh nhân đưa feedback, nội dung feedback, rating) có khớp với danh sách giả lập | Thông tin và số lượng hiển thị đúng theo danh sách giả lập|
| 5    | Bên trong phần comment, thực hiện đưa đánh giá bằng cách điền bình luận vào Textbox, đánh giá sao qua Rating và ấn nút comment để gửi feedback| Thông tin về bệnh nhân đánh giá, nội dung đánh giá, rating hiển thị được lên phần comment|

##### Demo
1. Đăng nhập:
- Nhập thông tin đăng nhập như bên dưới và bấm vào nút sign in
     - Username: pqkiet854
     - Password: pqkiet854
![alt text]() chèn ảnh

2. User Dashboard hiển thị lên, điều hướng qua trang Doctor_Infor (icon số 2 trên thanh navigate) để thực hiện việc xem danh sách bác sĩ:
![alt text]() chèn ảnh
Trang Doctor_Infor hiện lên:
![alt text]() chèn ảnh
Kiểm tra thấy:
- Danh sách bác sĩ hiển thị giao diện đúng số lượng
- Các thông tin trùng khớp với dự liệu giả lập
3. Click vào 1 item Bác sĩ nào đó trên trang Doctor_Infor để thực hiện điều hướng tới trang DoctorDetail (giả sử click vào item đầu tiên).
![alt text]() chèn ảnh
- Trang DoctorDetail được điều hướng đến, các thông tin hiển thị (thông tin bác sĩ, các feedback của bệnh nhân) trùng khớp với dữ liệu giả lập.
- Có phần comment cho việc xem feedback của bác sĩ và hỗ trợ thực hiện Feedback
![alt text]() chèn ảnh

4. Thực hiện đưa ra một feedback và đánh giá sao, sau đó nhấn gửi
- Nhập một đánh giá nào đó vào ô textbox, rating sao sau đó ấn gửi, thông tin hiển thị lên danh sách các feedback
![alt text]() chèn ảnh

### Hướng dẫn chạy ứng dụng
### Số giờ làm việc
### Điểm tự đánh giá
