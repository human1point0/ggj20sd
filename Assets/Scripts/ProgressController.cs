using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressController : MonoBehaviour
{
    public Transform winTransform;
    public Transform player;
    public Sprite[] sprites;

    private Slider _slider;
    private Image _handleImage;
    private float distance = 100;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _handleImage = _slider.handleRect.GetComponent<Image>();
    }


    private void OnEnable()
    {
        _slider.value = 0;
        distance = Mathf.Abs(player.position.z - winTransform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        var current = Mathf.Abs(player.position.z - winTransform.position.z);
        _slider.value = 1 - Mathf.Clamp01(current / distance);
        UpdateIcon();        
    }

    void UpdateIcon()
    {
        int index = Mathf.Clamp( (int)(sprites.Length * _slider.value), 0, sprites.Length - 1);
        _handleImage.sprite = sprites[index];
    }
}
