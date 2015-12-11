using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmashNetwork.Areas.Develop.Models;
using SmashNetwork.DAL;

namespace SmashNetwork.Areas.Develop.Controllers
{
    public class DevelopController : Controller
    {
        SmashNetworkDBContext db = new SmashNetworkDBContext();

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            DataViewModel model = new DataViewModel();
            model.tableName = "Users";
            model.table = db.getTableFromParameters(model.columnList, model.tableName);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DataViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.table = db.getTableFromParameters(model.columnList, model.tableName);
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create(string tableName = "")
        {
            DataViewModel model = new DataViewModel();
            model.tableName = tableName;
            string statement = "SELECT column_name, data_type "
                               + "FROM information_schema.columns"
                               + " WHERE table_name = '" + tableName + "'";

            model.table = db.getTableFromStatement(statement);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formCollection, string tableName = "")
        {
            DataViewModel model = new DataViewModel();
            model.tableName = tableName;

            string statement = "INSERT INTO " + tableName + " (";

            // Assuming [0] is the __RequestVerificationToken
            for (int i = 1; i < formCollection.AllKeys.Count(); i++)
            {
                statement += formCollection.AllKeys[i];
                if (i < formCollection.AllKeys.Count() - 1)
                {
                    statement += ", ";
                }
            }
            statement += ") VALUES (";

            // Assuming [0] is the __RequestVerificationToken
            for (int i = 1; i < formCollection.AllKeys.Count(); i++)
            {
                statement += "'" + formCollection[formCollection.AllKeys[i]] + "'";
                if (i < formCollection.AllKeys.Count() - 1)
                {
                    statement += ", ";
                }
            }
            statement += ")";
            db.executeStatement(statement);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(string tableName = "", string id = "")
        {
            DataViewModel model = new DataViewModel();
            model.tableName = tableName;
            model.id = id;
            string statement = "SELECT column_name, data_type "
                               + "FROM information_schema.columns"
                               + " WHERE table_name = '" + tableName + "'";
            model.table = db.getTableFromStatement(statement);

            statement = "SELECT * FROM " + tableName + " WHERE ID = " + id;
            model.valuesTable = db.getTableFromStatement(statement);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection, string tableName = "", string id = "")
        {
            DataViewModel model = new DataViewModel();
            model.tableName = tableName;
            model.id = id;
            string statement = "UPDATE " + tableName + " SET ";

            // Assuming [0] is the __RequestVerificationToken
            for (int i = 1; i < formCollection.AllKeys.Count(); i++)
            {
                statement += formCollection.AllKeys[i] + "=";
                statement += "'" + formCollection[formCollection.AllKeys[i]] + "'";
                if (i < formCollection.AllKeys.Count() - 1)
                {
                    statement += ", ";
                }
            }
            statement += " WHERE ID = " + id;
            db.executeStatement(statement);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(string tableName = "", string id = "")
        {
            DataViewModel model = new DataViewModel();
            model.tableName = tableName;
            model.id = id;
            string statement = "SELECT column_name, data_type "
                               + "FROM information_schema.columns"
                               + " WHERE table_name = '" + tableName + "'";
            model.table = db.getTableFromStatement(statement);

            statement = "SELECT * FROM " + tableName + " WHERE ID = " + id;
            model.valuesTable = db.getTableFromStatement(statement);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection formCollection, string tableName = "", string id = "")
        {
            DataViewModel model = new DataViewModel();
            model.tableName = tableName;
            model.id = id;
            string statement = "DELETE FROM " + tableName + " WHERE ID = " + id;
            db.executeStatement(statement);

            return View(model);
        }
    }
}
