using TMPro;
using UnityEngine;

public class SigninPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    // 로그인 팝업에서 "확인" 버튼 클릭 시 동작할 함수
    public delegate void OnSignupButtonClicked();
    private OnSignupButtonClicked _onSignupButtonClicked;

    public void Show(OnSignupButtonClicked onSignupButtonClicked)
    {
        _onSignupButtonClicked = onSignupButtonClicked;
        Show();
    }
    

    public void OnClickConfirmButton()
    {
        // Input Field에 입력된 값을 체크해서 서버에 전달

        var username = usernameInputField.text;
        var password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            // 입력 값이 누락되었다는 팝업 표시
            GameManager.Instance.OpenConfirmPanel("입력 값이 누락되었습니다.", () => {});
        }
        
        // singinData 생성
        SigninData signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;
        
        // 서버로 SigninData 전달하면서 로그인 진행
        StartCoroutine(NetworkManager.Instance.SignIn(signinData, () =>
        {
            Hide();
        }, () =>
        {
            usernameInputField.text = "";
            passwordInputField.text = "";
        }));
    }

    public void OnClickCancelButton()
    {
        Hide();
    }
}
