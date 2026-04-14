using Catalog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Contracts
{
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest
    {
        Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
