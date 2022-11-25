using UnityEngine;
using TMPro;

public class UITextInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _infoText;

    public void SendInfo(string message, float duration)
	{
		if (!gameObject.activeSelf || message != _infoText.text)
			//AudioManager.Instance.PlaySFX("Info");
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
