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

namespace FronkonGames.Glitches.ColorBlindness
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Enums. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class ColorBlindness
  {
    /// <summary> Color vision deficiency. </summary>
    public enum Deficiency
    {
      /// <summary> Red-green vision deficiency, 1% of males, 0.03% of females. </summary>
      Protanomaly,

      /// <summary> Red-green color blindness, 6% of males, 0.4% of females. </summary>
      Deuteranomaly,

      /// <summary> Blue-yellow color blindness, 0.01% for males and females. </summary>
      Tritanomaly,

      /// <summary> Reds are greatly reduced, 1% of males, 0.02% of females. </summary>
      Protanopia,

      /// <summary> Greens are greatly reduced, 1% of males. </summary>
      Deuteranopia,

      /// <summary> Blues are greatly reduced, 0.003% population. </summary>
      Tritanopia,

      /// <summary> Total color blindness, 0.001% population. </summary>
      Achromatopsia,

      /// <summary> Partial color blindness, 0.00001% population. </summary>
      Achromatomaly,
    }
  }
}
