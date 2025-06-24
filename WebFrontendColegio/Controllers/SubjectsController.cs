using Microsoft.AspNetCore.Mvc;
using WebFrontendColegio.Models;

namespace WebFrontendColegio.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly HttpClient _httpClient;

        public SubjectsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7148/");
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _httpClient.GetFromJsonAsync<List<SubjectDTO>>("api/school/subjects");
            return View(subjects);
        }
    }
}