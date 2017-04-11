using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial.FileUpload.Models;

namespace Tutorial.FileUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveTutorial(TutorialModel tutorial)
        {
            

            BinaryReader br = new BinaryReader(tutorial.Attachment.InputStream);
            Byte[] bytes = br.ReadBytes(tutorial.Attachment.ContentLength);
            br.Close();

            string strQuery = "insert into tblFiles1(Title, DescriptionCol,Attachment) values (@Title,@Description, @Attachment)";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = tutorial.Title;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = tutorial.Description;
            cmd.Parameters.Add("@Attachment", SqlDbType.Binary).Value = bytes;
            InsertUpdateData(cmd);

            return Json("Tutorial Saved", JsonRequestBehavior.AllowGet);
        }


        private Boolean InsertUpdateData(SqlCommand cmd)
        {
            String strConnString = System.Configuration.ConfigurationManager
            .ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

    }
}