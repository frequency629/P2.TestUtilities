using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace P2.TestUtilities
{
    public class Builder<T> where T : new()
    {
        private Func<T> baseObjectDelegate;
        private readonly IList<(PropertyInfo, object)> changes;
        
        public Builder(Func<T> baseObjectDelegate)
        {
            this.baseObjectDelegate = baseObjectDelegate;
            changes = new List<(PropertyInfo, object)>();
        }

        public T Build()
        {
            var defaultModel = MakeBaseObject();

            foreach (var (property, value) in changes)
            {
                property.SetValue(defaultModel, value);
            }
            return defaultModel;
        }

        public Builder<T> From(Func<T> baseObjectDelegate)
        {
            this.baseObjectDelegate = baseObjectDelegate;

            return this;
        }

        public Builder<T> With<TProperty>(Expression<Func<T, TProperty>> expression, object value)
        {
            var property = (PropertyInfo)((MemberExpression)expression.Body).Member;

            changes.Add((property, value));
            
            return this;
        }

        private T MakeBaseObject() => baseObjectDelegate.Invoke();

    }
}
