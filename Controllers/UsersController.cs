using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Users.Models;
using Users.ViewModels;

namespace Users.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User blockedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (blockedUser != null)
            {
                if (blockedUser.LockoutEnd != null && blockedUser.LockoutEnd > DateTime.Now)
                {
                    blockedUser.LastLoginDate = DateTimeOffset.Now;
                    blockedUser.UserStatus = "Blocked";
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                await _userManager.UpdateAsync(user);
            }

            //If you delete yourself
            if (user == blockedUser)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Block(string id)
        {
            User blockedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (blockedUser != null)
            {
                if (blockedUser.LockoutEnd != null && blockedUser.LockoutEnd > DateTime.Now)
                {
                    blockedUser.LastLoginDate = DateTimeOffset.Now;
                    blockedUser.UserStatus = "Blocked";
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                user.UserStatus = "Blocked";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");   
        }


        [HttpPost]
        public async Task<ActionResult> UnBlock(string id)
        {
            User blockedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (blockedUser != null)
            {
                if (blockedUser.LockoutEnd != null && blockedUser.LockoutEnd > DateTime.Now)
                {
                    blockedUser.LastLoginDate = DateTimeOffset.Now;
                    blockedUser.UserStatus = "Blocked";
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now;
                user.UserStatus = "Ublocked";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUsers()
        {
            User blockedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (blockedUser != null)
            {
                if (blockedUser.LockoutEnd != null && blockedUser.LockoutEnd > DateTime.Now)
                {
                    blockedUser.LastLoginDate = DateTimeOffset.Now;
                    blockedUser.UserStatus = "Blocked";
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> BlockUsers()
        {
            User blockedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (blockedUser != null)
            {
                if (blockedUser.LockoutEnd != null && blockedUser.LockoutEnd > DateTime.Now)
                {
                    blockedUser.LastLoginDate = DateTimeOffset.Now;
                    blockedUser.UserStatus = "Blocked";
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                user.UserStatus = "Blocked";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UnBlockUsers()
        {
            User blockedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (blockedUser != null)
            {
                if (blockedUser.LockoutEnd != null && blockedUser.LockoutEnd > DateTime.Now)
                {
                    blockedUser.LastLoginDate = DateTimeOffset.Now;
                    blockedUser.UserStatus = "Blocked";
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.LockoutEnd = DateTime.Now;
                user.UserStatus = "Unblocked";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

    }
}
