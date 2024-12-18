namespace DbOperationWithEFcore_Curd.UserDataModel
{
    public class BooksModel
    {
        public string Title { get; set; }
        public string Descripation { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateOnly CreateDate { get; set; }
        public int LanguageId { get; set; }
        public int Country { get; set; }
    }
}
