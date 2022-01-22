namespace Blog.Areas.Admin.ViewModels
{
    public class PostIndexViewModel
    {
        public int Id { get; set; }  
        public string Title { get; set; }

        public string CategoryName { get; set; }

        public DateTime PublisedTime { get; set; }
    }
}
