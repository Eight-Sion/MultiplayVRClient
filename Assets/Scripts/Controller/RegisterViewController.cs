using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RegisterViewController : MonoBehaviour
{
    public Text TitleText;
    public InputField IDForm;
    public InputField PasswordForm;
    public InputField NicknameForm;
    public InputField PhoneNumberForm;
    public Button OkButton;
    public Button CancelButton;

    public void Init(string titleText, UnityAction<string, string, string, string>registAction, UnityAction cancelAction)
    {
        TitleText.text = titleText;
        OkButton.onClick.AddListener(()=> registAction?.Invoke(IDForm.text, PasswordForm.text, NicknameForm.text, PhoneNumberForm.text));
        CancelButton.onClick.AddListener(() => cancelAction?.Invoke());
    }
}
