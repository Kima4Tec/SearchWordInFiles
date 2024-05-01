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

        StreamWriter writer = new StreamWriter(outPutFile);

        SearchFiles(startFolder, searchWord, writer);

        writer.Close();

        DateTime endTime = DateTime.Now;
        var searchTime = endTime - startTime;
        
        Console.WriteLine($"Search complete in time: {searchTime:hh\\:mm\\:ss}. Output written to: " + outPutFile);
        Console.ReadLine();
    }

    static void SearchFiles(string folderPath, string searchWord, StreamWriter writer)
    {
        try
        {
            //foreach (string filePath in Directory.GetFiles(folderPath, "*.xml").Concat(Directory.GetFiles(folderPath, "*.xaml")))
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