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
        int postYFinal = 10;
        Panel lastPanel = null;

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
            splitContainerChat.Enabled = false;
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

            //Obtenemos mensajes del chat
            GetMessages();
        }

        private void SessionStart()
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();

            if (Business.Session.oUser != null)
            {
                iniciarSesionToolStripMenuItem.Enabled = false;
                cerrarSesionToolStripMenuItem.Enabled = true;
                splitContainerChat.Enabled = true;

                GetDataInit();
            }
        }

        private void GetMessages()
        {
            int idRoom = 0;
            panelMessages.Controls.Clear();
            lastPanel = null;

            try
            {
                idRoom = (int)cboRooms.SelectedValue;
            }
            catch { }

            if (idRoom > 0)
            {
                List<MessagesResponse> lst = new List<MessagesResponse>();

                MessagesRequest OMessagesRequest = new MessagesRequest();
                OMessagesRequest.AccessToken = Session.oUser.AccessToken;    //se recibe o obtiene el objeto con el AccessToken 
                OMessagesRequest.IdRoom = idRoom;                               //y el Idroom

                RequestUtil oRequestUtil = new RequestUtil();   //Se crea un objeto y se hace la peticion en si al "MessagesController"
                Reply oReply = oRequestUtil.Execute<MessagesRequest>(Constants.Url.MESSAGES, "post", OMessagesRequest);     //para traer todos los mensajes de la sala o room

                lst = JsonConvert.DeserializeObject<List<MessagesResponse>>(JsonConvert.SerializeObject(oReply.data));   //deserealizamos el obejto "oReply" el cual trae la lista de mensajes

                lst = lst.OrderBy(d => d.DateCreated).ToList(); //se oredena la la lista en forma ascendente

                foreach (MessagesResponse oMessage in lst)
                {
                    AddMessage(oMessage);
                }

            }
        }

        private void AddMessage(MessagesResponse oMessage)
        {
            Panel oPanel = new Panel();

            oPanel.Width = panelMessages.Width -30;
            oPanel.Height = 70;
            oPanel.BackColor = Color.LightGray;
            //oPanel.Location = new Point(10, postYFinal);
            //postYFinal += oPanel.Height + 10;

            if (lastPanel == null)
            {
                oPanel.Location = new Point(10, 10);
            }
            else
            {
                oPanel.Location = new Point(10, lastPanel.Location.Y + lastPanel.Height + 10);
            }

            lastPanel = oPanel;

            panelMessages.Controls.Add(oPanel);
            panelMessages.ScrollControlIntoView(oPanel);

            //Agregamos hijos
            TextBox txtMessage = new TextBox();
            txtMessage.Text = oMessage.Message;
            txtMessage.Location = new Point(10, 10);
            txtMessage.Width = oPanel.Width - 20;
            txtMessage.ReadOnly = true;
            txtMessage.BorderStyle = BorderStyle.None;
            oPanel.Controls.Add(txtMessage);
        }

        #endregion

        private void cboRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            //postYFinal = 10;
            //panelMessages.Controls.Clear();
            GetMessages();
        }
    }
}
