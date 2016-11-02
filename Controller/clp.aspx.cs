using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;
namespace PR3.view
{
    public partial class pr3 : System.Web.UI.Page
    {
        protected void Page_Load11(object sender, EventArgs e)
        {

        }

        protected void btnCrop_Click11(object sender, EventArgs e)
        {

            string confilename, confilepath;
            string fileName = Path.GetFileName(imgUpload.ImageUrl);
            string filePath = Path.Combine(Server.MapPath("~/img"), fileName);
            if (File.Exists(filePath))
            {

                System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                Rectangle CropArea = new Rectangle(

                    Convert.ToInt32(X.Value),
                    Convert.ToInt32(Y.Value),
                    Convert.ToInt32(W.Value),
                    Convert.ToInt32(H.Value));

                try
                {

                    Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                    using (Graphics g = Graphics.FromImage(bitMap))
                    {

                        g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                    }
                    int v = CropArea.Width;
                    string r = v.ToString();
                    test.Text = r;

                    int s = CropArea.Height;
                    string h = s.ToString();
                    haute.Text = h;
                    confilename = "Crop_" + fileName;
                    confilepath = Path.Combine(Server.MapPath("~/cropimg"), confilename);
                    bitMap.Save(confilepath);
                    cropimg.Visible = true;
                    cropimg.Src = "~/cropimg/" + confilename;
                    panCrop.Visible = false;
                }
                catch
                {
                    throw;
                }

            }
        }

        protected void btnUpLoad_Click11(object sender, EventArgs e)
        {
            string uploadFileName = "";
            string uploadFilePath = "";

            //if (FU1.HasFile)
            //{

                string ext = Path.GetExtension(FU1.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                {
                    uploadFileName = Guid.NewGuid().ToString() + ext;
                    uploadFilePath = Path.Combine(Server.MapPath("~/img"), uploadFileName);
                    try
                    {
                        FU1.SaveAs(uploadFilePath);
                        imgUpload.ImageUrl = "~/img/" + uploadFileName;
                        panCrop.Visible = true;
                        btnCrop.Visible = true;
                        cropimg.Visible = false;
                    }
                    catch
                    {
                        lblMsg.Text = "Error! Please try again";
                    }
                }
                else
                {
                    lblMsg.Text = "Select file type not allowed!";
                }

            }
            //else
            //{
            //    lblMsg.Text = "Please select file first!";

            //}
        }





    //}
}