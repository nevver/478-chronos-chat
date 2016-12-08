using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Flurl;
using Flurl.Http;
using System.Net.Http;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/**
 * @author David Do
 **/

namespace Auth
{
    public partial class listAllUsers : Form
    {

        System.Security.Cryptography.CspParameters cspp = new System.Security.Cryptography.CspParameters();
        System.Security.Cryptography.RSACryptoServiceProvider rsa;

        string keyName = DataContainer.User;

        public string output;
        string conversationID;
        public listAllUsers()
        {
            InitializeComponent();
        }

        private Form1 otherForm = new Form1();

        async void GetUsers(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string accessToken = DataContainer.ValueToShare.ToString();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();

                        System.Data.DataSet dataSet = JsonConvert.DeserializeObject<System.Data.DataSet>(mycontent);
                        System.Data.DataTable dataTable = dataSet.Tables["users"];

                        foreach (System.Data.DataRow row in dataTable.Rows)
                        {
                            string loggedinuser = DataContainer.User.ToString();
                            comboBox1.Items.Add(row["email"]);
                            comboBox1.Items.Remove(loggedinuser);
                        }
                    }
                }
            }
        }

        private void listAllUsers_Load(object sender, EventArgs e)
        {
            label1.Text = "Logged in as: " + DataContainer.User.ToString();
            GetUsers("https://www.chronoschat.co/conversations/index");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string userToChat2 = comboBox1.SelectedItem.ToString();
                DataContainer.messageToUser = userToChat2.ToString();
                Encrypt form1 = new Encrypt();
                form1.Show();
                // conversationsCreate2(userToChat2);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Select a Recipient First");
            }



        }

        public string id { get; set; }

        public string status { get; set; }

        async void conversationsCreate2(string email)
        {

          //  Form1 form1 = new Form1();
            string myToken = DataContainer.ValueToShare.ToString();
            var builtUrl = new Url("https://chronoschat.co/conversations/create");
            var client2 = builtUrl
                .WithOAuthBearerToken(myToken);
            var resp = await client2
                .WithHeader("Accept", "application/json")
                .PostUrlEncodedAsync(new
                {
                    recipient_email = email
                })
                .ReceiveString()
                ;

            string output = resp.ToString();
            listAllUsers messageID = JsonConvert.DeserializeObject<listAllUsers>(output);
            string output2 = messageID.ToString();
            conversationID = messageID.id.ToString();

            var builtUrl2 = new Url("https://chronoschat.co/messages/create");

            var client3 = builtUrl2
                .WithOAuthBearerToken(myToken);

            var resp2 = await client3
              .WithHeader("Accept", "application/json")
              .PostUrlEncodedAsync(new
              {
                 // body = textBox1.Text,
                  conversation_id = conversationID
              })
              .ReceiveString()
              ;

            string resp2Output = resp2.ToString();
            listAllUsers sendOutput = JsonConvert.DeserializeObject<listAllUsers>(resp2Output);
            string output3 = sendOutput.ToString();
            string sendStatus = sendOutput.status.ToString();
            MessageBox.Show(sendStatus);
            if (sendStatus == "Message Sent")
            {
               // textBox1.Clear();
            }
            else
            {
                MessageBox.Show("An unknown error occured. Please try again later");
            }
        }

        async void getMessages3(string email)
        {
            string myToken = DataContainer.ValueToShare.ToString();
            var builtUrl = new Url("https://chronoschat.co/conversations/create");
            var client2 = builtUrl
                .WithOAuthBearerToken(myToken);
            var resp = await client2
                .WithHeader("Accept", "application/json")
                .PostUrlEncodedAsync(new
                {
                    recipient_email = email
                })
                .ReceiveString()
                ;

            string output = resp.ToString();
            listAllUsers messageID = JsonConvert.DeserializeObject<listAllUsers>(output);
            string output2 = messageID.ToString();
            string conversationID = messageID.id.ToString();
            //  MessageBox.Show("Conversation ID: " + conversationID);

            using (HttpClient client = new HttpClient())
            {
                string accessToken = DataContainer.ValueToShare.ToString();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                using (HttpResponseMessage response = await client.GetAsync("https://chronoschat.co/messages/index?conversation_id=" + conversationID))
                {
                    byte[] body;
                    using (HttpContent content3 = response.Content)
                    {
                        string mycontent2 = await content3.ReadAsStringAsync();

                        if (mycontent2 == "{\"messages\":[]}")

                        {
                            MessageBox.Show("No New Messages");
                        }
                        else
                        {
                            System.Data.DataSet dataSet2 = JsonConvert.DeserializeObject<System.Data.DataSet>(mycontent2);
                            System.Data.DataTable dataTable2 = dataSet2.Tables["messages"];
                            DataTable table = new DataTable();
                            table.Columns.Add("Date Recevied", typeof(string));
                            table.Columns.Add("Message From", typeof(string));
                           // table.Columns.Add("Conversation ID", typeof(string));
                           // table.Columns.Add("User_ID", typeof(string));
                            table.Columns.Add("Message", typeof(string));
                            string usernameselected = comboBox1.SelectedItem.ToString();

                            foreach (System.Data.DataRow row in dataTable2.Rows)
                            {
                                // table.Rows.Add(row["created_at"], row["user_email"], row["conversation_id"], row["user_id"], row["body"]);
                                body = Dencrypt9(Convert.FromBase64String(row["body"].ToString()));

                                if (body != null)
                                {
                                    table.Rows.Add(row["created_at"], row["user_email"], Encoding.UTF8.GetString(body));
                                }
                                else
                                {
                                    table.Rows.Add(row["created_at"], row["user_email"], "???????????????????????");
                                }                                                                 
                            }
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }
        }
        static byte[] Dencrypt9(byte[] input)
        {
            byte[] dencrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(DataContainer.privateKey);
                try
                {
                    dencrypted = rsa.Decrypt(input, true);
                }
                catch
                {
                    //MessageBox.Show("Incorrect Private Key");
                    dencrypted = null;
                }
            }
            return dencrypted;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cspp.KeyContainerName = keyName;

            rsa = new RSACryptoServiceProvider(2048, cspp);
            DataContainer.privateKey = rsa.ExportParameters(true);          

            rsa.PersistKeyInCsp = true;
            try
            {
                string email = comboBox1.SelectedItem.ToString();
                if (email == null)
                {
                    MessageBox.Show("Select a person!");
                }
                else
                {
                    getMessages3(email);
                }

        }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select a user");
            }


}

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 home = new Form1();
            home.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Encrypt form1 = new Encrypt();
            form1.Show();
        }

       
        const string EncrFolder = @"c:\Encrypt\";
        string PubKeyFile = @"c:\Encrypt\rsaPublicKey_" + DataContainer.User.ToString();

        private void createKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("This will overwrite any existing keys for " + DataContainer.User.ToString() + ". Do you want to continue?", "WARNING", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                             // MessageBox.Show("New KeySet Created");
                cspp.KeyContainerName = DataContainer.User;
                rsa = new RSACryptoServiceProvider(2048, cspp);
                //store a key pair in the key container.
                rsa.PersistKeyInCsp = true;
                //if (rsa.PublicOnly == true)
                //  //  MessageBox.Show(  "Key: " + cspp.KeyContainerName + " - Public Only");
                //else
                //   // MessageBox.Show( "Key: " + cspp.KeyContainerName + " - Full Key Pair");

                string keyfile = PubKeyFile + ".txt";
       

                Directory.CreateDirectory(EncrFolder);
                StreamWriter sw = new StreamWriter(keyfile, false);
                sw.Write(rsa.ToXmlString(false));
                sw.Close();
                MessageBox.Show("Public Key Exported to: " + keyfile);

            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }

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
















