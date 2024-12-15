using System.Collections.Generic;

public interface ICombattantGroup
{
    public ICombattant Main { get; }
    public List<ICombattant> Side { get; }
    public ICombattantGroup Opponent { get; }
    public void ItIsYourTurn(ICombattantGroup opponent);
}