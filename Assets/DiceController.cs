using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [Header("Options")]
    public Rigidbody rBody;
    public List<Vector3> diceSides = new List<Vector3>();
    public Transform particleEffect;

    [HideInInspector]
    public DiceRoller manager;

    public int landedNumber = -1;

    public int thrown = 0;

    private bool active = false;

    public bool animate = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < diceSides.Count; i++)
        {
            if (i == landedNumber)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.TransformVector(diceSides[i]) * 2);
        }

    }

    private void Start()
    {
        active = true;
    }


    float delta = 0f;
    private void FixedUpdate()
    {
        if (active)
        {
            delta += Time.deltaTime;
            if (rBody.velocity.magnitude <= 0.05f && rBody.angularVelocity.magnitude <= 0.05f && delta >= 0.5f)
            {
                int bestIndex = 0;
                float highestDotProduct = Vector3.Dot(Vector3.up, transform.TransformVector(diceSides[0]));
                for (int i = 0; i < diceSides.Count; i++)
                {
                    float dot = Vector3.Dot(Vector3.up, transform.TransformVector(diceSides[i]));
                    if (dot > highestDotProduct)
                    {
                        bestIndex = i;
                        highestDotProduct = dot;

                    }
                }

                landedNumber = bestIndex;

                
                StartCoroutine("Finish");
                active = false;

            }

            if (delta >= 10)
            {
                manager.GetResult(1);
            }
        }
        
        if (animate)
        {
            Color col = transform.GetComponent<MeshRenderer>().material.color;
            col.a = Mathf.Lerp(col.a, 0, Time.deltaTime * 15);
            transform.GetComponent<MeshRenderer>().material.color = col;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform t = Instantiate(particleEffect, transform.position, Quaternion.identity);
        t.forward = -transform.forward;
        
    }

    IEnumerator Finish ()
    {

        transform.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2);
        animate = true;
        manager.GetResult(landedNumber + 1);
        yield return new WaitForSeconds(1);
     
        GameObject.Destroy(this.gameObject);
       
    }
    
}
