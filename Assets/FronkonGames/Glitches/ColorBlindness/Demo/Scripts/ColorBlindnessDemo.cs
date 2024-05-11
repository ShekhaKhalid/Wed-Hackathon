using System;
using UnityEngine;
using FronkonGames.Glitches.ColorBlindness;

/// <summary> Glitches: Color Blindness demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class ColorBlindnessDemo : MonoBehaviour
{
  private bool removeEffect;
  private ColorBlindness.Settings settings;

  private GUIStyle styleTitle;
  private GUIStyle styleLabel;
  private GUIStyle styleButton;

  private static readonly string[] DeficiencyDescription =
  {
    "Red-green vision deficiency, 1% of males, 0.03% of females",
    "Red-green color blindness, 6% of males, 0.4% of females",
    "Blue-yellow color blindness, 0.01% population",
    "Reds are greatly reduced, 1% of males, 0.02% of females",
    "Greens are greatly reduced, 1% of males",
    "Blues are greatly reduced, 0.003% population",
    "Total color blindness, 0.001% population",
    "Partial color blindness, 0.00001% population"
  };

  private void ResetEffect()
  {
    settings.ResetDefaultValues();
    settings.intensity = 1.0f;
    settings.comparator = 0.5f;
  }

  private void Awake()
  {
    if (ColorBlindness.IsInRenderFeatures() == false)
    {
      removeEffect = true;
      ColorBlindness.AddRenderFeature();
    }

    this.enabled = ColorBlindness.IsInRenderFeatures();
  }

  private void Start()
  {
    settings = ColorBlindness.GetSettings();
    ResetEffect();
  }

  private void OnGUI()
  {
    styleTitle = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.LowerCenter,
      fontSize = 32,
      fontStyle = FontStyle.Bold
    };

    styleLabel = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.UpperLeft,
      fontSize = 24
    };

    styleButton = new GUIStyle(GUI.skin.button)
    {
      fontSize = 24
    };

    GUILayout.BeginHorizontal();
    {
      GUILayout.BeginVertical("box", GUILayout.Width(450.0f), GUILayout.Height(Screen.height));
      {
        const float space = 10.0f;

        GUILayout.Space(space);

        GUILayout.Label("COLOR BLINDNESS DEMO", styleTitle);

        GUILayout.Space(space);

        settings.intensity = SliderField("Intensity", settings.intensity);

        GUILayout.Space(space * 2.0f);

        GUILayout.BeginHorizontal("box");
        {
          GUILayout.Label(DeficiencyDescription[(int)settings.deficiency], styleTitle);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(space);

        settings.deficiency = EnumField("Deficiency", settings.deficiency);
        settings.comparator = SliderField("Comparator", settings.comparator);

        GUILayout.Space(space * 2.0f);

        if (GUILayout.Button("RESET", styleButton) == true)
          ResetEffect();

        GUILayout.FlexibleSpace();
      }
      GUILayout.EndVertical();

      GUILayout.FlexibleSpace();
    }
    GUILayout.EndHorizontal();
  }

  private void OnDestroy()
  {
    settings.ResetDefaultValues();

    if (removeEffect == true)
      ColorBlindness.RemoveRenderFeature();
  }

  private bool ToogleField(string label, bool value)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = GUILayout.Toggle(value, string.Empty);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private float SliderField(string label, float value, float min = 0.0f, float max = 1.0f)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private int SliderField(string label, int value, int min, int max)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = (int)GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private Color ColorField(string label, Color value, bool alpha = true)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      float originalAlpha = value.a;

      UnityEngine.Color.RGBToHSV(value, out float h, out float s, out float v);
      h = GUILayout.HorizontalSlider(h, 0.0f, 1.0f);
      value = UnityEngine.Color.HSVToRGB(h, s, v);

      if (alpha == false)
        value.a = originalAlpha;
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private Vector3 Vector3Field(string label, Vector3 value, string x = "X", string y = "Y", string z = "Z", float min = 0.0f, float max = 1.0f)
  {
    GUILayout.Label(label, styleLabel);

    value.x = SliderField($"   {x}", value.x, min, max);
    value.y = SliderField($"   {y}", value.y, min, max);
    value.z = SliderField($"   {z}", value.z, min, max);

    return value;
  }

  private T EnumField<T>(string label, T value) where T : Enum
  {
    string[] names = System.Enum.GetNames(typeof(T));
    Array values = System.Enum.GetValues(typeof(T));
    int index = Array.IndexOf(values, value);

    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      if (GUILayout.Button("<", styleButton) == true)
        index = index > 0 ? index - 1 : values.Length - 1;

      GUILayout.Label(names[index], styleLabel);

      if (GUILayout.Button(">", styleButton) == true)
        index = index < values.Length - 1 ? index + 1 : 0;
    }
    GUILayout.EndHorizontal();

    return (T)(object)index;
  }
}
