namespace Notes.App.Controllers
{
    using Notes.App.Models.ViewModels;
    using Notes.App.Services;
    using Notes.App.Services.Interfaces;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Contracts;
    using SimpleMvc.Framework.Controllers;

    public class UserController :Controller
    {
        private readonly IUserService userService;

        public UserController()
        {
            this.userService = new UserService();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!IsValidModel(model))
            {               
                return View();
            }

            if (userService.Exist(model.Username))
            {
                this.Model.Errors.Add("Username is taken");
                return View();
            }

            userService.Save(model.Username, model.Password);
            this.SignIn(model.Username);

            return RedirectToAction("/home/index");
        }
    }
}
