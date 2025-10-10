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
    }
}
