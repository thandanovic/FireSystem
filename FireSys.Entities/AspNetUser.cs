namespace FireSys.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class AspNetUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string Discriminator { get; set; }
    }
}
