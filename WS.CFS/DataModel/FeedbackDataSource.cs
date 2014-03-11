using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;
using WS.CFS.DataModel;

namespace WS.CFS.Data
{
    public class FeedbackDataSource
    {
        private static string _baseUri = "ms-appx:///"; // Default base URI
        private const string _key = "UseLocalDataSource"; // LocalSettings key
        private ObservableCollection<Feedback> _feedbacks = new ObservableCollection<Feedback>();
        public ObservableCollection<Feedback> Feedbacks
        {
            get { return this._feedbacks; }
        }

        public async Task GetFeedbacksAsync()
        {
           
            bool useLocalData = true;

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(_key))
                useLocalData = (bool)ApplicationData.Current.LocalSettings.Values[_key];

            string jsonText = null;

            if (useLocalData)
            {
                Uri dataUri = new Uri(_baseUri + "DataModel/feedbacks.json");
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                jsonText = await FileIO.ReadTextAsync(file);
            }
            else
            {
                _baseUri = "http://16.179.108.104:8081/";
                var cts = new CancellationTokenSource();
                cts.CancelAfter(5000); // Wait up to 5 seconds

                try
                {
                    var client = new HttpClient();
                    var response = await client.GetAsync(new Uri(_baseUri + "api/feedback/list")).AsTask(cts.Token);

                    if (!response.IsSuccessStatusCode)
                    {
                        await new MessageDialog("Unable to load remote data (request failed)").ShowAsync();
                        return;
                    }

                    jsonText = await response.Content.ReadAsStringAsync();

                    

                }
                catch (OperationCanceledException)
                {
                    new MessageDialog("Unable to load remote data (operation timed out)").ShowAsync();
                }

            }

            try
            {
                
                var feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(jsonText);

                foreach (var f in feedbacks)
                {
                    _feedbacks.Add(f);
                }
            }
            catch (Exception)
            {
                
               new MessageDialog("Invalid JSON data").ShowAsync();
            }

        }

        public static Feedback GetFeedback(string id)
        {
            var feedbackDataSource = (FeedbackDataSource)App.Current.Resources["feedbackDataSource"];

            return feedbackDataSource.Feedbacks.FirstOrDefault(x=> x.Id == id);
        }

    }
}
