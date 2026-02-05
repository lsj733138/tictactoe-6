using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    [SerializeField] private Button singlePlayButton;
    [SerializeField] private Button dualPlayButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private GameObject settingUI;

    private void Awake()
    {
        singlePlayButton.onClick.AddListener(OnClickSinglePlayButton);
        dualPlayButton.onClick.AddListener(OnClickDualPlayButton);
        settingsButton.onClick.AddListener(OnClickSettings);
    }

    public void OnClickSinglePlayButton()
    {
        // TODO : 싱글 플레이 버튼 눌렀을 때
    }

    public void OnClickDualPlayButton()
    {
        // TODO : 2P 플레이 버튼 눌렀을 때
    }

    public void OnClickSettings()
    {
        // TODO : 설정 버튼 눌렀을 때
    }
}
