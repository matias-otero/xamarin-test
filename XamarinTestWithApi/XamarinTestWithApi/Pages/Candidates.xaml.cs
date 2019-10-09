using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTestWithApi.Models;

namespace XamarinTestWithApi.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Candidates : ContentPage
    {
        private const string Url = "http://172.17.196.49:45455/api/Candidates";
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
                    ClickCellCommand = new RelayCommand<Candidate>(ClickCell);
                    LoadingPage.IsRunning = false;
                    LoadingPage.IsVisible = false;
                    CandidatesListView.IsVisible = true;
                });
                base.OnAppearing();
            }
            catch (Exception)
            {
                ErrorMessage.Text = "Something went wrong. Try later.";
                ErrorMessage.IsVisible = true;
                LoadingPage.IsVisible = false;
                LoadingPage.IsRunning = false;
            }
        }
        private void ClickCell(Candidate item)
        {
            CandidateBio.IsVisible = true;
            CandidateBio.Text = item.EmailAddress;
        }

        public ICommand ClickCellCommand { get; set; }
    }
}
