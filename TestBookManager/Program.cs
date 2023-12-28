using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace TestBookManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var books = new List<Book>
            {
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "Harper Lee", Title = "To Kill a Mockingbird", EntryDate = "2023-01-10", BorrowCount = 5, Borrower = "John Doe", BorrowDate = "2023-11-15" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "George Orwell", Title = "1984", EntryDate = "2023-02-20", BorrowCount = 8, Borrower = "Jane Smith", BorrowDate = "2023-12-01" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "J.K. Rowling", Title = "Harry Potter and the Sorcerer's Stone", EntryDate = "2023-03-15", BorrowCount = 12, Borrower = "Alice Johnson", BorrowDate = "2023-10-21" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "J.R.R. Tolkien", Title = "The Lord of the Rings", EntryDate = "2023-04-05", BorrowCount = 7, Borrower = "Bob Brown", BorrowDate = "2023-09-18" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "F. Scott Fitzgerald", Title = "The Great Gatsby", EntryDate = "2023-05-25", BorrowCount = 3, Borrower = "Clara Davis", BorrowDate = "2023-08-30" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "Mary Shelley", Title = "Frankenstein", EntryDate = "2023-06-10", BorrowCount = 6, Borrower = "Derek Wilson", BorrowDate = "2023-07-22" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "Herman Melville", Title = "Moby Dick", EntryDate = "2023-07-15", BorrowCount = 4, Borrower = "Eva Martin", BorrowDate = "2023-06-11" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "Jane Austen", Title = "Pride and Prejudice", EntryDate = "2023-08-20", BorrowCount = 10, Borrower = "Frank Garcia", BorrowDate = "2023-05-19" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "Leo Tolstoy", Title = "War and Peace", EntryDate = "2023-09-30", BorrowCount = 2, Borrower = "Grace Lee", BorrowDate = "2023-04-27" },
                    new Book { ISBM = Guid.NewGuid(), AuthorName = "Mark Twain", Title = "The Adventures of Huckleberry Finn", EntryDate = "2023-10-12", BorrowCount = 9, Borrower = "Henry Rodriguez", BorrowDate = "2023-03-13" }
            };

            var filePath = "C:\\Noah.DangDocument\\ExportBooks\\Books.xlsx";
            //ExportBooksToExcel(books, filePath);

            var list = LoadBooksFromExcel(filePath);

            PrintBooks(list);

        }
        public static void ExportBooksToExcel(List<Book> books, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var fileInfo = new FileInfo(filePath);

            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add("Books");

                var headers = new string[] { "ISBM", "Author Name", "Title", "Entry Date", "Borrow Count", "Borrower", "Borrow Date" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true; // Đặt in đậm cho tiêu đề
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa tiêu đề
                    worksheet.Cells[1, i + 1].Style.Font.Size = 12; // Cỡ chữ cho tiêu đề
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray); // Màu nền cho tiêu đề
                }

                var row = 2;
                foreach (var book in books)
                {
                    worksheet.Cells[row, 1].Value = book.ISBM.ToString();
                    worksheet.Cells[row, 2].Value = book.AuthorName;
                    worksheet.Cells[row, 3].Value = book.Title;
                    worksheet.Cells[row, 4].Value = book.EntryDate;
                    worksheet.Cells[row, 5].Value = book.BorrowCount;
                    worksheet.Cells[row, 6].Value = book.Borrower;
                    worksheet.Cells[row, 7].Value = book.BorrowDate;

                    for (int col = 1; col <= headers.Length; col++)
                    {
                        worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa nội dung
                    }

                    row++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
        }

        public static List<Book> LoadBooksFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var books = new List<Book>();
            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File không tồn tại.", filePath);
            }

            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Books"];
                if (worksheet == null)
                {
                    throw new Exception("Không tìm thấy trang tính 'Books' trong file.");
                }

                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Bắt đầu từ hàng thứ 2 để bỏ qua tiêu đề
                {
                    var book = new Book
                    {
                        ISBM = Guid.Parse(worksheet.Cells[row, 1].Text),
                        AuthorName = worksheet.Cells[row, 2].Text,
                        Title = worksheet.Cells[row, 3].Text,
                        EntryDate = worksheet.Cells[row, 4].Text,
                        BorrowCount = int.Parse(worksheet.Cells[row, 5].Text),
                        Borrower = worksheet.Cells[row, 6].Text,
                        BorrowDate = worksheet.Cells[row, 7].Text
                    };

                    books.Add(book);
                }
            }

            return books;
        }

        public static void PrintBooks(List<Book> books)
        {
            Console.WriteLine("Danh Sách Sách trong Thư Viện");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"ISBM",-40} {"Tác giả",-20} {"Tên Sách",-30} {"Ngày Nhập",-15} {"Số lần mượn",-15} {"Người Mượn",-15} {"Ngày Mượn",-15}");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

            foreach (var book in books)
            {
                Console.WriteLine($"{book.ISBM,-40} {Truncate(book.AuthorName, 18),-20} {Truncate(book.Title, 28),-30} {book.EntryDate,-15} {book.BorrowCount,-15} {Truncate(book.Borrower, 13),-15} {book.BorrowDate,-15}");
            }
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
    }
}


    public class Book
    {
        public Guid ISBM { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string EntryDate { get; set; }
        public int BorrowCount { get; set; }
        public string Borrower { get; set; }
        public string BorrowDate { get; set; }
    }
