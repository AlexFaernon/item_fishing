public class WallClass
{
    public int Health;
    public int HealthRank;
    public int HealthMaxRank => new[] { 2, 3, 4, 5 }.Length - 1;
    public int MaxHealth => new[]{2, 3, 4, 5}[HealthRank];
    public int NextUpgradeCost => new[] { 10, 20, 30, 0 }[HealthRank];
}