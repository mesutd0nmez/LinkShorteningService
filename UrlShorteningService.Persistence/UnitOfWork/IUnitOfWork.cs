using System;
namespace UrlShorteningService.Persistence.UnitOfWork
{
	public interface IUnitOfWork
    {
        /// <summary>
        /// Senkron olarak verileri veritabanına yazar.
        /// </summary>
        void Commit();

        /// <summary>
        /// Asenkron olarak verileri veritabanına yazar.
        /// </summary>
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

