using CohortTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohortTestWebAPI.Analitycs {
    public class CohortAnalyzer<T> : IAnalyzer {
        IEnumerable<Order> orderList;
        int[,] grid;
        public CohortAnalyzer(IEnumerable<Order> modelEntities) {
            this.orderList = modelEntities;
        }
        public AnalizerRersult Result {
            get {
                if (grid == null)
                    throw new NotImplementedException();
                return new CsvAnalizerResult<int>(grid);
            }
        }
        public void Proccess() {
            Order order1 = orderList.First();
            string phoneNumberCursor = orderList.First().PhoneNumber;
            int rowCursor = orderList.First().DateAdded.Month - 1;
            int[] rowOfUniquePhoneNumbers = new int[12];
            grid = new int[12, 12];
            foreach (var order in orderList) {
                int monthNumber = order.DateAdded.Month - 1;
                if (phoneNumberCursor == order.PhoneNumber) {
                    if (rowOfUniquePhoneNumbers[monthNumber] == 1)
                        continue;
                }
                else {
                    for (int i = 0; i < rowOfUniquePhoneNumbers.Length; i++) {
                        rowOfUniquePhoneNumbers[i] = 0;
                    }
                    rowOfUniquePhoneNumbers[monthNumber] = 1;
                    phoneNumberCursor = order.PhoneNumber;
                    rowCursor = order.DateAdded.Month - 1;

                }
                grid[rowCursor, monthNumber]++;
                rowOfUniquePhoneNumbers[monthNumber] = 1;
            }
            //foreach (var order in orderList) {
            //    int monthNumber = order.DateAdded.Month - 1;
            //    if (phoneNumberCursor == order.PhoneNumber) {
            //        if (rowOfUniquePhoneNumbers[monthNumber] == 0)
            //            rowOfUniquePhoneNumbers[monthNumber] = 1;
            //        else
            //            continue;
            //    }
            //    else {
            //        for (int i = 0; i < rowOfUniquePhoneNumbers.Length; i++) {
            //            rowOfUniquePhoneNumbers[i] = 0;
            //        }
            //        phoneNumberCursor = order.PhoneNumber;
            //        rowCursor = order.DateAdded.Month - 1;

            //    }
            //    grid[rowCursor, monthNumber] ++;
            //}
        }
    }
}