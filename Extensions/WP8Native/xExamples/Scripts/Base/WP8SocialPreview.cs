using UnityEngine;
using System.Collections;

public class WP8SocialPreview : WPNFeaturePreview
{

    void OnGUI()
    {
        UpdateToStartPos();
        if (style == null)
            InitStyles();
        GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Native Social", style);
        StartY += YLableStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "TwitterShare"))
        {
            WP8SocialManager.instance.TwitterPost("Test");
        }

        StartX += XButtonStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "TwitterShareTexture"))
        {
            WP8SocialManager.instance.TwitterPost("Test", GetTexture());
        }

        StartX += XButtonStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "FacebookShare"))
        {
            WP8SocialManager.instance.FacebookPost("Test");
        }

        StartX += XButtonStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "FacebookShareTexture"))
        {
            WP8SocialManager.instance.FacebookPost("FacebookShareTexture", GetTexture());
        }

        StartY += YLableStep * 2;
        StartX = XStartPos;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "ShareMedia"))
        {
            WP8SocialManager.instance.ShareMedia("Test");
        }

        StartX += XButtonStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "ShareMediaTexture"))
        {
            WP8SocialManager.instance.ShareMedia("Test", GetTexture());
        }

        StartX += XButtonStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "SendMail"))
        {
            WP8SocialManager.instance.SendMail("Test@gmail.com", "Test", "Test2@gmail.com");
        }

        StartX += XButtonStep;
        if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "SendMailTexture"))
        {

        }
    }

    private Texture2D GetTexture()
    {
        var texture = new Texture2D(256, 256);
        return texture;
    }
}
