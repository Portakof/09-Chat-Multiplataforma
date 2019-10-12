using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilitiesChat.Models.WS;
using UtilitiesChat;
using ChatDestop.Bussines;
using Newtonsoft.Json;
using ChatDestop.Business;

namespace ChatDestop
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            SessionStart(); 
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            SessionStart();
            
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Business.Session.oUser = null;

            iniciarSesionToolStripMenuItem.Enabled = true;
            cerrarSesionToolStripMenuItem.Enabled = false;
            splitContainer1.Enabled = false;
        }

        #region HELPER
        private void GetDataInit()
        {
            List<ListRoomsResponse> lst = new List<ListRoomsResponse>();

            SecurityRequest oSecurityRequest = new SecurityRequest();
            oSecurityRequest.AccessToken =  Session.oUser.AccessToken;

            RequestUtil oRequesUtil = new RequestUtil();
            Reply oReply = oRequesUtil.Execute<SecurityRequest>
               (Constants.Url.ROOMS, "post", oSecurityRequest);

            lst = JsonConvert.DeserializeObject<List<ListRoomsResponse>>
                (JsonConvert.SerializeObject(oReply.data));

            cboRooms.DataSource = lst;
            cboRooms.DisplayMember = "Name";
            cboRooms.ValueMember = "Id";
        }

        private void SessionStart()
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();

            if (Business.Session.oUser != null)
            {
                iniciarSesionToolStripMenuItem.Enabled = false;
                cerrarSesionToolStripMenuItem.Enabled = true;
                splitContainer1.Enabled = true;

                GetDataInit();
            }
        }

        #endregion

    }
}
