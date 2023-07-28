using System.Collections.Generic;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Blog.Repository;

public interface IBlogRepository
{
    IEnumerable<Model.Blog> GetAll();
    Model.Blog CreateBlog(Model.Blog blog);

}