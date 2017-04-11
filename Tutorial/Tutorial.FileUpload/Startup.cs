using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tutorial.FileUpload.Startup))]
namespace Tutorial.FileUpload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
