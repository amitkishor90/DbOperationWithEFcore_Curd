using System.ComponentModel.DataAnnotations;

namespace DbOperationWithEFcore_Curd.Data
{
    public class CurrencyType
    {
        [Key]
        public int CurrencyId { get; set; }
        public int Currency { get; set; }
        public string Descripation { get; set; }
    }
}
