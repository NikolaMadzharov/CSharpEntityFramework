namespace MusicHub.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Castle.DynamicProxy.Generators.Emitters;

    [SuppressMessage("ReSharper", "StyleCop.SA1600")]
    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Datetime? 
ReleaseDate { get; set; }

        public decimal Price => this.Songs.Sum(x => x.Price);

        [ForeignKey(nameof(Producer))]
        public int? ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        public ICollection<Song> Songs { get; set; }

    }
}
