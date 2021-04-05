using Lab1_WebForm.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab1_WebForm
{
    public partial class _Default : Page
    {
        List<Student> Students = new List<Student>();
        List<Book> Books = new List<Book>();
        List<Loan> Loans = new List<Loan>();

        string StudentsFile = "";
        string BooksFile = "";
        string LoansFile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentsFile = Server.MapPath("Students.txt");
            BooksFile = Server.MapPath("Books.txt");
            LoansFile = Server.MapPath("Loans.txt");

            if (validateFiles())
            {
                using (StreamReader sr = new StreamReader(StudentsFile))
                {
                    Students = JsonConvert.DeserializeObject<List<Student>>(sr.ReadToEnd());
                }
                using (StreamReader sr = new StreamReader(BooksFile))
                {
                    Books = JsonConvert.DeserializeObject<List<Book>>(sr.ReadToEnd());
                }
                using (StreamReader sr = new StreamReader(LoansFile))
                {
                    Loans = JsonConvert.DeserializeObject<List<Loan>>(sr.ReadToEnd());
                }

                if (Students == null)
                {
                    Students = new List<Student>();
                }

                if (Books == null)
                {
                    Books = new List<Book>();
                }
                if (Loans == null)
                {
                    Loans = new List<Loan>();
                }

            }
            else
            {
                File.Create(StudentsFile);
                File.Create(BooksFile);
                File.Create(LoansFile);
            }
            LabelNotReturned.Text = "Not returned: " + Loans.Where(l => l.ReturnDate < DateTime.Now).Count();
            this.GridView1.DataSource = Loans;
        }

        bool validateFiles()
        {
            return File.Exists(LoansFile) &&
                File.Exists(StudentsFile) &&
                File.Exists(BooksFile);
        }

        void SaveAll()
        {
            using (StreamWriter sr = new StreamWriter(StudentsFile))
            {
                sr.Write(JsonConvert.SerializeObject(Students));
            }
            using (StreamWriter sr = new StreamWriter(BooksFile))
            {
                sr.Write(JsonConvert.SerializeObject(Books));
            }
            using (StreamWriter sr = new StreamWriter(LoansFile))
            {
                sr.Write(JsonConvert.SerializeObject(Loans));
            }
            GridView1.DataSource = Loans;
        }

        void CreateLoan(Student student, Book book, DateTime LoanDate, DateTime ReturnDate)
        {
            if (Loans.Where(l => l.BookId == book.Id).Count() < 1)
            {
                Loans.Add(
                    new Loan(
                        student.Id,
                        student.Name,
                        book.Id,
                        book.Title,
                        LoanDate,
                        ReturnDate
                    )
                    );
                SaveAll();
            }
        }

        void CreateStudent(string Id, string Name, string Address)
        {
            this.Students.Add(
                new Student(
                    Id,
                    Name,
                    Address
                    )
                );
            SaveAll();
        }

        void CreateBook(string Id, string Title, string Author, DateTime YearOfPublication)
        {
            this.Books.Add(
                new Book(
                    Id,
                    Title,
                    Author,
                    YearOfPublication
                    )
                );
            SaveAll();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.CreateBook(
                this.TextBookID.Text,
                this.TextTitle.Text,
                this.tbAuthor.Text,
                this.Calendar1.SelectedDate
                );
            TextBookID.Text = "";
            TextTitle.Text = "";
            tbAuthor.Text = "";
            Calendar1.SelectedDate = DateTime.Now;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CreateStudent(this.TextID.Text, this.TextName.Text, this.TextAddress.Text);
            this.TextID.Text = "";
            this.TextName.Text = "";
            this.TextAddress.Text = "";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            CreateLoan(new Student(this.TextStudentID.Text), 
                new Book(TextBook.Text), 
                Calendar2.SelectedDate, 
                Calendar3.SelectedDate);
            this.TextStudentID.Text = "";
            this.TextBook.Text = "";
            Calendar2.SelectedDate = DateTime.Now;
            Calendar3.SelectedDate = DateTime.Now;
        }
    }
}