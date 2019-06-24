using DotNetMVC5Demo.App_Start;
using DotNetMVC5Demo.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetMVC5Demo.Controllers
{
    public class UserController : Controller
    {
        MongoContext _dbContext;

        public UserController()
        {
            _dbContext = new MongoContext();
        }

        // GET: User
        public ActionResult Index()
        {
            var users = _dbContext._database.GetCollection<UserModel>("users").FindAll().ToList();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                // TODO: Add insert logic here
                var document = _dbContext._database.GetCollection<BsonDocument>("users");

                var query = Query.And(Query.EQ("Name", user.Name), Query.EQ("Role", user.Role));

                var count = document.FindAs<UserModel>(query).Count();

                if (count == 0)
                {
                    var result = document.Insert(user);
                }
                else
                {
                    TempData["Message"] = "user ALready Exist";
                    return View("Create", user);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
