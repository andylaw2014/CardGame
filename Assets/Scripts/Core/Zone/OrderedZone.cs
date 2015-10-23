using System.Collections;
using System.Collections.Generic;

public abstract class OrderedZone : AbstractGameObject, IEnumerable<Card>, IZone
{
    public IEnumerator<Card> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract Zone Name { get; }
    public void Remove(Card card)
    {
        throw new System.NotImplementedException();
    }

    public void AfterRemove(Card card)
    {
        throw new System.NotImplementedException();
    }

    public void AfterAdd(Card card)
    {
        throw new System.NotImplementedException();
    }
}
