using LiteNetLib;
using Source.Shared;

namespace Source.Client
{
    public class ClientPlayerStateSyncHandler : ClientMonoPacketHandler
    {
        public ClientNetworkManager networkManager;
        public ClientPlayerManager playerManager;

        private PlayerStatePacket cachedPlayerState;

        public override int HandlerId => (int) Handlers.PlayerState;

        public override void Initialize()
        {
            base.Initialize();

            cachedPlayerState = new PlayerStatePacket();
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            // If is for self, ignore
            // Else apply

            cachedPlayerState.Deserialize(reader);

            if (cachedPlayerState.NetworkId == networkManager.localNetworkId)
            {
                return;
            }

            if (!playerManager.playersById.TryGetValue(cachedPlayerState.NetworkId, out var player))
            {
                return;
            }

            player.transform.position = cachedPlayerState.Position;
            player.transform.rotation = cachedPlayerState.Rotation;
        }

        private void FixedUpdate()
        {
            // If local player exists
            // Send self position

            var localPlayer = playerManager.localPlayer;

            if (!localPlayer)
            {
                return;
            }

            cachedPlayerState.Set(localPlayer.networkId, localPlayer.transform.position, localPlayer.transform.rotation);
            cachedWriter.Reset();
            cachedWriter.Put(cachedPlayerState);
            client.SendAsPacketHandlerToServer(this, cachedWriter, DeliveryMethod.Unreliable);
        }
    }
}