using UnityEngine;
using static Constants;

public class MainPanelController : MonoBehaviour
{
    [SerializeField] private GameObject signupPanelPrefab;
    
    public void OnClickSinglePlayButton()
    {
        GameManager.Instance.ChangeToGameScene(GameType.SinglePlay);
    }

    public void OnClickDualPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(GameType.DualPlay);
    }

    public void OnClickSettingsButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }

    public void OnClickMultiPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(GameType.MultiPlay);
    }

    #region 점수, 로그아웃
    public void OnClickGetScore()
    {
        StartCoroutine(NetworkManager.Instance.GetScore((score) =>
        {
            GameManager.Instance.OpenConfirmPanel($"현재 점수 : {score.score}", () =>
            {
            });
        }, () =>
        {

        }));
    }
    
    public void OnClickSignout()
    {
        StartCoroutine(NetworkManager.Instance.Signout((result) =>
        {
            GameManager.Instance.OpenConfirmPanel($"로그아웃 : {result.message}", () =>
            {
                PlayerPrefs.DeleteKey("SID");
            });
        }, () =>
        {

        }));
    }
    #endregion
}