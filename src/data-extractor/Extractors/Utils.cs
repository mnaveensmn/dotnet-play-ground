using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data_extractor.Extractors
{
    public class Utils
    {
        public static void ListFilesNames()
        {

            foreach (string fileLocation in Directory.EnumerateFiles("/Users/naveenkumar/Account/RX/emperia-colleqt/reedexpo.digital.connectionbox/test/Reedexpo.Digital.ConnectionBox.Integration.Test/features", "*.feature"))
            {
                var filename = Path.GetFileName(fileLocation);
                Console.WriteLine(filename);
            }
        }
    }
}