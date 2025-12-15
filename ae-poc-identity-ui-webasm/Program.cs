using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ae.Poc.Identity.Ui;
using Ae.Poc.Identity.Ui.Extensions;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog();
builder.Logging.AddConfiguration(
  builder.Configuration.GetSection("Logging"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddAppConfiguration(builder.Configuration)
  .AddAppMapper()
  .AddAppServices();

var app = builder.Build();

await app.RunAsync();

