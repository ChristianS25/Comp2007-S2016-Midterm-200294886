<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_2002948861.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
         <h1>Todos List</h1>
         <br />
        <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Todo</a>
     <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                    ID="GridView1" AutoGenerateColumns="false" DataKeyNames="TodoId"
                    OnRowDeleting="GridView1_RowDeleting" AllowPaging="true" PageSize="10" 
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoName" HeaderText="Todo" Visible="true" SortExpression="TodoName" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="Todo Notes" Visible="true" SortExpression="TodoNotes" />
                        <asp:templatefield HeaderText="Completed?">
                            <itemtemplate>
                                <asp:checkbox ID="Completed" runat="server"></asp:checkbox>
                            </itemtemplate>
                        </asp:templatefield>
                         <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="~/TodoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="TodoId" DataNavigateUrlFormatString="TodoDetails.aspx?TodoId={0}" />
                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
      </div>
</asp:Content>
