using System;
using System.Linq.Expressions;
using Moq;

namespace MijnSauna.Backend.Tests.Extensions.Moq
{
    public static class Any
    {
        public static Expression<Func<T, Boolean>> Predicate<T>()
        {
            return It.IsAny<Expression<Func<T, Boolean>>>();
        }
    }
}