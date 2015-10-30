namespace  Infrastructure
{
    // Contravariance Interfaces
    public interface IReceive<in T> where T : IMessage
    {
        void Receive(T message);
    }
}
