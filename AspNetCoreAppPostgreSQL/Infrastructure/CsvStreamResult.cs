using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Infrastructure
{
    public class CsvStreamResult<T> : FileResult
    {
        private readonly IEnumerable<T> _items;
        private readonly ClassMap<T> _classMap;
        private readonly string _message;

        /// <inheritdoc />
        public CsvStreamResult(IEnumerable<T> items, ClassMap<T> classMap, string message = null)
            : base("text/csv")
        {
            _items = items;
            _classMap = classMap;
            _message = message;
        }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.SetContentHeaders(this);

            return context.WriteExcelToResponse(_items, _classMap, _message);
        }
    }
}
