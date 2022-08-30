using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace Okta.IdentityEngine.AspNet
{
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// Adds a style entry
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TagBuilder Style(this TagBuilder tagBuilder, string name, string value)
        {
            return tagBuilder.Css(name, value);
        }

        /// <summary>
        /// Adds the specified class name to the tagbuilder if no value is specified.
        /// Adds a style entry if a value is specified.
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="styleOrClassName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TagBuilder Css(this TagBuilder tagBuilder, string styleOrClassName, string value = null)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (tagBuilder.Attributes.ContainsKey("style"))
                {
                    string current = tagBuilder.Attributes["style"];
                    tagBuilder.Attributes["style"] = string.Format("{0}{1}:{2};", current, styleOrClassName, value);
                }
                else
                {
                    tagBuilder.Attributes.Add("style", string.Format("{0}:{1};", styleOrClassName, value));
                }
            }
            else
            {
                tagBuilder.AddCssClass(styleOrClassName);
            }
            return tagBuilder;
        }

        public static TagBuilder AttrIf(this TagBuilder tagBuilder, bool condition, string name, string value)
        {
            if (condition)
            {
                return tagBuilder.Attr(name, value);
            }
            else
            {
                return tagBuilder;
            }
        }

        public static TagBuilder Attr(this TagBuilder tagBuilder, string name, string value, bool underscoreToDashes = true)
        {
            if (underscoreToDashes)
            {
                name = name.Replace("_", "-");
            }
            tagBuilder.MergeAttribute(name, value, true);
            return tagBuilder;
        }


        public static TagBuilder AddHtml(this TagBuilder tagBuilder, string html)
        {
            tagBuilder.InnerHtml.AppendHtml(html);
            return tagBuilder;
        }

        public static TagBuilder Html(this TagBuilder tagBuilder, string html)
        {
            tagBuilder.InnerHtml.SetHtmlContent(html);
            return tagBuilder;
        }

        public static TagBuilder Text(this TagBuilder tagBuilder, string text)
        {
            tagBuilder.InnerHtml.AppendLine(text);
            return tagBuilder;
        }

        public static string Render(this TagBuilder tagBuilder)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            IHtmlContent startTag = tagBuilder.RenderStartTag();
            IHtmlContent? body = tagBuilder.RenderBody();
            IHtmlContent endTag = tagBuilder.RenderEndTag();
            startTag.WriteTo(streamWriter, HtmlEncoder.Default);
            if (body != null)
            {
                body.WriteTo(streamWriter, HtmlEncoder.Default);
            }
            endTag.WriteTo(streamWriter, HtmlEncoder.Default);
            streamWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            return Encoding.UTF8.GetString(memoryStream.GetBuffer());
        }

        /// <summary>
        /// Wraps the current TagBuilder in the specified tagName and returns
        /// the parent.
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static TagBuilder Parent(this TagBuilder tagBuilder, string tagName)
        {
            return new TagBuilder(tagName).Child(tagBuilder);
        }

        public static TagBuilder Child(this TagBuilder tagBuilder, string html)
        {
            tagBuilder.InnerHtml.AppendHtmlLine(html);
            return tagBuilder;
        }

        /// <summary>
        /// Appends the specified child to the current TagBuilder
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static TagBuilder Child(this TagBuilder tagBuilder, TagBuilder child)
        {
            return tagBuilder.Child(child.Render());
        }

        public static TagBuilder ChildIf(this TagBuilder tagBuilder, bool condition, string child)
        {
            if (condition)
            {
                tagBuilder.Child(child);
            }

            return tagBuilder;
        }

        public static TagBuilder ChildIf(this TagBuilder tagBuilder, bool condition, TagBuilder child)
        {
            if (condition)
            {
                tagBuilder.Child(child);
            }

            return tagBuilder;
        }


        public static TagBuilder Select(this TagBuilder tagBuilder)
        {
            return tagBuilder.Attr("selected", "selected");
        }

        /// <summary>
        /// Adds selected="selected" if the condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static TagBuilder SelectIf(this TagBuilder tagBuilder, bool condition)
        {
            return tagBuilder.AttrIf(condition, "selected", "selected");
        }

        public static TagBuilder CheckedIf(this TagBuilder tagBuilder, bool condition)
        {
            return tagBuilder.AttrIf(condition, "checked", "checked");
        }
        /// <summary>
        /// Adds attribute type="radio"
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <returns></returns>
        public static TagBuilder Radio(this TagBuilder tagBuilder)
        {
            return tagBuilder.Attr("type", "radio");
        }

        /// <summary>
        /// Adds a label element as a child using the specified forName and text.
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="forName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TagBuilder Label(this TagBuilder tagBuilder, string forName, string text)
        {
            return tagBuilder.Child(new TagBuilder("label").Attr("for", forName).Text(text));
        }

        public static TagBuilder Value(this TagBuilder tagBuilder, string value)
        {
            return tagBuilder.Attr("value", value);
        }

        public static string Id(this TagBuilder tagBuilder)
        {
            return tagBuilder.Attributes["id"];
        }

        public static TagBuilder Id(this TagBuilder tagBuilder, string id)
        {
            return tagBuilder.Attr("id", id);
        }

        public static TagBuilder IdIf(this TagBuilder tagBuilder, bool condition, string id)
        {
            if (condition)
            {
                tagBuilder.Id(id);
            }

            return tagBuilder;
        }

        public static TagBuilder IdIfNone(this TagBuilder tagBuilder, string id)
        {
            string existing = tagBuilder.Attributes.ContainsKey("id") ? tagBuilder.Attributes["id"] : string.Empty;
            return tagBuilder.IdIf(string.IsNullOrEmpty(existing), id);
        }

        public static TagBuilder Name(this TagBuilder tagBuilder, string name)
        {
            return tagBuilder.Attr("name", name);
        }

        public static TagBuilder ValueIf(this TagBuilder tagBuilder, bool condition, string value)
        {
            return tagBuilder.AttrIf(condition, "value", value);
        }

        public static TagBuilder Type(this TagBuilder tagBuilder, string value)
        {
            return tagBuilder.Attr("type", value);
        }

        /// <summary>
        /// Applies all the attributes of the specified object
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static TagBuilder Attrs(this TagBuilder tagBuilder, object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return tagBuilder;
            }
            Type t = htmlAttributes.GetType();
            foreach (PropertyInfo prop in t.GetProperties())
            {
                tagBuilder.Attr(prop.Name.Replace("_", "-"), (string)prop.GetValue(htmlAttributes, null));
            }

            return tagBuilder;
        }

        /// <summary>
        /// Adds the specified htmlAttributes if the condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static TagBuilder AttrsIf(this TagBuilder tagBuilder, bool condition, object htmlAttributes)
        {
            if (condition)
            {
                tagBuilder.Attrs(htmlAttributes);
            }

            return tagBuilder;
        }
    }
}
