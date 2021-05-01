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
                BulletRegistation(spawnedBullet);
            }
            countOfBullets--;
        }
    }
    protected override GameObject SpawnBullet(Vector3 shotDiration)
    {
        float deflectionAngle = Mathf.PI / 180 * Random.Range(-deflectionOfFractions, deflectionOfFractions);
        float x = shotDiration.x * Mathf.Cos(deflectionAngle) - shotDiration.y * Mathf.Sin(deflectionAngle);
        float y = shotDiration.x * Mathf.Sin(deflectionAngle) + shotDiration.y * Mathf.Cos(deflectionAngle);
        shotDiration = new Vector3(x, y);
        return base.SpawnBullet(shotDiration);
    }
    protected override void BulletRegistation(GameObject bullet)
    {
        base.BulletRegistation(bullet);
        bullet.GetComponent<Bullet>().OnDestroy += actionOfChangeGun;
    }
}
