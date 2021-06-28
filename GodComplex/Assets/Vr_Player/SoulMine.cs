using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMine : MonoBehaviour
{
    [SerializeField] private VrPlayerUI _uiObject;

    [SerializeField] private int _delayAmount = 1; // Second count

    private float _goldAmount;
    private float _goldIncreaseValue = 20f;
    private float _timer;
    private bool _ableToMine;

    // Start is called before the first frame update
    void Start()
    {
        _uiObject = GameObject.Find("LeftHand Controller").GetComponent<VrPlayerUI>();
        _ableToMine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<StickUnitToGround>().placed)
        {
            _ableToMine = true;
            //GetComponent<StickUnitToGround>().enabled = false;
        }

        if (_ableToMine) MineGold();

    }

    private void MineGold()
    {
        _timer += Time.deltaTime;

        if (_timer >= _delayAmount)
        {
            _timer = 0f;
            _uiObject.AddGold(_goldIncreaseValue);
        }
    }
}
