using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private Image playerATurnPanel;
    [SerializeField] private Image playerBTurnPanel;
        
    // 뒤로 가기 버튼 
    public void OnClickBackButton()
    {
        //GameManager.Instance.ChangeToMainScene();
        
        GameManager.Instance.OpenConfirmPanel("게임을 종료합니다.", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
        
    }
    
    // 설정 팝업 표시
    public void OnClickSettingsButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }

    public void SetPlayerTurnPanel(Constants.PlayerType playerType)
    {
        switch (playerType)
        {
            case Constants.PlayerType.None:
                playerATurnPanel.color = Color.white;
                playerBTurnPanel.color = Color.white;
                break;
            case Constants.PlayerType.Player1:
                playerATurnPanel.color = Color.red;
                playerBTurnPanel.color = Color.white;
                break;
            case Constants.PlayerType.Player2:
                playerATurnPanel.color = Color.white;
                playerBTurnPanel.color = Color.blue;
                break;
        }
    }
}
