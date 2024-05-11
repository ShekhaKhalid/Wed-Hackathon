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

namespace FronkonGames.Glitches.ColorBlindness
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Coefficients. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class ColorBlindness
  {
    private static Vector3 ProtanomalyRedCoefficient = new(0.81667f, 0.18333f, 0.0f);
    private static Vector3 ProtanomalyGreenCoefficient = new(0.33333f, 0.66667f, 0.0f);
    private static Vector3 ProtanomalyBlueCoefficient = new(0.0f, 0.125f, 0.875f);

    private static Vector3 DeuteranomalyRedCoefficient = new(0.8f, 0.2f, 0.0f);
    private static Vector3 DeuteranomalyGreenCoefficient = new(0.25833f, 0.74167f, 0.0f);
    private static Vector3 DeuteranomalyBlueCoefficient = new(0.0f, 0.14167f, 0.85833f);

    private static Vector3 TritanomalyRedCoefficient = new(0.96667f, 0.3333f, 0.0f);
    private static Vector3 TritanomalyGreenCoefficient = new(0.0f, 0.73333f, 0.26667f);
    private static Vector3 TritanomalyBlueCoefficient = new(0.0f, 0.18333f, 0.81667f);

    private static Vector3 ProtanopiaRedCoefficient = new(0.56667f, 0.43333f, 0.0f);
    private static Vector3 ProtanopiaGreenCoefficient = new(0.55833f, 0.44167f, 0.0f);
    private static Vector3 ProtanopiaBlueCoefficient = new(0.0f, 0.24167f, 0.75833f);

    private static Vector3 DeuteranopiaRedCoefficient   = new(0.625f, 0.375f, 0.0f);
    private static Vector3 DeuteranopiaGreenCoefficient = new(0.7f, 0.3f, 0.0f);
    private static Vector3 DeuteranopiaBlueCoefficient  = new(0.0f, 0.3f, 0.7f);

    private static Vector3 TritanopiaRedCoefficient = new(0.95f, 0.05f, 0.0f);
    private static Vector3 TritanopiaGreenCoefficient = new(0.0f, 0.43333f, 0.56667f);
    private static Vector3 TritanopiaBlueCoefficient = new(0.0f, 0.475f, 0.525f);

    private static Vector3 AchromatopsiaCoefficients = new(0.299f, 0.587f, 0.114f);

    private static Vector3 AchromatomalyRedCoefficient = new(0.618f, 0.32f, 0.062f);
    private static Vector3 AchromatomalyGreenCoefficient = new(0.163f, 0.775f, 0.062f);
    private static Vector3 AchromatomalyBlueCoefficient = new(0.163f, 0.32f, 0.516f);
  }
}
