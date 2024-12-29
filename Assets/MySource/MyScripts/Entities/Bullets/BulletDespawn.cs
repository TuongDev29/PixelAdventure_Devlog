public class BulletDespawn : BaseMonoBehaviour
{
    public void Despawn()
    {
        BulletSpawner.Instance.Despawn(gameObject);
    }
}