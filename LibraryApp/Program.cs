using LibraryApp.Repositories;
using LibraryApp.Services;
using LibraryApp.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<ProgramController>().Start(args);
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
            {
                services.AddTransient<ProgramController>();

                string booksFilePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\books.json";

                services.AddSingleton<IBookRepository>(new BookJsonRepository(booksFilePath));
                services.AddTransient<IBookService, BookService>();
                services.AddTransient<IArgumentChecker, ArgumentChecker>();
                services.AddTransient<IArgumentParser, ArgumentParser>();
                services.AddTransient<IConsoleWriter, ConsoleWriter>();
                services.AddTransient<IFilteringService, FilteringService>();
                services.AddTransient<IBorrowingService, BorrowingService>();
            });
        }
    }
}
