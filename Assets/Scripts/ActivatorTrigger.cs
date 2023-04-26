using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatorTrigger : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public List<GameObject> objectsToDeactivate;
    bool firstTime = true;

    public bool terrorEnd = false;
    [SerializeField] Button replayButton;
    [SerializeField] Button backButton;
    [SerializeField] UIManager UI;

    GameManager GM;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
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

            if (terrorEnd)
            {
                replayButton.onClick.RemoveAllListeners();
                replayButton.onClick.AddListener(tutorialEnd);
                replayButton.onClick.AddListener(UI.PlayButtonSound);
                backButton.onClick.RemoveAllListeners();
                backButton.onClick.AddListener(tutorialEnd);
                backButton.onClick.AddListener(UI.PlayButtonSound);
            }
        }
    }

    void tutorialEnd()
    {
        GM.SaveRecords();
        UI.SwitchScene("TutorialExtraEnd");
    }
}
