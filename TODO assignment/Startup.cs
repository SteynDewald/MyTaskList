using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TODO_assignment.Startup))]
namespace TODO_assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
