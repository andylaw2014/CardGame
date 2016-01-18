namespace Assets.Scripts.Infrastructure.IdFactory
{
    internal interface IIdFactory
    {
        /// <summary>
        ///     Reutrn an unique Id.
        /// </summary>
        /// <param name="type">Type of the unique id.</param>
        /// <returns>Unique Id.</returns>
        string GetId(int type);
    }
}