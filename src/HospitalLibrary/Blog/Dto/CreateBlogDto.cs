namespace HospitalLibrary.Blog.Dto;

public class CreateBlogDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    
    public Model.Blog ToEntity()
    {
        return new Model.Blog
        {
            Title = Title,
            Content = Content,
        };
    }
}