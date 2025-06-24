using Microsoft.AspNetCore.Mvc;
using WebFrontendColegio.Models;

namespace WebFrontendColegio.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public EnrollmentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var students = await _httpClient.GetFromJsonAsync<List<StudentDTO>>("api/school/students");
            var subjects = await _httpClient.GetFromJsonAsync<List<SubjectDTO>>("api/school/subjects");

            var viewModel = new EnrollmentViewModel
            {
                Students = students ?? new List<StudentDTO>(),
                Subjects = subjects ?? new List<SubjectDTO>()
            };

            return View(viewModel);
        }
    }
}
