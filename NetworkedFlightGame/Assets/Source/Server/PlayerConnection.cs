using LiteNetLib;

namespace Source.Server
{
    public class PlayerConnection
    {
        public NetPeer peer;
        public string playerName;

        public PlayerConnection(NetPeer peer, string playerName)
        {
            this.peer = peer;
            this.playerName = playerName;
        }
        
        public int Id => peer.Id;
    }
}