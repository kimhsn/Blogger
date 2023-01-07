using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDotNet.Data;
using AppDotNet.Entities;
using System.Security.Claims;
using AppDotNet.Models;

namespace AppDotNet.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDotNetContext _context;

        public PostsController(AppDotNetContext context)
        {
            _context = context;
        }

        // GET: Posts
        [HttpGet("blogs/{id}/posts")]
        public async Task<IActionResult> Index(int id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isSupervisor = false;
            if (User.Identity.IsAuthenticated)
            {

                var role = (from a in _context.UserRoles
                            where a.UserId == UserId
                            select a.RoleId).ToList();

                isSupervisor = role[0] == "id_superviseur";
            }

            var post = await _context.Posts.Where(m => m.Blog.ID == id).Select(post => new PostModel()
            {
                ID = post.ID,
                Description = post.Description,
                Name = post.Name,
                NbLikes = post.NbLikes,
                AlreadyLiked = _context.Likes.Any(u => u.post == post && u.user.Id == UserId),
                BelongToMe = _context.Posts.Any(p => p.ID == post.ID && p.User.Id == UserId),
                IamIsupervisor = isSupervisor
            }).ToListAsync();

            return View(post);
          
        }


        // GET: Posts
        [HttpPost("posts/{id}/like")]
        public async Task<IActionResult> like(int id)
        {
            var test = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync( u => u.Email == User.Identity.Name);

            var post = await _context.Posts.FindAsync(id);


            //test 
            //var post = await _context.Likes.FirstOrDefault(like => like.);

            //ajouter 
            Likes like = new Likes();
            like.user = user;
            like.post = post;
            _context.Add(like);
            await _context.SaveChangesAsync();

            //modifier post
            //select count(userid) from dbo.likes where postID = 2;
            var nbLikes = (from lk in _context.Likes

                           where lk.post.ID.Equals(id)
                           select new{ lk.ID}).Count();

            post.NbLikes = nbLikes;
            await _context.SaveChangesAsync();

			return Json(nbLikes);
        }



        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/posts/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,NbLikes,CreatedTimestamp")] Post post)
        {
            int blogId = Int16.Parse(Request.Form["blog"]);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.ID == blogId);

          
            post.CreatedTimestamp = DateTime.Now;
            post.NbLikes = 0;
            post.User = user;
            post.Blog = blog;


            _context.Add(post);
            await _context.SaveChangesAsync();

            return Redirect("/blogs/"+blogId+"/posts");
            
            
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,NbLikes,CreatedTimestamp")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost("/posts/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'AppDotNetContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
                        int blogId = Int16.Parse(Request.Form["blog"]);

            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();

            
            return Redirect("/blogs/" + blogId + "/posts");
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.ID == id);
        }
    }
}
