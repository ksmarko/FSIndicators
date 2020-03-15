using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAppPostgreSQL.Models
{
    public class Investment
    {

        /// <summary>
        /// Investments
        /// Начальные инвестиции
        /// </summary>
        [Key]
        public double Investments { get; set; }
    }
}
