using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        private readonly OrderContext _dbContext;
        private readonly ILogger<RepositoryBase<T>> _logger;

        public RepositoryBase(OrderContext dbContext, ILogger<RepositoryBase<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}
