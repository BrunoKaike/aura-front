using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class PasswordFields: MonoBehaviour {
 
    public InputField passwordInput;

    // Esconde a visualização da senha ao pressionar o botão
    public void TogglePasswordVisibility()
    {
        passwordInput.contentType = passwordInput.contentType == InputField.ContentType.Password ?
            InputField.ContentType.Standard : InputField.ContentType.Password;
    }

}