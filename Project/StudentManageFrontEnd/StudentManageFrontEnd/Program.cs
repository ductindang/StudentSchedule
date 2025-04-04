using StudentManageFrontEnd.Services.IServices;
using StudentManageFrontEnd.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//-------

//---Add Http for connection
builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5171/");
});
builder.Services.AddHttpClient<IScheduleService, ScheduleService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5171/");
});
builder.Services.AddHttpClient<ISubjectService, SubjectService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5171/");
});
//---------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// open middleware Session
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
