using System.Collections.Generic;
using Pollr.Server.Common;

namespace Pollr.Server.Services
{
    public class PollManager
    {
        private readonly List<Vote> _votes = new List<Vote>();

        public IEnumerable<Vote> GetVotes()
        {
            return _votes;
        }

        public IEnumerable<Vote> Vote(Vote vote)
        {
            _votes.Add(vote);
            return _votes;
        }

        public void Reset()
        {
            _votes.Clear();
        }
    }
}
