using System;
using System.Reflection;
using Core.Domain;

namespace Core.Persistence
{
    public class SanitisedSql<T>
    {
        private readonly string _sqlTemplate;
        private readonly string _orderByTableName;
        private readonly string _orderBy;
        private readonly bool _orderByAscending;
        private readonly Type _objectType;

        public SanitisedSql(string sqlTemplate, string orderBy, bool orderByAscending, string orderByTableName)
        {
            _sqlTemplate = sqlTemplate;
            _orderByTableName = orderByTableName;
            _orderBy = orderBy;
            _orderByAscending = orderByAscending;
            _objectType = typeof(T);
        }

        public string ToSql()
        {
            var orderBySql = GetOrderBySqlFragment();

            return string.Format(_sqlTemplate, orderBySql);
        }

        private string GetOrderBySqlFragment()
        {
            var property = _objectType.GetProperty(_orderBy, BindingFlags.IgnoreCase
                                                             | BindingFlags.Public
                                                             | BindingFlags.FlattenHierarchy
                                                             | BindingFlags.Instance);
            
            if (property == null)
                throw new InvalidOperationException($"Error: Order by {_orderBy} not valid");

            var direction = _orderByAscending ? " ASC" : " DESC";
            if (_orderByTableName == string.Empty)
                return "ORDER BY " + property.Name + direction;
            return "ORDER BY " + _orderByTableName + "." + property.Name.ToSnakeCase() + direction;
        }
    }
}