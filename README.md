# MedSy - Đồ án môn học Lập Trình Windows
## Mục lục
- [Thông tin nhóm](#i-thông-tin-nhóm)
- [Các kỹ thuật (công nghệ) sử dụng trong đồ án](#ii-các-kỹ-thuật-công-nghệ-sử-dụng-trong-đồ-án)
- [Milestone 1](#iii-milestone-1)
    - [Làm việc nhóm](#1-làm-việc-nhóm)
    - [Các chức năng đã làm](#2-các-chức-năng-đã-làm)
    - [Hướng dẫn chạy ứng dụng](#3-hướng-dẫn-chạy-ứng-dụng-ở-milestone-1)
    - [Số giờ làm việc](#4-số-giờ-làm-việc)
    - [Điểm tự đánh giá](#5-điểm-tự-đánh-giá)
 - [Milestone 2](#iv-milestone-2)
    - [Làm việc nhóm](#1-làm-việc-nhóm-milestone2)
    - [Các chức năng đã làm](#2-các-chức-năng-đã-làm-milestone2)
    - [Hướng dẫn chạy ứng dụng](#3-hướng-dẫn-chạy-ứng-dụng-ở-milestone-2)
    - [Số giờ làm việc](#4-số-giờ-làm-việc-milestone2)
    - [Điểm tự đánh giá](#5-điểm-tự-đánh-giá-milestone2)
 - [Milestone 3](#v-milestone-3)
    - [Làm việc nhóm](#1-làm-việc-nhóm-milestone3)
    - [Các chức năng đã làm](#2-các-chức-năng-đã-làm-milestone3)
    - [Hướng dẫn chạy ứng dụng](#3-hướng-dẫn-chạy-ứng-dụng-ở-milestone-3)
    - [Số giờ làm việc](#4-số-giờ-làm-việc-milestone3)
    - [Điểm tự đánh giá](#5-điểm-tự-đánh-giá-milestone3)
## I. Thông tin nhóm
- 22120174 - Phạm Quốc Kiệt
- 22120353 - Nguyễn Quang Thông
## II. Các kỹ thuật (công nghệ) sử dụng trong đồ án
- Áp dụng mô hình MVVM
- WinUI 3
- ExpressJS: xây dựng socket server phục vụ tính năng chat và videl call realtime
- WebRTC: API phục vụ tính năng video call
- VNPAY API: tích hợp thanh toán trực tuyến
## III. Milestone 1
### 1. Làm việc nhóm
#### Phân công công việc
![alt text](./report_resource/{A9505644-AF5A-4751-9C7D-D7A4944BB2A6}.png)
![alt text](./report_resource/{1F3AC46A-7E9B-40AA-B5D2-401BB7F22F3F}.png)
#### Git flow
![alt text](./report_resource/{2CC9D224-49CB-46A2-9E92-230F10C33BD7}.png)
### 2. Các chức năng đã làm
#### a. Đăng nhập
##### Kiểm thử
- Ở milestone 1, tài khoản và mật khẩu sử dụng dữ liệu cứng với username = "pqkiet854", password = "pqkiet854" với role là bệnh nhân
- Nếu username và password chính xác thì chuyển sang cửa sổ chính của ứng dụng. Ngược lại, hiển thị thông báo sai thông tin và phải nhập lại
##### Demo
- Sai thông tin

![alt text](./report_resource/{5BBBA957-A905-47B5-8A77-C80D38157485}.png)
- Chuyển sang màn hình chính

![alt text](./report_resource/{5EA60369-D3C3-4FBB-B066-FD0E2A85DB84}.png)
#### b. Tư vấn trực tuyến thông qua nhắn tin
##### Kiểm thử
- Để kiểm thử tính năng chat realtime ở milestone 1, tạo 1 chat client web để gửi tin nhắn đến client winui. Khi chat client web gửi tin nhắn đến cho client winui, sẽ có các trường hợp:
    - Client winui đang offline: socket server sẽ lưu tin nhắn này xuống database để client winui có thể lấy được tất cả tin nhắn khi online (sẽ thực hiện khi kết nối ứng dụng winui này với database).
    - Client winui đang online, có các trường hợp:
        -  Client winui đang ở trong đoạn hội thoại với client web: cập nhật tin nhắn trực tiếp lên đoạn hội thoại
        - Client winui đang ở trong đoạn hội thoại với user khác
        - Client winui không ở trong trang chat: hiển thị 1 dấu hiệu ở thanh điều hướng để thông báo có tin nhắn tới và tắt dấu hiệu này khi người dùng chuyển qua trang chat
    - Trong từng trường hợp trên, gửi tin nhắn từ client web đến client winui để kiểm thử
- Về kiểm thử giao diện (xem chi tiết dưới demo)
    - Đảm bảo các tin nhắn được hiển thị đúng cách
    - Đảm bảo thông tin của các user khác được hiển thị đúng cách
##### Demo
- Khi client winui chưa kết nối tới bất kì user nào

![all text](./report_resource/Screenshot%202024-11-05%20224439.png)
- Khi client winui đang ở đoạn hội thoại với user **Lionel Messi** thì làm nổi bật button **John Doe** để thông báo có tin nhắn mới, khi nhấn vào button **John Doe** để mở đoạn hội thoại với **John Doe** thì chuyển màu nền trở lại như bình thường

![alt text](./report_resource/Screenshot%202024-11-05%20205530.png)
- Khi có tin nhắn mới gửi đến nhưng client winui không ở trang chat

![all text](./report_resource/Screenshot%202024-11-05%20210021.png)

- Khi client winui ở trong đoạn hội thoại với client web

![alt text](./report_resource/Screenshot%202024-11-05%20210328.png)

- Đảm bảo tin nhắn của người gửi và nhận được hiển thị theo style tương ứng

![alt text](./report_resource/Screenshot%202024-11-06%20104244.png)

- Khi nhấn vào button **Lionel Messi** ở bên trái, đảm bảo thông tin tương ứng của **Lionel Messi** được hiển thị ở đoạn hội thoại

![alt text](./report_resource/Screenshot%202024-11-06%20104557.png)

#### c. Xem danh sách bác sĩ
##### Kiểm thử
- Đảm bảo danh sách bác sĩ được hiển thị chính xác trên giao diện theo đúng bố cục đề ra, bao gồm các thông tin về tên, id, chuyên môn, giới tính, số năm kinh nghiệm

##### Điều kiện tiên quyết
- Ứng dụng đã khởi động và đăng nhập thành công.
- Dữ liệu bác sĩ có sẵn trong danh sách giả lập (sử dụng mock data).

##### Các bước kiểm thử

| Bước | Mô tả | Kết quả mong đợi |
|------|-------|------------------|
| 1    | Tạo một folder Doctor trong Services chứa DoctorMockDao nhằm tạo một danh sách giả lập chứa các bác sĩ với các thuộc tính liên quan (ID, tên, chuyên môn, giới tính, số năm kinh nghiệm,...) | Danh sách được khởi tạo thành công. |
| 2    | Tạo trang Doctor_Infor để xem danh sách bác sĩ, sử dụng Gridview để tổ chức các bác sĩ trên giao diện, hiển thị danh sách các bác sĩ. | Trang được tạo thành công, danh sách bác sĩ được hiển thị trên giao diện theo đúng format. |
| 3    | Kiểm tra số lượng bác sĩ hiển thị trong giao diện có khớp với danh sách giả lập. | Số lượng bác sĩ hiển thị đúng với số lượng trong danh sách giả lập. |
| 4    | Xác nhận thông tin từng bác sĩ hiển thị trên danh sách có đúng với các thông tin từ Mock data không | Thông tin từng bác sĩ hiển thị chính xác. |


#### d. Cho feedback về chất lượng bác sĩ
##### Kiểm thử
- Cho phép bệnh nhân đưa feedback và đánh giá sao cho bác sĩ

##### Điều kiện tiên quyết
- Ứng dụng đã khởi động và đăng nhập thành công.
- Dữ liệu bác sĩ có sẵn trong danh sách giả lập (sử dụng Mock Data).
- Dữ liệu các feedback và đánh giá của một bác sĩ có sẵn trong danh sách giả lập (sử dụng Mock Data)

##### Các bước kiểm thử

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
  
![alt text](./report_resource/Login_Precondition.png)

2. User Dashboard hiển thị lên, điều hướng qua trang Doctor_Infor (icon số 2 trên thanh navigate) để thực hiện việc xem danh sách bác sĩ:
![alt text](./report_resource/UserDashboard.png)

Trang Doctor_Infor hiện lên:

![alt text](./report_resource/Doctor_Infor.png)

Kiểm tra thấy:
- Danh sách bác sĩ hiển thị giao diện đúng số lượng (trường hợp dữ liệu giả lập rỗng, sẽ không hiển thị bác sĩ nào)
- Các thông tin trùng khớp với dự liệu giả lập
- Trên một trang hiển thị số lượng tối đa (SLTĐ) là 9 items, và có sử dụng phân trang nếu dữ liệu nhiều hơn SLTĐ đã set 

3. Click vào 1 item Bác sĩ nào đó trên trang Doctor_Infor để thực hiện điều hướng tới trang DoctorDetail (giả sử click vào item đầu tiên).
- Trang DoctorDetail được điều hướng đến, có thông tin chi tiết bác sĩ trùng khớp với dữ liệu giả lập.
- Có phần comment cho việc xem feedback của bác sĩ và hỗ trợ thực hiện Feedback (dữ liệu giả lập trùng khớp; trường hợp dữ liệu giả lập feedback rỗng hoặc DoctorID trong feedback không trùng với ID của bác sĩ trong danh sách hiện có, sẽ không hiển thị feedback)

![alt text](./report_resource/Doctor_Detailed.png)

4. Thực hiện đưa ra một feedback và đánh giá sao, sau đó nhấn gửi
- Nhập một đánh giá nào đó vào ô textbox, rating sao sau đó ấn gửi

![alt text](./report_resource/test_comment.png)

- Thông tin hiển thị lên phần comment

![alt text](./report_resource/comment_success.png)

- Trường hợp comment không hợp lệ (Không comment và rating nhưng bấm nút gửi, comment nhưng không rating) sẽ hiện thông báo cho người dùng
 
![alt text](./report_resource/InvalidCommentSituation.png)

### 3. Hướng dẫn chạy ứng dụng ở milestone 1
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
5. Vào folder của project WinUI MedSy -> Assets -> font -> fontAwesome -> webfonts -> mở file **fa-solid-900.ttf**. Nhấn **Install** để tải fontAwesome về máy. Bước này để tránh bị lỗi font icon khi chạy ứng dụng

![all text](./report_resource/Screenshot%202024-11-06%20185401.png)

6. Chạy ứng dụng winui (folder MedSy) và đăng nhập với username và password đều là **"pqkiet854"** để vào cửa sổ chính
7. (Optional) Truy cập địa chỉ http://localhost:5555 để mở client web (client web này được xây dựng với mục đích kiểm thử tính năng chat realtime ở milestone 1)

### 4. Số giờ làm việc

| STT | Tính năng | Mô tả | Số giờ làm việc |
|-----------|-------|------------------|------------------|
| 1    | Đăng nhập | Cho phép người dùng đăng nhập bằng tài khoản của mình | 1 |
| 2    | Tìm kiếm thông tin bác sĩ | Cho phép tìm kiếm bác sĩ theo chuyên khoa, số năm kinh nghiệm, giới tính, tìm theo tên | 1 |
| 3    | Tư vấn trực tuyến | Cho phép bệnh nhân trao đổi với bác sĩ về các vấn đề liên quan đến sức khoẻ thông qua tin nhắn | 2 |
| 4    | Đánh giá và phản hồi | Cho phép bệnh nhân gửi đánh giá và phản hồi về chất lượng khám của bác sĩ| 2 |
| Tổng số giờ làm việc | | | 6 |

### 5. Điểm tự đánh giá
| STT | Tính năng | Điểm tự đánh giá |
|------|-------|------------------|
| 1    | Đăng nhập | 9.5 |
| 2    | Tìm kiếm thông tin bác sĩ | 9.5 |
| 3    | Tư vấn trực tuyến | 9.5 |
| 4    | Đánh giá và phản hồi| 9.5 |
| Điểm đánh giá chung | | 9.5 |

## IV. Milestone 2
### 1. Làm việc nhóm <a id="1-làm-việc-nhóm-milestone2"></a>
#### Phân công công việc
![all text](./report_resource/PhanCongCVMilestone2.png)
#### Gitflow
![all text](./report_resource/GitflowMilestone2.png)
### 2. Các chức năng đã làm <a id="2-các-chức-năng-đã-làm-milestone2"></a>
#### a. Đặt lịch khám trực tuyến
#### b. Khám trực tuyến thông qua video
#### c. Lịch làm việc cá nhân hàng ngày hàng tuần
#### d. Phản hồi về lịch đặt khám của bệnh nhân
#### e. Quản lý bệnh án
#### f. Kê đơn thuốc cho bệnh nhân
Tài liệu kiểm thử các chức năng có thể xem tại đây: https://docs.google.com/document/d/1RY0cHzI2d7jwbLXKoADU_ok9_psdyXdH/edit?usp=sharing&ouid=103352419354890159322&rtpof=true&sd=true

Video demo các chức năng có thể xem tại đây: https://drive.google.com/file/d/1YKPbvwHld7VH5gADfua1ei8gzgkhR794/view?usp=drive_link
### 3. Hướng dẫn chạy ứng dụng ở milestone 2
1. Tải môi trường thực thi javascript NodeJS
2. Chuyển đến folder db_migration
```bash
cd db_migration
```
3. Thiết lập các thông số kết nối đến database trong file .env
4. Chạy các lệnh sau để tạo database và dữ liệu mẫu
```bash
knex migrate:latest
```
```bash
knex seed:run
```
5. Chuyển đến folder server
```bash
cd ..
cd server
```
6. Cài đặt các phụ thuộc
``` bash
npm install express socket.io
``` 
```
npm install config crypto dateformat fs qs
```
7. Khởi chạy socket server
``` bash
node server.js
```
8. Vào folder của project WinUI MedSy -> Assets -> font -> fontAwesome -> webfonts -> mở file **fa-solid-900.ttf**. Nhấn **Install** để tải fontAwesome về máy. Bước này để tránh bị lỗi font icon khi chạy ứng dụng

![all text](./report_resource/Screenshot%202024-11-06%20185401.png)

9. Chạy ứng dụng winui (folder MedSy) và đăng nhập với username và password đều là **"pqkiet854"** (vai trò là bệnh nhân) hoặc username **"johndoe""** và password **"pqkiet854"** (vai trò bác sĩ) để vào cửa sổ chính.

### 4. Số giờ làm việc <a id="4-số-giờ-làm-việc-milestone2"></a>

| STT | Tính năng | Mô tả | Số giờ làm việc |
|-----------|-------|------------------|------------------|
| 1    | Đặt lịch khám trực tuyến | Cho phép bệnh nhân đặt lịch khám ngoại trú thông qua ứng dụng | 1 |
| 2    | Khám trực tuyến thông qua video | Cho phép khám bệnh trực tuyến thông qua ứng dụng | 2 |
| 3    | Lịch làm việc cá nhân hàng ngày/ hàng tuần | Cho phép bác sĩ quản lý lịch trình làm việc trong ngày/tuần và theo dõi các lịch đặt khám của bệnh nhân | 2 |
| 4    | Phản hồi về lịch đặt khám của bệnh nhân | Cho phép bác sĩ từ chối/ chấp nhận lịch khám của bệnh nhân | 1 |
| 5    | Quản lý bệnh án | Cho phép bệnh nhân xem được các hồ sơ bệnh án đã khám của mình và cho phép bác sĩ quản lý hồ sơ bệnh án của bệnh nhân mà mình đã điều trị | 1 |
| 6    | Kê đơn thuốc cho bệnh nhân | Dựa vào các chẩn đoán cho phép bác sĩ kê đơn thuốc phù hợp với bệnh nhân | 1 |
| Tổng số giờ làm việc | | | 8 |

### 5. Điểm tự đánh giá <a id="5-điểm-tự-đánh-giá-milestone2"></a>
| STT | Tính năng | Điểm tự đánh giá |
|------|-------|------------------|
| 1    | Đặt lịch khám trực tuyến | 9.5 |
| 2    | Khám trực tuyến thông qua video | 9.5 |
| 3    | Lịch làm việc cá nhân hàng ngày/ hàng tuần | 9.5 |
| 4    | Phản hồi về lịch đặt khám của bệnh nhân | 9.5 |
| 5    | Quản lý bệnh án | 9.5 |
| 6    | Kê đơn thuốc cho bệnh nhân | 9.5 |
| Điểm đánh giá chung | | 9.5 |

## V. Milestone 3
### 1. Làm việc nhóm <a id="1-làm-việc-nhóm-milestone3"></a>
#### Phân công công việc
![all text](./report_resource/phancongcongviec_milestone3.png)
#### Git flow
![all text](./report_resource/gitflow_milestone3.png)
#### Họp nhóm
![all text](./report_resource/hopnhom_milestone3.png)
### 2. Các chức năng đã làm <a id="2-các-chức-năng-đã-làm-milestone3"></a>
#### a. Mua thuốc theo toa và thanh toán trực tuyến
#### b. Xem hoá đơn trực tuyến
#### c. Thống kê
#### d. Trợ lý thông minh
#### e. Cải thiện UI trang quản lý bệnh nhân
Tài liệu kiểm thử các chức năng có thể xem tại đây: https://docs.google.com/document/d/1Off0Ytp16BNc8iRU4r8Ro9Ead9VxN4ZF/edit?usp=sharing&ouid=103352419354890159322&rtpof=true&sd=true
### 3. Hướng dẫn chạy ứng dụng ở milestone 3
[Giống như milestone 2](#3-hướng-dẫn-chạy-ứng-dụng-ở-milestone-2)
### 4. Số giờ làm việc <a id="4-số-giờ-làm-việc-milestone3"></a>
| STT | Tính năng | Mô tả | Số giờ làm việc |
|-----------|-------|------------------|------------------|
| 1    | Mua thuốc theo toa và thanh toán trực tuyến | Cho phép đặt thuốc theo toa trực tiếp trên ứng dụng và thanh toán trực tuyến | 2 |
| 2    | Xem hoá đơn trực tuyến | Cho phép xem các đơn thuốc đã thanh toán trực tuyến | 1 |
| 3    | Thống kê | Thống kê tần suất khám bệnh với các loại bệnh lý, tần suất khám bệnh trực tuyến, thống kê doanh thu qua ngày/tháng/năm, thống kê, quản lý thuốc trong kho | 2 |
| 4    | Trợ lý thông minh | Thông báo nhắc nhở cho bác sĩ về các cuộc hẹn đặt khám của bênh nhân sắp tới mà mình đã chấp nhận, nhắc nhở bệnh nhân về các lịch khám sắp tới của mình | 2 |
| Tổng số giờ làm việc | | | 7 |
### 5. Điểm tự đánh giá <a id="5-điểm-tự-đánh-giá-milestone3"></a>
| STT | Tính năng | Điểm tự đánh giá |
|-----------|-------|------------------|
| 1    | Mua thuốc theo toa và thanh toán trực tuyến | 9.5 |
| 2    | Xem hoá đơn trực tuyến | 9.5 |
| 3    | Thống kê | 9.5 |
| 4    | Trợ lý thông minh | 9.5 |
| Điểm đánh giá chung | | 9.5 |
