using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Csharp
{
    class Program
    {
        class LabelAttribute : Attribute
        {
            public string LabelText { get; set; }
            public LabelAttribute(string labelText)
            {
                LabelText = labelText;
            }
        }
        class Book 
        {
            [Label("Название книги")]
            public string Title { get; private set; }
            [Label("Автор книги")]
            public string Author { get; private set; }
            [Label("Идентификатор книги")]
            public Guid Id { get; private set; }
            [Label("Международный стандартный книжный номер")]
            public string isbn;
            public string Isbn 
            {
                get { return isbn; }
                set
                {                    
                    if (!IsValidIsbn(value)) throw new ArgumentException("isbn number must contain 10 digits");
                    isbn = value;
                } 
            }
            [Label("ссылка на книгу")]
            public string Reference { get; set; }
            [Label("Год издания")]
            public int Released { get; set; }

            public Book(string title = "Noname",
                string author = "Unknown",
                string isbn = "007462542X",
                string reference = @"https://books.google.ru/",
                int released = 0)
            {
                Title = title;
                Author = author;
                Id = Guid.NewGuid();
                Isbn = isbn;
                Reference = reference;
                Released = released;
            }

            public void GetIdByTitle(string title)
            {
                Console.WriteLine($"Название книги: {title}  Id: {Id}");
            }
            public void GetBookByTitle(string title)
            {
                Console.WriteLine(title);
            }
            public void Print()
            {
                Console.WriteLine($"Id: {Id}  Title: {Title}");
            }
            public void PrintAsJson()
            {
                string jsonString = JsonSerializer.Serialize(this);
                Console.WriteLine(jsonString);
            }
        }      
        static bool IsValidIsbn(string isbn)
        {
            // length must be 10
            int n = isbn.Length;
            if (n != 10)
                return false;

            // Computing weighted sum of
            // first 9 digits
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = isbn[i] - '0';

                if (0 > digit || 9 < digit)
                    return false;

                sum += (digit * (10 - i));
            }

            // Checking last digit.
            char last = isbn[9];
            if (last != 'X' && (last < '0'
                             || last > '9'))
                return false;

            // If last digit is 'X', add 10
            // to sum, else add its value.
            sum += ((last == 'X') ? 10 :
                              (last - '0'));

            // Return true if weighted sum
            // of digits is divisible by 11.
            return (sum % 11 == 0);
        }
        public static bool InputIsValid(ConsoleKeyInfo input)
        {
            int num;
            if (char.IsDigit(input.KeyChar))
            {
                num = int.Parse(input.KeyChar.ToString()); // use Parse if it's a Digit                
            }
            else
            {                
                num = -1;  // Else we assign a default value
            }

            if(num < 1 || num > 3)
            {
                Console.WriteLine("\nДолжна быть введена цифра от 1 до 3");
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {

            var books = new List<Book>
            {
                new Book("And quiet flows the Don", "Mikhail Sholokhov"),
                new Book("The King Must Die", "Mary Reanult", "0394751043", released: 1958),
                new Book("To Kill a Mogingbird", "Harper Lee", "0060935464", @"https://bookscafe.net/read/harper_lee-to_kill_a_mockingbird-147382.html#p1", 1960)
            };

            foreach (var book in books)            
                Console.WriteLine(book.Title);
            
            Console.WriteLine("\nВведите номер книги от 1 до 3, для отображения всех параметров книги в формате JSON");            
            ConsoleKeyInfo userInput = Console.ReadKey(); // Get user input                                   
            
            while(userInput.Key != ConsoleKey.Escape)
            {
                if (InputIsValid(userInput))
                {
                    int index = int.Parse(userInput.KeyChar.ToString()) - 1;
                    Console.WriteLine("\n");
                    books[index].PrintAsJson();
                    Console.WriteLine("Нажмите escape для выхода");
                }
                userInput = Console.ReadKey();
            }            
        }
    }    
}
