using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegrationLibrary.BloodBank.Model;

public class Term
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public List<ScheduledPatients> ScheduledPatients { get; set; }
}