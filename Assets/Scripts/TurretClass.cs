public class TurretClass
{
    public int Health;
    public int HealthRank;
    public int DamageRank;
    public bool IsInstalled;
    public bool IsBroken;
    public bool IsBarrierInstalled;

    public TurretClass()
    {
        IsInstalled = false;
        IsBroken = false;
        IsBarrierInstalled = false;
    }
}
