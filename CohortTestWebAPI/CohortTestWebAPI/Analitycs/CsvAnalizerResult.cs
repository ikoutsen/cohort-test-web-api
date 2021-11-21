using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CohortTestWebAPI.Analitycs {
    public class CsvAnalizerResult<T> : AnalizerRersult {
        private string resultGrid;
        public CsvAnalizerResult(T[,] grid) {
            resultGrid = ConvertToCsv(grid);
        }
        public override string Data {
            get {
                return resultGrid;
            }
        }
        private string ConvertToCsv(T[,] grid) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < grid.GetLength(0); i++) {
                if (i > 0)
                    stringBuilder.AppendLine();
                string[] row = new string[grid.GetLength(1)];
                for (int j = 0; j < grid.GetLength(1); j++) {
                    row[j] = grid[i, j].ToString();
                }
                stringBuilder.AppendJoin(";", row);
            }
            return stringBuilder.ToString();
        }
    }
}