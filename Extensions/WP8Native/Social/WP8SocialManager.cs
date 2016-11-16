#define SA_DEBUG_MODE
using UnityEngine;
using System.Collections;

public class WP8SocialManager : WPN_Singletone<WP8SocialManager>
{
	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

    public void FacebookPost(string text)
    {
        FacebookPost(text, null);
    }

    public void FacebookPost(string text, Texture2D texture)
    {
        if (texture == null)
        {
#if (UNITY_WP8 && !UNITY_EDITOR) || SA_DEBUG_MODE
            WP8Native.Social.FacebookPost(text, "");
#endif
        }
        else
        {
#if (UNITY_WP8 && !UNITY_EDITOR) || SA_DEBUG_MODE
            byte[] val = texture.EncodeToPNG();
            string bytesString = System.Convert.ToBase64String(val);
            WP8Native.Social.FacebookPost(text, bytesString);
#endif
        }
    }

    public void TwitterPost(string text)
    {
        TwitterPost(text, null);
    }


    public void TwitterPost(string text, Texture2D texture)
    {
        if (texture == null)
        {
#if (UNITY_WP8 && !UNITY_EDITOR) || SA_DEBUG_MODE
            WP8Native.Social.TwitterPost(text, "");
#endif
        }
        else
        {


#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
            byte[] val = texture.EncodeToPNG();
            string bytesString = System.Convert.ToBase64String(val);

            WP8Native.Social.TwitterPost(text, bytesString);
#endif
        }

    }

    public void ShareMedia(string text)
    {
        ShareMedia(text, null);
    }

    public void ShareMedia(string text, Texture2D texture)
    {
#if (UNITY_WP8 && !UNITY_EDITOR) || SA_DEBUG_MODE
        if (texture != null)
        {
            byte[] val = texture.EncodeToPNG();
            string bytesString = System.Convert.ToBase64String(val);
            WP8Native.Social.ShareMedia(text, bytesString);
        }
        else
        {
            WP8Native.Social.ShareMedia(text, "");
        }
#endif
    }

    public void SendMail(string subject, string body, string recipients)
    {
        SendMail(subject, body, recipients, null);
    }

    public void SendMail(string subject, string body, string recipients, Texture2D texture)
    {
        if (texture == null)
        {
#if (UNITY_WP8 && !UNITY_EDITOR) 
			WP8Native.Social.SendMail(subject, body, recipients, string.Empty);
#endif
        }
        else
        {
#if (UNITY_WP8 && !UNITY_EDITOR) || SA_DEBUG_MODE
         //   byte[] val = texture.EncodeToPNG();
          //  string bytesString = System.Convert.ToBase64String(val);
            WP8Native.Social.SendMail(subject, body, recipients, string.Empty);
#endif
        }
    }
}
