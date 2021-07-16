using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
   // Character playerScript;

    private void Start()
    {
       // playerScript = GetComponent<Character>();
    }
    public enum CollectibleType
    {
        POWERUP,
        LIVES,
        Score
    }



    public CollectibleType currentCollectible;
    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {

            //switch (currentCollectible)
            //{
            //    case CollectibleType.POWERUP:
            //        Character pmScript = other.gameObject.GetComponent<Character>();
            //        pmScript.powerUp++;
                


            //        break;

            //    case CollectibleType.LIVES:
            //        pmScript = other.gameObject.GetComponent<Character>();
            //        pmScript.health++;

                    
            //        break;

            //    case CollectibleType.Score:
            //        pmScript = other.gameObject.GetComponent<Character>();
            //        pmScript.score++;

            //        break;

           
            //}
            Destroy(gameObject);
        }
    }
}
