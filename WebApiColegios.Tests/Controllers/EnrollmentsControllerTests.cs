using DomainLayer.DTOs;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.Entities;
using Moq;
using WebApiColegios.Controllers;

public class EnrollmentsControllerTests
{
    [Fact]
    public async Task EnrollStudent_ReturnsOk_WhenEnrollmentIsSuccessful()
    {
        // Arrange
        var mockService = new Mock<IEnrollmentService>();
        mockService.Setup(s => s.EnrollAsync(It.IsAny<EnrollmentDTO>()))
                   .Returns(Task.CompletedTask);

        var controller = new EnrollmentsController(mockService.Object);

        var dto = new EnrollmentDTO
        {
            StudentID = 1,
            SubjectIDs = new List<int> { 1, 2, 3 }
        };

        // Act
        var result = await controller.EnrollStudent(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Enrollment successful", okResult.Value);
    }

    [Fact]
    public async Task EnrollStudent_ReturnsBadRequest_WhenServiceThrowsException()
    {
        // Arrange
        var mockService = new Mock<IEnrollmentService>();
        mockService.Setup(s => s.EnrollAsync(It.IsAny<EnrollmentDTO>()))
                   .ThrowsAsync(new Exception("Too many high-credit subjects"));

        var controller = new EnrollmentsController(mockService.Object);

        var dto = new EnrollmentDTO
        {
            StudentID = 1,
            SubjectIDs = new List<int> { 1, 2, 3, 4 }
        };

        // Act
        var result = await controller.EnrollStudent(dto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Too many high-credit subjects", badRequest.Value);
    }

    [Fact]
    public async Task GetSubjectsByStudent_ReturnsSubjects()
    {
        // Arrange
        var studentId = 1;
        var mockService = new Mock<IEnrollmentService>();
        mockService.Setup(s => s.GetSubjectsByStudentIdAsync(studentId))
                   .ReturnsAsync(new List<Subject>
                   {
                       new Subject { SubjectID = 1, Name = "Math", Code = "MAT101", Credits = 4 }
                   });

        var controller = new EnrollmentsController(mockService.Object);

        // Act
        var result = await controller.GetSubjectsByStudent(studentId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var subjects = Assert.IsAssignableFrom<IEnumerable<Subject>>(okResult.Value);
        Assert.Single(subjects);
    }
}
