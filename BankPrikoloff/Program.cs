using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using DataAccess.Wrapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BankPrikoloff
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BankContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionString"]));
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
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prikoloff API",
                    Description = "API BankPrikoloff",
                    Contact = new OpenApiContact
                    {
                        Name = "�������",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "��������",
                        Url = new Uri("https://example.com/license")

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
                context.Database.Migrate();

                context.Database.EnsureCreated();

                context.AccountStatuses.AddRange(
                    new AccountStatus { Name = "ACTIVE" },
                    new AccountStatus { Name = "CLOSED" },
                    new AccountStatus { Name = "BLOCKED" }
                );

                context.AccountTypes.AddRange(
                    new AccountType {Name = "DEBET"},
                    new AccountType {Name = "LOAN"}
                );
                
                context.DepositStatuses.AddRange(
                    new DepositStatus { Name = "ACTIVE" },
                    new DepositStatus { Name = "CLOSED" },
                    new DepositStatus { Name = "PAUSED" },
                    new DepositStatus { Name = "CANCELED" },
                    new DepositStatus { Name = "EXPIRED" }
                );

                context.DocumentTypes.AddRange(
                    new DocumentType { Name = "DEPOSITAGR" },
                    new DocumentType { Name = "LOANAGR" },
                    new DocumentType { Name = "CLIENTAGR" }
                );

                context.LoanStatuses.AddRange(
                    new LoanStatus { Name = "ACTIVE" },
                    new LoanStatus { Name = "CLOSED" },
                    new LoanStatus { Name = "PAUSED" },
                    new LoanStatus { Name = "CANCELED" },
                    new LoanStatus { Name = "EXPIRED" }
                );

                context.MessageStatuses.AddRange(
                    new MessageStatus { Name = "SENDED" },
                    new MessageStatus { Name = "RECIVED" },
                    new MessageStatus { Name = "READ" }
                );

                context.OperationTypes.AddRange(
                    new Domain.Models.OperationType { Name = "BTWTHR" },
                    new Domain.Models.OperationType { Name = "TRANS" },
                    new Domain.Models.OperationType { Name = "RECIPT" },
                    new Domain.Models.OperationType { Name = "ADMISSION" },
                    new Domain.Models.OperationType { Name = "PURCHASE" },
                    new Domain.Models.OperationType { Name = "RETURN" }

                );

                context.OperationStatuses.AddRange(
                    new OperationStatus { Name = "PROCESSING" },
                    new OperationStatus { Name = "SUCCESSFULL" },
                    new OperationStatus { Name = "CANCELED" },
                    new OperationStatus { Name = "REJECTED" }

                );
                context.Roles.AddRange(
                    new Role { RoleName = "USER" },
                    new Role { RoleName = "ADMIN" },
                    new Role { RoleName = "SUPPORT" }
                );

                context.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder.WithOrigins(new[] { "https://localhost:7029", }).AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}