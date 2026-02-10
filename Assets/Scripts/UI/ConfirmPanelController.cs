using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TextMeshProUGUI messageText;
    
    public delegate void OnConfirmButtonClicked();
    private OnConfirmButtonClicked onConfirmButtonClicked;
    
    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked = null)
    {
        this.onConfirmButtonClicked = onConfirmButtonClicked;
        messageText.text = message;
        Show();
    }
    
    // x버튼 누르면
    public void OnClickCloseButton()
    {
        Hide(() =>
        {
            onConfirmButtonClicked?.Invoke();
        });
    }
}
