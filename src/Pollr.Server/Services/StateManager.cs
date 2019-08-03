namespace Pollr.Server.Services
{
    public class StateManager
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
    }
}
