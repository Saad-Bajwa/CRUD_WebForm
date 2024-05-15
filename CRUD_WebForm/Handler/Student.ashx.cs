using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace CRUD_WebForm.Handler
{
    /// <summary>
    /// Summary description for Student
    /// </summary>
    public class Student : IHttpHandler
    {
        SqlConnection myCon = new SqlConnection("Data Source=SAAD\\SQLEXPRESS;Initial Catalog=MasterDetails;Integrated Security=True;");
        int studentId = 0;
        string name = "";
        string major = "";
        string contact = "";
        DataSet ds = new DataSet();

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["intStudentId"] != null)
            {
                this.studentId = Convert.ToInt32(context.Request["intStudentId"]);
            }
            if (context.Request["varname"] != null)
            {
                this.name = context.Request["varname"].ToString();
            }
            if (context.Request["varcontact"] != null)
            {
                this.contact = context.Request["varcontact"].ToString();
            }
            if (context.Request["varmajor"] != null)
            {
                this.major = context.Request["varmajor"].ToString();
            }
            if (context.Request["Insert"] != null && context.Request["Insert"] == "1")
            {
                InsertStudent(name, contact, major);
            }
            else if (context.Request["GetStudentGrid"] !=null && context.Request["GetStudentGrid"] == "1")
            {
                PagedDataSource students = GetStudents();
                string output = BuildJQStudentGrid(students);
                context.Response.ContentType = "application/json";
                context.Response.Write(output);
            }
            else if (context.Request["EditStudent"] != null && context.Request["EditStudent"] == "1")
            {
                tblStudent std = EditStudent(studentId);
                string output = new JavaScriptSerializer().Serialize(std);
                context.Response.ContentType = "application/json";
                context.Response.Write(output);
            }
            else if (context.Request["Update"] != null && context.Request["Update"] == "1")
            {
                UpdateStudent(studentId, name, major, contact);
            }
            else if(context.Request["DeleteStudent"] != null && context.Request["DeleteStudent"] == "1")
            {
                DeleteStudent(studentId);
            }
        }
        void InsertStudent(string varName, string varMajor, string varContact)
        {
            string query = "Insert into tbl_Student(varName, varMajor, varContact) values ('" + varName + "','" + varMajor + "','" + varContact + "')";
            myCon.Open();
            SqlCommand cmd = new SqlCommand(query, myCon);
            cmd.ExecuteNonQuery();
            myCon.Close();
        }
        void DeleteStudent(int id)
        {
            string query = "Delete from tbl_Student where intStudentId = " + id + " ";
            myCon.Open();
            SqlCommand cmd = new SqlCommand(query,myCon);
            cmd.ExecuteNonQuery();
            myCon.Close();
        }
        void UpdateStudent(int id, string name, string major, string contact)
        {
            string query = "Update tbl_Student Set varName = '" + name + "', varMajor = '" + major + "', varContact = '" + contact + "' where intStudentId = " + id + " ";
            myCon.Open();
            SqlCommand cmd = new SqlCommand(query, myCon);
            cmd.ExecuteNonQuery();
            myCon.Close();
        }
        tblStudent EditStudent(int id)
        {
            tblStudent student = new tblStudent();
            string query = "Select * from tbl_Student where intStudentId = " + id + "";
            myCon.Open();
            SqlCommand cmd = new SqlCommand(query, myCon);
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                student.intStudentId = Convert.ToInt32(myReader[0]);
                student.varName = myReader[1].ToString();
                student.varMajor = myReader[2].ToString();
                student.varContact = myReader[3].ToString();    
            }
            myReader.Close();
            myCon.Close();
            return student;
        }
        PagedDataSource GetStudents()
        {
            PagedDataSource pds = new PagedDataSource();
            if (ds.Tables["load"] != null)
            {
                ds.Tables.Clear();
            }
            string query = "Select intStudentId,varName,varMajor,varContact from tbl_Student";
            myCon.Open();
            SqlDataAdapter myAdapter = new SqlDataAdapter(query,myCon);
            myAdapter.Fill(ds);
            if(ds.Tables.Count > 0)
            {
                pds.DataSource = ds.Tables[0].DefaultView;
            }
            myCon.Close();
            return pds;
        }
        string BuildJQStudentGrid(PagedDataSource pds)
        {
            JQGridResultsAdminManageSection result = new JQGridResultsAdminManageSection();
            List<JQGridRowAdminManageSection> rows = new List<JQGridRowAdminManageSection>();
            DataView dt = new DataView();
            dt = (DataView)pds.DataSource;
            for(int i=0; i<dt.Table.Rows.Count; i++)
            {
                JQGridRowAdminManageSection row = new JQGridRowAdminManageSection();
                row.id = dt.Table.Rows[i]["intStudentId"].ToString();
                row.cell = new string[5];
                row.cell[0] = dt.Table.Rows[i]["intStudentId"].ToString();
                row.cell[1] = dt.Table.Rows[i]["varName"].ToString();
                row.cell[2] = dt.Table.Rows[i]["varMajor"].ToString();
                row.cell[3] = dt.Table.Rows[i]["varContact"].ToString();
                string str1 = "";
                str1 += "<button class='btn btn-info' onClick=editStudent('" + dt.Table.Rows[i]["intStudentId"].ToString() + "') ><span>Edit</span></button> ";
                str1 += "<button class='btn btn-danger' onClick=deleteStudent('" + dt.Table.Rows[i]["intStudentId"].ToString() +"') ><span>Delete</span></button>";
                row.cell[4] = "<div class = 'dropdowngrid dropdowngrid-content'> " + str1 + "</div>";
                rows.Add(row);
            }
            result.rows = rows.ToArray();
            return new JavaScriptSerializer().Serialize(result);
        }

        public struct JQGridResultsAdminManageSection
        {
            public int records;
            public JQGridRowAdminManageSection[] rows;
        }
        public struct JQGridRowAdminManageSection
        {
            public string id;
            public string[] cell;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class tblStudent
    {
        public int intStudentId { get; set; }
        public string varName { get; set; }
        public string varMajor { get; set; }
        public string varContact { get; set; }
    }


}