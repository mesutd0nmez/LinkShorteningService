using System;
using System.Linq.Expressions;

namespace UrlShorteningService.Persistence.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Tüm verileri getir.
        /// </summary>
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        /// <summary>
        /// Belirtilen ifadeye göre filtrele ve verileri getir.
        /// </summary>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// Belirtilen ifadeye göre sırası ile filtrele, sırala, tabloları birleştir ve takip etmeyi kapat ile verileri getir.
        /// </summary>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        /// <summary>
        /// Belirtilen ifadeye göre sırası ile filtrele, sırala, tabloları birleştir ve takip etmeyi kapat ile verileri getir.
        /// </summary>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                       List<Expression<Func<TEntity, object>>> includes = null,
                                       bool disableTracking = true);

        /// <summary>
        /// Belirtilen ifadeye göre filtrele ve verileri çekmeden sorguyu getir.
        /// </summary>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Belirtilen id'ye göre veriyi getir.
        /// </summary>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Yeni kayıt ekle.
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Kayıtı güncelle.
        /// </summary>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Kayıtı sil.
        /// </summary>
        void Delete(TEntity entity);
    }
}

