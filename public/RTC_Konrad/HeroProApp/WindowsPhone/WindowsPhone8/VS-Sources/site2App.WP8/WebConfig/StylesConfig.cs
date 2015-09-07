using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class StylesConfig
    {
        public StylesConfig()
        {
            HiddenElements = new List<string>();
        }

        [DataMember(Name = "setViewport")]
        public bool SetViewport { get; set; }

        public double TargetWidth { get; set; }
        [DataMember(Name = "targetWidth")]
        public string TargetWidthString { get; set; }

        public double TargetHeight { get; set; }
        [DataMember(Name = "targetHeight")]
        public string TargetHeightString { get; set; }

        [DataMember(Name = "supressTouchAction")]
        public bool SupressTouch { get; set; }

        [DataMember(Name = "hiddenElements")]
        public List<string> HiddenElements { get; set; }

        [DataMember(Name = "customCssString")]
        public string CustomCSS { get; set; }

        // When complete, invoke this string into the browser window
        public string GetInvokeString()
        {
            StringBuilder _sb = new StringBuilder();
            // set up the viewport style
            if (SetViewport)
            {
                _sb.Append("@-ms-viewport {");
                if (TargetWidthString.Length != 0)
                    _sb.Append("width: " + TargetWidthString + ";");
                if (TargetHeightString.Length != 0)
                    _sb.Append("height: " + TargetHeightString + ";");
                _sb.Append("}");
            }

            //set up the supress touch action
            if (SupressTouch)
                _sb.Append("body{-ms-touch-action:none;}");

            // identify the hidden elements
            if (HiddenElements.Count != 0)
            {
                foreach (string s in HiddenElements)
                    _sb.Append(s + ",");
                _sb.Remove(_sb.Length - 2, 1);
                _sb.Append("{display:none !important;}");
            }

            // Add arbitrary CSS string to the end
            if (CustomCSS.Length != 0)
                _sb.Append(CustomCSS);

            string returnCSS = "var cssString = '" + _sb.ToString() + "';var styleEl = document.createElement('style');document.body.appendChild(styleEl);styleEl.innerHTML = cssString;";

            return returnCSS;
        }
    }
}
