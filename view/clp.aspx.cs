using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tessnet2;
using System.Drawing.Text;
using Image = System.Drawing.Image;
using System.Windows.Media;

namespace PR3.view
{
    public partial class pr3 : System.Web.UI.Page
    {
        private static int nbC;
        private static string mot;
        private const int maxWidth = 300;
        private const int maxHeight = 300;
        private static string let;
        public DataTable tableAH = null;
        private int[] TableCorrespondance = { 0, 15, 35, 60, 100, 150, 240 };

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if ((!SessionModel.Connecté) || (SessionModel.SessionID != Session.SessionID))
            {
                SessionModel.Reset();
                Response.Redirect("index.aspx");
            }
            String civ, pren, nom, soc;
            civ = SessionModel.Civilité;
            pren = SessionModel.PrénomUser;
            nom = SessionModel.NomUser;
            soc = SessionModel.Société;
            infoUtilisateur.Text = "BIENVENUE " + civ +
                ((pren == "" || pren.ToUpper() == "NULL") ? "" : (" " + pren)) +
                " " + nom +
                ((soc == "" || soc.ToUpper() == "NULL") ? "" : (" / " + soc));
            imagController imagC = new imagController();
            imagC.idimg = "jo";
            imagC.longueur = 3;
            imagC.hautueur = 2;
            load();
            // Création de l'objet Bdd pour l'intéraction avec la base de donnée MySQL
            nbLettreTxt.Visible = false;
            ImagModel bdd = new ImagModel();
            bdd.AddContact(imagC);
            if (!Page.IsPostBack)
            {
                Rad1.Checked = true;
                rad1Change();
            }
            DropDownList1_SelectedIndexChanged1(null, null);
            
            fonter();
        }

        public void AllImg()
        {
            TextBox4.ReadOnly = false;
            TextBox4.BackColor = System.Drawing.Color.White;
            txtpuis.ReadOnly = false;
            txtpuis.BackColor = System.Drawing.Color.White;
            test.ReadOnly = false;
            test.BackColor = System.Drawing.Color.White;
            haute.ReadOnly = false;
            haute.BackColor = System.Drawing.Color.White;
            txtespace.ReadOnly = false;
            txtespace.BackColor = System.Drawing.Color.White;
            DropDownList2.Enabled = true;
            Button1.Enabled = true;
            nbLed.ReadOnly = false;
            nbLed.BackColor = System.Drawing.Color.White;
            nbP.ReadOnly = false;
            nbP.BackColor = System.Drawing.Color.White;
            nbV.ReadOnly = false;
            nbV.BackColor = System.Drawing.Color.White;
            Text.ReadOnly = true;
            Text.BackColor = System.Drawing.Color.WhiteSmoke;
            Label4.Enabled = true;
            FU1.Enabled = true;
            FU1.Enabled = true;
            btnUpLoad.Visible = true;
        }

        public void AllText()
        {
            TextBox4.ReadOnly = false;
            TextBox4.BackColor = System.Drawing.Color.White;
            txtpuis.ReadOnly = false;
            txtpuis.BackColor = System.Drawing.Color.White;
            test.ReadOnly = false;
            test.BackColor = System.Drawing.Color.White;
            haute.ReadOnly = false;
            haute.BackColor = System.Drawing.Color.White;
            txtespace.ReadOnly = false;
            txtespace.BackColor = System.Drawing.Color.White;
            DropDownList2.Enabled = true;
            Button1.Enabled = true;
            nbLed.ReadOnly = false;
            nbLed.BackColor = System.Drawing.Color.White;
            nbP.ReadOnly = false;
            nbP.BackColor = System.Drawing.Color.White;
            nbV.ReadOnly = false;
            nbV.BackColor = System.Drawing.Color.White;
            Text.ReadOnly = false;
            Text.BackColor = System.Drawing.Color.White;
            FU1.Enabled = false;
            Label4.Enabled = false;
            Label4.BackColor = System.Drawing.Color.Gray;
            btnUpLoad.Visible = true;
        }

