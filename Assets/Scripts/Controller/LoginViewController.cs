using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginViewController : MonoBehaviour
{
    public Text TitleText;
    public InputField IdForm;
    public InputField PasswordForm;
    public Button LoginButton;

    public void Init(string titleText, UnityAction<string, string> loginAction)
    {
        TitleText.text = titleText;
        LoginButton.onClick.AddListener(()
            => loginAction.Invoke(IdForm.text, PasswordForm.text));
    }
    
}
