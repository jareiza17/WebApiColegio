using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Services;
using DomainLayer.DTOs;
using WebApiColegios.Controllers;
using ModelsLayer.Entities;

namespace WebApiColegios.Tests
{
    public class EnrollmentsControllerTests
    {
        private readonly Mock<IEnrollmentService> _mockService;
        private readonly EnrollmentsController _controller;

        public EnrollmentsControllerTests()
        {
            _mockService = new Mock<IEnrollmentService>();
            _controller = new EnrollmentsController(_mockService.Object);
        }

        [Fact]
        public async Task EnrollStudent_ReturnsOk_WhenEnrollmentIsSuccessful()
        {
            // Arrange
            var dto = new EnrollmentDTO
            {
                StudentID = 1,
                SubjectIDs = new List<int> { 1, 2 }
            };

            _mockService.Setup(s => s.EnrollAsync(dto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.EnrollStudent(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Enrollment successful", okResult.Value);
        }

        [Fact]
        public async Task EnrollStudent_ReturnsBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            var dto = new EnrollmentDTO
            {
                StudentID = 1,
                SubjectIDs = new List<int> { 1, 2, 3, 4 }
            };

            _mockService.Setup(s => s.EnrollAsync(dto))
                        .ThrowsAsync(new Exception("Too many high-credit subjects"));

            // Act
            var result = await _controller.EnrollStudent(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Too many high-credit subjects", badRequest.Value);
        }

        [Fact]
        public async Task GetSubjectsByStudent_ReturnsSubjects()
        {
            // Arrange
            var studentId = 1;
            var mockSubjects = new List<Subject>
            {
                new Subject { SubjectID = 1, Name = "Math", Code = "M101", Credits = 4 }
            };

            _mockService.Setup(s => s.GetSubjectsByStudentIdAsync(studentId))
                        .ReturnsAsync(mockSubjects);

            // Act
            var result = await _controller.GetSubjectsByStudent(studentId);

            // Accede al .Result
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var subjects = Assert.IsAssignableFrom<IEnumerable<Subject>>(okResult.Value);

            // Assert
            Assert.Single(subjects);
        }

    }
}