using COMP2007_S2016_MidTerm_2002948861.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Author: Chritian Simpson
//Student#: 200294886
//Date: 2016-06-23
//Function: this will be a form that allows users to add or edit data from the databse

namespace COMP2007_S2016_MidTerm_2002948861
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /* this method handles the cancel button click 
        *  event when it is pressed 
        */
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to the Default page
            Response.Redirect("~/Todolist.aspx");
        }


        /* this method handles the save button click 
        *  event when it is pressed 
        */
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // use the Todo model to create a new todo object and
                // save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a TodoID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current student from EF DB
                    newTodo = (from todo in db.Todos
                                  where todo.TodoID == TodoID
                                  select todo).FirstOrDefault();
                }

                // add form data to the new todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;

                // use LINQ to ADO.NET to add / insert new todo into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated todo list page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}