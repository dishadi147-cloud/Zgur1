using ConsoleApp1;

string title = "Bohemian Rhapsody";
string artist = "Queen";
string album = "A Night at the Opera";
int durationSeconds = 355;
string genre = "Rock";
int bitrate = 320;

Song s = new Song(title, artist, album, durationSeconds, genre, bitrate);
s.print();
s.Play();
s.Pause();
s.GetQualityInfo();
