using UnityEngine;

public class UsualPlatform : PlatformBase
{
    protected override int JumpForce => 8;
    protected override int MinHeight => 5;
    protected override int Probability => 5;
}