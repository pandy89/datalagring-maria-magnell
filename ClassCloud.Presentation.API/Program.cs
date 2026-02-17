using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Services;
using ClassCloud.Infrastructure;
using ClassCloud.Infrastructure.Persistence.Repositories;
using ClassCloud.Presentation.API.Middlewares;
using ClassCloud.Presentation.API.Extensions;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Exceptions.SqlServer;
using ClassCloud.Application.Dtos.Course;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClassCloudDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDdFile")).UseExceptionProcessor());


// Services
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CourseSessionService>();

// Repos
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("All", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidation();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["support"] = "support@domain.com";
    };
});

builder.Services.AddExceptionHandler<GlobalExecptionHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseCors("All");

#region Courses

var courses = app.MapGroup("/api/courses");

// Create course
courses.MapPost("/", async (CreateCourseDto dto, CourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.CreateCourseAsync(dto, ct);
    return result.Match(
        course => Results.Created($"/api/courses/{course.CourseCode}", course),
        errors => errors.ToProblemDetails()
    );
});

// Get all courses
courses.MapGet("/", async (CourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.GetAllCoursesAsync(ct);
    return Results.Ok(result);
});

// Get one course
courses.MapGet("/{courseCode}", async (string courseCode, CourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.GetOneCourseAsync(courseCode, ct);
    return result.Match(
        course => Results.Ok(course),
        errors => errors.ToProblemDetails()
        );
});

// Update course
courses.MapPut("/{courseCode}", async (string courseCode, UpdateCourseDto dto, CourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.UpdateCourseAsync(courseCode, dto, ct);
    return result.Match(
        course => Results.Ok(course),
        errors => errors.ToProblemDetails()
        );
});

// Delete course
courses.MapDelete("/{courseCode}", async (string courseCode, CourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.DeleteCourseAsync(courseCode, ct);
    return result.Match(
        _ => Results.NoContent(),
        errors => errors.ToProblemDetails()
    );
});
#endregion

#region CourseSessions
var courseSessions = app.MapGroup("/api/course-sessions").WithTags("Course Sessions");

courseSessions.MapGet("/{id}", async (int id, CourseSessionService service, CancellationToken ct) =>
{
    var result = await service.GetOneCourseSessionAsync(id, ct);
    return result.Match(
        cs => Results.Ok(cs),
        errors => errors.ToProblemDetails()
    );
});


#endregion

app.Run();