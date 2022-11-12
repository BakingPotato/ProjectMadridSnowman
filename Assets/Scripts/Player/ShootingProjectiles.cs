using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform hand;
    [SerializeField] float shootCooldown;

    [SerializeField] bool isPlayer;

    float _currentTime;
    bool _shooting = false;

    // Update is called once per frame
    void Update()
    {
        if (_shooting)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
                _shooting = false;
        }
    }

    public void Shoot(Vector3 direction, int inputDamage = -1)
    {
        if (_shooting)
            return;
        _shooting = true;
        _currentTime = shootCooldown;

        Projectile proj = Instantiate(projectilePrefab, hand.position, Quaternion.identity).GetComponent<Projectile>();

        if(isPlayer)
            proj.IgnoringLayer = gameObject.layer;

        if(inputDamage==-1)
            proj.Throw(direction);
        else
            proj.Throw(direction, inputDamage);
    }
}
