using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RPCSample : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private TextMeshProUGUI intText;
    private TextMeshProUGUI floatText;
    private Slider slider;
    private Slider intSlider;
    private TextMeshProUGUI stringText;
    private TextMeshProUGUI vector2Text;
    private TextMeshProUGUI vector2IntText;
    private TextMeshProUGUI vector3Text;
    private TextMeshProUGUI vector3IntText;
    private TextMeshProUGUI vector4Text;
    private Image colorImage;
    private TextMeshProUGUI rectText;
    private TextMeshProUGUI rectIntText;
    private Toggle boolToggle;

    public int intVal = 0;
    public float floatVal = 0.0f;
    [Range(0.0f, 1.0f)] public float sliderVal = 0.0f;
    [Range(0, 10)] public int intSliderVal = 0;
    public string stringVal = "Hello, World!";
    public Vector2 vector2Val = new Vector2(0.0f, 0.0f);
    public Vector2 vector2IntVal = new Vector2Int(0, 0);
    public Vector3 vector3Val = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 vector3IntVal = new Vector3Int(0, 0, 0);
    public Vector4 vector4Val = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
    public Color colorVal = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    public Rect rectVal = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
    public RectInt rectIntVal = new RectInt(0, 0, 0, 0);
    public bool boolVal = false;
    public RPC.RPCLayoutType layoutType;

    // Start is called before the first frame update
    void Start()
    {
        intText = canvas.transform.Find("Panel/Int/Value").GetComponent<TextMeshProUGUI>();
        floatText = canvas.transform.Find("Panel/Float/Value").GetComponent<TextMeshProUGUI>();
        slider = canvas.transform.Find("Panel/Slider/Value").GetComponent<Slider>();
        intSlider = canvas.transform.Find("Panel/IntSlider/Value").GetComponent<Slider>();
        stringText = canvas.transform.Find("Panel/String/Value").GetComponent<TextMeshProUGUI>();
        vector2Text = canvas.transform.Find("Panel/Vector2/Value").GetComponent<TextMeshProUGUI>();
        vector2IntText = canvas.transform.Find("Panel/Vector2Int/Value").GetComponent<TextMeshProUGUI>();
        vector3Text = canvas.transform.Find("Panel/Vector3/Value").GetComponent<TextMeshProUGUI>();
        vector3IntText = canvas.transform.Find("Panel/Vector3Int/Value").GetComponent<TextMeshProUGUI>();
        vector4Text = canvas.transform.Find("Panel/Vector4/Value").GetComponent<TextMeshProUGUI>();
        colorImage = canvas.transform.Find("Panel/Color/Value").GetComponent<Image>();
        rectText = canvas.transform.Find("Panel/Rect/Value").GetComponent<TextMeshProUGUI>();
        rectIntText = canvas.transform.Find("Panel/RectInt/Value").GetComponent<TextMeshProUGUI>();
        boolToggle = canvas.transform.Find("Panel/Bool/Value").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        intText.text = intVal.ToString();
        floatText.text = floatVal.ToString();
        slider.value = sliderVal;
        intSlider.value = intSliderVal;
        stringText.text = stringVal;
        vector2Text.text = vector2Val.ToString();
        vector2IntText.text = vector2IntVal.ToString();
        vector3Text.text = vector3Val.ToString();
        vector3IntText.text = vector3IntVal.ToString();
        vector4Text.text = vector4Val.ToString();
        colorImage.color = colorVal;
        rectText.text = rectVal.ToString();
        rectIntText.text = rectIntVal.ToString();
        boolToggle.isOn = boolVal;
    }
}
