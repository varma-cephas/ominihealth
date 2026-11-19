using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace OmniHealthCare.Pages.Patients
{
    public class EditModel : PageModel
    {

        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String conStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OmniHealthcareDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    String query = "SELECT * FROM Patients WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                patientInfo.id = "" + reader.GetInt32(0);
                                patientInfo.name = reader.GetString(1);
                                patientInfo.email = reader.GetString(2);
                                patientInfo.phone = reader.GetString(3);
                                patientInfo.address = reader.GetString(4);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            patientInfo.id = Request.Form["id"];
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

            try
            {
                String conStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OmniHealthcareDb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    String sqlQury = "UPDATE Patients SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQury, con))
                    {
                        cmd.Parameters.AddWithValue("@id", patientInfo.id);
                        cmd.Parameters.AddWithValue("@name", patientInfo.name);
                        cmd.Parameters.AddWithValue("@email", patientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", patientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", patientInfo.address);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            patientInfo.name = "";
            patientInfo.email = "";
            patientInfo.phone = "";
            patientInfo.address = "";


            successMessage = "Patient info updated successfully!";
            Response.Redirect("/Patients/Index");
        }
    }
}
