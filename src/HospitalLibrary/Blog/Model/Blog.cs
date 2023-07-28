using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Blog.Dto;

namespace HospitalLibrary.Blog.Model;

public class Blog
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Doctor.Model.Doctor Author { get; set; }
    
    public BlogDto ToDto()
    {
        return new BlogDto
        {
            Id = Id,
            Title = Title,
            Content = Content,
            AuthorId = Author.Id
        };
    }
}