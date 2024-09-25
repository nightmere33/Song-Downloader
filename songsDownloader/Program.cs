using System;
using System.Net;
using System.Threading;

class videoDownloader
{
    static void Main(string[] args)
    {
        //url of the song
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
        Console.WriteLine("NOTICE: ENTRE THE URL NOT THE LINK AND BE AWARE THAT NOT ALL THE URL'S YOU INSERT WILL BE DOWNLOADED |");
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
    t1:
        Console.Write(" \n paste the url here : ");

        string URL = Console.ReadLine();
        Console.Write("\n name your video ");
        string name = Console.ReadLine();

    a1:
        Console.Write("\n what format is your file \n 1/ MP4 \n 2/ MP3 \n 3/ JPG \n 4/ JEPG \n 5/ EXE \n insert the number : ");
        int num = Convert.ToInt32(Console.ReadLine());
        string format = "";
        switch (num)
        {
            case 1:
                format = "mp4";
                break;
            case 2:
                format = "mp3";
                break;
            case 3:
                format = "jpg";
                break;
            case 4:
                format = "jepg";
                break;
            case 5:
                format = "exe";
                break;
            default:
                Console.WriteLine("please enter a valid number ");
                goto a1;

        }
        //local path
        string directory = "";
        Console.Write($"\n do you want to store the file in this direction E:\\aa\\++\\+++ \n for yes entre y or else to continue down below :");
         string respond = Console.ReadLine();
        respond=respond.ToLower();
        if (respond == "y")
        {
            directory = "E:\\aa\\++\\+++";
        }
        else
        {
            Console.Write("\nEnter the directory to save the file: ");
             directory = Console.ReadLine();
        }
        string localPath = directory + "\\" + name + "." + format;

        using (WebClient webClient = new WebClient())
        {
            long totalSize = 0;
            long downloadedSize = 0;
            DateTime startTime = DateTime.Now;

            webClient.DownloadProgressChanged += (s, e) =>
            {
                if (totalSize == 0)
                {
                    totalSize = e.TotalBytesToReceive;
                    double totalSizeInMB = (double)totalSize / 1048576;
                    int ts = (int)totalSizeInMB;
                    Console.WriteLine($"\nTotal Size: {ts} MB");

                }

                downloadedSize = e.BytesReceived;
                TimeSpan elapsedTime = DateTime.Now - startTime;
                double downloadSpeed = (double)downloadedSize / elapsedTime.TotalSeconds;
                double remainingTime = (totalSize - downloadedSize) / downloadSpeed;
                TimeSpan remainingTimeSpan = TimeSpan.FromSeconds(remainingTime);

                Console.CursorVisible = false; // Hide the cursor
                Console.Write("\r{0} {1}% ETA: {2:mm\\:ss} Speed: {3} KB/s", new string('▬', e.ProgressPercentage / 2), e.ProgressPercentage, remainingTimeSpan, (downloadSpeed / 1024).ToString("0.00")); // Display the progress bar, estimated time, and download speed
            };


            webClient.DownloadFileCompleted += (s, e) =>
            {
                Console.CursorVisible = true; // Show the cursor again
            };
            try
            {
                Console.WriteLine("\nWait until the Download is finished\nStarting Download ...");
                webClient.DownloadFileAsync(new Uri(URL), localPath);

                while (webClient.IsBusy) { } // Wait until the download is completed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n An error occurred during download:{ex.Message}");
            }
        }

        Thread.Sleep(2000);
        Console.WriteLine("\n\nThe Download is successfully finished.");

        Console.Write("\n Do you want to download another video/image/song ? for yes press Y and for no press any key : ");
        string rep = Console.ReadLine();
        rep = rep.ToUpper();
        if (rep == "Y")
        {
            goto t1;
        }
        else
        {
            Console.WriteLine("\n Program Finished");
        }
    }
}
