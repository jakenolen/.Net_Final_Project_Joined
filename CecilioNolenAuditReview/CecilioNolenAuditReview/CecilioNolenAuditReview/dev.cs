using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CecilioNolenAuditReview
{
    public partial class dev : Form
    {

        OleDbConnection conn = new OleDbConnection();
        DataSet installsDS = new DataSet();
        OleDbDataAdapter installAdapter = new OleDbDataAdapter();
        OleDbCommandBuilder cmdBuilder;

        public dev()
        {
            InitializeComponent();
            conn.ConnectionString =
         "Provider=SQLNCLI11;" +
         "Server=74.117.171.115,32000;" +
         "Database=CS340318;" +
         "UID=UAFS18;" +
         "PWD=UApass100;";
            load_mainframe();
            dev_grid_CB.SelectedIndex = 0;

        }
        private void load_mainframe()
        {


            installsDS.Clear();
            dev_data_grid.DataSource = null;
  



            try
            {
                conn.Open();

                OleDbDataReader reader;
                OleDbCommand cmd = new OleDbCommand();

                string sql = "SELECT * FROM MAINFRAME;";
                cmd.CommandText = sql;
                cmd.Connection = conn;

                installAdapter.SelectCommand = cmd;

                installAdapter.Fill(installsDS, "Mainframe");
                dev_data_grid.DataSource = installsDS;
                dev_data_grid.DataMember = "Mainframe";

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        private void load_web()
        {
            installsDS.Clear();
            dev_data_grid.DataSource = null;
     
          
            try
            {
                conn.Open();

                OleDbDataReader reader;
                OleDbCommand cmd = new OleDbCommand();

                string sql = "SELECT * FROM WEB;";
                cmd.CommandText = sql;
                cmd.Connection = conn;

                installAdapter.SelectCommand = cmd;

                installAdapter.Fill(installsDS, "Web");
                dev_data_grid.DataSource = installsDS;
                dev_data_grid.DataMember = "Web";

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        private void load_desktop()
        {
            installsDS.Clear();
            dev_data_grid.DataSource = null;


            try
            {
                conn.Open();

                OleDbDataReader reader;
                OleDbCommand cmd = new OleDbCommand();

                string sql = "SELECT * FROM DESKTOP;";
                cmd.CommandText = sql;
                cmd.Connection = conn;

                installAdapter.SelectCommand = cmd;

                installAdapter.Fill(installsDS, "Desktop");
                dev_data_grid.DataSource = installsDS;
                dev_data_grid.DataMember = "Desktop";
                
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

        }
        private void dev_Load(object sender, EventArgs e)
        {

           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


           if(dev_grid_CB.SelectedItem.ToString() == "Mainframe")
            {
                
                load_mainframe();
                dev_mainframe_pannel.Visible = true;
                dev_web_pannel.Visible = false;
                dev_desktop_pannel.Visible = false;
            }
            if (dev_grid_CB.SelectedItem.ToString() == "Web")
            {
  
                load_web();
                dev_web_pannel.Visible = true;

                dev_mainframe_pannel.Visible = false;
                dev_desktop_pannel.Visible = false;
            }
            if (dev_grid_CB.SelectedItem.ToString() == "Desktop")
            {
      
                load_desktop();
                dev_desktop_pannel.Visible = true;

                dev_web_pannel.Visible = false;
                dev_mainframe_pannel.Visible = false;
            }
        }

        private void dev_addTab_btn_Click(object sender, EventArgs e)
        {
            dev_intalls_pannel.Visible = false;
            
            dev_mainframe_pannel.Visible = true;
             
        }

        private void dev_form_CB_SelectedIndexChanged(object sender, EventArgs e)
        {

           
 
        }
        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void dev_myInstalls_btn_Click(object sender, EventArgs e)
        {
            dev_intalls_pannel.Visible = true;
           
            dev_mainframe_pannel.Visible = false;
            dev_web_pannel.Visible = false;
            dev_desktop_pannel.Visible = false;
            load_mainframe();
            dev_grid_CB.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                int devID = 1000;
                int mgnId = 1001;
                string reviewd = "No";
                string type = "mainframe";
                string memeber = dev_member_txtBox.Text;
                string pustDate = dev_push_date_txtBox.Text;
                string projectName = dev_project_name_txtBox.Text;
                string actionDesc = dev_action_dec_txtBox.Text;
                string installer = dev_installer_txtBox.Text;
                string installerFormID = dev_installer_form_id_txtBox.Text;
                string installerDesc = dev_installer_dec_txtBox.Text;
                string comments = dev_installer_dec_txtBox.Text;

                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                String sql = "INSERT INTO MAINFRAME(DEVID, MGNID, REVIEWED, ENV_TYPE, MEMBER, PUSH_DATE, PROJECT_NAME,ACTION_DESC, INSTALLER, INSTALLFORMID, INSTALL_DESC, COMMENTS) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                
                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("DEVID", devID);
                cmd.Parameters.AddWithValue("MGNID", mgnId);
                cmd.Parameters.AddWithValue("REVIEWED", "NO");
                cmd.Parameters.AddWithValue("ENV_TYPE", "MAINFRAME");
                cmd.Parameters.AddWithValue("MEMBER", memeber);
                cmd.Parameters.AddWithValue("PUSH_DATE", pustDate);
                cmd.Parameters.AddWithValue("PROJECT_NAME", projectName);
                cmd.Parameters.AddWithValue("ACTION_DESC", actionDesc);
                cmd.Parameters.AddWithValue("INSTALLER", installer);
                cmd.Parameters.AddWithValue("INSTALLFORMID", installerFormID);
                cmd.Parameters.AddWithValue("INSTALL_DESC", installerDesc);
                cmd.Parameters.AddWithValue("COMMENTS", comments);

                cmd.ExecuteNonQuery();

                conn.Close();
                ClearTextBoxes();
                MessageBox.Show("New Install Has been added.");
                load_mainframe();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
                conn.Close();
            }
           
        }

        private void dev_add_web_btn_Click(object sender, EventArgs e)
        {

            try
            {
                int devID = 1000;
                int mgnId = 1001;
                string reviewd = "No";
                string type = "WEB";

                string projectName = dev_projectName_web_textBox.Text;
                string memember = dev_member_web_textBox.Text;
                string pushDate = dev_pushDate_web_textBox.Text;
                string formDIR = dev_formId_web_textBox.Text;
                string installId = dev_installId_web_textBox.Text;
                string destDIR = dev_destDIR_web_textBox.Text;
                string published = dev_published_web_textBox.Text;
                string destSrv = dev_destSrv_web_textBox.Text;
                string agendaNum = dev_agendaNum_web_textBox.Text;
                string reviewedBy = dev_reviewedBy_web_textBox.Text;
                string requestBy = dev_requestedBy_web_textBox.Text;
                string installRes = dev_installRes_web_textBox.Text;
                string lastUp = dev_lastUpdate_web_textBox.Text;
                string installDesc = dev_installDesc_web_textArea.Text;
                string error = dev_error_web_web_textBox.Text;
                string thirdParty = dev_thirdParty_web_textBox.Text;
                string errorDesc = dev_errorDesc_web_textArea.Text;
                string comments = dev_comments_web_textArea.Text;

                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                String sql = "INSERT INTO WEB(DEVID, MGNID, REVIEWED, ENV_TYPE, PROJECT_NAME, MEMEMBER, FROMDIR, INSTALLID, DESTSRV, DESTDIR, PUBLISHED, PUSH_DATE, INSTALL_DESC, AGENDA_NUM, REVIEWED_BY, REQUESTED_BY, INSTALL_RES, LAST_UPDATE, THIRD_PARTY, ERROR, ERROR_DESC, COMMENTS) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
         
                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("DEVID", devID);
                cmd.Parameters.AddWithValue("MGNID", mgnId);
                cmd.Parameters.AddWithValue("REVIEWED", "NO");
                cmd.Parameters.AddWithValue("ENV_TYPE", "WEB");
                cmd.Parameters.AddWithValue("PROJECT_NAME", projectName);

                cmd.Parameters.AddWithValue("MEMEBER", memember);
                cmd.Parameters.AddWithValue("FROMMDIR", formDIR);
                cmd.Parameters.AddWithValue("INSTALLID", installId);
                cmd.Parameters.AddWithValue("DESTSRV", destSrv);
                cmd.Parameters.AddWithValue("DESTDIR", destDIR);
                cmd.Parameters.AddWithValue("PUBLISHED", published);
                cmd.Parameters.AddWithValue("PUSH_DATE", pushDate);
                cmd.Parameters.AddWithValue("INSTALL_DESC", installDesc);
                cmd.Parameters.AddWithValue("AGENDA_NUM", agendaNum);
                cmd.Parameters.AddWithValue("REVIEWED_BY", reviewedBy);
                cmd.Parameters.AddWithValue("REQUESTED_BY", requestBy);
                cmd.Parameters.AddWithValue("INSTALL_RES", installRes);
                cmd.Parameters.AddWithValue("LAST_UPDATE", lastUp);
                cmd.Parameters.AddWithValue("THIRD_PARTY", thirdParty);
                cmd.Parameters.AddWithValue("ERROR", error);
                cmd.Parameters.AddWithValue("ERROR_DESC", errorDesc);
                cmd.Parameters.AddWithValue("COMMENTS", comments);
  


                cmd.ExecuteNonQuery();
                conn.Close();
                ClearTextBoxes();
                MessageBox.Show("New Install Has been added.");
                load_web();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
                conn.Close();
            }
        }

        private void dev_save_desktop_btn_Click(object sender, EventArgs e)
        {
            string test = "testing Commit";
            try
            {
                int devID = 1000;
                int mgnId = 1001;
                string reviewd = "No";
                string type = "mainframe";
                string projectName = dev_projectName_desk_textBox.Text;
                
                string deployedBy = dev_depBy_desk_textBox.Text;
                string deployedTo = dev_depBy_desk_textBox.Text;
                string installForm = dev_installForm_desk_textBox.Text;
                string buildSorce = dev_buildSource_desk_textBox.Text;
                string buildDetails = dev_buildDetails_desk_textBox.Text;
                string releaseDetails = dev_releaseDetails_desk_textBox.Text;
                string projectDetails = dev_projectDetails_desk_textBox.Text;
                string packageDetails = dev_packageDetails_desk_textBox.Text;
                string comments = dev_comments_desk_textArea.Text;
                

                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                String sql = "INSERT INTO DESKTOP(DEVID, MGNID, REVIEWED, ENV_TYPE, PROJECT_NAME, DEP_TO, DEP_BY, INSTALL_FORM, BUILD_SORCE, BUILD_DETAILS, PAC_DETAILS, RELEASE_DETAILS, PROJECT_DETAILS,  COMMENTS) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("DEVID", devID);
                cmd.Parameters.AddWithValue("MGNID", mgnId);
                cmd.Parameters.AddWithValue("REVIEWED", "NO");
                cmd.Parameters.AddWithValue("ENV_TYPE", "DESKTOP");
                cmd.Parameters.AddWithValue("PROJECT_NAME", projectName);
                cmd.Parameters.AddWithValue("DEP_TO", deployedTo);
                cmd.Parameters.AddWithValue("Dep_BY", deployedBy);
                cmd.Parameters.AddWithValue("INSTALL_FORM", installForm);
                cmd.Parameters.AddWithValue("BUILD_SORCE", buildSorce);
                cmd.Parameters.AddWithValue("BUILD_DETAILS", buildDetails);
                cmd.Parameters.AddWithValue("PAC_DETAILS", packageDetails);
                cmd.Parameters.AddWithValue("RELEASE_DETAILS", releaseDetails);
                cmd.Parameters.AddWithValue("PROJECT_DETAILS", projectDetails);
                cmd.Parameters.AddWithValue("COMMENTS", comments);


                cmd.ExecuteNonQuery();
                conn.Close();
                ClearTextBoxes();
                MessageBox.Show("New Install Has been added.");
                load_desktop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
                conn.Close();
            }
        }

        private void Label31_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Label42_Click(object sender, EventArgs e)
        {

        }
    }
}
