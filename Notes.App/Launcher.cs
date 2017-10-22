namespace Notes.App
{
    using Notes.Data;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main()
        {
            var server = new WebServer(8000, new ControllerRouter(), new ResourceRouter());

            try
            {
                using (var ctx = new NotesDbContext()) { ctx.Database.EnsureCreated(); }
            }
            catch (System.Exception e)
            {

                System.Console.WriteLine(e);
            }
            

            MvcEngine.Run(server);
        }
    }
}
