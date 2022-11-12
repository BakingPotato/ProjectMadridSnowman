using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    HealthManager _health;
    CharacterMovement _movement;

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<HealthManager>();
        _movement = GetComponent<CharacterMovement>();   
    }

    public void increaseMovementSpeed(float amount, float time)
    {
        if(time < 0)
          _movement.increaseSpeed(amount);
        else
          _movement.increaseSpeed_temp(amount, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
