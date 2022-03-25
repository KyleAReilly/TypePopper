using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //public Typer typer = null;
    GameObject typer = null;
    private int health;
    // Start is called before the first frame update
    private void Awake() 
    {
        typer = GameObject.FindGameObjectWithTag("Typer");
    }
    void Start()
    {
        
        //get length of current word and set as health
       //health = typer.currentWord.Length;
       health = typer.GetComponent<Typer>().currentWord.Length;
       
       

    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log(typer.GetComponent<Typer>().newTarget);
       ChangeTag();
       
    }

    //as projectile hits subtract health and destroy when health is 0
    private void OnCollisionEnter(Collision other) 
    {
        health -= 1;
        if(health < 1)
        Destroy(gameObject);
    }
    private void ChangeTag()
    {
       
        //IsWordComplete function
        
         if(typer.GetComponent<Typer>().newTarget == true)
         {
            
            StartCoroutine(TagChange());

         }
         typer.GetComponent<Typer>().newTarget = false;        
    }
    IEnumerator TagChange()
    {
        yield return new WaitForSeconds(.001f);
        gameObject.tag = "Untagged";
    }
    

}
