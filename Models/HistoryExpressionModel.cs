using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CalculatorWithHistory.Models
{
    public class HistoryExpressionModel
    {
        [PrimaryKey, AutoIncrement]
        public int ExpressionId { get; set; }
        public string Expression { get; set; }
    }
}
