using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    //팝업 패널의 Rect Transform
    [SerializeField] private RectTransform panelTransform;
    private CanvasGroup _panelCanvasGroup;

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
    public void Hide()
    {
        Debug.Log("Panel off)");

        _panelCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
