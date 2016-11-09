using Nancy;

namespace Coolector.Services.Operations.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "Coolector.Services.Operations" }));
        }
    }
}