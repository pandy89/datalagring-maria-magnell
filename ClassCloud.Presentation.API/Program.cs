using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.Locations;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Dtos.Teachers;
using ClassCloud.Application.Dtos.CourseSessions;
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
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<LocationService>();


// Repos
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();


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

// Create courseSession

courseSessions.MapPost("/", async (CreateCourseSessionDto dto, CourseSessionService courseSessionService, CancellationToken ct) =>
{
    var result = await courseSessionService.CreateCourseSessionAsync(dto, ct);
    return result.Match(
        coursesession => Results.Created($"/api/coursesession/{coursesession.StartDate}", coursesession),
        errors => errors.ToProblemDetails()
    );
});


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
participant.MapGet("/{email}", async (string Email, ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetOneParticipantAsync(Email, ct);
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

// Update participant
participant.MapPut("/{email}", async (string Email, UpdateParticipantDto dto, ParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.UpdateParticipantAsync(Email, dto, ct);
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
var teacher = app.MapGroup("/api/teacher").WithTags("Teacher");

// Create teacher
teacher.MapPost("/", async (CreateTeacherDto dto, TeacherService teacherService, CancellationToken ct) =>
{
    var result = await teacherService.CreateTeacherAsync(dto, ct);
    return result.Match(
        teacher => Results.Created($"/api/teacher/{teacher.Email}", teacher),
        errors => errors.ToProblemDetails()
    );
});

// Get one teacher by email
teacher.MapGet("/{email}", async (string email, TeacherService teacherService, CancellationToken ct) =>
{
    var result = await teacherService.GetOneTeacherAsync(email, ct);
    return result.Match(
        teacher => Results.Ok(teacher),
        errors => errors.ToProblemDetails()
        );
});

// Get all teacher order by email
teacher.MapGet("/", async (TeacherService teacherService, CancellationToken ct) =>
{
    var result = await teacherService.GetAllTeachersAsync(ct);
    return Results.Ok(result);
});

// Update teacher
teacher.MapPut("/{email}", async (string email, UpdateTeacherDto dto, TeacherService teacherService, CancellationToken ct) =>
{
    var result = await teacherService.UpdateTeacherAsync(email, dto, ct);
    return result.Match(
        teacher => Results.Ok(teacher),
        errors => errors.ToProblemDetails()
        );
});

// Delete teacher with email
teacher.MapDelete("/{email}", async (string Email, TeacherService teacherService, CancellationToken ct) =>
{
    var result = await teacherService.DeleteTeacherAsync(Email, ct);
    return result.Match(
        _ => Results.NoContent(),
        errors => errors.ToProblemDetails()
    );
});

#endregion

#region Location
var locations = app.MapGroup("/api/location").WithTags("Locations");

// Create location
locations.MapPost("/", async (CreateLocationDto dto, LocationService locationService, CancellationToken ct) =>
{
    var result = await locationService.CreateLocationAsync(dto, ct);
    return result.Match(
        location => Results.Created($"/api/location/{location.Name}", location),
        errors => errors.ToProblemDetails()
    );
});

// Get one location
locations.MapGet("/{name}", async (string name, LocationService locationService, CancellationToken ct) =>
{
    var result = await locationService.GetOneLocationAsync(name, ct);
    return result.Match(
        location => Results.Ok(location),
        errors => errors.ToProblemDetails()
        );
});

// Get all locations order by name
locations.MapGet("/", async (LocationService locationService, CancellationToken ct) =>
{
    var result = await locationService.GetAllLocationsAsync(ct);
    return Results.Ok(result);
});

// Update location
locations.MapPut("/{name}", async (string name, UpdateLocationDto dto, LocationService locationService, CancellationToken ct) =>
{
    var result = await locationService.UpdateLocationAsync(name, dto, ct);
    return result.Match(
        location => Results.Ok(location),
        errors => errors.ToProblemDetails()
        );
});

// Delete location with name
locations.MapDelete("/{name}", async (string name, LocationService locationService, CancellationToken ct) =>
{
    var result = await locationService.DeleteLocationAsync(name, ct);
    return result.Match(
        _ => Results.NoContent(),
        errors => errors.ToProblemDetails()
    );
});






#endregion

app.Run();