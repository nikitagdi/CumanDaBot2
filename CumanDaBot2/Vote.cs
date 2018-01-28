using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumanDaBot2
{
    class Voter
    {
        public Voter(long id, string name)
        {
            Id = id;
            Name = name;
        }

        private long Id { get; set; }
        private string Name { get; set; }
    }

    class Vote
    {
        public Vote()
        {
            Variants = new List<string>();
            Votes = new SortedDictionary<long, string>();
        }

        public bool addVariant(string variant)
        {
            if (Variants.Contains<string>(variant))
                return false;

            Variants.Add(variant);

            return true;
        }

        public bool vote(long voter, string variant)
        {
            string ret;
            if (Votes.TryGetValue(voter, out ret))
                return false;

            if (!Variants.Contains<string>(variant))
                throw new Exception();

            Votes.Add(voter, variant);

            return true;
        }

        public string getStatsString()
        {
            int[] voteCount = new int[Variants.Count];
            string ret;

            foreach (var variant in Votes)
                voteCount[Variants.IndexOf(variant.Value)]++;

            ret = "Результаты голосования: \n";
            foreach (var variant in Variants)
                ret += (1 + Variants.IndexOf(variant)) + ")" + variant + " - " + voteCount[Variants.IndexOf(variant)] + " голосов\n";

            return ret;
        }

        public List<string> Variants { get; set; }
        private SortedDictionary<long, string> Votes;
    }
}
