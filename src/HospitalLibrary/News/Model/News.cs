using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.News.Dto;

namespace HospitalLibrary.News.Model;

public class News
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public SystemAdministrator.Model.SystemAdministrator Author { get; set; }

    public NewsDto ToDto()
    {
        return new NewsDto
        {
            Title = Title,
            Content = Content,
        };
    }
}