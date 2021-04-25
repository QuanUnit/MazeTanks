using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    private void Update()
    {
        if(countOfBullets > 0)
        {
            if (Input.GetKeyDown(ownerPlayer.Input.GetShotButtonKey()))
            {
                Shot();
            }
        }
    }
}
