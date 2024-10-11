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
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}