namespace BookShop
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using BookShop.Models.Enums;

    using Data;

    using Initializer;

    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            var context = new BookShopContext();

           // int year = int.Parse(Console.ReadLine());

           string input = Console.ReadLine();

            // DbInitializer.ResetDatabase(context);
            Console.WriteLine(GetAuthorNamesEndingIn(context,input));
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var restriction = Enum.Parse<AgeRestriction>(command, true);

            var booksTitle = context.Books.Where(x => x.AgeRestriction == restriction).Select(x => x.Title)
                .OrderBy(x => x).AsNoTracking().ToArray();

            return string.Join(Environment.NewLine, booksTitle);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books.Where(x => x.Copies < 5000 && x.EditionType == EditionType.Gold)
                .Select(x => new { x.BookId, x.Title }).OrderBy(x => x.BookId).ToArray();

            var info = new StringBuilder();

            foreach (var book in goldenBooks) info.AppendLine(book.Title);

            return info.ToString().Trim();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Price > 40).OrderByDescending(x => x.Price).ToArray();

            var sb = new StringBuilder();

            foreach (var book in books) sb.AppendLine($"{book.Title} - ${book.Price:f2}");

            return sb.ToString().Trim();
        }


        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {

            var bookNotReleasedInTheGivenYear = context.Books.Where(x => x.ReleaseDate.Value.Year != year)
                .Select(x => new { x.Title, x.BookId }).OrderBy(x => x.BookId).ToArray();

            StringBuilder info = new StringBuilder();

            foreach (var book in bookNotReleasedInTheGivenYear)
            {
                info.AppendLine(book.Title);
            }

            return info.ToString().Trim();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {

            var categories = input.ToLower().Split();

            var bookTitles = context.Books.Where(x => x.BookCategories.Any(x => categories.Contains(x.Category.Name)))
                .Select(x => x.Title).OrderBy(x => x).ToArray();



            StringBuilder info = new StringBuilder();

            foreach (var book in bookTitles)
            {
                info.AppendLine(book);
            }

            return info.ToString().Trim();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime time = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var booksBeforeReleaseDate = context.Books
                .Where(x=>x.ReleaseDate.Value<time)
                .Select(x => new
                                 {
                                     x.Title,
                                     x.EditionType,
                                     x.Price,
                                     x.ReleaseDate
                                     
                                 })
                .OrderByDescending(x => x.ReleaseDate)
                .ToArray();

            StringBuilder info = new StringBuilder();

            foreach (var book in booksBeforeReleaseDate)
            {
                info.AppendLine($"{book.Title} - {book.EditionType} - {book.Price}");
            }

            return info.ToString().Trim();

        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors.Where(x => x.FirstName.EndsWith(input))
                .Select(x => new { FullName = $"{x.FirstName} {x.LastName}" }).OrderBy(x => x.FullName).ToArray();

            StringBuilder info = new StringBuilder();

            foreach (var author in authors)
            {
                info.AppendLine(author.FullName);
            }

            return info.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string[] bookTitles = context.Books
                .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int bookCount = context.Books
                .Where(x => x.Title.Length > lengthCheck)
                .AsNoTracking()
                .ToArray()
                .Length;

            return bookCount;
        }
    }


}