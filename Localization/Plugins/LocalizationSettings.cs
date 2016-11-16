using System;

using UnityEngine;
using System.Collections;

[System.Serializable]
public class LocalizationSettings : ScriptableObject
{
    public string[] sheetTitles;
 
    public bool useSystemLanguagePerDefault = true;
    public string defaultLangCode = "EN";



    //GENERAL
    public static LanguageCode GetLanguageEnum(string langCode)
    {
        langCode = langCode.ToUpper();
        foreach (LanguageCode item in Enum.GetValues(typeof(LanguageCode)))
        {
            if (item + "" == langCode)
            {
                return item;
            }
        }
        Debug.LogError("ERORR: There is no language: [" + langCode + "]");
		return LanguageCode.EN;
    }
}
