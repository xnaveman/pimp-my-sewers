using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private Slider _slider1;
    [SerializeField] private TextMeshProUGUI _sliderText1;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("000");
        });

        _slider1.onValueChanged.AddListener((v) => {
            _sliderText1.text = v.ToString("F2");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
