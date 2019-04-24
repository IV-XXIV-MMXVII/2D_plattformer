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
       StartCoroutine(coroutine);//These codes make turret shoot. Combined with the rest of the code the turret can shoot continusly at player. 
    }
    IEnumerator Shoot()
    {
        if (canShoot == true)//this code and code below allows the turret to continue to shoot at player. 
        {
            canShoot = false;
            Instantiate(bulletPrefab, gameObject.transform);//code that allows the turret to shoot in strait line and kill player. 
            yield return new WaitForSeconds(1f);//wait time between shots from turret. 
            canShoot = true;//allows to continue shooting. 
        }
        
    }
}
