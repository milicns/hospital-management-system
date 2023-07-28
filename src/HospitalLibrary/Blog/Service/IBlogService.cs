using System.Collections.Generic;
using HospitalLibrary.Blog.Dto;

namespace HospitalLibrary.Blog.Service;

public interface IBlogService
{
    IEnumerable<BlogDto> GetAllBlogs();
    BlogDto WriteBlog(CreateBlogDto blogDto);
}