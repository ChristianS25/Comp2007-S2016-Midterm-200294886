using COMP2007_S2016_MidTerm_2002948861.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Author: Chritian Simpson
//Function: this will be a list of all the tasks that are to be done in a grid view

namespace COMP2007_S2016_MidTerm_2002948861
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get the todo data
                this.GetTodo();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected TodoID using the Grid's DataKey collection
            int TODOID = Convert.ToInt32(GridView1.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected student in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the Todo class and store the query string inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                          where todoRecords.TodoID == TODOID
                                          select todoRecords ).FirstOrDefault();
                // remove the selected Todo from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodo();
            }
        }

        protected void GetTodo()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                // query the Todo Table using EF and LINQ
                var Todo = (from allTodos in db.Todos
                             select allTodos);

                // bind the result to the GridView
                GridView1.DataSource = Todo.AsQueryable().ToList();
                GridView1.DataBind();


            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.GetTodo();
        }

    }
}