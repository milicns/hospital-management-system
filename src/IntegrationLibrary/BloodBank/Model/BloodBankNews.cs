using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntegrationLibrary.BloodBank.Dto;

namespace IntegrationLibrary.BloodBank.Model;

public class BloodBankNews
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public BloodBankNewsState State { get; set; }
    
    public BloodBankNewsDto ToBloodBankNewsDto()
    {
        return new BloodBankNewsDto
        {
            Id = Id,
            Title = Title,
            Content = Content,
            State = State
        };
    }
    public PublishedNewsDto ToPublishedNewsDto()
    {
        return new PublishedNewsDto
        {
            Title = Title,
            Content = Content
        };
    }
}