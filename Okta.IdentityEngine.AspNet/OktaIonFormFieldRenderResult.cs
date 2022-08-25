using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Ion;

namespace Okta.IdentityEngine.AspNetCore
{
    public class OktaIonFormFieldRenderResult : OktaSignInViewRenderResult
    {
        public OktaIonFormFieldRenderResult(OktaSignInRenderContext context, IonFormField ionFormField): base(context)
        {
            this.IonFormField = ionFormField;
        }
        
        protected MvcIdentityStateRenderer MvcIdentityStateModelRenderer 
        {
            get => (MvcIdentityStateRenderer)RenderContext.Renderer;
        }

        public IonFormField IonFormField { get; private set; }

        public override string Render(string id)
        {
            if (IonFormField.IsForm(out IonForm form))
            {
                OktaIonFormRenderResult formRenderResult = new OktaIonFormRenderResult(RenderContext, form) { RenderSubmit = false };
                return formRenderResult.Render(id);
            }

            // TODO: reference sign in widget for appropriate markup
            Tag container = new Tag("div");
            Tag label = new Tag("label", new { @for = id });
            if (IonFormField.Label != null)
            {
                label.Text(IonFormField.Label).Attr("for", id);
            }
            Tag inputTag = new Tag("input").Id(id).Name(IonFormField.Name);
            if (IonFormField.Visible == null || IonFormField.Visible == true)
            {
                switch (IonFormField.TypeString)
                {
                    case "":
                    case "string":
                        inputTag.Type("text");
                        break;
                    case "boolean":
                        inputTag.Type("checkbox");
                        break;
                }
            }
            else
            {
                inputTag.Type("hidden");
            }
            container.AddHtml(label);
            container.AddHtml(inputTag);
            if (RenderContext.Options.Debug && IonFormField.Visible == false)
            {
                Tag debugLabel = new Tag("label").Text(IonFormField.Name);
                Tag readOnlyInput = new Tag("input").Id($"debug_{id}").Type("text").Attr("readonly", "readonly").Value(IonFormField.ValueString);
                container.AddHtml(debugLabel).AddHtml(readOnlyInput);
            }

            return container.Render();
        }

        protected override string GetId()
        {
            return IonFormField.Name ?? Guid.NewGuid().ToString();
        }
    }
}
