using UnityEngine;

public class LoginBlobertAniamtionScript : MonoBehaviour
{

    public LoginScreenBehaviour loginScreenBehaviour;

    public void CallCameraShake()
    {
        loginScreenBehaviour.Shake(2f,2);
    }
}
