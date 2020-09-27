using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public Projectile ProjectilePrefab;
    public static ProjectilePool Instance;

    private Queue<Projectile> Projectiles = new Queue<Projectile>();

    private void Awake()
    {
        Instance = this;
    }

    public Projectile GetProjectile()
    {
        Projectile projectile;
        if (Projectiles.Count > 0)
        {
            projectile = Projectiles.Dequeue();
        }
        else
        {
            projectile = Instantiate(ProjectilePrefab);
        }
        projectile.gameObject.SetActive(false);
        return projectile;
    }

    public void ReturnProjectile(Projectile p)
    {
        p.gameObject.SetActive(false);
        Projectiles.Enqueue(p);
    }
}
