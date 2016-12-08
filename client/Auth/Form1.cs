using System;
using System.Windows.Forms;
using Flurl.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;

namespace Auth
{
    public partial class Form1 : Form
    {

        private string loginJsonResponse;
        public static string sessionToken { get; set; }
        public string email { get; set; }
        private static string output;
        private string checkTrueResponse;

        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = tbUsername;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = tbUsername.Text;
            if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
            {
                MessageBox.Show("Invalid Username/Password");
            }
            else
            {
                tbUserNameLabel.Visible = false;
                DataContainer.User = tbUsername.Text.ToString();
                login();
            }
        }

        public string auth_token { get; set; }

        async void login()
        {

            try
            {

                HttpResponseMessage responseMessage = await "https://chronoschat.co/authenticate".PostUrlEncodedAsync(new
                {
                    email = tbUsername.Text.ToString(),
                    password = tbpassword.Text.ToString()
                });
                string responseJson = await responseMessage.Content.ReadAsStringAsync();
                loginJsonResponse = responseJson;
                Form1 authtoken = JsonConvert.DeserializeObject<Form1>(loginJsonResponse);
                tbUserNameLabel.Visible = true;
                tbUserNameLabel.Text = "Success! You're logged in!";
                tbUsername.Enabled = false;
                tbpassword.Clear();
                tbpassword.Enabled = false;
                sessionToken = authtoken.auth_token.ToString(); //store the token value in sessionToken to pass to /home
                DataContainer.ValueToShare = authtoken.auth_token.ToString();
                button1.Enabled = false;
                checkToken(sessionToken);

            }
            catch (FlurlHttpTimeoutException)
            {
                MessageBox.Show("Timed out!");
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response != null)
                {
                    MessageBox.Show("Invalid Username/Password");
                }
                else
                    MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void tbpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registrationForm form2 = new registrationForm();
            form2.FormClosing += FormIsClosing;
            Visible = false;
            form2.Show();
        }

        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            this.Show();
            this.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            home home = new Auth.home();
            home.Show();
        }

        async void checkToken(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string accessToken = token;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                using (HttpResponseMessage response = await client.GetAsync("https://www.chronoschat.co/home"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        output = mycontent.ToString();
                        home requestToken = JsonConvert.DeserializeObject<home>(output);
                        checkTrueResponse = requestToken.logged_in.ToString();

                        if (checkTrueResponse == "true")
                        {
                            listAllUsers form = new listAllUsers();
                            form.FormClosing += FormIsClosing;
                            Visible = false;
                            form.Show();
                        }
                        else
                        {
                            MessageBox.Show("Not Authorized");
                        }
                    }
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Coming Soon");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon");
        }

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string promptValue = Encrypt.ShowDialog("Enter Container Name", "Remove Key");


            CspParameters cp = new CspParameters();
            cp.KeyContainerName = promptValue;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
            MessageBox.Show("Key Deleted");


        }

        private void deleteKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string promptValue = Encrypt.ShowDialog("Enter Container Name", "Remove Key");


            CspParameters cp = new CspParameters();
            cp.KeyContainerName = promptValue;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
            MessageBox.Show("Key Deleted");
        }
    }
}

