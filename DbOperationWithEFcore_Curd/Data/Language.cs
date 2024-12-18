namespace DbOperationWithEFcore_Curd.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Book> FK_Books { get; set; }
    }
}
