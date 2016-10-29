using Prism.Mvvm;

namespace Charades
{
    public class Team : BindableBase
    {
        public Team(string name, string colour)
        {
            Name = name;
            Colour = colour;
        }

        public string Name { get; }
        public string Colour { get; }

        private int _score;
        public int Score
        {
            get { return _score; }
            set { SetProperty(ref _score, value); }
        }
    }
}