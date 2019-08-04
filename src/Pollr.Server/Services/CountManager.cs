namespace Pollr.Server.Services
{
    public class CountManager
    {
        private int _count;

        public int GetCount()
        {
            return _count;
        }

        public int BumpCount()
        {
            return ++_count;
        }

        public int ResetCount()
        {
            return _count = 0;
        }
    }
}
