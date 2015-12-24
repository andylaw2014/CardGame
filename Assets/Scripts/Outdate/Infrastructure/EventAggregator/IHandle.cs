namespace Assets.Scripts.Outdate.Infrastructure.EventAggregator
{
    public interface IHandle
    {
    }

    public interface IHandle<in TMessage> : IHandle
    {
        void Handle(TMessage message);
    }
}