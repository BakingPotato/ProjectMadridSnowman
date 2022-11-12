using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    HealthManager _health;
    CharacterMovement _movement;

    [Header("Prefabs de PowerUps")]
    [SerializeField] GameObject prefab_Umbrella;

    [SerializeField] GameObject prefabHam;

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

    public void instantiateUmbrella(float time)
    {
        StartCoroutine(powerUpUmbrella(time));
    }
    public void instantiateHam(float time)
    {
        StartCoroutine(powerUpHam(time));
    }


    IEnumerator powerUpUmbrella(float time)
    {
        //Lo instanciamos en el padre de 
        prefab_Umbrella.GetComponent<UmbrellaController>().player = this.gameObject;
        GameObject actualUmbrella = Instantiate(prefab_Umbrella, transform.parent);

        yield return new WaitForSeconds(time);

        //Destruimos el objeto
        Destroy(actualUmbrella);
    }
    IEnumerator powerUpHam(float time)
    {
        //Lo instanciamos en el padre de 
        prefabHam.GetComponent<HamController>().player = this.gameObject;
        GameObject actualHam = Instantiate(prefabHam, transform.parent);

        yield return new WaitForSeconds(time);

        //Destruimos el objeto
        Destroy(actualHam);
    }


}
