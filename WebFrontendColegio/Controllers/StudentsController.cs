using Microsoft.AspNetCore.Mvc;
using WebFrontendColegio.Models;

namespace WebFrontendColegios.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var students = await _httpClient.GetFromJsonAsync<List<StudentDTO>>("api/school/Students");
            return View(students);
        }
    }
}