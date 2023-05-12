using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Net.Sockets;

public class UITextInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _infoText;
    [SerializeField] CanvasGroup _myCanvasGroup;
    [SerializeField] RectTransform _myRT;
    bool _isHidden = true;
    Vector3 _offSet;

    private void Start()
    {
        _offSet = new Vector3(_myRT.localPosition.x, _myRT.transform.localPosition.y -300, _myRT.localPosition.z);
    }

    public void SendInfo(string message, float duration)
	{
        CancelInvoke();
        Invoke("HidePanel", duration);

        if (!_isHidden && message == _infoText.text)
            return;
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/2D/Objects/info_box");
        _infoText.text = message;

        _myCanvasGroup.alpha = 0;
        _myRT.transform.localPosition = _offSet;
        _myRT.DOAnchorPos(new Vector2(0, 0), 0.4f).SetEase(Ease.OutElastic, amplitude:1f, 0);
		_myCanvasGroup.DOFade(1, 0.4f);

        _isHidden = false;
	}

	void HidePanel()
	{
        _myCanvasGroup.alpha = 1;
        _myRT.DOAnchorPos(new Vector2(0, -300), 0.4f).SetEase(Ease.InOutQuint);
        _myCanvasGroup.DOFade(0, 0.4f);

        _isHidden = true;
    }
}
