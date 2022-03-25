using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject target;
    public int speed;
    Rigidbody rb;
    Vector3 targetLocation;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Target");

      targetLocation = new Vector3(target.transform.position.x, target.transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, 10f *Time.deltaTime);
        
        
    }
    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject);
        
    }
    //Last bullet spawns at the same time as new target
    //add delay to new target spawn

}
