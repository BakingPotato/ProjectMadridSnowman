using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorTrigger : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public List<GameObject> objectsToDeactivate;
    bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && firstTime)
        {
            foreach(GameObject o in objectsToActivate)
            {
                o.SetActive(true);
            }

            foreach (GameObject o in objectsToDeactivate)
            {
                o.SetActive(false);
            }

            firstTime = false;
        }
    }
}
