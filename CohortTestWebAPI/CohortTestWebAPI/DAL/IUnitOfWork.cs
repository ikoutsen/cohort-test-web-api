﻿using CohortTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohortTestWebAPI.DAL {
    public interface IUnitOfWork {
        public GenericRepository<Order> Orders { get; }
        public Task SaveAsync();
    }
}
