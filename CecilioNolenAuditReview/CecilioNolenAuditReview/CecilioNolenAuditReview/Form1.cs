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
    public partial class Form1 : Form
    {
        OleDbConnection conn = new OleDbConnection();
        DataSet newDS = new DataSet();
        OleDbDataAdapter newAdapter = new OleDbDataAdapter();
        OleDbCommandBuilder cmdBuilder;

        private string selected;
        private string selectedView;
        private string selectedSearch;
        private string dbSelection;

        public Form1()
        {
            InitializeComponent();

            conn.ConnectionString =
               "Provider=SQLNCLI11;" +
               "Server=74.117.171.115,32000;" +
               "Database=CS340318;" +
               "UID=UAFS18;" +
               "PWD=UApass100;";
        }

        //----- LOGIN ---------------------------------------------------------------------------------------------------------------------------------------

        private void LoginSubmit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OleDbDataReader reader;
                DataTable dt = new DataTable();
                OleDbCommand cmd = new OleDbCommand();
                var user = new List<Emp>();

                string sql = "SELECT * FROM ARCEMP WHERE ? = USERNAME AND ? = PASS;";
                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("Username", User_Txt.Text);
                cmd.Parameters.AddWithValue("Password", Pass_Txt.Text);

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "ARCEMP");

                reader = cmd.ExecuteReader();
                cmdBuilder = new OleDbCommandBuilder(newAdapter);

                while (reader.Read())
                {
                    Emp employee = new Emp(Convert.ToInt32(reader["ID"]),
                        reader["USERNAME"].ToString(),
                        reader["FNAME"].ToString(),
                        reader["LNAME"].ToString(),
                        reader["ROLE"].ToString());
                    user.Add(employee);

                    if (employee.EmpRole.Equals("DEV"))
                    {
                        Form1.ActiveForm.Hide();
                        dev dev1 = new dev();
                        dev1.ShowDialog();
                        dev1 = null;
                    }
                    else if (employee.EmpRole.Equals("MGN"))
                    {
                        Login_Panel.Visible = false;
                        Mgn_Panel.Visible = true;
                    }
                    else if (employee.EmpRole.Equals("ADT"))
                    {
                        Login_Panel.Visible = false;
                        //Adt_Panel.Visible = true;
                    }
                }

                foreach (Emp emp in user)
                {
                    Login_Panel.Visible = false;
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        private void Clear_Btn_Click(object sender, EventArgs e)
        {
            User_Txt.Text = "";
            Pass_Txt.Text = "";
        }

        //----- MAIN PAGE ---------------------------------------------------------------------------------------------------------------------------------------

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //----- VIEW ALL ---------------------------------------------------------------------------------------------------------------------------------------

        private void ViewAll_Btn_Click(object sender, EventArgs e)
        {
            Search.Visible = false;
            Flagged.Visible = false;
            View.Visible = true;
        }

        private void ViewZ_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            newDS.Clear();
            View_Dvg.DataSource = newDS;

            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                sql = "SELECT Z.ID, CONCAT(E.LNAME, ', ', E.FNAME), CONCAT(M.LNAME, ', ', M.FNAME), Z.REVIEWED, Z.ENV_TYPE, Z.PROJECT_NAME " +
                    "FROM MAINFRAME Z JOIN ARCEMP E " +
                    "ON Z.DEVID = E.ID " +
                    "JOIN ARCEMP M " +
                    "ON Z.MGNID = M.ID;";

                selectedView = "Z";

                cmd.CommandText = sql;
                cmd.Connection = conn;

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "MAINFRAME");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                View_Dvg.DataSource = newDS;
                View_Dvg.DataMember = "MAINFRAME";
                dbSelection = "MAINFRAME";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        private void ViewW_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            newDS.Clear();
            View_Dvg.DataSource = newDS;

            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                sql = "SELECT W.ID, CONCAT(E.LNAME, ', ', E.FNAME), CONCAT(M.LNAME, ', ', M.FNAME), W.REVIEWED, W.ENV_TYPE, W.PROJECT_NAME " +
                    "FROM WEB W JOIN ARCEMP E " +
                    "ON W.DEVID = E.ID " +
                    "JOIN ARCEMP M " +
                    "ON W.MGNID = M.ID;";

                selectedView = "W";

                cmd.CommandText = sql;
                cmd.Connection = conn;

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "WEB");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                View_Dvg.DataSource = newDS;
                View_Dvg.DataMember = "WEB";
                dbSelection = "WEB";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        private void ViewD_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            newDS.Clear();
            View_Dvg.DataSource = newDS;

            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                sql = "SELECT D.ID, CONCAT(E.LNAME, ', ', E.FNAME), CONCAT(M.LNAME, ', ', M.FNAME), D.REVIEWED, D.ENV_TYPE, D.PROJECT_NAME " +
                    "FROM DESKTOP D JOIN ARCEMP E " +
                    "ON D.DEVID = E.ID " +
                    "JOIN ARCEMP M " +
                    "ON D.MGNID = M.ID;";

                selectedView = "D";

                cmd.CommandText = sql;
                cmd.Connection = conn;

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "DESKTOP");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                View_Dvg.DataSource = newDS;
                View_Dvg.DataMember = "DESKTOP";
                dbSelection = "DESKTOP";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        private void View_Dvg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idString = View_Dvg.CurrentRow.Cells[0].Value.ToString();
            int id = Int32.Parse(idString);
            
            if (newDS.Tables.Count > 2)
            {
                newDS.Tables[2].Clear();
            }
            
            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                if (selectedView == "Z")
                {
                    sql = "SELECT * FROM MAINFRAME WHERE ? = ID;";
                }
                else if (selectedView == "W")
                {
                    sql = "SELECT * FROM WEB WHERE ? = ID;";
                }
                else if (selectedView == "D")
                {
                    sql = "SELECT * FROM DESKTOP WHERE ? = ID;";
                }

                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("ID", id);

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "Details");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                Edit_Dgv.DataSource = newDS;
                Edit_Dgv.DataMember = "Details";

                bindingSource1 = new BindingSource(newDS.Tables[0].DefaultView, "Details");

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        //----- SEARCH --------------------------------------------------------------------------------------------------------------------------------------- 

        private void Search_Btn_Click(object sender, EventArgs e)
        {
            Search.Visible = true;
            View.Visible = false;
            Flagged.Visible = false;

            newDS.Clear();
            Edit_Dgv.Columns.Clear();

        }

        private void SearchZ_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            selectedSearch = "Z";
        }

        private void SearchW_Btn_CheckedChanged(object sender, EventArgs e)
        {
            selectedSearch = "W";
        }

        private void SearchD_Btn_CheckedChanged(object sender, EventArgs e)
        {
            selectedSearch = "D";
        }

        private void Src_Btn_Click(object sender, EventArgs e)
        {
           newDS.Clear();
           Search_Dgv.DataSource = newDS;
           Search_Txt.Text = "";

           try
           {
               conn.Open();
               String sql = "";
               OleDbCommand cmd = new OleDbCommand();

               if(selectedSearch == "Z")
               {
                   sql = "SELECT Z.ID, E.LNAME, M.LNAME, Z.REVIEWED, Z.ENV_TYPE, Z.PROJECT_NAME " +
                   "FROM MAINFRAME Z JOIN ARCEMP E " +
                   "ON Z.DEVID = E.ID " +
                   "JOIN ARCEMP M " +
                   "ON Z.MGNID = M.ID " +
                   "WHERE E.LNAME = ?;";

                   dbSelection = "MAINFRAME";
               }
               else if(selectedSearch == "W")
               {
                   sql = "SELECT W.ID, CONCAT(E.LNAME, ', ', E.FNAME), CONCAT(M.LNAME, ', ', M.FNAME), W.REVIEWED, W.ENV_TYPE, W.PROJECT_NAME " +
                   "FROM WEB W JOIN ARCEMP E " +
                   "ON W.DEVID = E.ID " +
                   "JOIN ARCEMP M " +
                   "ON W.MGNID = M.ID ;";

                   dbSelection = "WEB";
               }
               else if(selectedSearch == "D")
               {
                   sql = "SELECT D.ID, CONCAT(E.LNAME, ', ', E.FNAME), CONCAT(M.LNAME, ', ', M.FNAME), D.REVIEWED, D.ENV_TYPE, D.PROJECT_NAME " +
                   "FROM DESKTOP D JOIN ARCEMP E " +
                   "ON D.DEVID = E.ID " +
                   "JOIN ARCEMP M " +
                   "ON D.MGNID = M.ID;";

                   dbSelection = "DESKTOP";
               }

               cmd.CommandText = sql;
               cmd.Connection = conn;

               cmd.Parameters.AddWithValue("PROJECT_NAME", Search_Txt.Text);

               newAdapter.SelectCommand = cmd;
               newAdapter.Fill(newDS, dbSelection);

               cmdBuilder = new OleDbCommandBuilder(newAdapter);
               Search_Dgv.DataSource = newDS;
               Search_Dgv.DataMember = dbSelection;

               conn.Close();
           }
           catch (Exception ex)
           {
               Console.WriteLine("ERROR: " + ex.ToString());
               conn.Close();
           }
           finally
           {
               conn.Close();
           }
        }

        private void Search_Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idString = Search_Dgv.CurrentRow.Cells[0].Value.ToString();
            int id = Int32.Parse(idString); 

            if (newDS.Tables.Count > 3)
            {
                newDS.Tables[3].Clear();
            }
            try
            {
                conn.Open();
                string sql = "";
                OleDbCommand cmd = new OleDbCommand();

                if (selectedSearch == "Z")
                {
                    sql = "SELECT * FROM MAINFRAME WHERE ID = ?;";
                }
                else if (selectedSearch == "W")
                {
                    sql = "SELECT * FROM WEB WHERE ID = ?;";
                }
                else if (selectedSearch == "D")
                {
                    sql = "SELECT * FROM DESKTOP WHERE ID = ?;";
                }

                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("ID", id);

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "Edit");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                Edit_Dgv.DataSource = newDS;
                Edit_Dgv.DataMember = "Edit";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }


        //----- Flagged ---------------------------------------------------------------------------------------------------------------------------------------

        private void Flagged_Btn_Click(object sender, EventArgs e)
        {
            Search.Visible = false;
            Flagged.Visible = true;
            View.Visible = false;
        }

        private void FlaggedZ_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            newDS.Clear();
            Flagged_Dvg.DataSource = newDS;

            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                sql = "SELECT ID, REVIEWED, ENV_TYPE FROM MAINFRAME WHERE REVIEWED = 'NO';";
                selected = "Z";

                cmd.CommandText = sql;
                cmd.Connection = conn;

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "MAINFRAME");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                Flagged_Dvg.DataSource = newDS;
                Flagged_Dvg.DataMember = "MAINFRAME";
                dbSelection = "MAINFRAME";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        private void FlaggedW_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            newDS.Clear();
            Flagged_Dvg.DataSource = newDS;

            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                sql = "SELECT ID, REVIEWED, ENV_TYPE FROM WEB WHERE REVIEWED = 'NO';";
                selected = "W";
                
                cmd.CommandText = sql;
                cmd.Connection = conn;

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "WEB");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                Flagged_Dvg.DataSource = newDS;
                Flagged_Dvg.DataMember = "WEB";
                dbSelection = "WEB";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        private void FlaggedD_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            newDS.Clear();
            Flagged_Dvg.DataSource = newDS;

            try
            {
                conn.Open();
                String sql = "";
                OleDbCommand cmd = new OleDbCommand();

                sql = "SELECT ID, REVIEWED, ENV_TYPE FROM DESKTOP WHERE REVIEWED = 'NO';";
                selected = "D";
                
                cmd.CommandText = sql;
                cmd.Connection = conn;

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "DESKTOP");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                Flagged_Dvg.DataSource = newDS;
                Flagged_Dvg.DataMember = "DESKTOP";
                dbSelection = "DESKTOP";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        private void Flagged_Dvg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idString = Flagged_Dvg.CurrentRow.Cells[0].Value.ToString();
            int id = Int32.Parse(idString);
                        
            if(newDS.Tables.Count > 4)
            {
                newDS.Tables[4].Clear();
            }
            try
            {
                conn.Open();
                string sql = "";
                OleDbCommand cmd = new OleDbCommand();

                if(selected == "Z")
                {
                    sql = "SELECT * FROM MAINFRAME WHERE ID = ?;";
                }
                else if(selected == "W")
                {
                    sql = "SELECT * FROM WEB WHERE ID = ?;";
                }
                else if(selected == "D")
                {
                    sql = "SELECT * FROM DESKTOP WHERE ID = ?;";
                }

                cmd.CommandText = sql;
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("ID", id);

                newAdapter.SelectCommand = cmd;
                newAdapter.Fill(newDS, "Edit");

                cmdBuilder = new OleDbCommandBuilder(newAdapter);
                Edit_Dgv.DataSource = newDS;
                Edit_Dgv.DataMember = "Edit";

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        //----- Update ---------------------------------------------------------------------------------------------------------------------------------------

        private void Save_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                bindingSource1.EndEdit();
                newAdapter.Update(newDS, dbSelection);
                MessageBox.Show("SAVED");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }
        }
        
        //----- Not Used ---------------------------------------------------------------------------------------------------------------------------------------

        private void Mgn_Panel_Paint(object sender, PaintEventArgs e)
        {
            // --- Not Used ---
        }
        private void Label13_Click(object sender, EventArgs e)
        {
            // --- Not Used ---
        }
        private void Flagged_Dvg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // --- Not Used ---
        }
        private void View_Dvg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // --- Not Used ---
        }
        private void GroupBox2_Enter(object sender, EventArgs e)
        {
            // --- Not Used ---
        }
        private void SaveYes_Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            // --- Not Used ---
        }
    }
}
