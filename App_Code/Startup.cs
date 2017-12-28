using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamMeyer.Startup))]
namespace ExamMeyer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