        //Fonction qui permet de detecter le nombre de caractères
        public int getText(Image imageToSplit)
        {
            string s = "";
            var image = new Bitmap(imageToSplit);
            var ocr = new Tesseract();
            ocr.SetVariable("load_system_dawg", false);
            ocr.SetVariable("load_freq_dawg", false);
            ocr.Init(Server.MapPath(@"\tessdata\"), "eng", false);
            var result = ocr.DoOCR(image, Rectangle.Empty);
            int nbLettre = 0;
            foreach (tessnet2.Word word in result)
            {
                s += Text.Text;

                Text.Text = let;

            }
            mot = s;
            return (nbC = nbLettre);
        }

        public int getNbOfCharacter(Image imageToSplit)
        {
            string s = "";
            var image = new Bitmap(imageToSplit);
            var ocr = new Tesseract();
            ocr.SetVariable("load_system_dawg", false);
            ocr.SetVariable("load_freq_dawg", false);
            ocr.Init(Server.MapPath(@"\tessdata\"), "eng", false);
            var result = ocr.DoOCR(image, Rectangle.Empty);
            int nbLettre = 0;
            foreach (tessnet2.Word word in result)
            {
                s += word.Text;
                nbLettre += word.Text.Length;
            }
            mot = s;
            return (nbC = nbLettre);
        }


        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        protected void btnCrop_Click1(object sender, EventArgs e)
        {
            string confilename, confilepath;
            string fileName = Path.GetFileName(imgUpload.ImageUrl);
            string filePath = Path.Combine(Server.MapPath("~/img"), fileName);
            btnCrop.Visible = false;
            if (File.Exists(filePath))
            {
                System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                try
                {
                    Rectangle CropArea = new Rectangle
                    (
                        Convert.ToInt32(X.Value),
                        Convert.ToInt32(Y.Value),
                        Convert.ToInt32(W.Value),
                        Convert.ToInt32(H.Value)
                    );
                    Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                    using (Graphics g = Graphics.FromImage(bitMap))
                    {
                        g.DrawImage(cropImage(orgImg, CropArea), new Point(0, 0));
                    }
                    int v = CropArea.Width;
                    string r = v.ToString();
                    test.Text = r;
                    int s = CropArea.Height;
                    string h = s.ToString();
                    haute.Text = h;
                    confilename = "Crp_" + fileName;
                    confilepath = Path.Combine(Server.MapPath("~/crpmg"), confilename);
                    bitMap.Save(confilepath);
                    cropimg.Visible = true;
                    cropimg.Src = "~/crpmg/" + confilename;
                    int nbCrtr = this.getNbOfCharacter(bitMap);
                    panCrop.Visible = false;
                    string prefixe = "caractère";
                    if (nbCrtr > 1) prefixe += "s";
                    Button1.Enabled = true;
                    lblMsg.Visible = false;
                    if (Rad2.Checked)
                    {
                        FU1.Visible = true;
                        btnUpLoad.Enabled = true;
                        Label4.Visible = true;
                        //btnUpLoad.Visible = true;
                    }
                    if (Rad1.Checked)
                    {
                        AllText();
                        FU1.Visible = false;
                        btnUpLoad.Enabled = true;
                        Label4.Visible = false;
                        //btnUpLoad.Visible = true;
                    }
                }
                catch (FormatException)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Erreur Crop", "alert('Veuillez d\\'abord choisir la zone à redimensionner')", true);
                }
                catch
                {
                    throw;
                }
            }
        }
        private bool IsText()
        {
            bool retval = true;
            if (Text.Text.Length < 1) retval = false;
            return retval;
        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {
            //if (btnUpLoad.Text == "Charger...")
            //{
                //if (FU1.HasFile)
                //{
                    string uploadFileName = "";
                    string ext = Path.GetExtension(FU1.FileName).ToLower();
                    
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                    {
                        HttpPostedFile pf = FU1.PostedFile;
                        uploadFileName = Guid.NewGuid().ToString() + ext;
                        Text.Text = FU1.FileName;
                        System.Drawing.Image imageToUplolad = System.Drawing.Image.FromStream(pf.InputStream);
                        int imageWidth = imageToUplolad.Width;
                        int imageHeigth = imageToUplolad.Height;
                        double rapport = (double)imageWidth / (double)imageHeigth;
                        if (imageWidth > maxWidth)
                        {
                            imageWidth = maxWidth;
                            imageHeigth = (int)(imageWidth / rapport);
                        }
                        if (imageHeigth > maxHeight)
                        {
                            imageHeigth = maxHeight;
                            imageWidth = (int)(imageHeigth * rapport);
                        }
                        imageToUplolad = ResizeBitmap((Bitmap)imageToUplolad, imageWidth, imageHeigth); ///new width, height
                        imageToUplolad.Save(Path.Combine(Server.MapPath("~/img/"), uploadFileName));
                        try
                        {
                            imgUpload.ImageUrl = "~/img/" + uploadFileName;
                            panCrop.Visible = true;
                            btnCrop.Visible = true;
                            cropimg.Visible = false;
                            lblMsg.Visible = true;
                            btnUpLoad.Enabled = true;
                            Text.ReadOnly = true;
                            Text.BackColor = System.Drawing.Color.White;
                            lblMsg.Visible = false;
                            //btnUpLoad.Visible = true;
                        }
                        catch
                        {
                            lblMsg.Text = "Erreur! Veuillez réessayer!";
                            btnUpLoad.Enabled = true;
                            Text.ReadOnly = true;
                            Text.BackColor = System.Drawing.Color.White;
                            btnUpLoad.Enabled = true;
                            //btnUpLoad.Visible = true;
                            lblMsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Veuillez selectionner un fichier!";
                        btnUpLoad.Enabled = true;
                        Text.ReadOnly = true;
                        Text.BackColor = System.Drawing.Color.White;
                        btnUpLoad.Enabled = true;
                        //btnUpLoad.Visible = true;
                        lblMsg.Visible = true;
                    }
                //}
                //else
                //{
                //    lblMsg.Text = "Veuillez selectionner un fichier";
                //    btnUpLoad.Enabled = true;
                //    Text.ReadOnly = true;
                //    Text.BackColor = System.Drawing.Color.White;
                //    btnUpLoad.Enabled = true;
                //    //btnUpLoad.Visible = true;
                //    lblMsg.Visible = true;
                //}
            //}
            //else
            //{
            //    if (IsText())
            //    {
            //        Text.ReadOnly = false;
            //        Text.BackColor = System.Drawing.Color.White;
            //        btnUpLoad.Enabled = true;
            //        btnUpLoad.Visible = true;
            //        insertionText();
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(GetType(), "Erreur Crop", "alert('Veuillez saisisser le zone de text ')", true);
            //    }
            //}
        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

        public void load()
        {
            TxtSearch.Attributes.Add("onkeyUp", "return doSearch();");
            TxtSearch.Attributes.Add("onfocus", String.Format("SetCursorToTextEnd({0})", TxtSearch.ID));
            CreationTableAH();
            TxtSearch_TextChanged(null, null);
        }

        private void CreationTableAH()
        {
            tableAH = new DataTable("TableLED");
            tableAH.Columns.Add(new DataColumn("Type", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Designation", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Puissance", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Voltage", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Espacement", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Tail", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Couleur", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Module chaine", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Longueur câble", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Module ml", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Profondeur", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Prix catalogue", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Luminosité", typeof(String)));
        }
        private DataTable tableBind;
        private void remplirTable(string conditions = "")
        {
            try
            {
                MySqlConnection conn = null;
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
                conn = new MySqlConnection(_strConn);
                conn.Open();
                string query = "SELECT type, designation, puissance, voltage, espacement, tail, couleur, modules_chaine, longueur_cable, modules_ml, profondeur, prix_catalogue, nblumen FROM led " + conditions;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                tableAH.Clear();
                while (reader.Read())
                {
                    DataRow row = tableAH.NewRow();
                    row[0] = reader["type"].ToString();
                    row[1] = reader["designation"].ToString();
                    row[2] = reader["puissance"].ToString();
                    row[3] = reader["voltage"].ToString();
                    row[4] = reader["espacement"].ToString();
                    row[5] = reader["tail"].ToString();
                    row[6] = reader["couleur"].ToString();
                    row[7] = reader["modules_chaine"].ToString();
                    row[8] = reader["longueur_cable"].ToString();
                    row[9] = reader["modules_ml"].ToString();
                    row[10] = reader["profondeur"].ToString();
                    row[11] = reader["prix_catalogue"].ToString();
                    row[12] = reader["nblumen"].ToString();
                    tableAH.Rows.Add(row);
                }
                tableBind = tableAH;
                LEDList.DataSource = tableBind;
                LEDList.DataBind();
                // Problème quand on va calculer....
                List_SelectedIndexChanged(null, null);
            }
            catch { };
        }

        public void insertImag()
        {
            try
            {
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(_strConn))
                {
                    // Ouverture de la connexion SQL
                    string im = txtespace.Text;
                    string lo = test.Text;
                    string larg = test.Text;
                    cn.Open();
                    // Requête SQL
                    string query = "INSERT INTO image (idimg, longueur, hauteur) VALUES ('" + im + "','" + lo + "','" + larg + "')";
                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        public void insertResult()
        {
            try
            {
                System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
                provider.NumberDecimalSeparator = ",";
                provider.NumberGroupSeparator = ".";
                decimal carb = Convert.ToDecimal(txtpuis.Text, provider);
                decimal ht0 = Convert.ToDecimal(haute.Text, provider);
                decimal ll0 = Convert.ToDecimal(test.Text, provider);
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(_strConn))
                {
                    decimal sulta = (ht0 * ll0);
                    string resulta = sulta.ToString();
                    //puissance         
                    decimal reltp = sulta * carb;
                    string resultpw = reltp.ToString();

                    string lt = TxtLed.Text;
                    cn.Open();
                    // Requête SQL
                    string query = "INSERT INTO resultat (type_Led, Puissance_Total,Nombre_Led,Alimantation) VALUES ('" + lt + "','" + resultpw + "','" + resulta + "','" + nbV.Text + "')";

                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        public void insertResult2()
        {
            try
            {
                System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
                provider.NumberDecimalSeparator = ",";
                provider.NumberGroupSeparator = ".";
                //puissance
                decimal carb = Convert.ToDecimal(txtpuis.Text, provider);
                //long
                decimal numl = Convert.ToDecimal(test.Text, provider);
                //hauteur
                decimal numh = Convert.ToDecimal(haute.Text, provider);
                //espacement
                decimal nume = Convert.ToDecimal(txtespace.Text, provider);
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
                using (MySqlConnection cn = new MySqlConnection(_strConn))
                {
                    //nombre led    
                    decimal resu = (numh * numl) / nume;
                    string nnbled = resu.ToString();
                    //convert       
                    string lt = TxtLed.Text;
                    //puissance
                    decimal r = resu * carb;
                    string resultp = r.ToString();
                    cn.Open();
                    // Requête SQL
                    string query = "INSERT INTO resultat (type_Led, Puissance_Total,Nombre_Led,Alimantation) VALUES ('" + lt + "','" + resultp + "','" + nnbled + "','" + nbV.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        private int chercheCorrespondance(double d)
        {
            int i = 0;
            while ((i < TableCorrespondance.Length) && (d > TableCorrespondance[i])) i++;
            if (i >= TableCorrespondance.Length) return Convert.ToInt32(d);
            else return TableCorrespondance[i];
        }
        private bool IsvAL()
        {

            bool retval = true;
            if (txtpuis.Text.Length < 1) retval = false;
            if (haute.Text.Length < 1) retval = false;
            if (test.Text.Length < 1) retval = false;
            return retval;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            AllImg();
            //btnUpLoad.Visible = true;
            //btnUpLoad.Visible = true;
            if (IsvAL())
            {
                //btnUpLoad.Visible = true;
                if (Rad1.Checked || Rad2.Checked)
                {
                    //btnUpLoad.Visible = true;
                    string fileName = Path.GetFileName(imgUpload.ImageUrl);
                    string filePath = Path.Combine(Server.MapPath("~/img"), fileName);
                    if (File.Exists(filePath))
                    {
                        if (Rad2.Checked)
                        {
                            AllImg();
                            //btnUpLoad.Visible = false;
                        }
                        if (Rad1.Checked)
                        {
                            
                            //btnUpLoad.Visible = true;
                            DropDownList2.Enabled = true;
                        }
                        if (IsvAL())
                        {
                            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
                            provider.NumberDecimalSeparator = ",";
                            provider.NumberGroupSeparator = ".";
                            decimal carb = Convert.ToDecimal(txtpuis.Text, provider);
                            decimal ht = Convert.ToDecimal(haute.Text, provider);
                            decimal ll = Convert.ToDecimal(test.Text, provider);
                            if (txtespace.Text.Equals(""))
                            {
                                decimal result = (ht * ll);
                                string afresult = result.ToString();
                                nbLed.Text = afresult;
                                decimal res = Convert.ToDecimal(result, provider);
                                decimal pw1 = chercheCorrespondance((double)res * (double)carb);
                                string resultpw = pw1.ToString();
                                nbP.Text = resultpw;
                                insertImag();
                                insertResult();
                            }
                            else
                            {
                                int k;
                                int echelle = 1;
                                switch (DropDownList2.SelectedIndex)
                                {
                                    case 0: k = 3; break;
                                    case 1: k = 5; break;
                                    case 2: k = 7; break;
                                    default: k = 9; break;
                                }
                                decimal carb2 = Convert.ToDecimal(txtpuis.Text, provider);
                                decimal ht2 = Convert.ToDecimal(haute.Text, provider); // Height
                                decimal result = Convert.ToInt32((echelle * ht2 * k * nbC) * Convert.ToInt32(LEDList.SelectedRow.Cells[9].Text) / (1000));
                                string afresult = result.ToString(); // Entier
                                nbLed.Text = afresult;
                                decimal pw1 = chercheCorrespondance((double)result * (double)carb2);
                                string resultpw = pw1.ToString();
                                nbP.Text = resultpw;
                                insertResult2();
                                insertImag();
                                drawImageTebk();
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Veuillez remplir le formulaire');</script>");
                            btnUpLoad.Visible = true;
                            AllImg();
                            FU1.Enabled = true;
                            //btnUpLoad.Visible = true;
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Choisissez le mode que vous aimerez utiliser');</script>");
                        btnUpLoad.Visible = true;
                        AllImg();
                        FU1.Enabled = true;
                        //btnUpLoad.Visible = true;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Choisissez le mode que vous aimerez utiliser');</script>!');</script>");
                    btnUpLoad.Visible = true;
                    AllImg();
                    FU1.Enabled = true;
                    //btnUpLoad.Visible = true;
                }
            }
            else
            {
                Text.ReadOnly = false;
                Text.BackColor = System.Drawing.Color.White;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Veuillez remplir le formulaire');</script>");
                //btnUpLoad.Visible = true;
                //btnUpLoad.Visible = true;
            }
        }

        protected void drawImageTebk()
        {
            string extension = Path.GetExtension(cropimg.Src);
            string filePath = Path.Combine(Server.MapPath("~/crpmg"), "temp" + extension);
            string nomFont;
            switch (DropDownList2.SelectedIndex)
            {
                case 0: nomFont = "enhanced_dot_digital-7"; break;
                case 1: nomFont = "advanced_dot_digital-7"; break;
                case 2: nomFont = "triple_dot_digital-7"; break;
                default: nomFont = ""; break;
            }
            Bitmap bitMap;
            using (Image org = (Image)Bitmap.FromFile(Server.MapPath(cropimg.Src)))
                bitMap = new Bitmap(org.Width, org.Height);
            using (Graphics graph = Graphics.FromImage(bitMap))
            {
                String text = mot;
                Rectangle ImageSize = new Rectangle(0, 0, bitMap.Width, bitMap.Height);
                graph.FillRectangle(System.Drawing.Brushes.White, ImageSize);
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Center;
                strFormat.LineAlignment = StringAlignment.Center;
                String temp = Server.MapPath("/library/font/" + nomFont + ".ttf");
                graph.DrawString(text, scalling(graph, temp, text, bitMap.Width, bitMap.Height), System.Drawing.Brushes.Black,
                new Rectangle(0, 0, bitMap.Width, bitMap.Height), strFormat);
            }
            bitMap.Save(filePath);
            cropimg.Src = "~/crpmg/" + "temp" + extension;
        }

        private Font scalling(Graphics g, String family, string text, int width, int height)
        {
            PrivateFontCollection p = new PrivateFontCollection();
            p.AddFontFile(family);
            Font fontFamily = new Font(p.Families[0], 10);
            SizeF RealSize = g.MeasureString(text, fontFamily);
            float ratio = (float)RealSize.Height / (float)RealSize.Width;
            float boxRatio = (float)height / (float)width;
            float ScaleRatio;
            if (ratio > boxRatio)
            {
                ScaleRatio = (float)height / (float)RealSize.Height;
            }
            else
            {
                ScaleRatio = (float)width / (float)RealSize.Width;
            }
            float ScaleFontSize = fontFamily.Size * ScaleRatio * 0.9f;
            return new Font(fontFamily.FontFamily, ScaleFontSize);
        }

        protected void List_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.LEDList, "Select$" + e.Row.RowIndex); // Activates click capability
            }
        }

        protected void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LEDList.SelectedIndex > -1)
            {
                TxtLed.Text = LEDList.SelectedRow.Cells[1].Text;
                txtpuis.Text = LEDList.SelectedRow.Cells[2].Text;
                txtespace.Text = LEDList.SelectedRow.Cells[4].Text;
                nbV.Text = LEDList.SelectedRow.Cells[3].Text;
                nbV.ReadOnly = true;
                nbV.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            else { }
        }

        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string req = TxtSearch.Text;
            if (req == "")
            {
                remplirTable();
                return;
            }
            // Condition dans la base : type, designation, puissance, voltage, espacement, tail, couleur, modules_chaine, longueur_cable, modules_ml, profondeur, prix_catalogue, nblumen
            req = " WHERE type LIKE '%" + req + "%' OR " +
                        " designation LIKE '%" + req + "%' OR " +
                        " puissance LIKE '%" + req + "%' OR " +
                        " voltage LIKE '%" + req + "%' OR " +
                        " espacement LIKE '%" + req + "%' OR " +
                        " tail LIKE '%" + req + "%' OR " +
                        " couleur LIKE '%" + req + "%' OR " +
                        " modules_chaine LIKE '%" + req + "%' OR " +
                        " longueur_cable LIKE '%" + req + "%' OR " +
                        " modules_ml LIKE '%" + req + "%' OR " +
                        " profondeur LIKE '%" + req + "%' OR " +
                        " prix_catalogue LIKE '%" + req + "%' OR " +
                        " nblumen LIKE '%" + req + "%'";
            remplirTable(req);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            SessionModel.Reset();
            try
            {
                Page.Session.Abandon();
            }
            catch { }
            // N'oubliez pas de reconfigurer "isActive = false" ici...
            Response.Redirect("index.aspx");
        }

        protected void Rad1_CheckedChanged(object sender, EventArgs e)
        {
            rad1Change();
        }

        public void vide()
        {
        }

        protected void Rad2_CheckedChanged(object sender, EventArgs e)
        {
            rad2Change();
        }

        protected void rad1Change()
        {
            if (Rad1.Checked)
            {
                btnUpLoad.Attributes.Add("style", "display:block");
                btnUpLoad.Text = "Inserer...";
                panCrop.Visible = false;
                cropimg.Visible = false;
                TextBox4.ReadOnly = false;
                TextBox4.BackColor = System.Drawing.Color.White;
                txtpuis.ReadOnly = true;
                txtpuis.BackColor = System.Drawing.Color.WhiteSmoke;
                test.ReadOnly = true;
                test.BackColor = System.Drawing.Color.WhiteSmoke;
                haute.ReadOnly = false;
                haute.BackColor = System.Drawing.Color.White;
                txtespace.ReadOnly = false;
                txtespace.BackColor = System.Drawing.Color.White;
                DropDownList2.Enabled = true;
                Button1.Enabled = false;
                nbLed.ReadOnly = true;
                nbLed.BackColor = System.Drawing.Color.WhiteSmoke;
                nbP.ReadOnly = true;
                nbP.BackColor = System.Drawing.Color.WhiteSmoke;
                Button1.Enabled = true;
                nbV.ReadOnly = true;
                nbV.BackColor = System.Drawing.Color.WhiteSmoke;
                Text.ReadOnly = false;
                Text.BackColor = System.Drawing.Color.White;
                FU1.Enabled = false;
                btnUpLoad.Enabled = true;
                Label4.Visible = true;
                
                lblMsg.Visible = false;

                DropDownList1.Visible = true;
                Label4.Visible = false;
                lParcourir.Visible = false;
                FU1.Visible = false;

                Text.Visible = true;
            }
        }

        protected void rad2Change()
        {
            if (Rad2.Checked)
            {
                btnUpLoad.Attributes.Add("style", "display:none");
                //btnUpLoad.Visible = false;
                TextBox4.ReadOnly = true;
                TextBox4.BackColor = System.Drawing.Color.WhiteSmoke;
                txtpuis.ReadOnly = true;
                txtpuis.BackColor = System.Drawing.Color.WhiteSmoke;
                test.ReadOnly = true;
                test.BackColor = System.Drawing.Color.WhiteSmoke;
                haute.ReadOnly = true;
                haute.BackColor = System.Drawing.Color.WhiteSmoke;
                txtespace.ReadOnly = true;
                txtespace.BackColor = System.Drawing.Color.WhiteSmoke;
                DropDownList2.Enabled = true;
                Button1.Enabled = false;
                nbLed.ReadOnly = true;
                nbLed.BackColor = System.Drawing.Color.WhiteSmoke;
                nbP.ReadOnly = true;
                nbP.BackColor = System.Drawing.Color.WhiteSmoke;
                nbV.ReadOnly = true;
                nbV.BackColor = System.Drawing.Color.WhiteSmoke;
                Text.ReadOnly = true;
                Text.BackColor = System.Drawing.Color.WhiteSmoke;
                Label4.Visible = true;
                FU1.Visible = true;
                FU1.Enabled = true;
                Button1.Enabled = true;
                FU1.BackColor = System.Drawing.Color.White;               
                Text.Text = "";
                btnUpLoad.Enabled = true;
                lblMsg.Visible = false; 
                DropDownList1.Visible = false;
                Label4.Visible = true;
                lParcourir.Visible = true;
                FU1.Visible = true;
                cropimg.Visible = false;
                Text.Visible = true;

                btnUpLoad.Enabled = true;
                //btnUpLoad.Visible = true;
                //btnUpLoad.Visible = false;
                //btnUpLoad.Text = "Charger...";
            }
        }

        protected void Text_TextChanged(object sender, EventArgs e)
        {

        }

        private Font scalling2(Graphics g, String family, string text, int width, int height)
        {
            Font fontFamily = new Font(family, 10);
            SizeF RealSize = g.MeasureString(text, fontFamily);
            float ratio = (float)RealSize.Height / (float)RealSize.Width;
            float boxRatio = (float)height / (float)width;
            float ScaleRatio;
            if (ratio > boxRatio)
            {
                ScaleRatio = (float)height / (float)RealSize.Height;
            }
            else
            {
                ScaleRatio = (float)width / (float)RealSize.Width;
            }
            float ScaleFontSize = fontFamily.Size * ScaleRatio;
            return new Font(fontFamily.FontFamily, ScaleFontSize);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        public void insertionText()
        {
            let = Text.Text;
            cropimg.Visible = true;
            string extension = ".jpg";
            string filePath = Path.Combine(Server.MapPath("~/crpmg"), "temp1" + extension);
            Bitmap bitMap = new Bitmap(maxWidth, maxHeight);
            using (Graphics graph = Graphics.FromImage(bitMap))
            {
                String text = let;
                Rectangle ImageSize = new Rectangle(0, 0, bitMap.Width, bitMap.Height);
                graph.FillRectangle(System.Drawing.Brushes.White, ImageSize);
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Center;
                strFormat.LineAlignment = StringAlignment.Center;
                string r = DropDownList1.SelectedItem.Value;
                graph.DrawString(text, scalling2(graph, r, text, bitMap.Width, bitMap.Height), System.Drawing.Brushes.Black,
                new Rectangle(0, 0, bitMap.Width, bitMap.Height), strFormat);
            }
            bitMap.Save(filePath);
            cropimg.Src = "~/crpmg/" + "temp1" + extension;
        }

        private void fonter()
        {
            var fnts = Fonts.GetFontFamilies(Server.MapPath(@"\Fonts\"));
            foreach (var item in fnts)
            {
                var i = item.FamilyNames.Values;
                foreach (var x in i)
                {
                    var y = new ListItem(x);
                    y.Attributes.Add("style", "font-family:" + x);
                    DropDownList1.Items.Add(y);
                }
            }
        }

        private void a()
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                DropDownList1.Font.Name = DropDownList1.SelectedItem.Value;
            }
            catch { }

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                var item = DropDownList1.Items[i];
                item.Attributes.Add("style", "font-family: " + item.Text);
            }
            if (Text.Text != "")
                insertionText();
        }
    }
}