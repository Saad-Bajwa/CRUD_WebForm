var intStudentId = 0;
$(document).ready(function () {
    LoadStudentGrid();
});


function insertStudent() {
    var varname = document.getElementById(nameJ).value;
    var varmajor = document.getElementById(majorJ).value;
    var varcontact = document.getElementById(contactJ).value;
    if (varname == "" || varname == null || varmajor == "" || varmajor == null || varcontact == "" || varcontact == null) {
        toastr.warning("Please Enter all the fields", "Empty Field Message", { timeout: 1500 });
    }
    else {
        if (intStudentId == 0) {
            $.ajax({
                url: 'Handler/Student.ashx?Insert=1' + '&varname=' + varname + '&varmajor=' + varmajor + '&varcontact=' + varcontact,
                type: 'Post',
                dataType: 'json',
                success: function () {
                    toastr.success("Successfully added the record", "Insert Message", { timeout: 1500 })
                },
                error: function () {
                    toastr.error("Error in saving record", "Error in saving", { timeout: 1500 })
                }
            });
        }
        else {
            $.ajax({
                url: 'Handler/Student.ashx?Update=1' + '&intStudentId=' + intStudentId + '&varname=' + varname + '&varmajor=' + varmajor + '&varcontact=' + varcontact,
                type: 'Post',
                dataType: 'json',
                success: function () {
                    emptyFields();
                    intStudentId = 0;
                    $('#name').val("");
                    $('#major').val("");
                    $('#contact').val("");
                    toastr.success("Successfully updated the record", "Insert Message", { timeout: 1500 })

                },
                error: function () {
                    toastr.error("Error in saving record", "Error in saving", { timeout: 1500 })
                }
            });
        }
    }
}
function emptyFields() {
    $('#name').val("");
    $('#major').val("");
    $('#contact').val("");
}

function LoadStudentGrid() {
    $('#tblStudentGrid').jqGrid({
        type: 'GET',
        url: 'Handler/Student.ashx?GetStudentGrid=1',
        datatype: 'json',
        colNames: ['ID', 'Student Name', 'Major', 'Contact', 'Action'],
        colModel: [
            { name: 'intProductID', width: 10, hidden: true },
            { name: 'varName', width: 50, sortable: true },
            { name: 'varMajor', width: 50, sortable: true },
            { name: 'varContact', width: 50, sortable: true },
            { name: 'Action', width: 50, sortable: true }
        ]
    });
    /*$('#tblProductGrid').jqGrid('navGrid', '#tblStudentGridPager', { edit: false, add: false, del: false, search: false });*/
    var DataGridBranch = jQuery('#tblStudentGrid');
    DataGridBranch.jqGrid('setGridWidth', parseInt($(window).width()) - 380);
    $(window).resize(function () {
        DataGridBranch.jqGrid('setGridWidth', parseInt($(window).width()) - 380);
    });
}

function editStudent(id) {
    event.preventDefault();
    intStudentId = id;
    $.ajax({
        url: 'Handler/Student.ashx?EditStudent=1' + '&intStudentId=' + intStudentId,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            intStudentId = response.intStudentId;
            $('#name').val(response.varName);
            $('#major').val(response.varMajor);
            $('#contact').val(response.varContact);
            $('#addStudent').val("Update Student");
        },
        error: function () {
            console.error("Error");
        }
    });
}

function deleteStudent(id) {
    intStudentId = id;
    $.ajax({
        url: 'Handler/Student.ashx?DeleteStudent=1' + '&intStudentId=' + intStudentId,
        type: 'Post',
        success: function () {
            debugger;
            intStudentId = 0;
            toastr.success("Successfully deleted the record", "Insert Message", { timeout: 1500 })
            LoadStudentGrid();
        },
        error: function () {
            console.error("Error");
        }
    });
}
