using System.Collections.Generic;
using IntegrationLibrary.BloodBank.Model;

namespace IntegrationLibrary.BloodBank.Dto;

public class CreateNewsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Term> Terms { get; set; }
    
    
    public BloodBankNews ToEntity()
    {
        return new BloodBankNews
        {
            Id = Id,
            Title = Title,
            Content = Content,
            State = BloodBankNewsState.Received
        };
    }
}