using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KerryShaleFanPage.Shared.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TEntity? GetLast();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity? GetById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity?> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
