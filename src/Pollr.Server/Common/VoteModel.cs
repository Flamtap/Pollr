using System.ComponentModel.DataAnnotations;

namespace Pollr.Server.Common
{
    public class VoteModel
    {
        [Required]
        public string Value { get; set; }
    }
}
