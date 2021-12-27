using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Csharp
{
    class Program
    {
        //3) C#
        //Описать объект "Книга" с параметрами(атрибутами) и методами, которые могут быть у книги на ваше усмотрение
        //Предоставить листинг кода + реализовать вызов из консоли отображение всех параметров этого объекта в формате JSON        
        class LabelAttribute : Attribute
        {
            public string LabelText { get; set; }
            public LabelAttribute(string labelText)
            {
                LabelText = labelText;
            }
        }
        //class Library 
        //{
        //    public string Name;
        //    public int Count;
        //    //public List<Book> Catalogue = new List<Book>();

        //    private readonly Book[] books = new[]
        //    {
        //        new Book(1, "And quiet flows the Don", @"https://www.litres.ru/mihail-sholohov/tihiy-don/"),
        //        new Book(2, "The King Must Die", @"https://www.amazon.co.uk/King-Must-Die-Mary-Renault/dp/0099463520"),
        //        new Book(2, "Kill the Mogingbird", @"https://bookscafe.net/read/harper_lee-to_kill_a_mockingbird-147382.html#p1") 
        //    };
        //    public Library()
        //    {
        //        Console.WriteLine("Создание новой библиотеки");
        //        Name = "Noname";
        //        Count = books.Length;
        //    }
        //    public Library(string name) : this()
        //    {
        //        Name = name;
        //        Count = books.Length;
        //    }
        //    public Book[] GetAllById(int id)
        //    {
        //        return books.Where(book => book.Id.Equals(id)).ToArray();
        //    }            
        //    public Book[] GetAllByTitle(string title)
        //    {
        //        return books.Where(book => book.Title.Contains(title)).ToArray();
        //    }
        //}
        //public Library(List<Book> catalogue)
        //    {
        //        Catalogue = catalogue;
        //    }
        //public Catalog() { catalogue = new Catalogue[0]; }

        //public void Add(Tovar t)
        //{
        //    Array.Resize(ref tovar, tovar.Length + 1);
        //    tovar[tovar.Length - 1] = t;
        //}
        public static string MakeIsbn(string isbn) // string must have 9 digits
        {
            if (isbn == null)
                throw new ArgumentNullException();
                        
            isbn = isbn.Normalize();
            if (isbn.Length != 9)
                throw new ArgumentException();

            int result;
            for (int i = 0; i != 9; i++)
                if (!int.TryParse(isbn[i].ToString(), out result))
                    throw new ArgumentException();
            int sum = 0;
            for (int i = 0; i != 9; i++)
                sum += (i + 1) * int.Parse(isbn[i].ToString());

            int remainder = sum % 11;
            if (remainder == 10)
                return isbn + 'X';
            else
                return isbn + (char)('0' + remainder);
        }
        class Book 
        {
            [Label("Название книги")]
            public string Title { get; set; }
            [Label("Автор книги")]
            public string Author { get; set; }
            [Label("Идентификатор книги")]
            public Guid Id { get; set; }
            [Label("Международный стандартный книжный номер")]
            static string Isbn { get; set; }
            [Label("ссылка на книгу")]
            public string Reference { get; set; }
            //int Rating { get; }            
            
            //[Label("Дата издания")]
            //int Released { get; }
            //int Sequel_id { get; }
            //private bool IsRead = false;
            //[Label("Количество страниц")]
            //private int Pages = 1;
            //public void assignId()
            //{
            //    Id = isbn;
            //    isbn++;
            //}

            //public Book(Guid id, string title, string reference)
            //{
            //    Id = id;
            //    //Isbn = isbn;
            //    Title = title;
            //    Reference = reference;                
            //}            

            public Book(string title = "Noname", string author = "Unknown", )
            {
                Title = title;
                Author = author;
                Id = Guid.NewGuid();
            }

            public void GetBookById(int id)
            {
                Console.WriteLine(id);
            }
            public void GetBookByTitle(string title)
            {
                Console.WriteLine(title);
            }
            public void Print()
            {
                Console.WriteLine($"Id: {Id}  Title: {Title}");
            }
        }
        static void PrintAsJson(object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);            
            Console.WriteLine(jsonString);
        }
        static void Main(string[] args)
        {
            var book = new Book("And quiet flows the Don", "Sholohov");
            //var book1 = new Book("And quiet flows the Don", "Sholohov", @"https://www.litres.ru/mihail-sholohov/tihiy-don/");
            //var book2 = new Book(2, "The King Must Die", @"https://www.amazon.co.uk/King-Must-Die-Mary-Renault/dp/0099463520");
            //var book3 = new Book(2, "Kill the Mogingbird", @"https://bookscafe.net/read/harper_lee-to_kill_a_mockingbird-147382.html#p1");
            //MethodInfo method = book1.GetType().GetMethod("Print");
            //method.Invoke(book1, null);
            //Console.WriteLine(book);

            //var lib = new Library();
            //Console.WriteLine(lib);
            //Console.ReadKey();
            //Type type = typeof(Book);
            //Console.WriteLine($"Type: {type}");

            //string jsonString = JsonSerializer.Serialize(book);
            //Console.WriteLine(book.Title);
            //book.Print();
            //Console.WriteLine(jsonString);

            //PrintAsJson(book);

            //Console.WriteLine(book);
            Console.Write("Введите идентификатор книги для отображения параметров в формате JSON: ");
            var idFmConsole = Console.ReadLine();
            Console.WriteLine(idFmConsole.ToString());
            //PrintAsJson(book.Id);
            
            Console.WriteLine("Нажмите любую клaвишу для выхода из программы");
            Console.ReadLine();
        }
    }    
}
