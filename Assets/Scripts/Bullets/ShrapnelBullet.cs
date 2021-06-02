using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelBullet : Bullet
{
    public event Action<List<GameObject>> OnExplode;
    public KeyCode keyForExplode { get; set; }

    [SerializeField] private List<GameObject> fractionsPrefabs;
    [SerializeField] [Range(0, 50)] private int countOfFractions;
    [SerializeField] [Range(0, 100f)] private float explosionForce;

    private void Update()
    {
        if (Input.GetKeyDown(keyForExplode))
            DestroyBullet();
    }
    private void Explode()
    {
        List<GameObject> spawnedFractions = new List<GameObject>();

        for(int i = 0; i < countOfFractions; i++)
        {
            float randomDeflectionAngle = Mathf.PI / 180 * UnityEngine.Random.Range(0, 360);
            int randomIndex = UnityEngine.Random.Range(0, fractionsPrefabs.Count);
            float randomAngle = UnityEngine.Random.Range(0, 360f);

            GameObject fraction = Instantiate(fractionsPrefabs[randomIndex], transform.position, Quaternion.Euler(0, 0, randomAngle));
            spawnedFractions.Add(fraction);

            Vector3 forceDiration = fraction.transform.up.RejectVector(randomDeflectionAngle);
            Rigidbody2D fractionRigidbody = fraction.GetComponent<Rigidbody2D>();
            fractionRigidbody.AddForce(forceDiration * explosionForce * UnityEngine.Random.Range(0.7f, 1f), ForceMode2D.Impulse);
            fractionRigidbody.angularVelocity = UnityEngine.Random.Range(-1, 1f) * 200f;
        }
        OnExplode?.Invoke(spawnedFractions);
    }
    protected override void DestroyBullet()
    {
        base.DestroyBullet();
        Explode();
    }
}
