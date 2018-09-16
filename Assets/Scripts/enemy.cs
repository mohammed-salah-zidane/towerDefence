using UnityEngine;


public class enemy : MonoBehaviour {

    public float speed = 10f;
    private Transform target;
    private int wavepointIndex =0;

    void Start() {

        target = waypoints.points[0];
        transform.Rotate(0,180,0);
        
    }
    int x;
    void Update() {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
           x= getnextwavepoint();
           if (x == 0 ||x == 1 ||x == 5 ||x == 6 ||x == 7 ||x == 9 )
               transform.Rotate(0, 90, 0);
           else
               transform.Rotate(0, -90, 0);
        }  
    }

     int getnextwavepoint()
        {
            if (wavepointIndex >= waypoints.points.Length - 1)
            {
                Destroy(gameObject); //destroy enemy when it reach the tower
                return 0; //to prevent from increasing index (index out of range avoidance) 
            }

         //go to next wave point
         wavepointIndex++; 
         target=waypoints.points[wavepointIndex];
         return wavepointIndex - 1;
        }


}
