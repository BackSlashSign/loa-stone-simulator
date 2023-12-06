//using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public StoneManager stoneManager;
    public Image successImage;
    public Image failureImage;
    public Image redSuccessImage;
    public Image redFailureImage;
    public Image defaultImage;
    public Image redDefaultImage;

    public TextMeshProUGUI presentPercentText;
    public TextMeshProUGUI activeNumberText;

    public bool reduceEffect = false;
    public bool craftComplete = false;

    public int activatedValue;

    public AudioClip successClip;
    public AudioClip failureClip;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private int _optionIndex;
    [SerializeField]
    private int[] _activityArr;
    [SerializeField]
    private int _activatedValue = 0;
    [SerializeField]
    private Image[] _imageContainer;

    public void Init()
    {
        craftComplete = false;
        stoneManager = GetComponentInParent<StoneManager>();
        _imageContainer = GetComponentsInChildren<Image>();
        _activityArr = new int[10];
        _activatedValue = 0;
        for (int i = 0; i < _activityArr.Length; i++)
        {
            _activityArr[i] = -1;
            if (reduceEffect)
            {
                _imageContainer[i].sprite = redDefaultImage.sprite;
            }
            else
            {
                _imageContainer[i].sprite = defaultImage.sprite;
            }
        }
        TextUpdate();
    }
    private void Awake()
    {
        Init();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip= null;
        if(gameObject.tag == "Option_1")
        {
            _optionIndex = 0;
        }
        else if (gameObject.tag == "Option_2")
        {
            _optionIndex = 1;
        }
        else if (gameObject.tag == "Option_3")
        {
            _optionIndex = 2;
        }
    }
    public void TextUpdate()
    {
        presentPercentText.text = "성공확률 : " + stoneManager.GetPresentPercentValue() + "%";
        activeNumberText.text = "X" + _activatedValue; 
    }

    private void Active(bool per_25)
    {
        _audioSource.clip = successClip;
        _audioSource.Play();
        //활성도 +1
        for(int i = 0; i < _activityArr.Length;i++)
        {
            if (_activityArr[i] == -1)
            {
                _activityArr[i] = 1;
                _activatedValue++;
                Debug.Log("active image");
                if (_activityArr[_activityArr.Length - 1] != -1)
                {
                    craftComplete = true;
                    Debug.Log("craft complete activated value :"+ _activatedValue);
                    stoneManager.ButtonEnable(_optionIndex, false);
                    ActivenumberSync();
                }
                if (per_25)
                {
                    return;
                }
                stoneManager.SetPresentPercent(false);
                return;
            }
        }
        return;
    }

    private void DeActive(bool per_75)
    {
        _audioSource.clip = failureClip;
        _audioSource.Play();
        for (int i = 0; i < _activityArr.Length; i++)
        {
            if (_activityArr[i] == -1)
            {
                _activityArr[i] = 0;

                Debug.Log("deactive image");

                if (_activityArr[_activityArr.Length - 1] != -1)
                {
                    craftComplete = true;
                    Debug.Log("craft complete activated value :" + _activatedValue);
                    stoneManager.ButtonEnable(_optionIndex, false);
                    ActivenumberSync();
                }
                if (per_75)
                {
                    return;
                }
                stoneManager.SetPresentPercent(true);
                return;
            }
        }
        //활성도 -1
    }

    public void Calculate()
    {
        int random = UnityEngine.Random.Range(1, 100);
        int presentPercent = stoneManager.GetPresentPercentValue();
        Debug.Log("percent = " + random);
        switch (presentPercent)         
        {
            case 25:
                if (random >= 1 && random <= 25)
                {
                    Debug.Log("25 세공 성공. random : " + random);
                    //값 증가 +1
                    //성공이미지로 변경
                    Active(true);
                    break;
                }
                else 
                {
                    Debug.Log("25 세공 실패. random : " + random);
                    DeActive(false);
                    break;
                }
            case 35:
                if (random >= 1 && random <= 35)
                {
                    Debug.Log("35 세공 성공. random : " + random);
                    //값증가
                    Active(false);
                    break;
                }
                else 
                {
                    Debug.Log("35 세공 실패. random : " + random);
                    DeActive(false);
                    break;
                }
            case 45:
                if (random >= 1 && random <= 45)
                {
                    Debug.Log("45 세공 성공. random : " + random);
                    Active(false);
                    break;
                }
                else
                {
                    Debug.Log("45 세공 실패. random : " + random);
                    DeActive(false);
                    break;
                }
            case 55:
                if (random >= 1 && random <= 55)
                {
                    Debug.Log("55 세공 성공. random : " + random);
                    Active(false);
                    break;
                }
                else 
                {
                    Debug.Log("55 세공 실패. random : " + random);
                    DeActive(false);
                    break;
                }
            case 65:
                if (random >= 1 && random <= 65)
                {
                    Debug.Log("65 세공 성공. random : " + random);
                    Active(false);
                    break;
                }
                else 
                {
                    Debug.Log("65 세공 실패. random : " + random);
                    DeActive(false);
                    break;
                }
            case 75:
                if (random >= 1 && random <= 75)
                {
                    Debug.Log("75 세공 성공. random : " + random);
                    Active(false);
                    break;
                }
                else 
                {
                    Debug.Log("75 세공 실패. random : " + random);
                    DeActive(true);
                    break;
                }
        }
        presentPercent = stoneManager.GetPresentPercentValue();
        ImageSync();
    }

    private void ImageSync()
    {
        for (int i = 0; i < _activityArr.Length; i++)
        {
            if (_activityArr[i] == 1)
            {
                if (reduceEffect)
                {
                    _imageContainer[i].sprite = redSuccessImage.sprite;
                }
                else
                {
                    _imageContainer[i].sprite = successImage.sprite;
                }
            }
            if (_activityArr[i] == 0)
            {
                if (reduceEffect)
                {
                    _imageContainer[i].sprite = redFailureImage.sprite;
                }
                else
                {
                    _imageContainer[i].sprite = failureImage.sprite;
                }
            }
        }
    }

    public int ActivenumberSync()
    {
        activatedValue = _activatedValue;
        return activatedValue; 
    }
}
