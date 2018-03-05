<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="BanHang.DangNhap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>Công ty GPM Việt Nam</title>
      <link href="css/style.css" rel="stylesheet" />
</head>
<body>
   <hgroup>
  <h1>HỆ THỐNG QUẢN LÝ BÁN HÀNG</h1>
  <h3>Đăng Nhập Hệ Thống</h3>
</hgroup>
    <form id="form1" runat="server" method="post">
        <div class="group">
            <input type="text" name ="txtDangNhap" id="txtDangNhap" autocomplete="new-password"  runat="server"/>
              <span class="highlight"></span><span class="bar"></span>
            <label>Tên Đăng Nhập</label>
         </div>
          <div class="group">
            <input type="password" name ="txtMatKhau" id="txtMatKhau" autocomplete="new-password"  runat="server"><span class="highlight"></span><span class="bar" aria-grabbed="undefined"/></span>
            <label>Mật Khẩu</label>
          </div>
   
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button id="btnDangNhapQuanLy" class="button buttonBlue" runat="server"  onserverclick="btnDangNhapQuanLy_Click">Đăng nhập</button>
    </form>
     <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script> 
  <script src="css/index.js"></script>
</body>
</html>
