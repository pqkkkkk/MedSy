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
- Xây dựng server expressJS phục vụ cho tính năng chat và video call realtime thông qua phương thức websocket
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
- Ở milestone 1, tài khoản và mật khẩu sử dụng dữ liệu cứng với username = "pqkiet854", password = "pqkiet854" với role là bệnh nhân
- Nếu username và password chính xác thì chuyển sang cửa sổ chính của ứng dụng. Ngược lại, hiển thị thông báo sai thông tin và phải nhập lại
##### Demo
- Sai thông tin

![alt text](./report_resource/{5BBBA957-A905-47B5-8A77-C80D38157485}.png)
- Chuyển sang màn hình chính

![alt text](./report_resource/{5EA60369-D3C3-4FBB-B066-FD0E2A85DB84}.png)
#### Tư vấn trực tuyến thông qua nhắn tin
##### Kiểm thử
- Để kiểm thử tính năng chat realtime ở milestone 1, tạo 1 chat client web và gửi tin nhắn đến client winui
- Khi chat client web gửi tin nhắn đến cho client winui, sẽ có các trường hợp:
    - Client winui đang offline: socket server sẽ lưu tin nhắn này xuống database để thực hiện truy vấn khi client winui này online (sẽ thực hiện khi kết nối ứng dụng winui này với database).
    - Client winui đang online, có các trường hợp:
        -  Client winui đang ở trong đoạn hội thoại với client web: cập nhật tin nhắn trực tiếp lên giao tiếp
        - Client winui đang ở trong đoạn hội thoại với user khác:
        - Client winui không ở trong trang chat: hiển thị 1 dấu hiệu ở thanh điều hướng để thông báo có tin nhắn tới và tắt dấu hiệu này khi người dùng chuyển qua trang chat 
- Trong từng trường hợp trên, gửi tin nhắn từ client web đến client winui để kiểm thử
##### Demo
- Khi client winui chưa kết nối tới bất kì user nào

![all text](./report_resource/Screenshot%202024-11-05%20224439.png)
- Khi client winui đang ở đoạn hội thoại với user **Lionel Messi** thì làm nổi bật button **John Doe** để thông báo có tin nhắn mới, khi nhấn vào button **John Doe** để mở đoạn hội thoại với **John Doe** thì chuyển màu nền trở lại như bình thường

![alt text](./report_resource/Screenshot%202024-11-05%20205530.png)
- Khi client winui không ở trang chat

![all text](./report_resource/Screenshot%202024-11-05%20210021.png)
- Khi client winui ở trong đoạn hội thoại với client web

![all text](/report_resource/Screenshot%202024-11-05%20210328.png)
#### Xem danh sách các bác sĩ. Cho feedback về chất lượng của bác sĩ
##### Kiểm thử
##### Demo
### Hướng dẫn chạy ứng dụng ở milestone 1
1. Tải môi trường thực thi javascript NodeJS
2. Chuyển đến folder server
```bash
cd server
```
3. Cài đặt các phụ thuộc
``` bash
npm install express socket.io
``` 
4. Khởi chạy socket server
``` bash
node server.js
```
5. Chạy ứng dụng winui (folder MedSy) và đăng nhập với username và password đều là **"pqkiet854"** để vào cửa sổ chính
6. (Optional) Truy cập địa chỉ http://localhost:5555 để mở client web (client web này được xây dựng với mục đích kiểm thử tính năng chat realtime ở milestone 1)

### Số giờ làm việc
### Điểm tự đánh giá