using Prism.Mvvm;
using System.Windows.Media;

namespace Charades;

public class Team : BindableBase
{
    public Team(string name, Color colour)
    {
        Name = name;
        Colour = colour;
    }

    public string Name { get; }
    public Color Colour { get; }

    private int _score;
    public int Score
    {
        get { return _score; }
        set { SetProperty(ref _score, value); }
    }
}