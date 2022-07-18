using DomainValidation.Application.Behaviors;
using DomainValidation.Application.Commands.Validators;
using DomainValidation.Application.MappingProfiles;
using DomainValidation.Application.Queries;
using DomainValidation.Core.Repositories;
using DomainValidation.Infrastructure.Repositories;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetCustomerByIdQuery).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CreatedValidationBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CreateCustomerCommandValidator).Assembly);

builder.Services.AddAutoMapper(typeof(CustomerMappingProfile).Assembly);
builder.Services.AddSingleton<ICustomersRepository, CustomersRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
