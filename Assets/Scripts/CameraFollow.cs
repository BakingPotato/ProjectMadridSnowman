using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 _currentVelocity;
    Vector3 _offset;
    [SerializeField] Transform target;
    [SerializeField] float smoothTime;

	private void Start()
	{
		_offset = transform.position - target.position;
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
}
