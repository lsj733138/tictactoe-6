using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    // [SerializeField] private Button singlePlayButton;
    // [SerializeField] private Button dualPlayButton;
    // [SerializeField] private Button settingsButton;

    private void Awake()
    {
        // singlePlayButton.onClick.AddListener(OnClickSinglePlayButton);
        // dualPlayButton.onClick.AddListener(OnClickDualPlayButton);
        // settingsButton.onClick.AddListener(OnClickSettingsButton);
    }

    public void OnClickSinglePlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.SinglePlay);
    }

    public void OnClickDualPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.DualPlay);
    }

    public void OnClickSettingsButton()
    {
        // TODO : 설정 버튼 눌렀을 때
        GameManager.Instance.OpenSettingsPanel();
    }
}
