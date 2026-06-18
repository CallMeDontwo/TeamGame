namespace ET
{
    public struct EnterWorld
    {
        public string WorldId;
    }

    public struct EnterGame
    {
        public string GameName;
        public int RoomId;
        public int MachineId;
    }
}