<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Mobile.Master" CodeBehind="Student.aspx.cs" Inherits="CRUD_WebForm.Student" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent" ID="content1">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" integrity="sha512-3pIirOrwegjM6erE5gPSwkUzO+3cTjpnV9lexlNZqvupR64iZBnOOTiiLPb9M36zpMScbmUNIcHUqKD47M719g==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js" integrity="sha512-VEd+nq25CkR676O+pLBnDW09R7VQX9Mdiij052gVCp5yVH3jGtH70Ho/UUv4mJDsEdTvqRCFZg0NKGiojGnUCw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqgrid/5.8.6/js/jquery.jqGrid.min.js" integrity="sha512-1mgShKv6gA71WQw47fS84EWJQCVCDjY/PujUTHhEWs8491Td4LGiL24XedDzkVASTMky5nAx/Ku1ucdOqq3X9w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqgrid/5.8.6/css/ui.jqgrid-bootstrap-ui.min.css" integrity="sha512-Le5SZRnS7S4ArmiYClxIKwWLrs9SoHPyh+oQG7zSBbvm0QQosIoKLW6w7etCry7JlH4Zg4Yr54rWcHf9tq741g==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="JS/Student.js"></script>
    <script>
        var nameJ = '<%=name.ClientID%>';
        var majorJ = '<%=major.ClientID%>';
        var contactJ = '<%=contact.ClientID%>';
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="FeaturedContent" ID="content2">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="content3">
    <div class="container">
        <div class="row">
            <div class="offset-5 col-2">
                <h1>Student Form</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-4">
                <label class="form-label">Name</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="name" placeholder="John" type="text" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-4">
                <label class="form-label">Major</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="major" type="text" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-4">
                <label class="form-label">Contact</label>
                <asp:TextBox runat="server" ClientIDMode="Static" ID="contact" type="number" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-2">
                <input type="submit" value="Add Student" class="btn btn-primary" onclick="insertStudent()" id="addStudent" />
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <table id="tblStudentGrid">
            </table>
            <div id="tblStudentGridPager">
            </div>
        </div>
    </div>
</asp:Content>

