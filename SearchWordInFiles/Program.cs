using NLog;


class Program
{
    public static Logger logger = LogManager.GetCurrentClassLogger();

    static void Main()
    {
        string startFolder = @"C:\Windows\";
        string searchWord = "coffee";
        string outPutFile = @"c:\dev\searchResults.txt";
        DateTime startTime = DateTime.Now;

        //Writing text showing user the input and action
        Console.WriteLine($"Searching for {searchWord}...");

        //Starting an instance of StreamWriter with outPutFile as parameter.
        StreamWriter writer = new StreamWriter(outPutFile);

        //Calling method SearchFiles
        SearchFiles(startFolder, searchWord, writer);

        //Close StreamWriter
        writer.Close();

        //Search time is noted
        DateTime endTime = DateTime.Now;
        var searchTime = endTime - startTime;
        
        //Output to console
        Console.WriteLine($"Search complete in time: {searchTime:hh\\:mm\\:ss}. Output written to: " + outPutFile);
        Console.ReadLine();
    }

    static void SearchFiles(string folderPath, string searchWord, StreamWriter writer)
    {
        try
        {
            //Returns the names of files (including their path) that match the specified
            //search pattern in the specified directory
            //XML://foreach (string filePath in Directory.GetFiles(folderPath, "*.xml").Concat(Directory.GetFiles(folderPath, "*.xaml")))
            foreach (string filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                string fileContent = File.ReadAllText(filePath);

                if (fileContent.Contains(searchWord))
                {
                    logger.Info("filePath: " + filePath);

                    //Writing search result to console
                    Console.WriteLine(filePath);
                    
                    //Writing to txt-file
                    writer.WriteLine($"{filePath}");
                }
            }
            //Do the same search for subfolders
            foreach (string subFolderPath in Directory.GetDirectories(folderPath))
            {
                SearchFiles(subFolderPath, searchWord, writer);
            }
        }
        catch (Exception ex)
        {
            logger.Error("Fejl: " + ex);
        }
    }

}