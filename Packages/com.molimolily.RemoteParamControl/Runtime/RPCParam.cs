using System;
using UnityEngine;

namespace RPC
{
    [Serializable]
    public abstract class RPCParamBase
    {
        public string name;
        public RPCLayoutType layoutType;
        public string address;

        public float min;
        public float max;

        public RPCParamBase(string name, RPCLayoutType layoutType, string address)
        {
            this.name = name;
            this.layoutType = layoutType;
            this.address = address;
        }

        public abstract object GetValue();
        public abstract void SetValue(object value);
    }

    public class RPCParamFloat : RPCParamBase
    {
         public float value;

        public RPCParamFloat(string name, RPCLayoutType layoutType, float value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = Convert.ToSingle(value);
        }
    }

    public class RPCParamInt : RPCParamBase
    {
         public int value;

        public RPCParamInt(string name, RPCLayoutType layoutType, int value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = Convert.ToInt32(value);
        }
    }

    public class RPCParamVector2 : RPCParamBase
    {
         public Vector2 value;

        public RPCParamVector2(string name, RPCLayoutType layoutType, Vector2 value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Vector2 vec)
            {
                this.value = vec;
            }
            else
            {
                Debug.LogError("Invalid value type for Vector2");
            }
        }
    }

    public class RPCParamVector2Int : RPCParamBase
    {
         public Vector2Int value;

        public RPCParamVector2Int(string name, RPCLayoutType layoutType, Vector2Int value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Vector2Int vec)
            {
                this.value = vec;
            }
            else
            {
                Debug.LogError("Invalid value type for Vector2Int");
            }
        }
    }

    public class RPCParamVector3 : RPCParamBase
    {
         public Vector3 value;

        public RPCParamVector3(string name, RPCLayoutType layoutType, Vector3 value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Vector3 vec)
            {
                this.value = vec;
            }
            else
            {
                Debug.LogError("Invalid value type for Vector3");
            }
        }
    }

    public class RPCParamVector3Int : RPCParamBase
    {
         public Vector3Int value;

        public RPCParamVector3Int(string name, RPCLayoutType layoutType, Vector3Int value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Vector3Int vec)
            {
                this.value = vec;
            }
            else
            {
                Debug.LogError("Invalid value type for Vector3Int");
            }
        }
    }

    public class RPCParamVector4 : RPCParamBase
    {
         public Vector4 value;

        public RPCParamVector4(string name, RPCLayoutType layoutType, Vector4 value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Vector4 vec)
            {
                this.value = vec;
            }
            else
            {
                Debug.LogError("Invalid value type for Vector4");
            }
        }
    }

    public class RPCParamColor : RPCParamBase
    {
         public Color value;

        public RPCParamColor(string name, RPCLayoutType layoutType, Color value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Color color)
            {
                this.value = color;
            }
            else
            {
                Debug.LogError("Invalid value type for Color");
            }
        }
    }

    public class RPCParamRect : RPCParamBase
    {
         public Rect value;

        public RPCParamRect(string name, RPCLayoutType layoutType, Rect value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is Rect rect)
            {
                this.value = rect;
            }
            else
            {
                Debug.LogError("Invalid value type for Rect");
            }
        }
    }

    public class RPCParamRectInt : RPCParamBase
    {
         public RectInt value;

        public RPCParamRectInt(string name, RPCLayoutType layoutType, RectInt value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is RectInt rect)
            {
                this.value = rect;
            }
            else
            {
                Debug.LogError("Invalid value type for RectInt");
            }
        }
    }

    public class RPCParamBool : RPCParamBase
    {
         public bool value;

        public RPCParamBool(string name, RPCLayoutType layoutType, bool value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = Convert.ToBoolean(value);
        }
    }

    public class RPCParamString : RPCParamBase
    {
          public string value;

        public RPCParamString(string name, RPCLayoutType layoutType, string value, string address) : base(name, layoutType, address)
        {
            this.value = value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override void SetValue(object value)
        {
            this.value = value as string;
        }
    }

}