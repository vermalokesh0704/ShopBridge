using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopbridge.Blayer
{
    public class bl_product
    {
       
        public string Product_id { get; set; }

        [Required(ErrorMessage = "Product Code required")]
        [StringLength(5, ErrorMessage = "The {0} value cannot exceed {1} characters. ", MinimumLength = 3)]
        public string Product_Code { get; set; }

        [Required(ErrorMessage = "Product Name required")]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters. ", MinimumLength = 5)]
        public string Product_Name { get; set; }

        [Required(ErrorMessage = "Product Description required")]
        [StringLength(500, ErrorMessage = "The {0} value cannot exceed {1} characters. ", MinimumLength = 10)]
        public string Product_Description { get; set; }

        [Required(ErrorMessage = "Product Price required")]
        public Double Product_Price { get; set; }

        [Required(ErrorMessage = "Product Quantity required")]
        public Int64 Product_Quantity { get; set; }

        [Required(ErrorMessage = "User id required")]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ", MinimumLength = 2)]
        public string User_id { get; set; }
        public string Client_ip { get; set; }
        public string Entry_Date_Time { get; set; }

    }

    public class codes
    {
        string statecode, districtcode, c_year, s1, s, nid, aid;
        public string Statecode { get { return statecode; } set { statecode = value; } }
        public string Districtcode { get { return districtcode; } set { districtcode = value; } }
        public string C_year { get { return c_year; } set { c_year = value; } }
        public string S1 { get { return s1; } set { s1 = value; } }
        public string S { get { return s; } set { s = value; } }
        public string Nid { get { return nid; } set { nid = value; } }
        public string Aid { get { return aid; } set { aid = value; } }

    }
}
