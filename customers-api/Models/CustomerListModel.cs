using System;

namespace CustomersService.Models
{
    /// <summary>
    /// Representation of a customer used for lists and tables
    /// </summary>
    public class CustomerListModel
    {
        /// <summary>
        /// Unique Identifier (GUID)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Full name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer's country of residence
        /// </summary>
        public string Country { get; set; }
    }


}
