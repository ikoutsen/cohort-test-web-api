using CohortTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohortTestWebAPI.DAL {
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private OrderContext context;
        private GenericRepository<Order> orderRepository;

        public UnitOfWork(OrderContext context) {
            this.context = context;
        }

        public GenericRepository<Order> Orders {
            get {
                if (this.orderRepository == null)
                    this.orderRepository = new GenericRepository<Order>(context);
                return orderRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposed && disposing)
                context.Dispose();
            disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task SaveAsync() {
            await context.SaveChangesAsync();
        }
    }
}
