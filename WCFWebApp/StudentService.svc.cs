using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Activation;
using WCFWebApp;

namespace Demo
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode =
    AspNetCompatibilityRequirementsMode.Allowed)]
    public class StudentService
    {
        [OperationContract]
        public Student GetStudentById(int studentId)
        {
            Student student = new Student();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetStudentById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = studentId;

                cmd.Parameters.Add(parameter);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.Ime = rdr["Ime"].ToString();
                    student.Prezime = rdr["Prezime"].ToString();
                }
            }

            return student;
        }
    }
}

