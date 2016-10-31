using System.Collections.Generic;
using System.Linq;

namespace Charades
{
    public class Words : List<string>
    {
        public Words()
        {
            AddRange(new[] {
                "Minister",
                "Exchanging the Rings",
                "Skits",
                "Wedding Band",
                "Engagement Ring",
                "Vows",
                "Proposing",
                "Wedding Shower",
                "Signing the Register",
                "Best Man",
                "Popping the Question",
                "Maid of Honour",
                "Ringbearer",
                "Giving away the Bride",
                "Family Bible",
                "Flowers",
                "Church Pew",
                "Wilhelmina Peppermints",
                "Parents of the Bride",
                "Processional",
                "Pipe Organ",
                "Bridal Registry",
                "Receiving Line",
                "Bridal Party",
                "Cutting the Cake",
                "Walking down the aisle",
                "Wedding Feast",
                "Wedding Banns",
                "Tuxedo",
                "Veil",
                "Till death do us part",
                "Tying the knot",
                "Mother of the Bride",
                "Marriage License",
                "Immanuel URC",
                "Wedding Bells",
                "Confetti",
                "Love",
                "Cupid",
                "Something Old, Something New",
                "Toast",
                "Limousine",
                "Home Sweet Home",
                "Updo",
                "Grow Old Together"
                }.OrderBy(s => s.Split(' ').First().Reverse().First())
            );
        }
    }
}