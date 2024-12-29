public class BulletController : BaseMonoBehaviour
{
    public BulletDespawn BulletDespawn;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent<BulletDespawn>(ref this.BulletDespawn, gameObject);
    }
}