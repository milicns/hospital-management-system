namespace HospitalLibrary.News.Dto;

public class PublishNewsDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }

    public Model.News ToEntity()
    {
        return new Model.News
        {
            Title = Title,
            Content = Content,
            AuthorId = AuthorId
        };
    }
}