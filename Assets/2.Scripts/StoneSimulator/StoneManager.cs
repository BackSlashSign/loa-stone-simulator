using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneManager : MonoBehaviour
{
    [SerializeField]
    private Options[] _options = new Options[3];
    [SerializeField]
    private int presentPercent;
    [SerializeField]
    private int initialPercent = 75;
    [SerializeField]
    private RecentList recentList;
    [SerializeField]
    private AdsManager _adsManager;
    [SerializeField]
    private AudioSource _audioSource;

    public AudioClip buttonAudioClip;
    public Button[] craftButtons = new Button[3];
    public int craftCount = 0;
    public void Init()
    {
        _options = GetComponentsInChildren<Options>();
        presentPercent = initialPercent;
        _options[0].Init();
        _options[1].Init();
        _options[2].Init();
        craftButtons[0].enabled = true;
        craftButtons[1].enabled = true;
        craftButtons[2].enabled = true;
    }
    private void Awake()
    {
        Init();
        _audioSource= GetComponent<AudioSource>();
        _audioSource.clip= buttonAudioClip;
        _adsManager = gameObject.GetComponent<AdsManager>();
        craftCount = 0;
    }
    public int GetPresentPercentValue()
    {
        return presentPercent;
    }
    public int SetPresentPercent(bool value)        // 1 || -1
    {
        if (value)
        {
            presentPercent += 10;
            Debug.Log("È®·ü Áõ°¡ : " + presentPercent + "%");
            return presentPercent;
        }
        else if (!value)
        {
            presentPercent -= 10;
            Debug.Log("È®·ü °¨¼Ò : " + presentPercent + "%");
            return presentPercent;
        }
        else
        {
            Debug.LogError("SetPresentPercent() error");
            return -2;
        }
    }
    public void ButtonClicked_1()
    {
        _audioSource.Play();
        Calculate(0);
    }
    public void ButtonClicked_2()
    {
        _audioSource.Play();
        Calculate(1);
    }
    public void ButtonClicked_3()
    {
        _audioSource.Play();
        Calculate(2);
    }

    private void Calculate(int index)
    {
        switch (index)
        {
            case 0:
                _options[0].Calculate();
                Debug.Log("Calculate 0");
                break;
            case 1:
                _options[1].Calculate();
                Debug.Log("Calculate 1");
                break;
            case 2:
                _options[2].Calculate();
                Debug.Log("Calculate 2");
                break;
        }
        _options[0].TextUpdate();
        _options[1].TextUpdate();
        _options[2].TextUpdate();
        if (_options[0].craftComplete == true && _options[1].craftComplete == true && _options[2].craftComplete == true)
        {
            CraftComplete();
        }
    }

    private void CraftComplete()
    {
        Debug.Log("Craft Complete!");
        craftButtons[0].enabled = false;
        craftButtons[1].enabled = false;
        craftButtons[2].enabled = false;
        recentList.SetText(_options[0].activatedValue, _options[1].activatedValue, _options[2].activatedValue);
        craftCount++;
        if(craftCount%10 == 0)
        {
            _adsManager.ShowAdInterstitial();
            Init();
        }
    }

    public void ButtonEnable(int index, bool on)
    {
        if(on)
        {
            craftButtons[index].enabled = on;
        }
        else
        {
            craftButtons[index].enabled = on;
        }
    }
}
