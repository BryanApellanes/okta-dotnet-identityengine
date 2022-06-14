using Microsoft.AspNetCore.Mvc;
using Okta.IdentityEngine.Ion;

namespace Okta.IdentityEngine.AspNetCore
{
    public class OktaIonFormRenderResult : OktaSignInViewRenderResult
    {
        public OktaIonFormRenderResult(OktaSignInRenderContext context, IonForm ionForm):base(context)
        {
            this.IonForm = ionForm ?? throw new ArgumentNullException(nameof(ionForm));
            this.RenderSubmit = true;
        }

        protected MvcIdentityStateRenderer MvcIdentityStateModelRenderer 
        {
            get => (MvcIdentityStateRenderer)RenderContext.Renderer;
        }

        public IonForm IonForm { get; private set; }
        public bool RenderSubmit { get; set; }

        public override string Render(string id)
        {
            Tag form = new Tag("form", new { action = $"{RenderContext.MiddlewarePath}/{IonForm.Name}", method = IonForm.Method })
                .Id(id);
            Tag fieldSet = new Tag("fieldset");
            if (IonForm.Name != null)
            {
                fieldSet.AddHtml(new Tag("legend").Text(IonForm.Name));
            };                

            foreach(IonFormField formField in IonForm.Fields)
            {
                OktaIonFormFieldRenderResult formFieldRenderResult = new OktaIonFormFieldRenderResult(RenderContext, formField);
                fieldSet.AddHtml(formFieldRenderResult.Render($"{id}_{formField.Name}"));
            }
            if (RenderSubmit)
            {
                fieldSet.AddHtml(new Tag("input").Type("submit").Value("Next"));
            }
            form.AddHtml(fieldSet);
            return form.Render();
        }

        protected override string GetId()
        {
            return IonForm.Name;
        }
    }
}
