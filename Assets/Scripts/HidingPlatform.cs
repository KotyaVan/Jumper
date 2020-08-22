public class HidingPlatform : PlatformBase
{
    protected override int JumpForce => 10;
    public override int MinHeight => 300;
    public override int Probability => 20;

    protected override void AfterCollisionCallBack()
    {
        MakeDisActive();
    }
}