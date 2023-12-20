using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feuchte_Rapport
{
    public partial class FMainForm : Form
    {
        FPCIHumidity pciForm;
        FPCI_Maintenance pciMaintenance;
        FReport fReport;
        FLogin formLogin;
        FPCIInfo fPCIInfo;
        MainFormData data;
        DataSet ds;
        BindingSource bs;
        SqlDataAdapter sqlAdapter;

        public FMainForm()
        {
            InitializeComponent();
            sqlAdapter = new SqlDataAdapter();
            ds = new DataSet();
            bs = new BindingSource();
            data = new MainFormData();

            labPCI.Visible = false;
            labPCIMessung.Visible = false;
            labPCIRapport.Visible = false;
            labPCIReports.Visible = false;
            labPCIInfo.Visible = false;
            btnPCIMessung.Visible = false;
            btnPCIRapport.Visible = false;
            btnPCIReports.Visible = false;
            btnPCIInnfo.Visible = false;

            labAbwasser.Visible = false;
            labAbRapport.Visible = false;
            labAbReports.Visible = false;
            labSewageInfo.Visible = false;
            btnAbRapport.Visible = false;
            btnAbReports.Visible = false;
            btnSewageInfo.Visible = false;

            labSandkran.Visible = false;
            labCraneInfo.Visible = false;
            labCraneRapport.Visible = false;
            labCraneReport.Visible = false;
            btnCraneRapport.Visible = false;
            btnCraneReports.Visible = false;
            btnCraneInfo.Visible = false;

        }
        // Titel:       MainForm Load
        // Datum:       25.10.2016    Autor:            Augnhofer
        // Version:     1.0           Ort/Projekt:      Donawitz
        //---------------------------------------------------------------------
        // History:
        // 25.10.2016  Augenhofer  (Donawitz)
        //             (text)
        //---------------------------------------------------------------------
        private void MainForm_Load(object sender, EventArgs e)
        {
            DialogResult result;

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            Console.WriteLine(principal.Identity.Name);



            //if (principal.Identity.Name == @"VOESTALPINE\Kohlelab")
            //    data.groupID = 1;
            //else if (principal.Identity.Name == @"VOESTALPINE\HO_WASSE")
            //    data.groupID = 2;
            //else if (principal.Identity.Name == @"VOESTALPINE\2195_G_HO_SANDKRAN")
            //    data.groupID = 3;
            //else 
            //    data.groupID = 99;


            //if (data.connect())
            //{
            if (Properties.Settings.Default.Facility == "PCI")
                data.groupID = 1;
            if (Properties.Settings.Default.Facility == "Abwasserwirtschaft")
                data.groupID = 2;
            if (Properties.Settings.Default.Facility == "Sandkran")
                data.groupID = 3;
            if (Properties.Settings.Default.Facility == "Admin")
                data.groupID = 99;

            UserRights(data);
            //}

            //Verhindert das Flackern
            this.BackColor = Color.White;
            SetDoubleBuffered(panel1);

        }

        //Methode welche die private Methode DoubleBuffered setzen kann (ohne Ableitung)
        public static void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, control, new object[] { true });
        }

        private void btnPCIMessung_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCIHumidity))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            pciForm = new FPCIHumidity(data);
            pciForm.MdiParent = this;
            pciForm.WindowState = FormWindowState.Maximized;
            pciForm.Show();
        }
        
        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void UserRights(MainFormData data)
        {
            if (data.groupID == 1)
            {
                labPCI.Visible = true;
                labPCIMessung.Visible = true;
                labPCIRapport.Visible = true;
                labPCIReports.Visible = true;
                labPCIInfo.Visible = true;
                btnPCIMessung.Visible = true;
                btnPCIRapport.Visible = true;
                btnPCIReports.Visible = true;
                btnPCIInnfo.Visible = true;

                labSandkran.Visible = false;
                labCraneInfo.Visible = false;
                labCraneRapport.Visible = false;
                labCraneReport.Visible = false;
                btnCraneRapport.Visible = false;
                btnCraneReports.Visible = false;
                btnCraneInfo.Visible = false;

                labAbwasser.Visible = false;
                labAbRapport.Visible = false;
                labAbReports.Visible = false;
                labSewageInfo.Visible = false;
                btnAbRapport.Visible = false;
                btnAbReports.Visible = false;
                btnSewageInfo.Visible = false;
            }
            if (data.groupID == 2)
            {
                labPCI.Visible = false;
                labPCIMessung.Visible = false;
                labPCIRapport.Visible = false;
                labPCIReports.Visible = false;
                labPCIInfo.Visible = false;
                btnPCIMessung.Visible = false;
                btnPCIRapport.Visible = false;
                btnPCIReports.Visible = false;
                btnPCIInnfo.Visible = false;

                labSandkran.Visible = false;
                labCraneInfo.Visible = false;
                labCraneRapport.Visible = false;
                labCraneReport.Visible = false;
                btnCraneRapport.Visible = false;
                btnCraneReports.Visible = false;
                btnCraneInfo.Visible = false;

                labAbwasser.Visible = true;
                labAbRapport.Visible = true;
                labAbReports.Visible = true;
                labSewageInfo.Visible = true;
                btnAbRapport.Visible = true;
                btnAbReports.Visible = true;
                btnSewageInfo.Visible = true;

                tabMenu.Controls.Add(labAbwasser, 0, 0);
                tabMenu.Controls.Add(btnAbRapport, 1, 1);
                tabMenu.Controls.Add(labAbRapport, 1, 3);
                tabMenu.Controls.Add(btnAbReports, 1, 4);
                tabMenu.Controls.Add(labAbReports, 1, 6);
                tabMenu.Controls.Add(btnSewageInfo, 1, 7);
                tabMenu.Controls.Add(labSewageInfo, 1, 9);
            }

            if (data.groupID == 3)
            {
                labPCI.Visible = false;
                labPCIMessung.Visible = false;
                labPCIRapport.Visible = false;
                labPCIReports.Visible = false;
                labPCIInfo.Visible = false;
                btnPCIMessung.Visible = false;
                btnPCIRapport.Visible = false;
                btnPCIReports.Visible = false;
                btnPCIInnfo.Visible = false;

                labAbwasser.Visible = false;
                labAbRapport.Visible = false;
                labAbReports.Visible = false;
                labSewageInfo.Visible = false;
                btnAbRapport.Visible = false;
                btnAbReports.Visible = false;
                btnSewageInfo.Visible = false;

                labSandkran.Visible = true;
                labCraneInfo.Visible = true;
                labCraneRapport.Visible = true;
                labCraneReport.Visible = true;
                btnCraneRapport.Visible = true;
                btnCraneReports.Visible = true;
                btnCraneInfo.Visible = true;

                tabMenu.Controls.Add(labSandkran, 0, 0);
                tabMenu.Controls.Add(btnCraneRapport, 1, 1);
                tabMenu.Controls.Add(labCraneRapport, 1, 3);
                tabMenu.Controls.Add(btnCraneReports, 1, 4);
                tabMenu.Controls.Add(labCraneReport, 1, 6);
                tabMenu.Controls.Add(btnCraneInfo, 1, 7);
                tabMenu.Controls.Add(labCraneInfo, 1, 8);

            }

            if (data.groupID >= 99)
            {
                labPCI.Visible = true;
                labPCIMessung.Visible = true;
                labPCIRapport.Visible = true;
                labPCIReports.Visible = true;
                labPCIInfo.Visible = true;
                btnPCIMessung.Visible = true;
                btnPCIRapport.Visible = true;
                btnPCIReports.Visible = true;
                btnPCIInnfo.Visible = true;

                labAbwasser.Visible = true;
                labAbRapport.Visible = true;
                labAbReports.Visible = true;
                labSewageInfo.Visible = true;
                btnAbRapport.Visible = true;
                btnAbReports.Visible = true;
                btnSewageInfo.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //data.disconnect();
            Application.Exit();
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnPCIRapport_Click(object sender, EventArgs e)
        {
            string _anlage = "PCI";

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCI_Maintenance))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            pciMaintenance = new FPCI_Maintenance(data, _anlage);
            pciMaintenance.MdiParent = this;
            pciMaintenance.WindowState = FormWindowState.Maximized;
            pciMaintenance.Show();
        }

        private void btnAbRapport_Click(object sender, EventArgs e)
        {
            string _anlage = "Sewage";

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCI_Maintenance))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            pciMaintenance = new FPCI_Maintenance(data, _anlage);
            pciMaintenance.MdiParent = this;
            pciMaintenance.WindowState = FormWindowState.Maximized;
            pciMaintenance.Show();
        }

        private void btnPCIReports_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FReport))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            SqlConnection mainConnection = new SqlConnection();
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;
            fReport = new FReport("PCI", mainConnection);
            fReport.MdiParent = this;
            fReport.WindowState = FormWindowState.Maximized;
            fReport.Show();
        }

        private void btnAbReports_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FReport))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            SqlConnection mainConnection = new SqlConnection();
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;
            fReport = new FReport("Sewage", mainConnection);
            fReport.MdiParent = this;
            fReport.WindowState = FormWindowState.Maximized;
            fReport.Show();
        }

        private void btnPCIInnfo_Click(object sender, EventArgs e)
        {
            string _anlage = "PCI";

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCIInfo))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            fPCIInfo = new FPCIInfo(_anlage);
            fPCIInfo.MdiParent = this;
            fPCIInfo.WindowState = FormWindowState.Maximized;
            fPCIInfo.Show();
        }

        private void btnSewageInfo_Click(object sender, EventArgs e)
        {
            string _anlage = "Sewage";

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCIInfo))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            fPCIInfo = new FPCIInfo(_anlage);
            fPCIInfo.MdiParent = this;
            fPCIInfo.WindowState = FormWindowState.Maximized;
            fPCIInfo.Show();
        }

        private void btnCraneRapport_Click(object sender, EventArgs e)
        {
            string _anlage = "Crane";

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCI_Maintenance))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            pciMaintenance = new FPCI_Maintenance(data, _anlage);
            pciMaintenance.MdiParent = this;
            pciMaintenance.WindowState = FormWindowState.Maximized;
            pciMaintenance.Show();
        }

        private void btnCraneReports_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FReport))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            SqlConnection mainConnection = new SqlConnection();
            mainConnection.ConnectionString = Properties.Settings.Default.LaPass_ConnectionString;
            fReport = new FReport("Crane", mainConnection);
            fReport.MdiParent = this;
            fReport.WindowState = FormWindowState.Maximized;
            fReport.Show();
        }

        private void btnCraneInfo_Click(object sender, EventArgs e)
        {
            string _anlage = "Crane";

            foreach (Form mdiChilds in this.MdiChildren)
            {
                if (mdiChilds.GetType() == typeof(FPCIInfo))
                {
                    mdiChilds.Focus();
                    return;
                }
            }
            fPCIInfo = new FPCIInfo(_anlage);
            fPCIInfo.MdiParent = this;
            fPCIInfo.WindowState = FormWindowState.Maximized;
            fPCIInfo.Show();
        }

    }
}
