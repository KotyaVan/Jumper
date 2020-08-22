public class YellowPlatform : PlatformBase
{
    protected override int JumpForce => 3;

    protected override void AfterCollisionCallBack()
    {
        MakeDisActive();
    }
}