using Blog.Data;
using Blog.Repository;
using Blog.Repository.Implimentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Builder For DB Connection
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
},
    ServiceLifetime.Transient);

//Creating Object of Blog.Repostory(This have to be Created when we willing to use that repostory in Our Project)
builder.Services.AddTransient<IUserAccount, UserAccountRepository>(p => new UserAccountRepository(builder.Services.BuildServiceProvider().GetService<BlogContext>()));
builder.Services.AddTransient<IUser, UserRepository>(p => new UserRepository(builder.Services.BuildServiceProvider().GetService<BlogContext>()));
builder.Services.AddTransient<IPost, PostRepository>(p => new PostRepository(builder.Services.BuildServiceProvider().GetService<BlogContext>()));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

