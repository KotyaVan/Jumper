public class HidingPlatform : PlatformBase
{
    protected override int JumpForce => 10;
    protected override int MinHeight => 5;
    protected override int Probability => 5;

    protected override void AfterCollisionCallBack()
    {
        MakeDisActive();
    }
}