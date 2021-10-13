using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    [Header("Sign IN")]
    public TMP_InputField emailSignin;
    public TMP_InputField passwordSignin;
    public TMP_Text warningSininText;

    [Header("Sign UP")]
    public TMP_InputField emailSignup;
    public TMP_InputField passwordSignup;
    public TMP_InputField confirmPasswordSinup;
    public TMP_Text warningSignupText;

    [Header("Firebase")]
    private string databaseURL = "https://sia-firebase-125eb-default-rtdb.asia-southeast1.firebasedatabase.app/user";
    private string apikey = "AIzaSyA9rERnZuGm9k4gNXePzvFh_NJ4TdCFmHU";
    private string idToken = "9ddqSj28nVB0nUOf09Qe4ArqRrTbhRruDA2ALMRd";
    public static fsSerializer serializer = new fsSerializer();
    public static string localId;
    private string getLocalId;

    [Header("User Info")]
    public int rank = 0;
    public string username;
    public int score = 0;

    private UserInfo userInfo;
    public void SignUpButton()
    {
        SignUp(emailSignup.text, passwordSignup.text);
    }
    public void SignInButton()
    {
        SignIn(emailSignin.text, passwordSignin.text);
    }
    private void PosttoDatabase()
    {
        UserInfo user = new UserInfo(rank, username, score);
        Debug.Log($"{rank} : {username} : {score}");
        RestClient.Put($"{databaseURL}/{localId}.json", user).Then(response =>
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
    private void SignUp(string _email, string _password)
    {
        if(_email == "")
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
                Debug.Log("Create User");
                idToken = response.idtoken;
                localId = response.localId;
                username = _email;
                PosttoDatabase();
            }).Catch(error =>
            {
                Debug.Log("Signup Error");
            });
        }
        
    }
    private void SignIn(string _email, string _password)
    {
        string userData = "{\"email\":\"" + _email + "\",\"password\":\"" + _password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key={apikey}", userData).Then(response =>
        {
            idToken = response.idtoken;
            localId = response.localId;
            warningSininText.text = "Login Complete";
            Debug.Log("Signin Complete");
        }).Catch(error =>
        {
            Debug.Log("Signin Error");
        });
    }
    private void GetUserName()
    {
        RestClient.Get<UserInfo>($"{databaseURL}/{localId}.json?auth={idToken}").Then(response =>
        {
            username = response.username;
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
}
