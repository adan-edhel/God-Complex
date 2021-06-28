using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class VrPlayerUI : MonoBehaviour
{
    [SerializeField] private XRController _leftHand;
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private TextMeshProUGUI _goldText;

    [SerializeField] private int _delayAmount = 1; // Second count

    private float _goldAmount;
    private float _goldIncreaseValue = 10f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _goldAmount = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDeviceAtXRNode(_leftHand.controllerNode).TryGetFeatureValue(CommonUsages.gripButton, out bool gripping);
        if (gripping) _uiCanvas.SetActive(true);
        else _uiCanvas.SetActive(false);

        //if (Input.GetKey(KeyCode.K)) _uiCanvas.SetActive(true);

        _timer += Time.deltaTime;

        if (_timer >= _delayAmount)
        {
            _timer = 0f;
            GenerateGold();
        }
    }

    private void GenerateGold()
    {
        _goldAmount = _goldAmount + _goldIncreaseValue;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _goldText.text = "Gold: " + (int)_goldAmount;
    }

    public float GetGoldAmount()
    {
        return _goldAmount;
    }

    public void RemoveGoldAmount(float amount)
    {
        _goldAmount -= amount;
    }

    public void AddGold(float amount)
    {
        _goldAmount = _goldAmount + amount;
    }
}
