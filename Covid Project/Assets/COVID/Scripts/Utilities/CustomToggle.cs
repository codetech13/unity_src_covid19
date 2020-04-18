using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{

    [SerializeField] GameObject inactive;
    [SerializeField] GameObject active;

    [SerializeField] bool isOn = false;

    public bool IsOn { get => isOn; set => isOn = value; }

    public delegate void OnClickDone();
    public OnClickDone onClickDone;

    public UnityAction<CustomToggle> OnClickDoneAction;

    private void Awake()
    {
        //GetComponent<Button>().onClick.AddListener(OnClick);
        RefreshView();
    }

    void OnClick() {
        if (IsOn)
        {
            IsOn = false;
        }
        else {
            IsOn = true;
        }
        RefreshView();

        if (onClickDone != null)
        {
            onClickDone();
        }
    }

    public void RefreshView() {
        if (IsOn)
        {
            inactive.SetActive(false);
            active.SetActive(true);
        }
        else {
            inactive.SetActive(true);
            active.SetActive(false);
        }
    }

    public void SetValues(bool value) {
        if (value) {
            IsOn = true;
        }
        else
        {
            isOn = false;
        }
        RefreshView();

        if (onClickDone != null)
        {
            onClickDone();
        }
    }
}
