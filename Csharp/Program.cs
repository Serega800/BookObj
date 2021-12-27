using System;
using System.Collections.Generic;
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
        class Library
        {
            public string Name;
            public List<Book> Catalogue = new List<Book>();
            
            public Library(List<Book> catalogue)
            {
                Catalogue = catalogue;
            }
            //public Catalog() { catalogue = new Catalogue[0]; }

            public void Add(Tovar t)
            {
                Array.Resize(ref tovar, tovar.Length + 1);
                tovar[tovar.Length - 1] = t;
            }

            public void PrintLib()
            {
                foreach (Book lib in Catalogue)
                    lib.Print();
            }
        }
        class Book : Library
        {
            [Label("Идентификатор книги")]
            public int Id { get; }
            [Label("Международный стандартный книжный номер")]
            static string Isbn { get; set; }
            [Label("Название книги")]
            public string Title { get; }
            public string Reference { get; set; }
            //int Rating { get; }
            [Label("Автор книги")]
            public string Author { get; }
            [Label("Дата издания")]
            int Released { get; }
            int Sequel_id { get; }
            private bool IsRead = false;
            [Label("Количество страниц")]
            private int Pages = 1;
            //public void assignId()
            //{
            //    Id = isbn;
            //    isbn++;
            //}

            public Book(int id, string title, string reference)
            {
                Id = id;
                //Isbn = isbn;
                Title = title;
                Reference = reference;                
            }
            public void GetBookByTitle(string title)
            {
                Console.WriteLine(title);
            }
            public void Print()
            {
                Console.WriteLine($"Id: {Id}  Title: {Title}");
            }
            public void Test()
            {
                Console.WriteLine("Write test");
            }
        }
        static void PrintAsJson(object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);            
            Console.WriteLine(jsonString);
        }
        static void Main(string[] args)
        {
            var book = new Book(1, "And quiet flows the Don", @"https://www.litres.ru/mihail-sholohov/tihiy-don/");
            var book1 = new Book(2, "The King Must Die", @"https://www.amazon.co.uk/King-Must-Die-Mary-Renault/dp/0099463520");
            var book2 = new Book(2, "Kill the Mogingbird", @"https://bookscafe.net/read/harper_lee-to_kill_a_mockingbird-147382.html#p1");
            MethodInfo method = book1.GetType().GetMethod("Print");
            method.Invoke(book1, null);

            Console.ReadKey();
            Type type = typeof(Book);
            Console.WriteLine($"Type: {type}");

            //string jsonString = JsonSerializer.Serialize(book);
            //Console.WriteLine(book.Title);
            //book.Print();
            //Console.WriteLine(jsonString);

            PrintAsJson(book);

            //Console.WriteLine(book);
            Console.Write("Введите идентификатор книги для отображения параметров в формате JSON: ");
            var idFmConsole = Console.ReadLine();
            Console.WriteLine(idFmConsole.ToString());
            PrintAsJson(book.Id);
            
            Console.WriteLine("Нажмите любую клaвишу для выхода из программы");
            Console.ReadLine();
        }
    }    
}
