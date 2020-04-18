using Danish.Covid.Country;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CountryDetailsPopUp : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image flag;
    [SerializeField] TMPro.TMP_Text countryName;
    [SerializeField] TMPro.TMP_Text cases;
    [SerializeField] TMPro.TMP_Text todayCases;
    [SerializeField] TMPro.TMP_Text death;
    [SerializeField] TMPro.TMP_Text todayDeath;
    [SerializeField] TMPro.TMP_Text recovered;
    [SerializeField] TMPro.TMP_Text active;
    [SerializeField] TMPro.TMP_Text critical;
    [SerializeField] TMPro.TMP_Text cpMillion;
    [SerializeField] TMPro.TMP_Text dpMillion;
    [SerializeField] TMPro.TMP_Text tests;
    [SerializeField] TMPro.TMP_Text tpMillion;

    public void SetView(AllCountryData _data)
    {
        cases.text = _data.cases.ToString();
        countryName.text = _data.country.ToString();
        todayCases.text = _data.todayCases.ToString();
        death.text = _data.deaths.ToString();
        todayDeath.text = _data.todayDeaths.ToString();
        recovered.text = _data.recovered.ToString();
        active.text = _data.active.ToString();
        critical.text = _data.critical.ToString();
        cpMillion.text = _data.casesPerOneMillion.ToString();
        dpMillion.text = _data.deathsPerOneMillion.ToString();
        tests.text = _data.tests.ToString();
        tpMillion.text = _data.testsPerOneMillion.ToString();

        StartCoroutine(DownloadFlagCoroutine(_data.countryInfo.flag));
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
