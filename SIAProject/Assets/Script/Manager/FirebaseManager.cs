using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    [Header("Sign IN")]
    public TMP_InputField emailSignin;
    public TMP_InputField passwordSignin;
    public TMP_Text warningSigninText;

    [Header("Sign UP")]
    public TMP_InputField usernameSignup;
    public TMP_InputField emailSignup;
    public TMP_InputField passwordSignup;
    public TMP_InputField confirmPasswordSinup;
    public TMP_Text warningSignupText;

    [Header("Firebase")]
    private string databaseURL = "https://sia-firebase-125eb-default-rtdb.asia-southeast1.firebasedatabase.app/users";
    private string apikey = "AIzaSyA9rERnZuGm9k4gNXePzvFh_NJ4TdCFmHU";
    private string idToken ;
    private string getLocalId;
    public static fsSerializer serializer = new fsSerializer();
    public static string localId;

    [Header("User Info")]
    public int rank = 0;
    public string username;
    public int score = 0;

    [Header("Turn On/Off Scene")]
    public GameObject signInUI;
    public GameObject signUpUI;

    private UserInfo userInfo;
    
    public void SignUpButton()
    {
        SignUp(usernameSignup.text,emailSignup.text, passwordSignup.text);
    }
    public void SignInButton()
    {
        SignIn(emailSignin.text, passwordSignin.text);
    }
    
    private void SignUp(string _username,string _email, string _password)
    {
        if (_username == "")
        {
            warningSignupText.text = "Username Missng";
        }
        else if(_email == "")
        {
            warningSignupText.text = "Email Missing";
        }
        else if (passwordSignup.text != confirmPasswordSinup.text)
        {
            warningSignupText.text = "Password Doesn't Macth";
        }
        else
        {
            string userData = "{\"email\":\"" + _email + "\",\"password\":\"" + _password + "\",\"returnSecureToken\":true}";
            RestClient.Post<SignResponse>($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key={apikey}", userData).Then(response =>
            {
                string emailVerification = "{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + response.idToken + "\"}";
                RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key={apikey}", emailVerification);
                localId = response.localId;
                username = _username;
                PosttoDatabase(response.idToken);

            }).Catch(error =>
            {
                Debug.Log(error);
            });
        }
        
    }
    private void SignIn(string _email, string _password)
    {
        string userData = "{\"email\":\"" + _email + "\",\"password\":\"" + _password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key={apikey}", userData).Then(response =>
        {
            Debug.Log(response.idToken);
            string emailVerification = "{\"idToken\":\"" + response.idToken + "\"}";
            RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/getAccountInfo?key={apikey}", emailVerification).Then(
                emailResponse =>
                {
                    fsData emailVerificationData = fsJsonParser.Parse(emailResponse.Text);
                    EmailConfirm emailConfirmationInfo = new EmailConfirm();
                    serializer.TryDeserialize(emailVerificationData, ref emailConfirmationInfo).AssertSuccessWithoutWarnings();
                    if (emailConfirmationInfo.users[0].emailVerified)
                    {
                        idToken = response.idToken;
                        localId = response.localId;
                        warningSigninText.text = "Login Complete";
                        Debug.Log("Signin Complete");
                        //GetUserName();
                        StartCoroutine(SignInScene());
                    }
                    else
                    {
                        Debug.Log("You are stupid, you need to verify your email dumb");
                    }
                });

        }).Catch(error =>
        {
            Debug.Log("Signin Error");
        });
    }
    private void PosttoDatabase(string idTokenTemp = "")
    {
        if(idTokenTemp == "")
        {
            idTokenTemp = idToken;
        }
        UserInfo user = new UserInfo(rank, username, score);
        Debug.Log($"{rank} : {username} : {score}");
        RestClient.Put($"{databaseURL}/{localId}.json?auth={idTokenTemp}", user).Then(response =>
        {
            Debug.Log("Put Database");
        }).Catch(error =>
        {
            Debug.Log("Put Database Error");
        });
    }
    private void RetrieveFromDatabase()
    {
        RestClient.Get<UserInfo>($"{databaseURL}/{localId}.json?auth={idToken}").Then(response =>
        {
            Debug.Log(" Get Retrieve From Database");
            userInfo = response;
        }).Catch(error =>
        {
            Debug.Log("Error Retrieve From Database");
        });
    }
    private void GetUserName()
    {
        RestClient.Get<UserInfo>($"{databaseURL}/{localId}.json?auth={idToken}").Then(response =>
        {
            username = response.username;
            Debug.Log(username);
            UserSelf.Instance.getUsername = username;
        }).Catch(error =>
        {
            Debug.Log("Error Get User Name");
        });
    }
    private void GetLocalID()
    {
        RestClient.Get($"{databaseURL}.json?auth={idToken}").Then(response =>
        {
            var username = emailSignin.text;
            Debug.Log("Check GetLocalID");
            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string, UserInfo> users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users.Values)
            {
                if (user.username == username)
                {
                    getLocalId = user.localId;
                    RetrieveFromDatabase();
                    Debug.Log("GetLocalID Complete");
                    break;
                }
            }
        }).Catch(error =>
        {
            Debug.Log("GetLocalID Error");
        });

    }
    public IEnumerator SignInScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
    public void SignUpScene()
    {
        signUpUI.SetActive(false);
        signInUI.SetActive(true);
    }
}
