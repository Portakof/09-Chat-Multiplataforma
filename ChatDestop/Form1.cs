using ChatDestop.Bussines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatDestop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Equals("") || txtPass.Text.Equals(""))
            {
                MessageBox.Show("Los dos campos son obligatorios");
                return;
            }

            AccessRequest oAR = new AccessRequest();
            oAR.Email = txtUser.Text.Trim();
            oAR.Password = UtilitiesChat.Tools.Encrypt.GetSHA256(txtPass.Text.Trim());

            RequestUtil oRequesUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply =
                oRequesUtil.Execute<AccessRequest>(Constants.Url.ACCESS, "post", oAR);

            if (oReply.result == 1)
            {
                UserResponse oUserResponse =
                    JsonConvert.DeserializeObject<UserResponse>
                    (JsonConvert.SerializeObject(oReply.data));

                MessageBox.Show("Entrastes");
            }
            else
            {
                MessageBox.Show(oReply.message);
            }
            

            
        }
    }
}
