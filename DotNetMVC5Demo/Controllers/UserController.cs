using DotNetMVC5Demo.App_Start;
using DotNetMVC5Demo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
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
        public ActionResult Details(string id)
        {
            var userId = Query<UserModel>.EQ(x => x._id, new ObjectId(id));
            var user = _dbContext._database.GetCollection<UserModel>("users").FindOne(userId);
            return View(user);
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
        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<UserModel>("users");

            var userDetailscount = document.FindAs<UserModel>(Query.EQ("_id", new ObjectId(id))).Count();

            if (userDetailscount > 0)
            {
                var userObjectId = Query<UserModel>.EQ(p => p._id, new ObjectId(id));

                var userDetail = _dbContext._database.GetCollection<UserModel>("users").FindOne(userObjectId);

                return View(userDetail);
            }
            return RedirectToAction("Index");
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UserModel user)
        {
            try
            {
                user._id = new ObjectId(id);
                //Mongo Query  
                var userObjectId = Query<UserModel>.EQ(p => p._id, new ObjectId(id));
                // Document Collections  
                var collection = _dbContext._database.GetCollection<UserModel>("users");
                // Document Update which need Id and Data to Update  
                var result = collection.Update(userObjectId, Update.Replace(user), UpdateFlags.None);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            var userId = Query<UserModel>.EQ(x => x._id, new ObjectId(id));
            var user = _dbContext._database.GetCollection<UserModel>("users").FindOne(userId);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, UserModel user)
        {
            try
            {
                //Mongo Query  
                var userObjectid = Query<UserModel>.EQ(p => p._id, new ObjectId(id));
                // Document Collections  
                var collection = _dbContext._database.GetCollection<UserModel>("users");
                // Document Delete which need ObjectId to Delete Data   
                var result = collection.Remove(userObjectid, RemoveFlags.Single);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
