﻿using UnityEngine;

public class GunSystem : MonoBehaviour {

    public float damage = 20f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public GameObject impactEffect; 

    public Camera fpsCam;

    private float nextTimeToFire = 0f;

	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
	}

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }
}
