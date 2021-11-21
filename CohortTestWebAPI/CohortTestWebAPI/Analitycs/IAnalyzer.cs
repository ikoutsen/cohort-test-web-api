using CohortTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohortTestWebAPI.Analitycs {
    public interface IAnalyzer {
        public AnalizerRersult Result { get; }
        public void Proccess();
    }
}
