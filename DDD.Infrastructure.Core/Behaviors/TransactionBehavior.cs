using DDD.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Core.Behaviors
{
    /// <summary>
    /// 事务的具体行为方式
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TDbContext : EFContext
    {
        ILogger _logger;
        TDbContext _dbContext;
        public TransactionBehavior(TDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }


                var strategy = _dbContext.Database.CreateExecutionStrategy();//数据库的策略，比如重试

                await strategy.ExecuteAsync(async () => //执行策略
                {
                    Guid transactionId;
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- 开始事务 {TransactionId} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();//执行后续的事务操作

                        _logger.LogInformation("----- 提交事务 {TransactionId} {CommandName}", transaction.TransactionId, typeName);


                        await _dbContext.CommitTransactionAsync(transaction);//提交事务

                        transactionId = transaction.TransactionId;
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
