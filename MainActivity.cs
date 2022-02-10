using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Path = System.IO.Path;

namespace MediaPickerEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button pickbtn;
        private Button capbtn;
        private ImageView imageView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            pickbtn = FindViewById<Button>(Resource.Id.pbtn);
            capbtn = FindViewById<Button>(Resource.Id.cbtn);
            imageView = FindViewById<ImageView>(Resource.Id.iV);

            pickbtn.Click += picButton_Click;
            capbtn.Click += capButton_Click;
        }

        private async void capButton_Click(object sender, EventArgs e)
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            //if (photo == null)
            //{
            //    photo = null;
            //    return;
            //}
            //var stream = await Photo.LoadPhotoAsync();
            //imageView.SetImageBitmap(BitmapFactory.DecodeStream(stream));
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                imageView.SetImageBitmap(BitmapFactory.DecodeStream(stream));
            }


        }


        private async void picButton_Click(object sender, EventArgs e)
        {
            var res = await MediaPicker.PickPhotoAsync();


            if (res.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) || (res.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))) ;
            {
                var stream = await res.OpenReadAsync();
                imageView.SetImageBitmap(BitmapFactory.DecodeStream(stream));
            }

        }

    }
}