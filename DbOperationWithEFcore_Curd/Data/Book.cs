namespace DbOperationWithEFcore_Curd.Data
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Descripation { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateOnly CreateDate { get; set; }
        public int LanguageId { get; set; }
        public int Country { get; set; }

        public Language Language { get; set; }

    }
}
