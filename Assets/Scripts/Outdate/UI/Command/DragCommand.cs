namespace Assets.Scripts.Outdate.UI.Command
{
    public class DragCommand : IUiCommand
    {
        public readonly Targetable Destination;
        public readonly Draggable Source;

        public DragCommand(Draggable source, Targetable destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}