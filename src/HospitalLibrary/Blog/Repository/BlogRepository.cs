using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Blog.Repository;

public class BlogRepository : IBlogRepository
{   
    private readonly HospitalDbContext _context;
    

    public BlogRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public Model.Blog CreateBlog(Model.Blog blog)
    {
        var createdBlog = _context.Blogs.Add(blog);
        _context.SaveChanges();
        return createdBlog.Entity;
    }
    
    public IEnumerable<Model.Blog> GetAll()
    {
        return _context.Blogs.Include(blog => blog.Author).ToList();
    }
    
}