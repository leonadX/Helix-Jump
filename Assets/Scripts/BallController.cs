using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulsForce = 5f;
    private Vector3 startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    void Awake(){
      startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(ignoreNextCollision)
           return;

        if(isSuperSpeedActive){
           if(!collision.transform.GetComponent<Goal>()){
              Destroy(collision.transform.parent.gameObject, 0.3f);
           }
        }else{
          // Adding restart functionality
          DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
          if(deathPart)
             deathPart.HitDeathPart();
        }

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up  * impulsForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", 0.2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update(){
        if(perfectPass >= 3 && !isSuperSpeedActive){
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    private void AllowCollision(){
      ignoreNextCollision = false;
    }

    public void ResetBall(){
      transform.position = startPos;
    }
}
