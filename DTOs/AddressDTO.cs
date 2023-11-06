﻿namespace proj_csharp_kiminoyume.DTOs
{
    public class AddressDTO: BaseDTO
    {
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }

        public int PersonId { get; set; }
    }
}
