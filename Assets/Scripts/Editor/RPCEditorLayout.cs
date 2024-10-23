using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPC
{
    public abstract class RPCEditorLayout<T>
    {
        public GUIContent Label { get; protected set; }
        public T Value { get; protected set; }
        public T prevValue { get; protected set; }

        public delegate void ValueChangedHandler(T newValue);
        public event ValueChangedHandler OnValueChanged;

        public RPCEditorLayout(GUIContent label, T value)
        {
            this.Label = label;
            this.Value = value;
        }

        protected abstract void Update();

        protected void CheckAndTriggerValueChanged()
        {
            if (!EqualityComparer<T>.Default.Equals(prevValue, Value))
            {
                OnValueChanged?.Invoke(Value);
            }
        }
        public virtual void Draw()
        {
            this.prevValue = this.Value;
            Update();
            CheckAndTriggerValueChanged();
        }

    }

    public class RPCSlider : RPCEditorLayout<float>
    {
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }

        public RPCSlider(GUIContent label, float value, float minValue, float maxValue) : base(label, value)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public RPCSlider(string label, float value, float minValue, float maxValue) : this(new GUIContent(label), value, minValue, maxValue)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Slider(Label, Value, MinValue, MaxValue);
        }
    }

    public class RPCIntSlider : RPCEditorLayout<int>
    {
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        public RPCIntSlider(GUIContent label, int value, int minValue, int maxValue) : base(label, value)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public RPCIntSlider(string label, int value, int minValue, int maxValue) : this(new GUIContent(label), value, minValue, maxValue)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.IntSlider(Label, Value, MinValue, MaxValue);
        }
    }

    public class RPCToggle : RPCEditorLayout<bool>
    {
        public RPCToggle(GUIContent label, bool value) : base(label, value)
        {
        }

        public RPCToggle(string label, bool value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Toggle(Label, Value);
        }
    }

    public class RPCFloat : RPCEditorLayout<float>
    {
        public RPCFloat(GUIContent label, float value) : base(label, value)
        {
        }

        public RPCFloat(string label, float value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.FloatField(Label, Value);
        }
    }

    public class RPCInt : RPCEditorLayout<int>
    {
        public RPCInt(GUIContent label, int value) : base(label, value)
        {
        }

        public RPCInt(string label, int value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.IntField(Label, Value);
        }
    }

    public class RPCVector2 : RPCEditorLayout<Vector2>
    {
        public RPCVector2(GUIContent label, Vector2 value) : base(label, value)
        {
        }

        public RPCVector2(string label, Vector2 value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Vector2Field(Label, Value);
        }
    }

    public class RPCVector2Int : RPCEditorLayout<Vector2Int>
    {
        public RPCVector2Int(GUIContent label, Vector2Int value) : base(label, value)
        {
        }

        public RPCVector2Int(string label, Vector2Int value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Vector2IntField(Label, Value);
        }
    }

    public class RPCVector3 : RPCEditorLayout<Vector3>
    {
        public RPCVector3(GUIContent label, Vector3 value) : base(label, value)
        {
        }

        public RPCVector3(string label, Vector3 value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Vector3Field(Label, Value);
        }
    }

    public class RPCVector3Int : RPCEditorLayout<Vector3Int>
    {
        public RPCVector3Int(GUIContent label, Vector3Int value) : base(label, value)
        {
        }

        public RPCVector3Int(string label, Vector3Int value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Vector3IntField(Label, Value);
        }
    }

    public class RPCVector4 : RPCEditorLayout<Vector4>
    {
        public RPCVector4(GUIContent label, Vector4 value) : base(label, value)
        {
        }

        public RPCVector4(string label, Vector4 value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.Vector4Field(Label, Value);
        }
    }

    public class RPCColorField : RPCEditorLayout<Color>
    {
        public RPCColorField(GUIContent label, Color value) : base(label, value)
        {
        }

        public RPCColorField(string label, Color value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.ColorField(Label, Value);
        }
    }

    public class RPCRectField : RPCEditorLayout<Rect>
    {
        public RPCRectField(GUIContent label, Rect value) : base(label, value)
        {
        }

        public RPCRectField(string label, Rect value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.RectField(Label, Value);
        }
    }

    public class RPCRectIntField : RPCEditorLayout<RectInt>
    {
        public RPCRectIntField(GUIContent label, RectInt value) : base(label, value)
        {
        }

        public RPCRectIntField(string label, RectInt value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.RectIntField(Label, Value);
        }
    }

    public class RPCTextField : RPCEditorLayout<string>
    {
        public RPCTextField(GUIContent label, string value) : base(label, value)
        {
        }

        public RPCTextField(string label, string value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = EditorGUILayout.TextField(Label, Value);
        }
    }

    public class RPCEnumPopup<T> : RPCEditorLayout<T>
    {
        public RPCEnumPopup(GUIContent label, T value) : base(label, value)
        {
        }

        public RPCEnumPopup(string label, T value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = (T)(object)EditorGUILayout.EnumPopup(Label, (System.Enum)(object)Value);
        }
    }

    public class RPCLayoutObjectField<T> : RPCEditorLayout<T> where T : Object
    {
        public RPCLayoutObjectField(GUIContent label, T value) : base(label, value)
        {
        }

        public RPCLayoutObjectField(string label, T value) : this(new GUIContent(label), value)
        {
        }

        protected override void Update()
        {
            Value = (T)(object)EditorGUILayout.ObjectField(Label, (Object)(object)Value, typeof(T), true);
        }
    }
}