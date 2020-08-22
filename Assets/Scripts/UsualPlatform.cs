using UnityEngine;

public class UsualPlatform : PlatformBase
{
    protected override int JumpForce => 8;
    public override int MinHeight => 0;
    public override int Probability => 20;
}