using COMP2007_S2016_MidTerm_2002948861.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.ModelBinding;
using System.Linq.Dynamic;

//Author: Chritian Simpson
//Student#: 200294886
//Date: 2016-06-23
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

        /* this method handles the delete button click 
        *  event when it is pressed 
        */
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
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"+deletedTodo);
                // remove the selected Todo from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodo();
            }
        }

        /* this method handles the populating the  
        *  grid view with data from the database 
        */
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

        /* this method handles the page index changing on the grid view
        *   
        */
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.GetTodo();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            GridView1.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodo();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < GridView1.Columns.Count - 1; index++)
                    {
                        if (GridView1.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }

    }
}