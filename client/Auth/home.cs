using System;
using System.Windows.Forms;
using Flurl.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Auth
{
    public partial class home : Form
    {

        private static string output;
        private string checkTrueResponse;

        public string logged_in { get; set; }
        public object body { get; internal set; }

        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetRequest("https://www.chronoschat.co/home");
        }

        async void GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoxfQ.SGcpoMJoMhe16ADOG9CyTD2opi4sL9XapyQbl8DcOWY";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();

                        output = mycontent.ToString();
                        textBox2.Text = output;

                        home requestToken = JsonConvert.DeserializeObject<home>(output);
                        textBox2.Text = requestToken.logged_in.ToString();
                        checkTrueResponse = requestToken.logged_in.ToString();

                        if (checkTrueResponse == "true")
                        {
                            listAllUsers form = new listAllUsers();
                            form.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Not Authorized");
                        }

                    }

                }

            }

        }

    }
}
