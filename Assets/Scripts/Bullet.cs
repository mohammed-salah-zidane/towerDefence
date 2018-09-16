using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;   //nearest enemy to shoot it
    public float speed = 70f;   //bullet speed
    public GameObject effect;   //to refernce BulletImpact game object

    //setting the target of the bullet
    public void Seek(Transform _target)
    {
        target = _target;
    }
	// Update is called once per frame
	void Update () {

        // if no target destroy bullet
        if (target == null)
        {
            Destroy(gameObject);
            return; //destroy take time until finished and that may cause a problem we use return to avoid any problems
        }

        Vector3 dir = target.position - transform.position;   //calculate direction of bullet
        float DistanceThisFrame = speed * Time.deltaTime;    //calculte the distance the bullet will move in each frame مسافة =سرعة*زمن 

        //if the distance between bullet and target (dir.magnitude) <= the distance the bullet will move in that frame then the bullet Hited the target
        if (dir.magnitude <= DistanceThisFrame)
        {
            HitTarget(); //execute Hit Function and return 
            return;
        }

        //translate bullet towards the target  distance = distance the bullet will move in that frame
        transform.Translate(dir.normalized*DistanceThisFrame,Space.World);
	}


    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(effect, transform.position, transform.rotation); //Instantiate Bullet  Effect
        Destroy(effectIns, 1.5f); //destroy effect after 1.5 sec
        Destroy(target.gameObject); //destroy enemy 
        Destroy(gameObject);  //destroy bullet
    }
}
