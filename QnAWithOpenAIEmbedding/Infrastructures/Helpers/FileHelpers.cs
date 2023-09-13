using CsvHelper;
using Newtonsoft.Json;
using QnAWithOpenAIEmbedding.Models;
using System.Globalization;

namespace QnAWithOpenAIEmbedding.Infrastructures.Helpers
{
    public static class FileHelpers
    {
        public static List<TrainingDataset> GetEmbeddingContent()
        {
            using var file = File.Open("./Infrastructures/DataFiles/winter_olympics_2022.csv", FileMode.Open);
            using TextReader reader = new StreamReader(file);
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<TrainingDataset>().ToList();

            foreach (var record in records)
                record.Embedding = JsonConvert.SerializeObject(record.GetEmbeddingAsList());
            return records;
        }
    }
}
