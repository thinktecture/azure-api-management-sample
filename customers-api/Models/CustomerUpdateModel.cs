using System.ComponentModel.DataAnnotations;

namespace CustomersService.Models
{
    /// <summary>
    /// Contract to submit customer modifications
    /// </summary>
    public class CustomerUpdateModel
    {
        /// <summary>
        /// First name of the customer
        /// </summary>
        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string FirstName { get; set; }


        /// <summary>
        /// Last name of the customer
        /// </summary>
        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string LastName { get; set; }

        /// <summary>
        /// City where the customer is located
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string City { get; set; }


        /// <summary>
        /// Postal code of customer's city
        /// </summary>
        [MinLength(3)]
        [MaxLength(7)]
        public string Zip { get; set; }


        /// <summary>
        /// Customer's country of residence
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Country { get; set; }
    }


}
