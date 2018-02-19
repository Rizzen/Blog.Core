namespace Blog.Core.Models.Settings
{
    public class SiteSettings
    {
        public string Author { get; set; }
        
        public string Description { get; set; }
        
        public int PostsPerPage { get; set; }
        
        public string SiteName { get; set; }
        
        public string PostsFolderPath { get; set; }
    }
}