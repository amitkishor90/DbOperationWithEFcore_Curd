using System.ComponentModel.DataAnnotations;

namespace DbOperationWithEFcore_Curd.Data
{
    public class CurrencyType
    {
        [Key]
        public int CurrencyId { get; set; }
        public string Title { get; set; }
        public string Descripation { get; set; }
    }
}
