using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Okta.IdentityEngine.AspNet;
using FluentAssertions;

namespace Okta.IdentityEngine.Tests
{
    public class TagShould
    {
        [Fact]
        public void AcceptText()
        {
            Tag tag = new Tag("div", new { name = "the-name" })
                .Text("hello");

            string output = tag.Render();

            output.Should().Be("<div name=\"the-name\">hello</div>");
        }

        [Fact]
        public void AcceptHtml()
        {
            Tag tag = new Tag("div")
                    .Html(new Tag("span")
                        .Text("inner span"));
        
            string output = tag.Render();

            output.Should().Be("<div><span>inner span</span></div>");
        }

        [Fact]
        public void HaveDataAttributes()
        {
            Tag data = new Tag("div", new { data_monkey = "the data" });

            string output = data.Render();

            output.Should().Be("<div data-monkey=\"the data\"></div>");
        }

        [Fact]
        public void AddDataAttribute()
        {
            Tag data = new Tag("span").Data("monkey", "see no evil");

            string output = data.Render();

            output.Should().Be("<span data-monkey=\"see no evil\"></span>");
        }

        [Fact]
        public void AcceptChildTagInConstructor()
        {
            Tag tag = new Tag("div", new Tag("span", new { id = "inner-id" }).Text("inside span"));

            string output = tag.Render();

            output.Should().Be("<div><span id=\"inner-id\">inside span</span></div>");
        }

        [Fact]
        public void AddEventHandler()
        {
            Tag tag = new Tag("button").Click("doTheThings");

            string output = tag.Render();

            output.Should().Be("<button onclick=\"doTheThings\"></button>");
        }

        [Fact]
        public void AddUncheckedRadioButton()
        {
            Tag tag = new Tag("div").Radio();

            string output = tag.Render();

            output.Should().Be("<div><input type=\"radio\"></input></div>");
        }

        [Fact]
        public void AddCheckedRadioButton()
        {
            Tag tag = new Tag("li").Radio(true);

            string output = tag.Render();

            output.Should().Be("<li><input checked=\"checked\" type=\"radio\"></input></li>");
        }

        [Fact]
        public void AddUncheckedCheckBox()
        {
            Tag tag = new Tag("p").CheckBox();

            string output = tag.Render();

            output.Should().Be("<p><input type=\"checkbox\"></input></p>");
        }

        [Fact]
        public void AddCheckedCheckBox()
        {
            Tag tag = new Tag("p").CheckBox(true);

            string output = tag.Render();

            output.Should().Be("<p><input checked=\"checked\" type=\"checkbox\"></input></p>");
        }

        [Fact]
        public void AddInputOfSpecifiedType()
        {
            Tag tag = new Tag("div").Input("button");

            string output = tag.Render();

            output.Should().Be("<div><input type=\"button\"></input></div>");
        }

        [Fact]
        public void AddTextArea()
        {
            Tag tag = new Tag("div").TextArea();

            string output = tag.Render();

            output.Should().Be("<div><textarea cols=\"40\" rows=\"10\"></textarea></div>");
        }

        [Fact]
        public void AddTextAreaWithContent()
        {
            Tag tall = new Tag("div").TextArea("text area content", 60);

            string tallOutput = tall.Render();

            tallOutput.Should().Be("<div><textarea cols=\"40\" rows=\"60\">text area content</textarea></div>");
        }

        [Fact]
        public void AddTextBox()
        {
            Tag tag = new Tag("div").TextBox();

            string output = tag.Render();

            output.Should().Be("<div><input type=\"text\"></input></div>");
        }

        [Fact]
        public void AddTextBoxWithValue()
        {
            Tag tag = new Tag("div").TextBox("text value");

            string output = tag.Render();

            output.Should().Be("<div><input type=\"text\" value=\"text value\"></input></div>");
        }

        [Fact]
        public void AddStyle()
        {
            Tag tag = new Tag("div").Style("margin-left: 8px").Text("monkey");

            string output = tag.Render();

            output.Should().Be("<div style=\"margin-left: 8px\">monkey</div>");
        }
    }
}
