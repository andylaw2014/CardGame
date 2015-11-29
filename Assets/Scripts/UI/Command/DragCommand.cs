namespace Assets.Scripts.UI.Command
{
    public class DragCommand : IUiCommand
    {
        public readonly Draggable Source;
        public readonly Targetable Destination;
        public DragCommand(Draggable source, Targetable destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}