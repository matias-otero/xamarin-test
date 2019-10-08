using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTestWithApi.Models;

namespace XamarinTestWithApi.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Candidates : ContentPage
    {
        private const string Url = "https://hr-app-api.azurewebsites.net/api/Candidates";
        private static readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Candidate> _candidates;
        public Candidates()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                string content = await _client.GetStringAsync(Url).ConfigureAwait(false);
                List<Candidate> candidates = JsonConvert.DeserializeObject<List<Candidate>>(content);
                _candidates = new ObservableCollection<Candidate>(candidates);

                Device.BeginInvokeOnMainThread(() =>
                {
                    MyListView.ItemsSource = _candidates;
                    LoadingPage.IsRunning = false;
                    LoadingPage.IsVisible = false;
                    CandidatesListView.IsVisible = true;
                });
                base.OnAppearing();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
