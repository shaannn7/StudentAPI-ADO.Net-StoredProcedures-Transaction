using Microsoft.Data.SqlClient;
using PracticalW4.Model;
using System.Data;

namespace PracticalW4.Repostory
{
    public class StudentRepostory:IStudents
    {
        public IConfiguration _Configuration { get; set; }
        public string Csring {  get; set; }

        public StudentRepostory(IConfiguration configuration)
        { 
            _Configuration = configuration;
            Csring = _Configuration["ConnectionStrings:DefaultConnection"];
        }

        public Students GetStudents(int id)
        {
            using(SqlConnection conn =new SqlConnection(Csring))
            {
                
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("STUDENT_FIND",conn);
               
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ID", id);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read()) 
                {
                   Students students = new Students();
                    students.StudentId = Convert.ToInt32(sqlDataReader["STUDENT_ID"]);
                    students.StudentName = sqlDataReader["STUDENT_NAME"].ToString();
                    students.StudentMark =Convert.ToInt32(sqlDataReader["MARK"]);
                    students.StudentStatus = sqlDataReader["STUDENT_STATUS"].ToString();
                    return students;
                }
                return null;
            }
        }

        public List<Students> GetAllStudents()
        {
            using(SqlConnection con = new SqlConnection(Csring))
            {
                List<Students> students = new List<Students>();
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("GET_STUDENTS", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    Students students1 = new Students
                    {
                        StudentId = Convert.ToInt32(sqlDataReader["STUDENT_ID"]),
                        StudentName = sqlDataReader["STUDENT_NAME"].ToString(),
                        StudentMark = Convert.ToInt32(sqlDataReader["MARK"]),
                        StudentStatus = sqlDataReader["STUDENT_STATUS"].ToString()
                    };
                    students.Add(students1);
                }
                return students;
            }
        }

        public void AddStudents(Students students)
        {
            using(SqlConnection con =new SqlConnection(Csring))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("ADD_STUDENTS", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@NAME", students.StudentName);
                sqlCommand.Parameters.AddWithValue("@MARK",students.StudentMark);
                sqlCommand.Parameters.AddWithValue("@STATUS", students.StudentStatus);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateStudents (Students students)
        {
            using( SqlConnection con =new SqlConnection(Csring))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE_STUDENTS", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ID", students.StudentId);
                sqlCommand.Parameters.AddWithValue("@NAME", students.StudentName);
                sqlCommand.Parameters.AddWithValue("@MARK", students.StudentMark);
                sqlCommand.Parameters.AddWithValue("@STATUS", students.StudentStatus);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void DeleteStudents(int id)
        {
            using(SqlConnection con =new SqlConnection(Csring))
            {
                con.Open();
                SqlTransaction sqlTransaction = con.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("DELETE_STUDENTS", con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlCommand.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    sqlTransaction.Rollback();
                    Console.WriteLine("PAAK UUUU");
                }
                 
            }
        }


    }
}
