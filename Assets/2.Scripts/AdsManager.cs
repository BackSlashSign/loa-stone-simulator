using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using Unity.VisualScripting;

public class AdsManager : MonoBehaviour
{
    private AdBanner adBanner;
    private AdInterstitial adInterstitial;

    private void Awake()
    {
        adBanner = GetComponent<AdBanner>();
        adInterstitial= GetComponent<AdInterstitial>();
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => {/* This callback is called once the MobileAds SDK is initialized.*/});
    }

    public void ShowAdInterstitial()
    {
        adInterstitial.ShowAdInterstitial();
    }
    public void ShowAdBanner()
    {
        adBanner.RequestBanner();
    }

}
