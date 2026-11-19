using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace OmniHealthCare.Pages.Patients
{
    public class CreateModel : PageModel
    {

        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            patientInfo.name = Request.Form["name"];
            patientInfo.email = Request.Form["email"];
            patientInfo.phone = Request.Form["phone"];
            patientInfo.address = Request.Form["address"];


            if (patientInfo.name.Length.Equals(0) || patientInfo.email.Length.Equals(0)
                || patientInfo.phone.Length.Equals(0) || patientInfo.address.Length.Equals(0))
            {
                errorMessage = "All fields are required";
                return;
            }


            String conStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OmniHealthcareDb;Integrated Security=True";

            //insert
            try
            {
                using(SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    String sqlQury = "INSERT INTO Patients(name, email, phone, address) VALUES (@name, @email, @phone, @address)";
                    using(SqlCommand cmd = new SqlCommand(sqlQury, con))
                    {
                        cmd.Parameters.AddWithValue("@name", patientInfo.name);
                        cmd.Parameters.AddWithValue("@email", patientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", patientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", patientInfo.address);

                        cmd.ExecuteNonQuery();
                    }
                }

            } catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            patientInfo.name = "";
            patientInfo.email = "";
            patientInfo.phone = "";
            patientInfo.address = "";


            successMessage = "New patient added successfully!";
        }
    }
}
