using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{
    [SerializeField] private float _inputTimeToLive;
    [SerializeField] private List<CheatItem> _cheatItems;

    private string _currentInput;
    private float _inputTime;

    private void Awake()
    {
        Keyboard.current.onTextInput += onTextInput;
    }
    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= onTextInput;
    }

    private void onTextInput(char symbol)
    {
        _currentInput += symbol;
        _inputTime = _inputTimeToLive;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheatItem in _cheatItems)
        {
            if (_currentInput.Contains(cheatItem.Name))
            {
                cheatItem.Action?.Invoke();
                _currentInput = string.Empty;
            }
        }
    }

    private void Update()
    {
        if (_inputTime < 0)
        {
            _currentInput = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }
}
 
[Serializable]
public class CheatItem
{
    public string Name;
    public UnityEvent Action;
}