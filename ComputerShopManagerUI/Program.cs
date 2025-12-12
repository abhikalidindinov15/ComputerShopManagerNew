var builder = WebApplication.CreateBuilder(args);

// Register HttpClient for Web API calls
builder.Services.AddHttpClient("ComputerShopAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5235"); // Web API URL
});

builder.Services.AddRazorPages();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
