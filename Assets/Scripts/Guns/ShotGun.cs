using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] private uint fractionCount;
    [SerializeField][Range(0, 30f)] private float deflectionOfFractions;

    private void Update()
    {
        if(Input.GetKeyDown(ownerPlayer.Input.GetShotButtonKey()))
        {
            Shot();
        }
    }
    protected override void Shot()
    {
        if(countOfBullets > 0)
        {
            for(int i = 0; i < fractionCount; i++)
            {
                GameObject spawnedBullet = SpawnBullet(transform.up);
                BulletRegistration(spawnedBullet);
            }
            countOfBullets--;
        }
    }
    protected override GameObject SpawnBullet(Vector3 shotDiration)
    {
        float deflectionAngle = Mathf.PI / 180 * Random.Range(-deflectionOfFractions, deflectionOfFractions);
        Vector3 dir = shotDiration.RejectVector(deflectionAngle);
        return base.SpawnBullet(dir);
    }
    protected override void BulletRegistration(GameObject bullet)
    {
        base.BulletRegistration(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += actionOfChangeGun;
    }
}
