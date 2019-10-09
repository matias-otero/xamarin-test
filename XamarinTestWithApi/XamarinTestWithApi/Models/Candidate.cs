using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace XamarinTestWithApi.Models
{
    public class Candidate : INotifyPropertyChanged
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string LastName { get; set; }
        public int DNI { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string LinkedInProfile { get; set; }
        public string AdditionalInformation { get; set; }
        //public EnglishLevel EnglishLevel { get; set; }
        //public CandidateStatus Status { get; set; }
        //public Consultant Recruiter { get; set; }
        //public Office PreferredOffice { get; set; }
        //public DateTime ContactDay { get; set; }
        //public IList<CandidateSkill> CandidateSkills { get; set; }

        //private string _completeName;
        private string _name;
        private string _lastName;

        [JsonProperty("name")]
        public string Name 
        {
            //get { return $"{Name} {LastName}"; }
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty("lastName")]
        public string LastName
        {
            //get { return $"{Name} {LastName}"; }
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string CompleteName
        {
            get { return $"{_name} {_lastName}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
