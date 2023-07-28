using System.Collections.Generic;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Dto;

public class BloodBankNewsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public BloodBankNewsState State { get; set; }

    public BloodBankNews ToEntity()
    {
        return new BloodBankNews
        {
            Id = Id,
            Title = Title,
            Content = Content,
            State = State,
        };
    }
}