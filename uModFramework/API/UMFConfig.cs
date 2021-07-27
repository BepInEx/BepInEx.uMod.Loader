using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UModFramework.API
{
    public class UMFConfig : IDisposable
    {
        protected string ConfigName { get; set; }

        public UMFConfig()
        {
            ConfigName = Assembly.GetCallingAssembly().GetName().Name;
        }

        // really can't be assed implementing this right now
        public T Read<T>(string key, UMFConfigParser<T> parser, params string[] comments)
        {
            return parser.Parse(parser.Default());
        }

        public void Write<T>(string key, UMFConfigParser<T> parser, params string[] comments)
        {
            // do nothing
        }

        public void DeleteConfig()
        {
            // do nothing
        }

        public void DeleteConfig(bool backupOld = false)
        {
            // do nothing
        }

        public void Dispose() { }
    }

    public abstract class UMFConfigParser<T>
    {
        public virtual T Parse(string value)
        {
            return default;
        }

        public abstract override string ToString();

        public virtual string Default()
        {
            return null;
        }

        public virtual string Range()
        {
            return null;
        }

        public virtual string Vanilla()
        {
            return null;
        }

        public virtual string Allowed()
        {
            return null;
        }

        public virtual string Restart()
        {
            return null;
        }

        public virtual string IsKeyBind()
        {
            return null;
        }
    }

    public class UMFConfigReadOnly<T> : UMFConfigParser<T>
    {
        public override T Parse(string value)
        {
            return (T)value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => (T)Convert.ChangeType(x, typeof(T)));
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }

    public class UMFConfigString : UMFConfigParser<string>
    {
        public UMFConfigString(string defaultValue = "", string vanillaValue = "", bool isKeyBind = false, bool requiresRestart = false, params string[] allowedValues)
        {
            DefaultValue = defaultValue.Trim();
            VanillaValue = vanillaValue.Trim();
            KeyBind = isKeyBind;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override string Parse(string value) => value.Trim();

        public override string ToString() => DefaultValue;

        public override string Default() => DefaultValue;

        public override string Vanilla() => VanillaValue;

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues);
        }

        public override string Restart() => RequiresRestart.ToString();

        public override string IsKeyBind() => KeyBind.ToString();

        public string DefaultValue;

        public string VanillaValue;

        public bool KeyBind;

        public bool RequiresRestart;

        public string[] AllowedValues;
    }

    public class UMFConfigStringArray : UMFConfigParser<string[]>
    {
        public UMFConfigStringArray(string[] defaultValues = null, bool isKeyBind = false, bool requiresRestart = false, params string[] allowedValues)
        {
            DefaultValues = defaultValues;
            KeyBind = isKeyBind;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override string[] Parse(string value)
            => value.Trim().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

        public override string ToString() => string.Join(",", DefaultValues);

        public override string Default()
        {
            return string.Join(", ", DefaultValues.Take(2).ToArray());
        }

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues);
        }

        public override string Restart() => RequiresRestart.ToString();

        public override string IsKeyBind() => KeyBind.ToString();

        public string[] DefaultValues;

        public bool KeyBind;

        public bool RequiresRestart;

        public string[] AllowedValues;
    }

    public class UMFConfigBool : UMFConfigParser<bool>
    {
        public UMFConfigBool(bool? defaultValue = null, bool? vanillaValue = null, bool requiresRestart = false)
        {
            DefaultValue = defaultValue;
            VanillaValue = vanillaValue;
            RequiresRestart = requiresRestart;
        }

        public override bool Parse(string value)
            => bool.TryParse(value, out var parsedBool) ? parsedBool : DefaultValue.GetValueOrDefault();

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Vanilla() => VanillaValue.ToString();

        public override string Restart() => RequiresRestart.ToString();

        public bool? DefaultValue;

        public bool? VanillaValue;

        public bool RequiresRestart;
    }

    public class UMFConfigInt : UMFConfigParser<int>
    {
        public UMFConfigInt(int defaultValue = 0, int minValue = 0, int maxValue = 0, int vanillaValue = 0, bool requiresRestart = false, params int[] allowedValues)
        {
            DefaultValue = defaultValue;
            MinValue = Math.Min(minValue, maxValue);
            MaxValue = Math.Max(minValue, maxValue);
            VanillaValue = vanillaValue;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override int Parse(string value)
            => int.TryParse(value, out var parsedInt) ? parsedInt : DefaultValue;

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Range()
        {
            if (MaxValue == 0)
                return null;

            return MinValue + "," + MaxValue;
        }

        public override string Vanilla()
        {
            if (VanillaValue == 0)
                return null;

            return VanillaValue.ToString();
        }

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues.Select(x => x.ToString()).ToArray());
        }

        public override string Restart() => RequiresRestart.ToString();

        public int DefaultValue;

        public int MinValue;

        public int MaxValue;

        public int VanillaValue;

        public bool RequiresRestart;

        public int[] AllowedValues;
    }

    public class UMFConfigFloat : UMFConfigParser<float>
    {
        public UMFConfigFloat(float defaultValue = 0f, float minValue = 0f, float maxValue = 0f, int decimals = 1, float vanillaValue = 0f, bool requiresRestart = false, params float[] allowedValues)
        {
            DefaultValue = defaultValue;
            MinValue = Math.Min(minValue, maxValue);
            MaxValue = Math.Max(minValue, maxValue);
            Decimals = decimals;
            VanillaValue = vanillaValue;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override float Parse(string value)
            => float.TryParse(value, out var parsedFloat) ? parsedFloat : DefaultValue;

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Range()
        {
            if (MaxValue == 0f)
                return null;

            return $"{MinValue},{MaxValue},{Decimals}";
        }

        public override string Vanilla()
        {
            if (VanillaValue == 0f)
                return null;

            return VanillaValue.ToString();
        }

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues.Select(x => x.ToString()).ToArray());
        }

        public override string Restart() => RequiresRestart.ToString();

        public float DefaultValue;

        public float MinValue;

        public float MaxValue;

        public int Decimals;

        public float VanillaValue;

        public bool RequiresRestart;

        public float[] AllowedValues;
    }

    public class UMFConfigDouble : UMFConfigParser<double>
    {
        public UMFConfigDouble(double defaultValue = 0.0, double minValue = 0.0, double maxValue = 0.0, int decimals = 2, double vanillaValue = 0.0, bool requiresRestart = false, params double[] allowedValues)
        {
            DefaultValue = defaultValue;
            MinValue = Math.Min(minValue, maxValue);
            MaxValue = Math.Max(minValue, maxValue);
            Decimals = decimals;
            VanillaValue = vanillaValue;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override double Parse(string value)
            => double.TryParse(value, out var parsedDouble) ? parsedDouble : DefaultValue;

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Range()
        {
            if (MaxValue == 0.0)
                return null;

            return $"{MinValue},{MaxValue},{Decimals}";
        }

        public override string Vanilla()
        {
            if (VanillaValue == 0.0)
                return null;

            return VanillaValue.ToString();
        }

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues.Select(x => x.ToString()).ToArray());
        }

        public override string Restart() => RequiresRestart.ToString();

        public double DefaultValue;

        public double MinValue;

        public double MaxValue;

        public int Decimals;

        public double VanillaValue;

        public bool RequiresRestart;

        public double[] AllowedValues;
    }

    public class UMFConfigVersion : UMFConfigParser<Version>
    {
        public UMFConfigVersion(Version defaultValue = null, bool requiresRestart = false)
        {
            DefaultValue = defaultValue;
            RequiresRestart = requiresRestart;
        }

        public override Version Parse(string value) => DefaultValue;

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Restart() => RequiresRestart.ToString();

        public Version DefaultValue;

        public bool RequiresRestart;
    }

    public class UMFConfigKeyCode : UMFConfigParser<KeyCode>
    {
        public UMFConfigKeyCode(KeyCode defaultValue = KeyCode.None, bool requiresRestart = false, params KeyCode[] allowedValues)
        {
            DefaultValue = defaultValue;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override KeyCode Parse(string value)
            => (KeyCode)Enum.Parse(typeof(KeyCode), value.Trim(), true);

        public override string ToString() => DefaultValue.ToString();

        public override string Default() => DefaultValue.ToString();

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues.Select(x => x.ToString()).ToArray());
        }

        public override string Restart() => RequiresRestart.ToString();

        public KeyCode DefaultValue;

        public bool RequiresRestart;

        public KeyCode[] AllowedValues;
    }

    public class UMFConfigKeyCodeArray : UMFConfigParser<KeyCode[]>
    {
        public UMFConfigKeyCodeArray(KeyCode[] defaultValues = null, bool requiresRestart = false, params KeyCode[] allowedValues)
        {
            DefaultValues = defaultValues;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override KeyCode[] Parse(string value)
        {
            return value.Trim()
                .Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => (KeyCode)Enum.Parse(typeof(KeyCode), x, true))
                .ToArray();
        }

        public override string ToString()
        {
            return string.Join(",", DefaultValues.Select(x => x.ToString()).ToArray());
        }

        public override string Default()
        {
            return string.Join(", ", DefaultValues.Select(x => x.ToString()).ToArray(), 0, 2);
        }

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues.Select(x => x.ToString()).ToArray());
        }

        public override string Restart() => RequiresRestart.ToString();

        public KeyCode[] DefaultValues;

        public bool RequiresRestart;

        public KeyCode[] AllowedValues;
    }

    public class UMFConfigDirectory : UMFConfigParser<string>
    {
        public UMFConfigDirectory(string defaultValue = "", bool requiresRestart = false, params string[] allowedValues)
        {
            DefaultValue = defaultValue;
            RequiresRestart = requiresRestart;
            AllowedValues = allowedValues;
        }

        public override string Parse(string value)
        {
            if (!Directory.Exists(value))
            {
                try
                {
                    Directory.CreateDirectory(value);
                }
                catch { }
            }

            if (Directory.Exists(value))
                return value;

            return DefaultValue;
        }

        public override string ToString() => DefaultValue;

        public override string Default() => DefaultValue;

        public override string Allowed()
        {
            if (AllowedValues.Length == 0)
                return null;

            return string.Join(",", AllowedValues);
        }

        public override string Restart() => RequiresRestart.ToString();

        public string DefaultValue;

        public bool RequiresRestart;

        public string[] AllowedValues;
    }

    public class UMFConfigColorHexRGBA : UMFConfigParser<Color>
    {
        public UMFConfigColorHexRGBA(Color defaultValues = default, bool requiresRestart = false)
        {
            DefaultValue = defaultValues;
            RequiresRestart = requiresRestart;
        }

        public override Color Parse(string value)
        {
            return ColorUtility.TryParseHtmlString(value, out var result)
                ? result
                : Color.white;
        }

        public override string ToString()
            => "#" + ColorUtility.ToHtmlStringRGBA(DefaultValue);

        public override string Default()
            => "#" + ColorUtility.ToHtmlStringRGBA(DefaultValue);

        public override string Restart() => RequiresRestart.ToString();

        public Color DefaultValue;

        public bool RequiresRestart;
    }
}