namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Schema;

    using Data;
    using Initializer;

    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context.Album.Where(x => x.ProducerId.Value == producerId).Include(x => x.ProducerId)
                .Include(x => x.Songs).ThenInclude(x => x.Writer).ToArray().Select(
                    x => new
                    {
                        AlbumName = x.Name,
                        AlbumReleaseDate = $"{x.ReleaseDate:MM/dd/yyyy}",
                        ProducerName = x.Producer.Name,
                        Songs = x.Songs
                                     .Select(
                                         s => new
                                         {
                                             SongName = s.Name,
                                             SongPrice = s.Price,
                                             SongWriterName = s.Writer.Name
                                         }).OrderByDescending(x => x.SongName).ThenBy(x => x.SongWriterName)
                                     .ToArray(),
                        TotalPrice = x.Price
                    }).OrderByDescending(x => x.TotalPrice).ToArray();


            StringBuilder output = new StringBuilder();
            foreach (var album in albumInfo)
            {
                output.AppendLine($"-AlbumName: {album.AlbumName}");
                output.AppendLine($"-ReleaseDate: {album.AlbumReleaseDate}");
                output.AppendLine($"-ProducerName: {album.ProducerName}");
                output.AppendLine("-Songs:");

                int counter = 0;
                foreach (var song in album.Songs)
                {
                    output.AppendLine($"---#{++counter}");
                    output.AppendLine($"---SongName: {song.SongName}");
                    output.AppendLine($"---Price: {song.SongPrice:f2}");
                    output.AppendLine($"---Writer: {song.SongWriterName}");
                }

                output.AppendLine($"-AlbumPrice: {album.TotalPrice:f2}");
            }

            return output.ToString().Trim();

        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Include(x => x.SongPerformers)
                .ThenInclude(x => x.Performer)
                .Include(x => x.Writer)
                .Include(x => x.Album)
                .ThenInclude(x => x.Producer)
                .ToArray()
                .Where(x => x.Duration.TotalSeconds > duration)
                .Select(x => new
                                 {
                                     x.Name,
                                     PerformerName = x.SongPerformers
                                         .Select(x => $"{x.Performer.FirstName} {x.Performer.LastName}")
                                         .FirstOrDefault(),
                                     WriterName = x.Writer.Name,
                                     AlbumProducerName = x.Album.Producer.Name,
                                     Duration = x.Duration.ToString("c")
                                 })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.WriterName)
                .ThenBy(x => x.PerformerName)
                .ToArray();

            StringBuilder output = new StringBuilder();
            int counter = 0;
            foreach (var song in songs)
            {
                output.AppendLine($"-Song #{++counter}")
                    .AppendLine($"---SongName: {song.Name}")
                    .AppendLine($"---Writer: {song.WriterName}")
                    .AppendLine($"---Performer: {song.PerformerName}")
                    .AppendLine($"---AlbumProducer: {song.AlbumProducerName}")
                    .AppendLine($"---Duration: {song.Duration}");
            }

            return output.ToString().Trim();
        }
    }
}
