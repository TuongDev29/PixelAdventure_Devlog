using UnityEngine;

public class BulletSpawner : Spawner<EBulletCode>
{
    private static BulletSpawner _instance;
    public static BulletSpawner Instance => _instance;

    protected override void Awake()
    {
        base.Awake();

        if (BulletSpawner._instance != null) Debug.LogWarning("Only one BulletSpawner allow to exist");
        BulletSpawner._instance = this;
    }

    public void SpawnBullet(EBulletCode bulletType, Vector2 positon, Vector2 directionFly)
    {
        GameObject bullerObj = this.Spawn(bulletType, positon);

        BulletMovement bulletMovement = bullerObj.GetComponent<BulletMovement>();
        bulletMovement.SetDirectionFl(directionFly);
    }
}