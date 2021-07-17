using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using shopbridge.Blayer;
using shopbridge.Dlayer;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace shopbridge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        bl_product bl = new bl_product();
        dl_product dl = new dl_product();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        codes co = new codes();

        [HttpPost("Insert_Product")]
        public async Task<ReturnClass.ReturnBool> Insert_Product(bl_product prod)
        {
            try
            {

                bl.Product_id = Product_ID();
                bl.Product_Code = prod.Product_Code;
                bl.Product_Name = prod.Product_Name;
                bl.Product_Description = prod.Product_Description;
                bl.Product_Price = prod.Product_Price;
                bl.Product_Quantity = prod.Product_Quantity;
                bl.Entry_Date_Time = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                bl.Client_ip = Utilities.GetRemoteIPAddress(this.HttpContext, true);
                bl.User_id = prod.User_id;
                rb = await dl.Insert_Product_Details(bl);
            }
            catch (IOException io)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Insert_Product(io)", io);
                rb.message = "Unable to insert new product item";
            }
            catch (Exception ex)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Insert_Product(error)", ex);
            }
            return rb;
        }


        [HttpPost("Update_Product")]
        public async Task<ReturnClass.ReturnBool> Update_Product(bl_product prod)
        {
            try
            {

                bl.Product_id = prod.Product_id;
                bl.Product_Code = prod.Product_Code;
                bl.Product_Name = prod.Product_Name;
                bl.Product_Description = prod.Product_Description;
                bl.Product_Price = prod.Product_Price;
                bl.Product_Quantity = prod.Product_Quantity;
                bl.Entry_Date_Time = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                bl.Client_ip = Utilities.GetRemoteIPAddress(this.HttpContext, true);
                bl.User_id = prod.User_id;
                rb = await dl.Update_Product_Details(bl);
            }
            catch (IOException io)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Update_Product(io)", io);
                rb.message = "Unable to update new product item";
            }
            catch (Exception ex)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Update_Product(error)", ex);
            }
            return rb;
        }


        [HttpPost("{Product_id}")]
        public async Task<ReturnClass.ReturnBool> Delete_Product(string Product_id)
        {
            try
            {

                bl.Product_id = Product_id;
                rb = await dl.Delet_Product_Details(bl);
            }
            catch (IOException io)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Delete_Product(io)", io);
                rb.message = "Unable to Delete product item";
            }
            catch (Exception ex)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Delete_Product(error)", ex);
            }
            return rb;
        }


        [HttpGet("Display_Product")]
        public async Task<string> Display_Product()
        {
            try
            {

                dt = await dl.Display_Product_Details();
                dt.json= JsonConvert.SerializeObject(dt.table, Formatting.Indented);
            }
            catch (IOException io)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Display_Product(io)", io);
                dt.message = "Unable to display product items";
            }
            catch (Exception ex)
            {
                Gen_Error_Rpt.Write_Error("Product_Admin:Display_Product(error)", ex);
            }
            return dt.json;
        }

        public string Product_ID()
        {

            try
            {
                co.C_year = DateTime.Now.ToString("yyyy");
                dt = dl.Product_id(co);
                if (dt.table.Rows.Count > 0)
                {
                    co.S1 = dt.table.Rows[0]["pid"].ToString();
                    if (co.S1 == "")
                    {
                        co.S = "0";
                        co.Nid = Convert.ToString(Convert.ToInt32(co.S) + 1);
                    }
                    else
                        co.Nid = Convert.ToString(Convert.ToInt32(co.S1) + 1);
                }
                else
                {
                    co.S = "0";
                    co.Nid = Convert.ToString(Convert.ToInt32(co.S) + 1);
                }
                co.Aid = DateTime.Now.ToString("yy") + "9" + "1" + co.Nid.PadLeft(4, '0');
            }
            catch { co.Aid = DateTime.Now.ToString("yy") + "9" + "0001"; }
            return co.Aid;
        }
    }

}

