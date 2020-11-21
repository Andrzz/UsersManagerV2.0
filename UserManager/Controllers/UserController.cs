namespace UserManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using OperationsService.Operations;
    using System;
    using UserManager.Models;

    public class UserController : Controller
    {
        private readonly IOperations _operations;
        private readonly IConfiguration _configuration;
        public UserController(IOperations operations, IConfiguration configuration)
        {
            _operations = operations;
            _configuration = configuration;
        }

        // GET: User
        public IActionResult Index()
        {            
            return View(_operations.GetAllUsersRegistered(_configuration.GetConnectionString("ConnectionString")));
        }

        // GET: User/AddOrEdit/
        public IActionResult AddOrEdit(int? id)
        {
            UserViewModel userViewModel = new UserViewModel();
            if(id > 0)
            {
                userViewModel = FetchUserById((int)id);
            }
            return View(userViewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("UserId,Name,BirthDate,Gender")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _operations.AddOrEdit(_configuration.GetConnectionString("ConnectionString"), userViewModel.Name, userViewModel.BirthDate, userViewModel.Gender, id);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: User/Delete/5
        public IActionResult Delete(int? id)
        {
            UserViewModel viewModel = FetchUserById((int) id);
            return View(viewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _operations.Delete(_configuration.GetConnectionString("ConnectionString"), id);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public UserViewModel FetchUserById(int? id)
        {
            UserViewModel viewModel = new UserViewModel();
            var ddt = _operations.UpdateUser(_configuration.GetConnectionString("ConnectionString"), (int)id);
            if(ddt.Rows.Count == 1)
            {
                viewModel.UserId = Convert.ToInt32(ddt.Rows[0]["USERID"].ToString());
                viewModel.Name = ddt.Rows[0]["USERNAME"].ToString();
                viewModel.BirthDate = Convert.ToDateTime(ddt.Rows[0]["BIRTHDATE"].ToString());
                viewModel.Gender = Convert.ToChar(ddt.Rows[0]["GENDER"].ToString());
            }
            return viewModel;
        }
    }
}
