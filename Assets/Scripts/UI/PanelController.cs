using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    //팝업 패널의 Rect Transform
    [SerializeField] private RectTransform panelTransform;
    private CanvasGroup _panelCanvasGroup;

    // 패널을 숨길 때 작동할 함수를 받는 delegate
    public delegate void PanelControllerHideDelegate();
    
    private void Awake()
    {
        _panelCanvasGroup = GetComponent<CanvasGroup>();
    }

    // 팝업 표시 
    public void Show()
    {
        Debug.Log("Panel On");

        _panelCanvasGroup.alpha = 0;
        panelTransform.localScale = Vector3.zero;

        _panelCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        panelTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    // 팝업 숨기기
    public void Hide(PanelControllerHideDelegate onComplete = null)
    {
        Debug.Log("Panel off)");

        _panelCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            onComplete?.Invoke();
            Destroy(gameObject);
        });
    }
}
