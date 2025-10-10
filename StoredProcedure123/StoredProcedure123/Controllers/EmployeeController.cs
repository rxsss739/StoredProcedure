using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StoredProcedure123.Data;
using StoredProcedure123.Models;

namespace StoredProcedure123.Controllers
{
    public class EmployeeController : Controller
    {
        public StoredProcDbContext _context;
        public IConfiguration _confiq;

        public EmployeeController(StoredProcDbContext context, IConfiguration confiq)
        {
            _context = context;
            _confiq = confiq;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DynamicSQL()
        {
            string connectionString = _confiq.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchEmployees";
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (sdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = sdr["FirstName"].ToString();
                    details.LastName = sdr["LastName"].ToString();
                    details.Gender = sdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(sdr["Salary"]);
                    model.Add(details);
                }

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult DynamicSQL(string firstName, string lastName, string gender, int salary)
        {
            string connectionString = _confiq.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                StringBuilder sbCommand = new StringBuilder("SELECT * FROM EMPLOYEES WHERE 1=1");

                if (!string.IsNullOrEmpty(firstName))
                {
                    sbCommand.Append(" AND FirstName like '%" + firstName + "%'");
                    SqlParameter param = new SqlParameter("@FirstName", firstName);
                    cmd.Parameters.Add(param);
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    sbCommand.Append(" AND LastName like '%" + lastName + "%'");
                    SqlParameter param = new SqlParameter("@LastName", lastName);
                    cmd.Parameters.Add(param);
                }

                if (salary != 0)
                {
                    sbCommand.Append(" AND Salary = " + salary);
                    SqlParameter param = new SqlParameter("@Salary", salary);
                    cmd.Parameters.Add(param);
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    sbCommand.Append(" AND Gender like '%" + gender + "%'");
                    SqlParameter param = new SqlParameter("@Gender", gender);
                    cmd.Parameters.Add(param);
                }


                return View();
            }
    }
}
