using System.Collections.Generic;

namespace HospitalLibrary.SystemAdministrator.Model;

public class SystemAdministrator : User.Model.User
{
    public List<News.Model.News> News { get; set; }
}