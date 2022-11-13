using UnityEngine;
using TMPro;

public class UITextInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _infoText;

    public void SendInfo(string message, float duration)
	{
		gameObject.SetActive(true);
		_infoText.text = message;
		CancelInvoke();
		Invoke("HidePanel", duration);
	}

	void HidePanel()
	{
		gameObject.SetActive(false);
	}
}
