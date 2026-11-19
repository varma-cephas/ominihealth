using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace OmniHealthCare.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> listPatients = new List<PatientInfo>();
        public void OnGet()
        {
            listPatients.Clear();
            try
            {
                String conStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OmniHealthcareDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    String sqlQuety = "SELECT * FROM Patients";
                    using (SqlCommand cmd = new SqlCommand(sqlQuety, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo pInfo = new PatientInfo();
                                pInfo.id = "" + reader.GetInt32(0);
                                pInfo.name = reader.GetString(1);
                                pInfo.email = reader.GetString(2);
                                pInfo.phone = reader.GetString(3);
                                pInfo.address = reader.GetString(4);
                                pInfo.createdAt = reader.GetDateTime(5).ToString();

                                listPatients.Add(pInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class PatientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string createdAt;
    }
}
