using System;

namespace CohortTestWebAPI.Models {
    public class Order : ModelEntity {
        public string PhoneNumber { get; set; }
        public DateTime DateAdded { get; set; }
    }
}