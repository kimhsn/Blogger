using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDotNet.Data;
using AppDotNet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using NuGet.Packaging;
using AppDotNet.Models;


namespace AppDotNet.Controllers
{
    public class BlogsController : Controller
    {
        private readonly AppDotNetContext _context;

        public BlogsController(AppDotNetContext context)
        {
            _context = context;
        }

		// GET: Blogs
		[HttpGet("/blogs")]
		public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Blogs.Select(blog => new BlogModel()
                {
                    ID = blog.ID,
                    Name = blog.Name,
                    Prive = blog.Prive,
                    CreatedTimestamp = blog.CreatedTimestamp,
                    AdminEmail = blog.Admin.Email
                }).ToListAsync());

            } else
            {   //affiche que public
                return View(await _context.Blogs.Where(b => b.Prive == false).Select(blog => new BlogModel()
                {
                    ID = blog.ID,
                    Name = blog.Name,
                    Prive = blog.Prive,
                    CreatedTimestamp = blog.CreatedTimestamp,
                }).ToListAsync());
            }
                
        }

        [HttpGet("/user/role")]
        public async Task<IActionResult> getRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
               

                var role = (from a in _context.UserRoles
                                where a.UserId == user.Id
                                select a).ToList();
                return Json(role);
            }
            else
            {   
                return Json("");
            }

        }


        [HttpGet("/admin/blogs")]
        public async Task<IActionResult> getAdminBlogs()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);


                var blogs = (from b in _context.Blogs
                             join ur in _context.Users on b.Admin.Id equals "2f8b9ab3-ad7f-4f4c-9035-ac63965fde31"
                             select b.ID).Distinct().ToList();
                
                return Json(blogs);
            }
            else
            {
                return Json("");
            }                                                                                           

        }


        // GET: BlogsAdmins
        [HttpGet("/blogs/admins")]
        public async Task<IActionResult> getAdmins()
        {

             var q = (from us in _context.Users
                     join ur in _context.UserRoles on us.Id equals ur.UserId
                     join ro in _context.Roles on ur.RoleId equals ro.Id
                     where ro.Id.Equals("id_admin_blog")
                     select new
                     {
                         us.Id,
                         us.UserName,
                     }).ToList();
            return Json(q);

        }


        // GET: Blogs/Details/5
        [HttpGet("/blogs/details/{id}")]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return Json(blog);
        }

   
        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/blogs/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Prive,CreatedTimestamp")] Blog blog)
        {
            blog.CreatedTimestamp= DateTime.Now;

            if(Request.Form["Prive"].Equals("on"))
            {
                blog.Prive = true;
            } else
            {
                blog.Prive = false;
            }
            _context.Add(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("/blogs/{id}/admins/{idAdmin}/assignate")]
        public async Task<IActionResult> Assignate(int id, string idAdmin)
        {
            var blog = await _context.Blogs.FindAsync(id);
           
            var user = await _context.Users.FindAsync(idAdmin.ToString());

            blog.Admin = user;
			_context.Update(blog);
			await _context.SaveChangesAsync();
           

            return RedirectToAction(nameof(Index));
        }


        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/blogs/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Prive,CreatedTimestamp")] Blog blog)
        {
            if (id != blog.ID)
            {
                return NotFound();
            }

            try
            {
                if (Request.Form["Prive"].Equals("on"))
                {
                    blog.Prive = true;
                }
                else
                {
                    blog.Prive = false;
                }
                _context.Update(blog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(blog.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
           
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost("/blogs/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'AppDotNetContext.Blogs'  is null.");
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
          return _context.Blogs.Any(e => e.ID == id);
        }
    }
}
