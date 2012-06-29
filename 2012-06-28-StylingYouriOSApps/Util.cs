using System;
namespace Xaminar
{

    public enum VisualMode
    {
        None,
        TintColor,
        UIAppearance
    }
    public static class Util
    {
        public static VisualMode VisualMode = VisualMode.None;

        public static bool UseTintColor
        {
            get { return VisualMode == VisualMode.TintColor; }
        }

        public static bool UseUIAppearance
        {
            get { return VisualMode == VisualMode.UIAppearance; }
        }
    }
}

