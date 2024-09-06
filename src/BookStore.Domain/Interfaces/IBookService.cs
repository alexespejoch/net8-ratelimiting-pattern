using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll(CancellationToken cancellationToken = default);
        Task<Book> GetById(int id, CancellationToken cancellationToken = default);
        Task<Book> Add(Book book, CancellationToken cancellationToken = default);
        Task<Book> Update(Book book, CancellationToken cancellationToken = default);
        Task<bool> Remove(Book book, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetBooksByCategory(int categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> Search(string bookName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue, CancellationToken cancellationToken = default);
    }
}