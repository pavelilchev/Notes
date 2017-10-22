namespace Notes.App.Services
{
    using Notes.App.Services.Interfaces;
    using Notes.Data;
    using Notes.Models;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool Exist(string username)
        {
            using (var ctx = new NotesDbContext())
            {
                return ctx.Users.Any(u => u.Username == username);
            }
        }

        public void Save(string username, string password)
        {
            using (var ctx = new NotesDbContext())
            {
                ctx.Users.Add(new User
                {
                    Username = username,
                    Password = password
                });

                ctx.SaveChanges();
            }
        }
    }
}
