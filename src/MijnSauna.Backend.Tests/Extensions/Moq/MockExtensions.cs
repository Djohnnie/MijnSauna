using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq.Language.Flow;

namespace MijnSauna.Backend.Tests.Extensions.Moq
{
    public static class MockExtensions
    {
        public static IReturnsResult<TMock> ExecutesAsyncPredicateOn<TMock, TResult>(this ISetup<TMock, Task<TResult>> mock, IList<TResult> data) where TMock : class
        {
            return mock.Returns((Expression<Func<TResult, Boolean>> predicate) =>
            {
                var compiledPredicate = predicate.Compile();
                return Task.FromResult(data.SingleOrDefault(compiledPredicate));
            });
        }

        public static IReturnsResult<TMock> ExecutesAsyncPredicateOn<TMock, TResult>(this ISetup<TMock, Task<IList<TResult>>> mock, IList<TResult> data) where TMock : class
        {
            return mock.Returns((Expression<Func<TResult, Boolean>> predicate) =>
            {
                var compiledPredicate = predicate.Compile();
                return Task.FromResult((IList<TResult>)data.Where(compiledPredicate).ToList());
            });
        }
    }
}