using System;
using System.Linq;
using System.Windows.Forms;
using Flurl.Http;
using Newtonsoft.Json;
using System.Net.Http;

namespace Auth
{
    public partial class registrationForm : Form
    {
        private string regResponse;
        private string successResponse;

        public registrationForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void regSubmitButton_Click_1(object sender, EventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword1.Text;
            string password2 = tbPassword2.Text;

            if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
            {
                //  UserNameLabel.Visible = true;
                ///   UserNameLabel.Text = "Invalid Email";
                MessageBox.Show("Invalid Email");
                tbEmail.Clear();
            }
            else
            {
                //UserNameLabel.Visible = false;
                if (password.Length != 8)

                {
                    MessageBox.Show("Password Too Weak");
                    tbPassword1.Clear();
                    tbPassword2.Clear();

                }
                else
                {
                    UserNameLabel.Visible = false;
                    if (password != password2)
                    {
                        MessageBox.Show("Password Mismatch");
                        tbPassword1.Clear();
                        tbPassword2.Clear();
                    }

                    else
                    {
                        UserNameLabel.Visible = false;
                        registerUser();
                    }
                }

            }
        }

        public string status { get; set; }

        async void registerUser()
        {
            try
            {

                HttpResponseMessage responseMessage = await "https://chronoschat.co/registration".PostUrlEncodedAsync(new
                {
                    email = tbEmail.Text.ToString(),
                    password = tbPassword1.Text.ToString(),
                    password_confirmation = tbPassword2.Text.ToString()
                });
                string responseJson = await responseMessage.Content.ReadAsStringAsync();
                regResponse = responseJson;
                registrationForm regStatus = JsonConvert.DeserializeObject<registrationForm>(regResponse);
                successResponse = regStatus.status.ToString();

                if (successResponse == "Success")
                {
                    //UserNameLabel.Visible = true;
                    //UserNameLabel.Text = regStatus.status.ToString();
                    returnLabel.Visible = true;
                    regSubmitButton.Enabled = false;
                    MessageBox.Show("Success! Returning you to the login screen, please log in.");
                  //  System.Threading.Thread.Sleep(1000);
                    this.Close();
                }
                else
                {
                    UserNameLabel.Visible = true;
                    UserNameLabel.Text = "An error occured, try again";
                }

            }
            catch (FlurlHttpTimeoutException)
            {
                MessageBox.Show("Timed out!");
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response != null)
                    MessageBox.Show("Failed with response code " + ex.Call.Response.StatusCode);
                else
                    MessageBox.Show("Totally failed before getting a response! " + ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registrationForm_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Coming Soon.");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon.");

        }
    }
}
