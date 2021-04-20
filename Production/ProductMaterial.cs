using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 CREATE PROCEDURE AddProductMaterial(@Code varchar(31),
           @CompanyCode varchar(10),
           @MaterialID int,
           @GradeID int,
           @last_updated_on datetime2(7) OUTPUT,
           @last_updated_by varchar(50) OUTPUT,
		   @PmID int OUTPUT)
 */
namespace LabelPrinting
{
    public class ProductMaterial  //needs to be public to allow passing between forms, eg ProductMaterialMaint => EditProductMaterialRow
    {
        public int PmID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public int? MaterialID { get; set; }
        public int? GradeID { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string last_updated_by { get; set; }
        public string ImagePath { get; set; }  //read only

    }
}
