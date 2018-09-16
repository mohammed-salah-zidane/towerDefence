using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private Transform target;        //target enemy
    [Header("Attributes")]
    public float FireRate = 1f;      //Number of Bullets in 1 Sec
    private float FireCountDwon = 0f; 
    public float range = 15f;  // range coverd by the Gun

     [Header("Unity Setup")]
    public string enemyTag="Enemy"; 
    public Transform PartToRotate;   //refernce to the empty object which used to rotate head of the Turret
    public float RotateSpeed = 10f;  //Rotation Speed of The Turret

    public GameObject BulletPrefab; 
    public Transform FirePoint; 

	void Start () {
        //invoking UpdateTarget method every 0.5 sec starting at time 0 sec => ( at the start of the game )
        InvokeRepeating("UpdateTarget",0f,0.5f); 
	
	}



    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //holding Enemies which  in the Scene
        float ShortDistace = Mathf.Infinity;   //shortest distance between Turret and an enemy
        GameObject near = null;               //nearest enemy to the Turret

        //looping between enemies to find the nearest enemy to the Turret
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position,enemy.transform.position); //calculate distance between enemy and Turret
            if (distanceToEnemy < ShortDistace)
            {
                ShortDistace = distanceToEnemy;
                near = enemy;
            }
        }
        //setting the target to the nearest enemy if it within the range and not Null else that no target
        if (near != null && ShortDistace <= range)
        {
            target = near.transform;
        }
        else
        {
            target = null;
        }
    }
	



	void Update () {
        if (target == null)
        {
            //PartToRotate.rotation = Quaternion.Euler(0f,0f, 0f);
            return; //if there is no target do nothing
        }

        //make the Turret look at the Target by rotation
        Vector3 dir = target.position - transform.position; 
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation,Time.deltaTime*RotateSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        // to keep number of bullets under the allowed bullets number in 1 sec  (FireRate)
        if (FireCountDwon <= 0f)
        {
            Shoot();
            FireCountDwon = 1f / FireRate;
        }
        FireCountDwon -= Time.deltaTime;
	}



    void Shoot()
    {
       GameObject BulletG=(GameObject) Instantiate(BulletPrefab,FirePoint.position,FirePoint.rotation);
        Bullet bullet=BulletG.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target); //send target enemy refernce to bullet script to point to it 
        }
    }



    // Draw Wire Sphere with raduis=(range of the Turret) around  the Selected Turret 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
