using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dollar.Models.Home
{
    public class CsvFields
    {
        [Key]
        public string Store_id { get; set; }
        public string Store_name { get; set; }
        public string Store_phone { get; set; }


        public static CsvFields FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            CsvFields csvValues = new CsvFields();
            csvValues.Store_id = values[0];
            csvValues.Store_name = values[1];
            csvValues.Store_phone = values[2];
            return csvValues;
        }

    }
}
