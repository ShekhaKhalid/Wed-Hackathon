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
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.Profiling;

namespace FronkonGames.Glitches.ColorBlindness
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Render Pass. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class ColorBlindness
  {
    [DisallowMultipleRendererFeature]
    private sealed class RenderPass : ScriptableRenderPass
    {
      private readonly Settings settings;

      private RenderTargetIdentifier colorBuffer;
      private RenderTextureDescriptor renderTextureDescriptor;

#if UNITY_2022_1_OR_NEWER
      private RTHandle renderTextureHandle0;

      private readonly ProfilingSampler profilingSamples = new(Constants.Asset.AssemblyName);
      private ProfilingScope profilingScope;
#else
      private int renderTextureHandle0;
      
      private static readonly ProfilerMarker ProfilerMarker = new($"{Constants.Asset.AssemblyName}.Pass.Execute");
#endif
      private readonly Material material;

      private const string CommandBufferName = Constants.Asset.AssemblyName;

      private static class ShaderIDs
      {
        internal static readonly int Intensity = Shader.PropertyToID("_Intensity");
        internal static readonly int DeltaTime = Shader.PropertyToID("_DeltaTime");

        internal static readonly int RedCoefficients = Shader.PropertyToID("_RedCoefficients");
        internal static readonly int GreenCoefficients = Shader.PropertyToID("_GreenCoefficients");
        internal static readonly int BlueCoefficients = Shader.PropertyToID("_BlueCoefficients");
        internal static readonly int Comparator = Shader.PropertyToID("_Comparator");
        internal static readonly int ComparatorSize = Shader.PropertyToID("_ComparatorSize");
        internal static readonly int ComparatorColor = Shader.PropertyToID("_ComparatorColor");

        internal static readonly int Brightness = Shader.PropertyToID("_Brightness");
        internal static readonly int Contrast = Shader.PropertyToID("_Contrast");
        internal static readonly int Gamma = Shader.PropertyToID("_Gamma");
        internal static readonly int Hue = Shader.PropertyToID("_Hue");
        internal static readonly int Saturation = Shader.PropertyToID("_Saturation");
      }

      private static class TextureIDs
      {
        internal static readonly int Source = Shader.PropertyToID("_SourceTex");
      }

      /// <summary> Render pass constructor. </summary>
      public RenderPass(Settings settings)
      {
        this.settings = settings;

        string shaderPath = $"Shaders/{Constants.Asset.ShaderName}_URP";
        Shader shader = Resources.Load<Shader>(shaderPath);
        if (shader != null)
        {
          if (shader.isSupported == true)
            material = CoreUtils.CreateEngineMaterial(shader);
          else
            Log.Warning($"'{shaderPath}.shader' not supported");
        }
      }

      /// <inheritdoc/>
      public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
      {
        renderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        renderTextureDescriptor.depthBufferBits = 0;

#if UNITY_2022_1_OR_NEWER
        colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;

        RenderingUtils.ReAllocateIfNeeded(ref renderTextureHandle0, renderTextureDescriptor, settings.filterMode, TextureWrapMode.Clamp, false, 1, 0, $"_RTHandle0_{Constants.Asset.Name}");
#else
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;

        renderTextureHandle0 = Shader.PropertyToID($"_RTHandle0_{Constants.Asset.Name}");
        cmd.GetTemporaryRT(renderTextureHandle0, renderTextureDescriptor.width, renderTextureDescriptor.height, renderTextureDescriptor.depthBufferBits, settings.filterMode, RenderTextureFormat.ARGB32);
#endif
      }

      /// <inheritdoc/>
      public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
      {
        if (material == null ||
            renderingData.postProcessingEnabled == false ||
            settings.intensity <= 0.0f ||
            settings.affectSceneView == false && renderingData.cameraData.isSceneViewCamera == true)
          return;

        CommandBuffer cmd = CommandBufferPool.Get(CommandBufferName);

        if (settings.enableProfiling == true)
#if UNITY_2022_1_OR_NEWER
          profilingScope = new ProfilingScope(cmd, profilingSamples);
#else
          ProfilerMarker.Begin();
#endif

        material.shaderKeywords = null;
        material.SetFloat(ShaderIDs.Intensity, settings.intensity);
        material.SetFloat(ShaderIDs.DeltaTime, settings.ignoreDeltaTimeScale == true ? Time.unscaledDeltaTime : Time.deltaTime);

        switch (settings.deficiency)
        {
          case Deficiency.Protanomaly:
            material.SetVector(ShaderIDs.RedCoefficients, ProtanomalyRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, ProtanomalyGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, ProtanomalyBlueCoefficient);
            break;
          case Deficiency.Deuteranomaly:
            material.SetVector(ShaderIDs.RedCoefficients, DeuteranomalyRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, DeuteranomalyGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, DeuteranomalyBlueCoefficient);
            break;
          case Deficiency.Tritanomaly:
            material.SetVector(ShaderIDs.RedCoefficients, TritanomalyRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, TritanomalyGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, TritanomalyBlueCoefficient);
            break;
          case Deficiency.Protanopia:
            material.SetVector(ShaderIDs.RedCoefficients, ProtanopiaRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, ProtanopiaGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, ProtanopiaBlueCoefficient);
            break;
          case Deficiency.Deuteranopia:
            material.SetVector(ShaderIDs.RedCoefficients, DeuteranopiaRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, DeuteranopiaGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, DeuteranopiaBlueCoefficient);
            break;
          case Deficiency.Tritanopia:
            material.SetVector(ShaderIDs.RedCoefficients, TritanopiaRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, TritanopiaGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, TritanopiaBlueCoefficient);
            break;
          case Deficiency.Achromatopsia:
            material.SetVector(ShaderIDs.RedCoefficients, AchromatopsiaCoefficients);
            material.SetVector(ShaderIDs.GreenCoefficients, AchromatopsiaCoefficients);
            material.SetVector(ShaderIDs.BlueCoefficients, AchromatopsiaCoefficients);
            break;
          case Deficiency.Achromatomaly:
            material.SetVector(ShaderIDs.RedCoefficients, AchromatomalyRedCoefficient);
            material.SetVector(ShaderIDs.GreenCoefficients, AchromatomalyGreenCoefficient);
            material.SetVector(ShaderIDs.BlueCoefficients, AchromatomalyBlueCoefficient);
            break;
        }

        material.SetFloat(ShaderIDs.Comparator, settings.comparator);
        material.SetFloat(ShaderIDs.ComparatorSize, settings.comparator < 1.0f ? settings.comparatorSize : 0.0f);
        material.SetColor(ShaderIDs.ComparatorColor, settings.comparatorColor);

        material.SetFloat(ShaderIDs.Brightness, settings.brightness);
        material.SetFloat(ShaderIDs.Contrast, settings.contrast);
        material.SetFloat(ShaderIDs.Gamma, 1.0f / settings.gamma);
        material.SetFloat(ShaderIDs.Hue, settings.hue);
        material.SetFloat(ShaderIDs.Saturation, settings.saturation);

        cmd.SetGlobalTexture(TextureIDs.Source, colorBuffer);

#if UNITY_2022_1_OR_NEWER
        cmd.Blit(colorBuffer, renderTextureHandle0, material);
        cmd.Blit(renderTextureHandle0, colorBuffer);
#else
        Blit(cmd, colorBuffer, renderTextureHandle0, material);
        Blit(cmd, renderTextureHandle0, colorBuffer);
#endif

        if (settings.enableProfiling == true)
#if UNITY_2022_1_OR_NEWER
          profilingScope.Dispose();
#else
          ProfilerMarker.End();
#endif

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
      }

#if UNITY_2022_1_OR_NEWER
      private void Dispose()
      {
        renderTextureHandle0?.Release();
      }
#else
      /// <inheritdoc/>
      public override void FrameCleanup(CommandBuffer cmd)
      {
        if (renderTextureHandle0 != -1)
          cmd.ReleaseTemporaryRT(renderTextureHandle0);
      }
#endif
    }
  }
}
