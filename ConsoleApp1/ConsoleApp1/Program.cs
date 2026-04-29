using System;
using System.Collections.Generic;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        string title = "Bohemian Rhapsody";
        string artist = "Queen";
        string album = "A Night at the Opera";
        int durationSeconds = 355;
        string genre = "Rock";
        int bitrate = 320;

        Song s = new Song(title, "Queen", 1975, artist, album, durationSeconds, genre, bitrate);
        s.print();
        s.Play();
        s.Pause();
        s.GetQualityInfo();

        Console.WriteLine("\n" + new string('=', 50) + "\n");

        List<MediaItem> mediaLibrary = new List<MediaItem>();

        mediaLibrary.Add(new Song("Bohemian Rhapsody", "Queen", 1975, "Freddie Mercury", "A Night at the Opera", 355, "Rock", 320));
        mediaLibrary.Add(new Movie("Inception", "Christopher Nolan", 2010, "Christopher Nolan", 8.8));
        mediaLibrary.Add(new Podcast("Tech Talk", "John Doe", 2023, "Jane Smith", 42));


        foreach (MediaItem item in mediaLibrary)
        {
            item.print();
            item.Play();
            Console.WriteLine();
        }
    }
}