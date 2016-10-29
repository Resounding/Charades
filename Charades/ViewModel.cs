using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Mvvm;

namespace Charades
{
    public class ViewModel : BindableBase
    {
        private readonly MediaPlayer _buzzPlayer = new MediaPlayer();
        private readonly MediaPlayer _dingPlayer = new MediaPlayer();
        private readonly DispatcherTimer _timer;

        public ViewModel()
        {
            _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Render, OnTimer,
                Application.Current.Dispatcher) {IsEnabled = false};

            CurrentTeam = TeamA;

            var exePath = Assembly.GetExecutingAssembly().Location;
            var folder = Path.GetDirectoryName(exePath);
            var buzz = Path.Combine(folder, "buzz.mp3");
            _buzzPlayer.Open(new Uri($"file://{buzz}"));
            _buzzPlayer.MediaEnded += (sender, args) => {
                _buzzPlayer.Position = TimeSpan.Zero;
                _buzzPlayer.Pause();
            };

            var ding = Path.Combine(folder, "ding.wav");
            _dingPlayer.Open(new Uri($"file://{ding}"));
            _dingPlayer.MediaEnded += (sender, args) => {
                _dingPlayer.Position = TimeSpan.Zero;
                _dingPlayer.Pause();
            };

            StartCommand = new DelegateCommand(Start);
            PauseCommand = new DelegateCommand(Pause);
            ResetCommand = new DelegateCommand(Reset);
            SetTeamCommand = new DelegateCommand<Team>(SetCurrentTeam);
            GuessedCommand = new DelegateCommand(Guessed);
            SkippedCommand = new DelegateCommand(Skipped);
            NextWordCommand = new DelegateCommand(AdvanceWord);
        }

        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand SetTeamCommand { get; }
        public ICommand GuessedCommand { get; }
        public ICommand SkippedCommand { get; }
        public ICommand NextWordCommand { get; }

        public void Start()
        {
            if (_wordIndex == -1) {
                AdvanceWord();
            }

            if (_timer.IsEnabled) {
                Reset();
            }

            _timer.IsEnabled = true;
        }

        public void Pause()
        {
            _timer.IsEnabled = false;
        }

        public void Reset()
        {
            Seconds = 120;
        }

        private void OnTimer(object sender, EventArgs args)
        {
            if (Seconds > 1) {
                Seconds--;
            } else if(Seconds == 1) {
                Seconds--;
                _buzzPlayer.Play();
            } else {
                Pause();
                Reset();
            }
            OnPropertyChanged(() => TimeRemaining);
            OnPropertyChanged(() => Seconds);
        }

        public string TimeRemaining => TimeSpan.FromSeconds(Seconds).ToString(@"mm\:ss");

        private void AdvanceWord()
        {
            _wordIndex++;
            OnPropertyChanged(() => CurrentWord);
        }

        private int _seconds = 120;
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                SetProperty(ref _seconds, value);
                OnPropertyChanged(() => TimeRemaining);
            }
        }

        private int _wordIndex = -1;
        private readonly Words _words = new Words();
        public string CurrentWord => _wordIndex == -1 ? string.Empty : _words[_wordIndex];

        public Team TeamA { get; } = new Team("Team A", "LightBlue");
        public Team TeamB { get; } = new Team("Team B", "LightGreen");
        public Team TeamC { get; } = new Team("Team C", "Yellow");
        public Team TeamD { get; } = new Team("Team D", "Lavender");

        private void Guessed()
        {
            _dingPlayer.Play();
            CurrentTeam.Score++;
            AdvanceWord();
        }

        private void Skipped()
        {
            CurrentTeam.Score--;
            AdvanceWord();
        }

        public void SetCurrentTeam(Team team)
        {
            CurrentTeam = team;
            Pause();

        }

        private Team _currentTeam;
        public Team CurrentTeam {
            get { return _currentTeam; }
            private set { SetProperty(ref _currentTeam, value); }
        }
    }
}