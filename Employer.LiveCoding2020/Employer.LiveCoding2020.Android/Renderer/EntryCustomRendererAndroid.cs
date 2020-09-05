using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Employer.LiveCoding2020.Control;
using Employer.LiveCoding2020.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryCustom), typeof(EntryCustomRendererAndroid))]
namespace Employer.LiveCoding2020.Droid.Renderer
{
    public class EntryCustomRendererAndroid : EntryRenderer
    {
        EntryCustom element;
        public EntryCustomRendererAndroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (EntryCustom)this.Element;

            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(10f);
                gradientDrawable.SetStroke(1, Android.Graphics.Color.Gray);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(20, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }
    }
}