using System;

namespace AspNetCoreAppPostgreSQL.Models
{
    public class ARR
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата расчета. Для использования месяца/года
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// CFt
        /// Чистый денежный поток месяца t
        /// </summary>
        public double Profit { get; set; }
    }
}
