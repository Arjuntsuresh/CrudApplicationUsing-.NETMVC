using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SampleCrudApplication.Models;
using SampleCrudApplication.Repository;

namespace SampleCrudApplication.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult GetAccountDetails()
        {
            AccountRepository accountRepository = new AccountRepository();
            ModelState.Clear();
            return View(accountRepository.GetAllDetails());
        }

        //Show adding user view page
        public ActionResult AddAccountDetail()
        {
            return View();
        }
        //creating new user
        [HttpPost]
        public ActionResult AddAccountDetail(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AccountRepository accountRepository = new AccountRepository();
                    if (accountRepository.AddAccountDetails(account))
                    {
                        ViewBag.Message = "Data Inset Successfully.";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Error occurred while inserting!" + ex.Message;
                return View();
            }

        }
        //Edit user get method
        public ActionResult EditAccount(int? id)
        {
            AccountRepository accountRepository = new AccountRepository();
            return View(accountRepository.GetAllDetails().Find(bank => bank.id == id));
        }
        //Edit user post method
        [HttpPost]
        public ActionResult EditAccount(int? id, Account account)
        {
            try
            {
                AccountRepository accountRepository = new AccountRepository();
                accountRepository.EditAccount(account);
                return RedirectToAction("GetAccountDetails");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occurred while editing!" + ex.Message;
                return View();
            }


        }
        public ActionResult Delete(int id)
        {
            try
            {
                AccountRepository accountRepository = new AccountRepository();
                if (accountRepository.DeleteDetails(id))
                {
                    ViewBag.AlertMessage = ("User detail deleted successfully.");
                }
                return RedirectToAction("GetAccountDetails");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occurred while deleting!" + ex.Message;
                return View();
            }

        }

    }
}