namespace Delivery.Domain.Entities
{
    public class Customer
    {
        /// <summary>
        /// Customer's name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Customer's e-mail
        /// </summary>
        public required string Email { get; set; }

    }

}
