using OpenFoodFacts.Services;
using OpenFoodFacts.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OpenFoodFacts.Models.Products;
using System.Text.Json;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;
using OpenFoodFacts.Services.Interfaces;
using System.Text;

namespace OpenFoodFacts.CRON
{
    public class CronTask : CronJobService
    {
        private readonly FoodsService _foodsService;
        private readonly IMongoCollection<Food> _foodsCollection;
        private readonly IMongoCollection<Cron> _cronCollection;
        private readonly string _downloadPath;
        private readonly string _localDirectory;
        public CronTask(IScheduleConfig<CronTask> config, IOptions<CronSettings> cronSettings, IOptions<OpenFoodFactsDatabaseSettings> openFoodFactsDatabaseSettings) : base(config.CronExpression, config.TimeZoneInfo)
        {
            var mongoClient = new MongoClient(openFoodFactsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(openFoodFactsDatabaseSettings.Value.DatabaseName);

            _foodsCollection = mongoDatabase.GetCollection<Food>(openFoodFactsDatabaseSettings.Value.FoodsCollectionName);

            _cronCollection = mongoDatabase.GetCollection<Cron>(openFoodFactsDatabaseSettings.Value.CronCollectionName);

            _downloadPath = cronSettings.Value.DownloadPath;
            _localDirectory = cronSettings.Value.LocalDirectory;
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {

            try
            {

                //Gets the last file imported number

                Cron cronDoc = _cronCollection.Find<Cron>(x => true)
                                              .SortByDescending(d => d.LastUpdate_t)
                                              .Limit(1).FirstOrDefault<Cron>();


                //Downloads the file

                int intFileNumber = 0;
                if(cronDoc != null)
                    int.TryParse(cronDoc.FileUploaded, out intFileNumber);
                intFileNumber++;
                string strFileNumber = intFileNumber.ToString();

                if (intFileNumber <= 9)
                    strFileNumber = "0" + strFileNumber;
                else
                    strFileNumber = "01";
                

                string address = _downloadPath;
                string filename = "products_"+ strFileNumber + ".json.gz";
                string decompressedFileName = "products_" + strFileNumber + ".json";
                string directory = _localDirectory;
                string downloadPath = directory + filename;
                
                System.Uri uri = new System.Uri(address + filename);

                WebClient webClient = new WebClient();
                webClient.DownloadFile(uri, downloadPath);

                //Unzip the file
                FileInfo file = new FileInfo(downloadPath);

                using (FileStream originalFileStream = file.OpenRead())
                {
                    using (FileStream decompressedFileStream = File.Create(directory+decompressedFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }

                //Imports the data into MongoDB
                IEnumerable<string> json = System.IO.File.ReadLines(directory+decompressedFileName);

                for (int i = 0; i < 100; i++)
                {
                    if (json != null)
                    {
                        Food deserializedFood = JsonSerializer.Deserialize<Food>(json.ElementAt(i));

                        if (deserializedFood.code != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (char c in deserializedFood.code)
                            {
                                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                                {
                                    sb.Append(c);
                                }
                            }
                            deserializedFood.code = sb.ToString();
                        }
                        deserializedFood.imported_t = DateTime.Now;
                        deserializedFood.Status = Food.status.published;
                        _foodsCollection.InsertOneAsync(deserializedFood);

                    }

                }

                //Updates Cron DB
                Cron cronObject = new Cron();
                cronObject.DbReadWriteConnection = _cronCollection
                    .Database.Client.Cluster.Description.State.ToString();
                cronObject.LastUpdate_t = DateTime.Now;
                cronObject.FileUploaded = strFileNumber;
                cronObject.OnlineTime_t = Process.GetCurrentProcess().TotalProcessorTime;
                cronObject.UsedMemory = Process.GetCurrentProcess().PrivateMemorySize64;
                _cronCollection.InsertOneAsync(cronObject);

            
            }
            catch (Exception ex)
            {
                ex = ex.InnerException;
                throw new Exception(ex.Message);
            }

        
            return base.DoWork(cancellationToken);
        }


    }
}
