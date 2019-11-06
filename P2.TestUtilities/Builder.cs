using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using CloneExtensions;

namespace P2.TestUtilities
{
    public class Builder<T> where T : new()
    {
        private readonly IList<(PropertyInfo, object)> changes;
        private T defaultModel;
        
        public Builder(T defaultModel)
        {
            this.defaultModel = defaultModel;
            changes = new List<(PropertyInfo, object)>();
        }

        public T Build()
        {
            foreach (var (property, value) in changes)
            {
                property.SetValue(defaultModel, value);
            }
            return defaultModel;
        }

        public Builder<T> From(T baseModel)
        {
            defaultModel = baseModel.GetClone();

            return this;
        }

        public Builder<T> With<TProperty>(Expression<Func<T, TProperty>> expression, object value)
        {
            var property = (PropertyInfo)((MemberExpression)expression.Body).Member;

            changes.Add((property, value));
            
            return this;
        }
    }
}
