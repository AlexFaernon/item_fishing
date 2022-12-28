public class TurretClass
{
    public readonly Side Side;
    public readonly Side PositionOnWall;
    public int Health;
    public int HealthRank;
    public int DamageRank;
    public bool IsInstalled;
    public bool IsBroken;
    public bool IsBarrierInstalled;

    public TurretClass(Side side, Side positionOnWall)
    {
        Side = side;
        PositionOnWall = positionOnWall;
    }
}
