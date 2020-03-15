using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Infrastructure
{
    internal static class CsvExportExtensions
    {
        private static void WriteExcelSeparator(this CsvWriter writer)
        {
            var textWriter = writer.Context.Writer;
            textWriter.WriteLine($"sep={writer.Configuration.Delimiter}");
        }

        public static void SetContentHeaders(this ActionContext context, FileResult result)
        {
            context.HttpContext.Response.ContentType = result.ContentType;

            if (!string.IsNullOrEmpty(result.FileDownloadName))
            {
                var dispositionHeaderValue = new ContentDispositionHeaderValue("attachment")
                {
                    FileNameStar = result.FileDownloadName
                };
                context.HttpContext.Response.Headers["Content-Disposition"] = dispositionHeaderValue.ToString();
            }
        }

        public static async Task WriteExcelToResponse<T>(this ActionContext context, IEnumerable<T> items, ClassMap<T> classMap, string message = null)
        {
            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                ShouldSkipRecord = record => record.All(string.IsNullOrEmpty),
                MissingFieldFound = null,
                Delimiter = ","
            };

            config.RegisterClassMap(classMap);

            using (var streamWriter = new StreamWriter(context.HttpContext.Response.Body))
            {
                using (var csvWriter = new CsvWriter(streamWriter, config))
                {
                    csvWriter.WriteExcelSeparator();
                    csvWriter.WriteRecords(items);

                    csvWriter.WriteField(message);
                }
            }
        }
    }
}
