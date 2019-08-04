namespace Pollr.Server.Common
{
    public class Vote
    {
        // ReSharper disable once UnusedMember.Global | Parameter-less constructor needed for serialization
        public Vote()
        {
        }

        public Vote(string voter, string value)
        {
            Voter = voter;
            Value = value;
        }

        public string Voter { get; set; }

        public string Value { get; set; }
    }
}
