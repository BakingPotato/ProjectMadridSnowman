using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 _currentVelocity;
    Vector3 _offset;
    [SerializeField] Transform target;
    [SerializeField] float smoothTime;
    [SerializeField] LayerMask buildLayer;

	private void Start()
	{
		_offset = transform.position - target.position;
	}

	private void Update()
	{
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        direction.z -= 0.5f;
        if (Physics.Raycast(transform.position, direction, out hit, 100, buildLayer))
        {
            Debug.Log(hit.transform.name);
        }
    }

	private void FixedUpdate()
	{
        //Por si acaso el jugador es destruido
        if (target)
        {
            Vector3 targetPos = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _currentVelocity, smoothTime);
        }
	}

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Vector3 direction = target.position - transform.position;
        direction.z -= 0.5f;
        Gizmos.DrawRay(transform.position, direction);
    }
}
