using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDotNet.Pages
{
    public class BlogModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public BlogModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}