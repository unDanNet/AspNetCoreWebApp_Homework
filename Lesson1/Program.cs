using System.Text.Json;

namespace Lesson1
{
    public static class Program
    {
        private const int FirstPostId = 4;
        private const int LastPostId = 13;

        private static readonly Uri ApiBaseUri = new Uri("https://jsonplaceholder.typicode.com");
        private static readonly HttpClient Client = new HttpClient();

        private const string ResultsFileName = "results.txt";

        public static async Task Main(string[] args)
        {
            var result = new List<Task<Post>>();

            for (var id = FirstPostId; id <= LastPostId; id++)
            {
                result.Add(GetPostById(id));
            }

            await Task.WhenAll(result);

            await WriteResultToFile(result);
        }

        private static async Task<Post> GetPostById(int id)
        {
            var requestUri = new Uri(ApiBaseUri, $"posts/{id}");
            
            try
            {
                var response = await Client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                
                return JsonSerializer.Deserialize<Post>(body);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not get post at {requestUri}: {e.Message}");
                return null;
            }
        }

        private static async Task WriteResultToFile(List<Task<Post>> result)
        {
            await using var writer = new StreamWriter(ResultsFileName, false);

            foreach (var task in result)
            {
                var post = task.Result;
                var fileOutput = $"{post.UserId}\n{post.Id}\n{post.Title}\n{post.Body}\n";

                try
                {
                    await writer.WriteLineAsync(fileOutput);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not write the post data to the {ResultsFileName} file: {e.Message}");
                }
            }
        }
    }
}

