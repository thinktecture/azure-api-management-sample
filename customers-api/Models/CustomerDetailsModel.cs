using System;

namespace CustomersService.Models
{
    /// <summary>
    /// Detailed customer representation
    /// </summary>
    public class CustomerDetailsModel
    {
        /// <summary>
        /// Unique Identifier (GUID)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name of the customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// City where the customer is located
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Postal code which belongs to the city
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Customer's country of residence
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Creation Timestamp
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }


}
