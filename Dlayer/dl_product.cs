using MySql.Data.MySqlClient;
using shopbridge.Blayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopbridge.Dlayer
{
    public class dl_product
    {
        db_maria_connection db = new db_maria_connection();
        Utilities util = new Utilities();
        ReturnClass.ReturnDataTable dt = new ReturnClass.ReturnDataTable();
        ReturnClass.ReturnBool rb = new ReturnClass.ReturnBool();
        public async Task<ReturnClass.ReturnBool> Insert_Product_Details(bl_product bl)
        {
            string query = @"Insert into product_items (Product_id,Product_Code,Product_Name,Product_Description,Product_Price,Product_Quantity,User_id,Client_ip,Entry_Date_Time)Values(@Product_id,@Product_Code,@Product_Name,@Product_Description,@Product_Price,@Product_Quantity,@User_id,@Client_ip,@Entry_Date_Time)";
            MySqlParameter[] pm = new MySqlParameter[]
            {
                     new MySqlParameter("Product_id", bl.Product_id),
                       new MySqlParameter("Product_Code", bl.Product_Code),
                       new MySqlParameter("Product_Name", bl.Product_Name),
                       new MySqlParameter("Product_Description", bl.Product_Description),
                       new MySqlParameter("Product_Price", bl.Product_Price),
                       new MySqlParameter("Product_Quantity", bl.Product_Quantity),
                       new MySqlParameter("User_id", bl.User_id),
                       new MySqlParameter("Client_ip", bl.Client_ip),
                       new MySqlParameter("Entry_Date_Time", bl.Entry_Date_Time)
            };
            rb = await db.executeInsertQuery_async(query, pm);
            return rb;
        }

        public async Task<ReturnClass.ReturnBool> Update_Product_Details(bl_product bl)
        {
            string query = @"update product_items set Product_Code=@Product_Code,Product_Name=@Product_Name,Product_Description=@Product_Description,Product_Price=@Product_Price,Product_Quantity=@Product_Quantity,User_id=@User_id,Client_ip=@Client_ip,Entry_Date_Time=@Entry_Date_Time where Product_id=@Product_id";
            MySqlParameter[] pm = new MySqlParameter[]
            {
                     new MySqlParameter("Product_id", bl.Product_id),
                       new MySqlParameter("Product_Code", bl.Product_Code),
                       new MySqlParameter("Product_Name", bl.Product_Name),
                       new MySqlParameter("Product_Description", bl.Product_Description),
                       new MySqlParameter("Product_Price", bl.Product_Price),
                       new MySqlParameter("Product_Quantity", bl.Product_Quantity),
                       new MySqlParameter("User_id", bl.User_id),
                       new MySqlParameter("Client_ip", bl.Client_ip),
                       new MySqlParameter("Entry_Date_Time", bl.Entry_Date_Time)
            };
            rb = await db.executeInsertQuery_async(query, pm);
            return rb;
        }


        public async Task<ReturnClass.ReturnBool> Delet_Product_Details(bl_product bl)
        {
            string query = @"delete from product_items where Product_id=@Product_id";
            MySqlParameter[] pm = new MySqlParameter[]
            {
                     new MySqlParameter("Product_id", bl.Product_id)
            };
            rb = await db.executeInsertQuery_async(query, pm);
            return rb;
        }
        public async Task<ReturnClass.ReturnDataTable> Display_Product_Details()
        {
            string query = @"Select * from  product_items";
            dt = await db.executeSelectQuery_async(query);
            return dt;
        }


        public ReturnClass.ReturnDataTable Display_Product_Detailss()
        {
            string query = @"Select * from  product_items";
            dt = db.executeSelectQuery(query);
            return dt;
        }

        public ReturnClass.ReturnDataTable Product_id(codes co)
        {
            ReturnClass.ReturnDataTable rb = new ReturnClass.ReturnDataTable();
            string qr = @"SELECT IFNULL(MAX(CAST(SUBSTRING(Product_id,6,4) as int)),0) as pid  from product_items 
                                WHERE year(Entry_Date_Time) = @c_year ";
            MySqlParameter[] pr = new MySqlParameter[]{
            new MySqlParameter("c_year",co.C_year),
        };
            rb = db.executeSelectQuery(qr, pr);
            return rb;
        }

    }
}
