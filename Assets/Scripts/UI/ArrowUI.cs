using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{

    public GameObject target;
    public GameObject player;

    private RectTransform arrowRT;
    private void Awake()
    {
        arrowRT = this.GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        Vector3 fromPos = player.transform.position;
        Vector3 toPos = target.transform.position;
        fromPos.y = 0.0f;
        toPos.y = 0.0f;

        Vector3 dir = (toPos - fromPos).normalized;
        float angle = 0;
        angle = Vector3.Angle(dir, Vector3.forward);
        Debug.Log("x: " + dir.x + "z: " + dir.z);
        Vector3 angleRot = new Vector3(0, 0, angle);
        arrowRT.localEulerAngles = angleRot;
    }
}
