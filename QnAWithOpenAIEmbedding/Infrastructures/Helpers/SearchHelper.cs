using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenAI_API;
using QnAWithOpenAIEmbedding.Models;
using System.Security.Cryptography.Xml;

namespace QnAWithOpenAIEmbedding.Infrastructures.Helpers
{
    public class SearchHelper
    {
        public async static Task<List<TextRelatednessModel>> StringsRankedByRelatedness(string query, List<TrainingDataset> records, int topN = 100)
        {
            var queryEmbedding = await GetQueryEmbedding(query);
            var stringsAndRelatednesses = records.Select(record =>
            {
                var recordEmbedding = JsonConvert.DeserializeObject<float[]>(record.Embedding);
                return new TextRelatednessModel
                {
                    Text = record.Text,
                    Relatedness = CosineSimilarity(queryEmbedding, recordEmbedding)
                };
            }).ToList();

            return stringsAndRelatednesses.OrderByDescending(x => x.Relatedness).Take(topN).ToList();
        }

        private static async Task<float[]> GetQueryEmbedding(string query)
        {
            var apiclient = new OpenAIAPI(AppConfig.CurrentConiguration["OpenAI:APIKey"]);
            var queryEmbedding = await apiclient.Embeddings.GetEmbeddingsAsync(query); //uses 'text-embedding-ada-002' embedding model by default
            return queryEmbedding;
        }

        private static double CosineSimilarity(float[] x, float[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("Vectors must have the same dimensions.");

            var dotProduct = x.Zip(y, (xi, yi) => xi * yi).Sum();
            var xMagnitude = Math.Sqrt(x.Sum(xi => xi * xi));
            var yMagnitude = Math.Sqrt(y.Sum(yi => yi * yi));

            if (xMagnitude == 0.0 || yMagnitude == 0.0)
                return 0;

            return dotProduct / (xMagnitude * yMagnitude);

            // Uncomment this if you need a cosin distance instead of similarity
            //return 1 - (dotProduct / (xMagnitude * yMagnitude));
        }
    }
}
