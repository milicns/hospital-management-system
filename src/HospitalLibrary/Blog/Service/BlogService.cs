using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Blog.Dto;
using HospitalLibrary.Blog.Repository;
using HospitalLibrary.Doctor.Repository;

namespace HospitalLibrary.Blog.Service;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IDoctorRepository _doctorRepository;
    
    public BlogService(IBlogRepository blogRepository, IDoctorRepository doctorRepository)
    {
        _blogRepository = blogRepository;
        _doctorRepository = doctorRepository;
    }
    
    public BlogDto WriteBlog(CreateBlogDto createBlogDto)
    {
        var blogForCreation = createBlogDto.ToEntity();
        blogForCreation.Author = _doctorRepository.GetById(createBlogDto.AuthorId);
        return _blogRepository.CreateBlog(blogForCreation).ToDto();
    }
    
    public IEnumerable<BlogDto> GetAllBlogs()
    {
        return _blogRepository.GetAll().Select(blog => blog.ToDto());
    }
    
}