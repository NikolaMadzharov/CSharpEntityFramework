namespace MusicHub.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class SongPerformer
    {
        
        public int SongId { get; set; }

        public virtual Song Song { get; set; }

        public int PerformerId { get; set; }

        public virtual Performer Performer { get; set; }

        
    }
}