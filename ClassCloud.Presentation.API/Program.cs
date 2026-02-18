using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Services;
using ClassCloud.Infrastructure;
using ClassCloud.Infrastructure.Persistence.Repositories;
using ClassCloud.Presentation.API.Extensions;
using ClassCloud.Presentation.API.Middlewares;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClassCloudDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbFile")).UseExceptionProcessor());


// Services
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CourseSessionService>();
builder.Services.AddScoped<ParticipantService>();

// Repos
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();


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

var courses = app.MapGroup("/api/courses").WithTags("Courses");

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

#region Participants
var participant = app.MapGroup("/api/participant").WithTags("Participant");

// Create participant
participant.MapPost("/", async (CreateParticipantDto dto, ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.CreateParticipantAsync(dto, ct);
    return result.Match(
        participant => Results.Created($"/api/participant/{participant.Email}", participant),
        errors => errors.ToProblemDetails()
    );
});

// Get one participant
participant.MapGet("/{email}", async (string email, ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetOneParticipantAsync(email, ct);
    return result.Match(
        participant => Results.Ok(participant),
        errors => errors.ToProblemDetails()
        );
});

// Get all participant order by email
participant.MapGet("/", async (ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetAllParticipantsAsync(ct);
    return Results.Ok(result);
});

// Update course
participant.MapPut("/{email}", async (string email, UpdateParticipantDto dto, ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.UpdateParticipantAsync(email, dto, ct);
    return result.Match(
        participant => Results.Ok(participant),
        errors => errors.ToProblemDetails()
        );
});


// Delete participant with email
participant.MapDelete("/{email}", async (string Email, ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.DeleteParticipantAsync(Email, ct);
    return result.Match(
        _ => Results.NoContent(),
        errors => errors.ToProblemDetails()
    );
});

#endregion

#region Teachers


#endregion

app.Run();