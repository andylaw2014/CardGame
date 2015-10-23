using System.Collections.Generic;

public interface IGraveyardQuery : IZoneQuery
{
    IEnumerable<Card> Units { get;}
}