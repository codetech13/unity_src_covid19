using System.Collections;
using System.Collections.Generic;
using Danish.Covid.Country;
using UnityEngine;
using UnityEngine.Networking;

public class IndianStatesDetailsPopUp : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image flag;
    [SerializeField] TMPro.TMP_Text stateName;
    [SerializeField] TMPro.TMP_Text cases;
    [SerializeField] TMPro.TMP_Text death;
    [SerializeField] TMPro.TMP_Text recovered;
    [SerializeField] TMPro.TMP_Text confirmedCasesIndian;
    [SerializeField] TMPro.TMP_Text confirmedCasesForeign;

    public void SetView(IndianStatesRegionalLatest _data)
    {
        cases.text = _data.TotalConfirmed.ToString();
        stateName.text = _data.Loc.ToString();
        death.text = _data.Deaths.ToString();
        recovered.text = _data.Discharged.ToString();
        confirmedCasesIndian.text = _data.ConfirmedCasesIndian.ToString();
        confirmedCasesForeign.text = _data.ConfirmedCasesForeign.ToString();

        //StartCoroutine(DownloadFlagCoroutine(_data.countryInfo.flag));
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
