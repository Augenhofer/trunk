using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace Feuchte_Rapport
{
    public partial class FLogin : Form
    {
        MainFormData mainData;
        //SqlDataAdapter getUserName;
        DataSet ds;
        string user, pass;
        private int groupID = -1;
        //private byte[] hashPass;

        public FLogin(MainFormData data)
        {
            InitializeComponent();
            mainData = data;
            ds = new DataSet();
            labHint.Visible = false;
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            //SqlDataAdapter getUserName = new SqlDataAdapter("select UserName from [LaPass].[dbo].[UserGroup]", IntecoLib.UserData.mainConnection);
            //if (IntecoLib.UserData.mainConnection.State == ConnectionState.Open)
            //{
            //    getUserName.Fill(ds, "Name");
            //    cmbBoxUser.DataSource = ds;
            //    cmbBoxUser.DisplayMember = "Name.UserName";
            //    cmbBoxUser.SelectedIndex = 1;
            //}
        }

        private void Abbrechen_Click(object sender, EventArgs e)
        {
            //mainData.disconnect();
            Application.Exit();
        }

        private void FLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mainData.groupID <= 0)
                Application.Exit();
        }

        private void Anmelden_Click(object sender, EventArgs e)
        {
            labHint.Visible = false;
            user = cmbBoxUser.Text.ToString();
            pass = passText.Text.ToString();

            if (pass == null || pass == "" ||(groupID = checklogin(user, pass)) <= 0)
            {
                cmbBoxUser.Text = "";
                passText.Text = "";
                labHint.Visible = true;
            }
            else
            {
                //mainData.language = readUserData.GetString(3);
                //mainData.userID = (int)readUserData.GetInt32(0);
                mainData.groupID = groupID;
                mainData.user = user;
                this.Close();
            }
        }

        private int checklogin(string user, string pass)
        {
            //SqlCommand checklogincmd = new SqlCommand("SELECT Permisson FROM [LaPass].[dbo].[UserGroup] where UserName = '" + user + "' and [Password] = '" + pass + "'", IntecoLib.UserData.mainConnection);

            //if (checklogincmd.ExecuteScalar() != null)
            //{
            //    groupID = (int)checklogincmd.ExecuteScalar();
            //}
            return groupID; ;
        }
    }
}
