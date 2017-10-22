namespace Notes.App.Services.Interfaces
{
    public interface IUserService
    {
        bool Exist(string username);

        void Save(string username, string password);
    }
}
