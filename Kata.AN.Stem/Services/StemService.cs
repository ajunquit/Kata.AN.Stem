namespace Kata.AN.Stem.Services
{
    public class StemService: IStemService
    {
        private const string URI = "https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt";
        private List<string> _wordList;
        public StemService()
        {
            this._wordList = new List<string>();
            InitializeWordList();
        }

        public List<string> GetWords(string stem)
        {
            return this._wordList.Where(x => x.Contains(stem)).ToList();
        }

        public List<string> GetWords()
        {
            return this._wordList.ToList();
        }

        private void InitializeWordList()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(URI).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    this._wordList.AddRange(content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }
    }

    public interface IStemService
    {
        List<string> GetWords(string stem);
        List<string> GetWords();
    }
}
