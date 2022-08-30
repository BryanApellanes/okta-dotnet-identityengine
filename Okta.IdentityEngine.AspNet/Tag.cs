using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace Okta.IdentityEngine.AspNet
{
    public class Tag : IHtmlContent
    {
        TagBuilder _html;
        public Tag(string tagName, object? attributes = null)
        {
            _html = new TagBuilder(tagName);
            if (attributes != null)
            {
                Attrs(attributes);
            }
        }

        public Tag(string tagName, Tag child): this(tagName)
        {
            Child(child);
        }

        public static implicit operator string(Tag e)
        {
            if(e != null)
            {
                return e.Render();
            }
            return string.Empty;
        }

        protected TagBuilder TagBuilder
        {
            get
            {
                return _html;
            }
        }

        public Tag AttrFormat(string name, string valueFormat, params object[] values)
        {
            return Attr(name, string.Format(valueFormat, values));
        }

        public Tag Attr(string name, string value)
        {
            _html.Attr(name, value);
            return this;
        }

        public Tag AttrIf(bool condition, string name, string value)
        {
            if(condition)
            {
                _html.Attributes.Add(name, value);
            }
            return this;
        }

        public Tag Attrs(object attributes)
        {
            if (attributes != null)
            {
                Type attrType = attributes.GetType();
                PropertyInfo[] props = attrType.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object? val = prop.GetValue(attributes, null);
                    _html.AttrIf(val != null, prop.Name, (string)val);
                }
            }

            return this;
        }

        public Tag Attrs(Dictionary<string, string> attrs)
        {
            foreach (string key in attrs.Keys)
            {
                _html.AttrIf(!string.IsNullOrWhiteSpace(attrs[key]), key, attrs[key]);
            }

            return this;
        }

        public Tag Id(string id)
        {
            _html.Id(id);
            return this;
        }

        public string Id()
        {
            return _html.Id();
        }

        public Tag Radio(object? attributes = null)
        {
            return Radio(false, attributes);
        }

        public Tag Radio(bool chked, object? attributes = null)
        {
            Tag radio = new Tag("input", attributes).Type("radio");
            if (chked)
            {
                radio.Attrs(new { @checked = "checked" });
            }

            return SubTag(radio);
        }

        public Tag CheckBox(object? attributes = null)
        {
            return CheckBox(false, attributes);
        }

        public Tag CheckBox(bool chked, object? attributes = null)
        {
            Tag checkBox = new Tag("input", attributes).Type("checkbox");
            if (chked)
            {
                checkBox.Attrs(new { @checked = "checked" });
            }
            return SubTag(checkBox);
        }

        public Tag TextArea(string? value = null, int rows = 10, int cols = 40, object attributes = null)
        {
            Tag textArea = new Tag("textarea", new { rows = rows.ToString(), cols = cols.ToString() })
                .Attrs(attributes);
            if (!string.IsNullOrEmpty(value))
            {
                textArea.Text(value);
            }

            return SubTag(textArea);
        }

        public Tag TextBox(string? value = null, object? attributes = null)
        {
            Tag textBox = new Tag("input", attributes).Type("text");
            if (!string.IsNullOrEmpty(value))
            {
                textBox.Value(value);
            }
            return SubTag(textBox);
        }

        public Tag Input(string type, object? attributes = null)
        {
            return SubTag(new Tag("input", attributes).Type(type));
        }

        public Tag Name(string name)
        {
            return Attrs(new { name = name });
        }

        public Tag Value(string value)
        {
            return Attrs(new { value = value });
        }

        public Tag Type(string type)
        {
            return Attrs(new { type = type });
        }

        public Tag ChildIf(bool condition, Tag tag)
        {
            if (condition)
            {
                Child(tag);
            }

            return this;
        }

        public Tag AddHtml(Tag tag)
        {
            return Child(tag);
        }


        /// <summary>
        /// Same as SubTag.  Adds a subtag to the current Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Tag Child(string tagName, string text)
        {
            return SubTag(tagName, text);
        }

        /// <summary>
        /// Same as Child.  Adds a subtag to the current
        /// Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Tag SubTag(string tagName, string text)
        {
            _html.Child(new TagBuilder(tagName).Text(text));
            return this;
        }

        public Tag Child(Tag tag)
        {
            return SubTag(tag);
        }

        public Tag SubTag(Tag tag)
        {
            _html.Child(tag.TagBuilder);
            return this;
        }

        public Tag CssClassIf(bool condition, string className)
        {
            if (condition)
            {
                CssClass(className);
            }

            return this;
        }

        public Tag CssClass(string className)
        {
            _html.AddCssClass(className);
            return this;
        }

        public Tag Style(string value)
        {
            _html.Attr("style", value);
            return this;
        }

        public Tag Text(string text)
        {
            _html.Text(text);
            return this;
        }

        public Tag Html(Tag tag)
        {
            return Html(tag.TagBuilder);
        }

        public Tag Html(TagBuilder tagBuilder)
        {
            return Html(tagBuilder.Render());
        }

        public Tag AddHtmlIf(bool condition, Tag tag)
        {
            if (condition)
            {
                return AddHtml(tag);
            }
            return this;
        }

        public Tag AddHtmlIf(bool condition, string html)
        {
            if (condition)
            {
                return AddHtml(html);
            }
            return this;
        }

        public Tag AddHtml(string html)
        {
            _html.AddHtml(html);
            return this;
        }

        public Tag Html(string html)
        {
            _html.Html(html);
            return this;
        }

        public Tag DataClick(string dataClick)
        {
            _html.AttrIf(!string.IsNullOrWhiteSpace(dataClick), "data-click", dataClick);
            return this;
        }

        public Tag Data(string name, string value)
        {
            _html.AttrIf(!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value), string.Format("data-{0}", name), value);
            return this;
        }

        /// <summary>
        /// Adds an onabort attribute with the specified value
        /// </summary>
        /// <param name="abort">The value</param>
        /// <returns>The Tag</returns>
        public Tag Abort(string abort)
        {
            return this.On("abort", abort);
        }

        /// <summary>
        /// Adds an onblur attribute with the specified value
        /// </summary>
        /// <param name="blur">The value</param>
        /// <returns>The Tag</returns>
        public Tag Blur(string blur)
        {
            return this.On("blur", blur);
        }


        /// <summary>
        /// Adds an onchange attribute with the specified value
        /// </summary>
        /// <param name="change">The value</param>
        /// <returns>The Tag</returns>
        public Tag Change(string change)
        {
            return this.On("change", change);
        }

        /// <summary>
        /// Adds an onclick attribute with the specified value
        /// </summary>
        /// <param name="click">The value</param>
        /// <returns>The Tag</returns>
        public Tag Click(string click)
        {
            return this.On("click", click);
        }

        /// <summary>
        /// Adds an ondblclick attribute with the specified value
        /// </summary>
        /// <param name="dblclick">The value</param>
        /// <returns>The Tag</returns>
        public Tag DblClick(string dblclick)
        {
            return this.On("dblclick", dblclick);
        }

        /// <summary>
        /// Adds an onerror attribute with the specified value
        /// </summary>
        /// <param name="error">The value</param>
        /// <returns>The Tag</returns>
        public Tag Error(string error)
        {
            return this.On("error", error);
        }

        /// <summary>
        /// Adds an onfocus attribute with the specified value
        /// </summary>
        /// <param name="focus">The value</param>
        /// <returns>The Tag</returns>
        public Tag Focus(string focus)
        {
            return this.On("focus", focus);
        }

        /// <summary>
        /// Adds an onkeydown attribute with the specified value
        /// </summary>
        /// <param name="keydown">The value</param>
        /// <returns>The Tag</returns>
        public Tag Keydown(string keydown)
        {
            return this.On("keydown", keydown);
        }

        /// <summary>
        /// Adds an onkeypress attribute with the specified value
        /// </summary>
        /// <param name="keypress">The value</param>
        /// <returns>The Tag</returns>
        public Tag Keypress(string keypress)
        {
            return this.On("keypress", keypress);
        }

        /// <summary>
        /// Adds an onkeyup attribute with the specified value
        /// </summary>
        /// <param name="keyup">The value</param>
        /// <returns>The Tag</returns>
        public Tag Keyup(string keyup)
        {
            return this.On("keyup", keyup);
        }

        /// <summary>
        /// Adds an onload attribute with the specified value
        /// </summary>
        /// <param name="load">The value</param>
        /// <returns>The Tag</returns>
        public Tag Load(string load)
        {
            return this.On("load", load);
        }

        /// <summary>
        /// Adds an onmousedown attribute with the specified value
        /// </summary>
        /// <param name="mousedown">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mousedown(string mousedown)
        {
            return this.On("mousedown", mousedown);
        }

        /// <summary>
        /// Adds an onmousemove attribute with the specified value
        /// </summary>
        /// <param name="mousemove">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mousemove(string mousemove)
        {
            return this.On("mousemove", mousemove);
        }

        /// <summary>
        /// Adds an onmouseout attribute with the specified value
        /// </summary>
        /// <param name="mouseout">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mouseout(string mouseout)
        {
            return this.On("mouseout", mouseout);
        }

        /// <summary>
        /// Adds an onmouseover attribute with the specified value
        /// </summary>
        /// <param name="mouseover">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mouseover(string mouseover)
        {
            return this.On("mouseover", mouseover);
        }

        /// <summary>
        /// Adds an onmouseup attribute with the specified value
        /// </summary>
        /// <param name="mouseup">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mouseup(string mouseup)
        {
            return this.On("mouseup", mouseup);
        }
        /// <summary>
        /// Adds an onreset attribute with the specified value
        /// </summary>
        /// <param name="reset">The value</param>
        /// <returns>The Tag</returns>
        public Tag Reset(string reset)
        {
            return this.On("reset", reset);
        }
        /// <summary>
        /// Adds an onresize attribute with the specified value
        /// </summary>
        /// <param name="resize">The value</param>
        /// <returns>The Tag</returns>
        public Tag Resize(string resize)
        {
            return this.On("resize", resize);
        }

        /// <summary>
        /// Adds an onunload attribute with the specified value
        /// </summary>
        /// <param name="unload">The value</param>
        /// <returns>The Tag</returns>
        public Tag Unload(string unload)
        {
            return this.On("unload", unload);
        }

        /// <summary>
        /// Wraps the current tag in a tag of the specified type and returns
        /// the wrapper tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Tag Wrap(string tagName)
        {
            return new Tag(tagName).Html(this);
        }

        static internal List<string> events = new List<string>(new string[] { "abort", "blur", "change", "click",
                                   "dblclick", "error", "focus", "keydown",
                                   "keypress", "keyup", "load", "mousedown",
                                   "mousemove", "mouseout", "mouseover", "mouseup",
                                   "reset", "resize", "select", "submit", "unload"});

        public Tag On(string eventName, string value)
        {
            if (!events.Contains(eventName))
            {
                throw new InvalidOperationException($"The specified eventName is invalid: {eventName}");
            }

            _html.AttrIf(!string.IsNullOrWhiteSpace(value), string.Format("on{0}", eventName), value);
            return this;
        }

        public string Render(Encoding encoding = null)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            WriteTo(streamWriter, HtmlEncoder.Default);
            streamWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            Encoding enc = encoding ?? Encoding.UTF8;

            return enc.GetString(TrimBuffer(memoryStream.GetBuffer()).ToArray());
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            _html.TagRenderMode = TagRenderMode.Normal;
            _html.WriteTo(writer, encoder);
        }

        private IEnumerable<byte> TrimBuffer(byte[] buffer)
        {
            foreach (byte b in buffer)
            {
                if (b != 0)
                {
                    yield return b;
                }
            }
        }
    }
}
