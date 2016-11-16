using UnityEngine;
using System.Collections;

public class UM_ShareUtility : MonoBehaviour {

	public static void TwitterShare(string status) {
		TwitterShare(status, null);
	}
	
	public static void TwitterShare(string status, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			if(texture != null) {
				AndroidSocialGate.StartShareIntent("Share", status, texture, "twi");
			} else {
				AndroidSocialGate.StartShareIntent("Share", status, "twi");
			}


			break;
		case RuntimePlatform.IPhonePlayer:
			IOSSocialManager.instance.TwitterPost(status, null, texture);
			break;

		case RuntimePlatform.WP8Player:
			WP8SocialManager.instance.TwitterPost(status, texture);
			break;
		}
	}
	
	
	public static void FacebookShare(string message) {
		FacebookShare(message, null);
	}
	
	public static void FacebookShare(string message, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			if(texture != null) {
				AndroidSocialGate.StartShareIntent("Share", message, texture, "facebook.katana");
			} else {
				AndroidSocialGate.StartShareIntent("Share", message, "facebook.katana");
			}

			break;
		case RuntimePlatform.IPhonePlayer:
			IOSSocialManager.instance.FacebookPost(message, null, texture);
			break;
		case RuntimePlatform.WP8Player:
			WP8SocialManager.instance.FacebookPost(message, texture);
			break;
		}
	}
	
	
	public static void ShareMedia(string caption, string message) {
		ShareMedia(caption, message, null);
	}
	
	public static void ShareMedia(string caption, string message, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			if(texture != null) {
				AndroidSocialGate.StartShareIntent("Share", message, texture);
			} else {
				AndroidSocialGate.StartShareIntent("Share", message);
			}

			break;
		case RuntimePlatform.IPhonePlayer:
			IOSSocialManager.instance.ShareMedia(message, texture);
			break;
		case RuntimePlatform.WP8Player:
			WP8SocialManager.instance.ShareMedia(message, texture);
			break;
		}
	}



	public static void SendMail(string subject, string body, string recipients) {
		SendMail(subject, body, recipients, null);
	}
	
	public static void SendMail(string subject, string body, string recipients, Texture2D texture) {
		
		switch(Application.platform) {
		case RuntimePlatform.Android:
			AndroidSocialGate.SendMail("Send Mail", body, subject, recipients, texture);
			break;
		case RuntimePlatform.IPhonePlayer:
			IOSSocialManager.instance.SendMail(subject, body, recipients, texture);
			break;
		case RuntimePlatform.WP8Player:
			WP8SocialManager.instance.SendMail(subject, body, recipients, texture);
			break;
		}
		
	}


}
