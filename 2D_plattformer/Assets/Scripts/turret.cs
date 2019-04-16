using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    IEnumerator coroutine;

    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        coroutine = Shoot();
       StartCoroutine(coroutine);
    }
    IEnumerator Shoot()
    {
        if (canShoot == true)
        {
            canShoot = false;
            Instantiate(bulletPrefab, gameObject.transform);
            yield return new WaitForSeconds(1f);
            canShoot = true;
        }
        
    }
}
