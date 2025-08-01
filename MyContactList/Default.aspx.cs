using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyContactList
{
    public partial class Default : System.Web.UI.Page
    { // Get the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(); // Load the contacts when the page first loads
            }
        }

        // Method to fetch data and bind it to the GridView
        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ContactID, FirstName, LastName, PhoneNumber, Email FROM Contacts", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvContacts.DataSource = dt;
                        gvContacts.DataBind(); // This displays the data
                    }
                }
            }
        }

        // Event handler for the "Save Contact" button click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get values from the textboxes
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;

            string query = "INSERT INTO Contacts (FirstName, LastName, PhoneNumber, Email) VALUES (@FirstName, @LastName, @PhoneNumber, @Email)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Use parameters to prevent SQL Injection!
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phone);
                    cmd.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Show a success message and clear the form
            lblMessage.Text = "Contact saved successfully!";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";

            // Refresh the grid to show the new contact
            BindGrid();
        }
        // Fires when the "Delete" button is clicked
        protected void gvContacts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            // 1. Get the ContactID (the primary key) from the DataKeys collection for the selected row
            int contactId = Convert.ToInt32(gvContacts.DataKeys[e.RowIndex].Value);

            // 2. Write and execute the DELETE query
            string query = "DELETE FROM Contacts WHERE ContactID=@ContactID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ContactID", contactId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // 3. Refresh the grid to show that the record is gone
            BindGrid();

            // Optional: Show a success message
            lblMessage.Text = "Contact deleted successfully!";
        }
        // Add these three methods to your Default.aspx.cs file

        // Fires when the "Edit" button is clicked
        protected void gvContacts_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            // Set the row to edit mode
            gvContacts.EditIndex = e.NewEditIndex;
            BindGrid(); // Re-bind the grid to show the edit controls
        }

        // Fires when the "Cancel" button is clicked
        protected void gvContacts_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            // Exit edit mode
            gvContacts.EditIndex = -1;
            BindGrid(); // Re-bind the grid to its normal state
        }

        // Fires when the "Update" button is clicked
        protected void gvContacts_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            // Get the ContactID (the primary key) from the DataKeys collection
            int contactId = Convert.ToInt32(gvContacts.DataKeys[e.RowIndex].Value);

            // Find the TextBox controls in the edited row
            TextBox txtFirstName = (TextBox)gvContacts.Rows[e.RowIndex].FindControl("txtEditFirstName");
            TextBox txtLastName = (TextBox)gvContacts.Rows[e.RowIndex].FindControl("txtEditLastName");
            TextBox txtPhone = (TextBox)gvContacts.Rows[e.RowIndex].FindControl("txtEditPhoneNumber");
            TextBox txtEmail = (TextBox)gvContacts.Rows[e.RowIndex].FindControl("txtEditEmail");

            // Execute the UPDATE query
            string query = "UPDATE Contacts SET FirstName=@FirstName, LastName=@LastName, PhoneNumber=@PhoneNumber, Email=@Email WHERE ContactID=@ContactID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@ContactID", contactId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Exit edit mode and refresh the grid
            gvContacts.EditIndex = -1;
            BindGrid();
        }
    }
}