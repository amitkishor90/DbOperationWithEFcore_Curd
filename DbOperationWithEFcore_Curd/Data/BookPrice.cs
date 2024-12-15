using System.ComponentModel.DataAnnotations;

namespace DbOperationWithEFcore_Curd.Data
{
    public class BookPrice
    {
        [Key]
        public int BookID { get; set; }
        public int Amount { get; set; }
        public int CurrencyId { get; set; }
    }
}
