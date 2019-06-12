using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using HtmlAgilityPack;
using SecurityWeakness.Core.Extensibility;

namespace SecurityWeakness.Core
{
    internal class HtmlSanitizer : IHtmlSanitizer
    {
        private readonly HashSet<string> BlackListDefault =
            new HashSet<string>  {
            {"script"},
            {"iframe"},
            {"form"} ,
            {"object"},
            {"embed"},
            {"link"},
            {"head"},
            {"meta"}
        };

        public string SanitizeHtml(string html, params string[] blackList)
        {
            return Sanitize(html, new HashSet<string>(blackList));
        }

        public string Sanitize(string html)
        {
            return Sanitize(html, BlackListDefault);
        }

        private string Sanitize(string html, HashSet<string> blackList)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(html);


            SanitizeHtmlNode(doc.DocumentNode, blackList);

            //return doc.DocumentNode.WriteTo();

            string output = null;

            // Use an XmlTextWriter to create self-closing tags
            using (StringWriter sw = new StringWriter())
            {
                XmlWriter writer = new XmlTextWriter(sw);
                doc.DocumentNode.WriteTo(writer);
                output = sw.ToString();

                // strip off XML doc header
                if (!string.IsNullOrEmpty(output))
                {
                    int at = output.IndexOf("?>");
                    output = output.Substring(at + 2);
                }

                writer.Close();
            }
            doc = null;
            return output;
        }

        private void SanitizeHtmlNode(HtmlNode node, HashSet<string> blackList)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                // check for blacklist items and remove
                if (blackList.Contains(node.Name))
                {
                    node.Remove();
                    return;
                }

                if (node.Name == "style")
                {
                    if (string.IsNullOrEmpty(node.InnerText))
                    {
                        if (node.InnerHtml.Contains("expression") || node.InnerHtml.Contains("javascript:"))
                            node.ParentNode.RemoveChild(node);
                    }
                }

                if (node.HasAttributes)
                {
                    for (int i = node.Attributes.Count - 1; i >= 0; i--)
                    {
                        HtmlAttribute currentAttribute = node.Attributes[i];

                        var attr = currentAttribute.Name.ToLower();
                        var val = currentAttribute.Value.ToLower();

                        if (attr.StartsWith("on"))
                            node.Attributes.Remove(currentAttribute);

                        else if (
                                 //(attr == "href" || attr== "src" || attr == "dynsrc" || attr == "lowsrc") &&
                                 val != null &&
                                 val.Contains("javascript:"))
                            node.Attributes.Remove(currentAttribute);

                        else if (attr == "style" &&
                                 val != null &&
                                 val.Contains("expression") || val.Contains("javascript:") || val.Contains("vbscript:"))
                            node.Attributes.Remove(currentAttribute);
                    }
                }
            }

            if (node.HasChildNodes)
            {
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--)
                {
                    SanitizeHtmlNode(node.ChildNodes[i], blackList);
                }
            }
        }
    }
}
