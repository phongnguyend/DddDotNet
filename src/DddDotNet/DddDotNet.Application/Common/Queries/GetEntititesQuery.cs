using DddDotNet.Domain.Entities;
using DddDotNet.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Application
{
    public class GetEntititesQuery<TEntity> : IQuery<List<TEntity>>
         where TEntity : AggregateRoot<Guid>
    {
    }

    internal class GetEntititesQueryHandler<TEntity> : IQueryHandler<GetEntititesQuery<TEntity>, List<TEntity>>
    where TEntity : AggregateRoot<Guid>
    {
        private readonly IRepository<TEntity, Guid> _repository;

        public GetEntititesQueryHandler(IRepository<TEntity, Guid> repository)
        {
            _repository = repository;
        }

        public Task<List<TEntity>> HandleAsync(GetEntititesQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            return _repository.ToListAsync(_repository.GetAll());
        }
    }
}
