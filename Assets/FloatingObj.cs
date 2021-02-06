using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    // Start is called before the first frame update

    bool floatup = false;

     Vector3 p;
    void Start()
    {
        p  = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(floatup)
        StartCoroutine(floatingup());
         else if(!floatup)
        StartCoroutine(floatingdown());
    }

    IEnumerator floatingup() {
        p.y += 0.1f * Time.deltaTime;
        transform.position = p;
        yield return new WaitForSeconds(1f);
        floatup = false;
    }
    IEnumerator floatingdown(){
        p.y -= 0.1f * Time.deltaTime;
        transform.position = p;
        yield return new WaitForSeconds(1);
        floatup = true;
    }

//      var floatup;
//  function Start(){
//      floatup = false;
//  }
//  function Update(){
//      if(floatup)
//          floatingup();
//      else if(!floatup)
//          floatingdown();
//  }
   
}
