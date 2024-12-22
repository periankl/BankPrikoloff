using BankPrikoloff.Authorization;
using BankPrikoloff.Helpers;
using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using DataAccess.Wrapper;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BankPrikoloff
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BankContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionString"]));
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();
            builder.Services.AddScoped<IAccountJWTService, AccountJWTService >();
            //builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<ITredService, TredService>(); 
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IDepositTypeService, DepositTypeService>();
            builder.Services.AddScoped<IDepositService, DepositService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<ILoanTypeService, LoanTypeService>();
            builder.Services.AddScoped<IOperationHistoryService, OperationHistoryService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMapster();
            builder.Services.AddLogging();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prikoloff API",
                    Description = "API BankPrikoloff",
                    Contact = new OpenApiContact
                    {
                        Name = "Bank Prikoloff",
                        Url = new Uri("https://BankPrikoloff.ru")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "��������",
                        Url = new Uri("https://example.com/license")

                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var sevices = scope.ServiceProvider;

                var context = sevices.GetRequiredService<BankContext>();
                await context.Database.MigrateAsync();

                context.Database.EnsureCreated();

                if(context.AccountStatuses.Count() < 4)
                {
                    context.AccountStatuses.AddRange(
                    new AccountStatus { Name = "ACTIVE" },
                    new AccountStatus { Name = "CLOSED" },
                    new AccountStatus { Name = "BLOCKED" }
                    );
                }

                if(context.AccountTypes.Count() < 2)
                {
                    context.AccountTypes.AddRange(
                    new AccountType { Name = "DEBET" },
                    new AccountType { Name = "LOAN" }
                    );
                }

                if(context.DepositStatuses.Count() < 2)
                {
                    context.DepositStatuses.AddRange(
                    new DepositStatus { Name = "ACTIVE" },
                    new DepositStatus { Name = "CLOSED" },
                    new DepositStatus { Name = "PAUSED" },
                    new DepositStatus { Name = "CANCELED" },
                    new DepositStatus { Name = "EXPIRED" }
                    );
                }

                if(context.DocumentTypes.Count() < 2)
                {
                    context.DocumentTypes.AddRange(
                    new DocumentType { Name = "DEPOSITAGR" },
                    new DocumentType { Name = "LOANAGR" },
                    new DocumentType { Name = "CLIENTAGR" }
                    );
                }

                if(context.LoanStatuses.Count() < 5)
                {
                    context.LoanStatuses.AddRange(
                    new LoanStatus { Name = "ACTIVE" },
                    new LoanStatus { Name = "CLOSED" },
                    new LoanStatus { Name = "PAUSED" },
                    new LoanStatus { Name = "CANCELED" },
                    new LoanStatus { Name = "EXPIRED" }
                    );
                }

                if(context.MessageStatuses.Count() < 3)
                {
                    context.MessageStatuses.AddRange(
                    new MessageStatus { Name = "SENDED" },
                    new MessageStatus { Name = "RECIVED" },
                    new MessageStatus { Name = "READ" }
                    );
                }

                if(context.OperationTypes.Count() < 7)
                {
                    context.OperationTypes.AddRange(
                    new Domain.Models.OperationType { Name = "BTWTHR" },
                    new Domain.Models.OperationType { Name = "TRANS" },
                    new Domain.Models.OperationType { Name = "RECIPT" },
                    new Domain.Models.OperationType { Name = "ADMISSION" },
                    new Domain.Models.OperationType { Name = "PURCHASE" },
                    new Domain.Models.OperationType { Name = "RETURN" }
                    );
                }

                if(context.OperationStatuses.Count() < 4)
                {
                    context.OperationStatuses.AddRange(
                    new OperationStatus { Name = "PROCESSING" },
                    new OperationStatus { Name = "SUCCESSFULL" },
                    new OperationStatus { Name = "CANCELED" },
                    new OperationStatus { Name = "REJECTED" }
                    );
                }

                if(context.Roles.Count() < 3) 
                {
                    context.Roles.AddRange(
                    new Role { RoleName = "USER" },
                    new Role { RoleName = "ADMIN" },
                    new Role { RoleName = "SUPPORT" }
                    );
                }

                context.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder
                .WithOrigins(new[] { "https://bankprikoloff.onrender.com/", "https://bankprikoloffapitest.onrender.com/" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            //CORS для локальной разработки
            /*             app.UseCors(builder => builder.WithOrigins(new[] { "http://localhost:7269/", })
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin());
             */
            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}