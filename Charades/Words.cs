using System.IO;
using System.Reflection;

namespace Charades;

public class Words : List<string>
{
    public Words()
    {
        var location = Assembly.GetExecutingAssembly().Location;
        var folder = Path.GetDirectoryName(location);
        var file = Path.Combine(folder, "words.txt");
        var lines = File.ReadAllLines(file);

        var random = new Random();

        AddRange(lines.OrderBy(_ => random.Next()));
    }
}
