using Newtonsoft.Json;

namespace QnAWithOpenAIEmbedding.Models
{
    public class TrainingDataset
    {
        public string Text { get; set; }

        public string Embedding { get; set; }

        public float[] GetEmbeddingAsList()
        {
            return JsonConvert.DeserializeObject<float[]>(Embedding);
        }
    }
}
