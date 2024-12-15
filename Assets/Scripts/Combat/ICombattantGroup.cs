using System.Collections.Generic;

public interface ICombattantGroup
{
    public ICombattant Main { get; }
    public List<ICombattant> Side { get; }
    public List<ICombattant> All { get; }
    public ICombattantGroup Opponent { get; }
    public void ItIsYourTurn(ICombattantGroup opponent);
}