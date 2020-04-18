using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CountryPrefabData : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image flag;
    [SerializeField] TMP_Text countryNameTxt;
    [SerializeField] TMP_Text totalCaseTxt;
    [SerializeField] TMP_Text totalDeathTxt;

    public void SetCountryData(string _countryName, string flagUrl, float totalCases, float totalDeaths)
    {
        countryNameTxt.text = _countryName;
        totalCaseTxt.text = totalCases.ToString();
        totalDeathTxt.text = totalDeaths.ToString();
        StartCoroutine(DownloadFlagCoroutine(flagUrl));
    }

    private IEnumerator DownloadFlagCoroutine(string url)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        yield return webRequest.SendWebRequest();


        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Texture2D texture2d = DownloadHandlerTexture.GetContent(webRequest);

            Sprite sprite = null;
            sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);

            if (sprite != null)
            {
                flag.sprite = sprite;
            }
        }
    }

}
