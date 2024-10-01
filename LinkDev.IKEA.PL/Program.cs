using Link.Dev.IKEA.BLL.Services.Departments;
using Link.Dev.IKEA.DAL.Data;
using Link.Dev.IKEA.DAL.Persistence.Repositories.Departments;
using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Servcies.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.EntityFrameworkCore;
using LinkDev.IKEA.BLL.Common.Services.Attachments;
using Microsoft.AspNetCore.Identity;
using LinkDev.IKEA.DAL.Entites.Identity;


namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
			//
			//builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IDepratmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployesService, EmployeeService>();

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            builder.Services.AddAutoMapper(m=> m.AddProfile(new MappingProfile()));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) => {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

            }).AddEntityFrameworkStores<ApplicationDbContext>();


            // configyration on cookeis
            builder.Services.ConfigureApplicationCookie(options => {

                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);

                options.LogoutPath = "/Account/SignIn";

			});


            builder.Services.AddAuthentication(options => {

                options.DefaultAuthenticateScheme = "Identity.Application";
                options.DefaultChallengeScheme = "Identity.Application";


			}).AddCookie("Hamda",".AsodotNet.Hamada",options => {

				options.LoginPath = "/Account/LogIn";
				options.AccessDeniedPath = "/Home/Error";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);

				options.LogoutPath = "/Account/SignIn";
			}) ;

			 var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
