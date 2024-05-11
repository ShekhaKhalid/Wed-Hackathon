////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using static FronkonGames.Glitches.ColorBlindness.Inspector;

namespace FronkonGames.Glitches.ColorBlindness.Editor
{
  /// <summary> Color Blindness inspector. </summary>
  [CustomPropertyDrawer(typeof(ColorBlindness.Settings))]
  public class SparkFeatureSettingsDrawer : Drawer
  {
    private ColorBlindness.Settings settings;

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

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<ColorBlindness.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 0.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Color Blindness.
      /////////////////////////////////////////////////
      Separator();

      settings.deficiency = (ColorBlindness.Deficiency)EnumPopup("Deficiency", "Color vision deficiency.", settings.deficiency, ColorBlindness.Deficiency.Protanomaly);
      EditorGUILayout.BeginHorizontal();
      {
        FlexibleSpace();

        EditorGUILayout.TextArea(DeficiencyDescription[(int)settings.deficiency], Styles.miniLabelButton);
      }
      EditorGUILayout.EndHorizontal();

      settings.comparator = Slider("Comparator", "Divide the screen in two, on the left the normal view, on the right the view with some defect. [0, 1].", settings.comparator, 0.0f, 1.0f, 0.0f);
      IndentLevel++;
      settings.comparatorSize = Slider("Size", "Width of the comparator separation bar in pixels [0, 20]. Default 5.", settings.comparatorSize, 0, 20, 5);
      settings.comparatorColor = ColorField("Color", "Separation bar color.", settings.comparatorColor, ColorBlindness.Settings.DefaultComparatorColor);
      IndentLevel--;

      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

        settings.affectSceneView = Toggle("Affect the Scene View?", "Does it affect the Scene View?", settings.affectSceneView);
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "Filter mode. Default Bilinear.", settings.filterMode, FilterMode.Bilinear);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          "Render pass injection. Default BeforeRenderingPostProcessing.",
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
        settings.ignoreDeltaTimeScale = Toggle("Ignore delta time scale", "Set to true to ignore delta time scaling", settings.ignoreDeltaTimeScale);
        settings.enableProfiling = Toggle("Enable profiling", "Enable render pass profiling", settings.enableProfiling);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Misc.
      /////////////////////////////////////////////////
      Separator();

      BeginHorizontal();
      {
        if (MiniButton("documentation", "Online documentation") == true)
          Application.OpenURL(Constants.Support.Documentation);

        if (MiniButton("support", "Do you have any problem or suggestion?") == true)
          SupportWindow.ShowWindow();

        if (EditorPrefs.GetBool($"{Constants.Asset.AssemblyName}.Review") == false)
        {
          Separator();

          if (MiniButton("write a review <color=#800000>❤️</color>", "Write a review, thanks!") == true)
          {
            Application.OpenURL(Constants.Support.Store);

            EditorPrefs.SetBool($"{Constants.Asset.AssemblyName}.Review", true);
          }
        }

        FlexibleSpace();

        if (Button("Reset") == true)
          settings.ResetDefaultValues();
      }
      EndHorizontal();
    }
  }
}
