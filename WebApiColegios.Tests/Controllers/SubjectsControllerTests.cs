using DomainLayer.DTOs;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiColegios.Controllers;

namespace WebApiColegios.Tests
{
    public class SubjectsControllerTests
    {
        private readonly Mock<ISubjectService> _mockService;
        private readonly SubjectsController _controller;

        public SubjectsControllerTests()
        {
            _mockService = new Mock<ISubjectService>();
            _controller = new SubjectsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfSubjects()
        {
            var subjects = new List<SubjectDTO>
            {
                new SubjectDTO { SubjectID = 1, Name = "Math", Code = "M101", Credits = 4 }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(subjects);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<IEnumerable<SubjectDTO>>(okResult.Value);
            Assert.Single(returned);
        }

        [Fact]
        public async Task GetById_ReturnsSubject_WhenExists()
        {
            var subject = new SubjectDTO { SubjectID = 1, Name = "Science", Code = "S101", Credits = 3 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(subject);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<SubjectDTO>(okResult.Value);
            Assert.Equal("Science", returned.Name);
        }

        [Fact]
        public async Task Create_ReturnsCreatedSubject()
        {
            var createDto = new CreateSubjectDTO { Name = "English", Code = "ENG101", Credits = 2 };
            var resultDto = new SubjectDTO { SubjectID = 2, Name = "English", Code = "ENG101", Credits = 2 };

            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(resultDto);

            var result = await _controller.Create(createDto);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<SubjectDTO>(created.Value);
            Assert.Equal("English", returned.Name);
        }

        [Fact]
        public async Task Update_ReturnsNoContent()
        {
            var updateDto = new UpdateSubjectDTO { Name = "Updated", Code = "UPD101", Credits = 3 };

            var result = await _controller.Update(1, updateDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}