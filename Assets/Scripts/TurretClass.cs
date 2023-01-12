public class TurretClass
{
    public int Health;
    public int HealthRank;
    public int DamageRank;
    public bool IsInstalled;
    public bool IsBroken;
    public bool IsBarrierInstalled;
    
    public int HealthMaxRank => new[] {2, 3}.Length - 1;
    public int MaxHealth => new[]{2, 3}[HealthRank];
    public int NextHealthUpgradeCost => new[] { 10, 0 }[HealthRank];
    
    public int DamageMaxRank => new[] { 10, 15, 20, 25 }.Length - 1;
    public int NextDamageUpgradeCost => new[] { 10, 20, 30, 0 }[DamageRank];
    public int Damage => new[] { 3, 5, 6, 8 }[DamageRank];

    public TurretClass()
    {
        IsInstalled = false;
        IsBroken = false;
        IsBarrierInstalled = false;
    }
}
