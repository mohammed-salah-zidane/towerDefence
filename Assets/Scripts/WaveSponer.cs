using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WaveSponer : MonoBehaviour {
    public Transform EnemyPrefab; //To Hold Enemy prefab   
    public Transform SpwanPoint;    //Setting Start point For Waves of enemies
    public float TimeBetweenWaves = 4f;   
    private float CountDown = 2f;
    private int WaveIndex=0;
    public Text CountDwonText;

    void Update()
    { 
    if(CountDown<=0f)
      {
          // we used coroutine to separate execution of this function from execution of main code (to create enemies as waves)
          StartCoroutine( SpwanWave()); 
          CountDown = TimeBetweenWaves;
      }

    //reduce countDwon time by one second
    CountDown -= Time.deltaTime; 
    //Display CountDown Timer as Text in the scene
    CountDwonText.text = Mathf.Round(CountDown).ToString();
    }


     IEnumerator SpwanWave()
    {
        WaveIndex++;
        for (int i = 0; i < WaveIndex; i++)
        {
            SpwanEnemy();
            //wait 0.5sec among the enemies of the same Wave
            yield return new WaitForSeconds(0.5f);
        }
    }

     void SpwanEnemy()
     {
         
         //create new enemy at starting point of Waves
        

         Instantiate(EnemyPrefab, SpwanPoint.position, SpwanPoint.rotation);
       
     }
}
