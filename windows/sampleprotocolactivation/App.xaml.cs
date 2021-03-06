using Microsoft.ReactNative;
using Microsoft.ReactNative.Managed;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace sampleprotocolactivation
{

    sealed partial class App : ReactApplication
    {
        public ReactContext _reactContext { get; private set; }

        public App()
        {
#if BUNDLE
            JavaScriptBundleFile = "index.windows";
            InstanceSettings.UseWebDebugger = false;
            InstanceSettings.UseFastRefresh = false;
#else
            JavaScriptMainModuleName = "index";
            InstanceSettings.UseWebDebugger = true;
            InstanceSettings.UseFastRefresh = true;
#endif

#if DEBUG
            InstanceSettings.UseDeveloperSupport = true;
#else
            InstanceSettings.UseDeveloperSupport = false;
#endif

            Microsoft.ReactNative.Managed.AutolinkedNativeModules.RegisterAutolinkedNativeModulePackages(PackageProviders); // Includes any autolinked modules

            PackageProviders.Add(new Microsoft.ReactNative.Managed.ReactPackageProvider());
            PackageProviders.Add(new ReactPackageProvider());

            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            //base.OnLaunched(e);
            //var frame = Window.Current.Content as Frame;
            //frame.Navigate(typeof(MainPage));
            //Window.Current.Activate();

            base.OnLaunched(e);
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(typeof(MainPage), e.Arguments);
        }
        /// <summary>
        /// Invoked when the application is activated by some means other than normal launching.
        /// </summary>
        protected override void OnActivated(IActivatedEventArgs e)
        {
            var preActivationContent = Window.Current.Content;
            base.OnActivated(e);
            if (preActivationContent == null && Window.Current != null)
            {
                // Display the initial content
                var frame = (Frame)Window.Current.Content;
                frame.Navigate(typeof(MainPage), null);
            }
            else
            {
                if (e.Kind == ActivationKind.Protocol)
                {
                    ProtocolActivatedEventArgs eventArgs = e as ProtocolActivatedEventArgs;
                    var destination = eventArgs.Uri.AbsolutePath.Replace("/", "");
                    Debug.WriteLine($"{ destination }");

                    //.CallJSFunction("sampleprotocolactivation","navigateto", destination);

                    
                    //ReactN
                    //rns.Notifications.SendNotification("NavigationDesination", null, destination);

                    // TODO: Handle URI activation
                    // The received URI is eventArgs.Uri.AbsoluteUri
                }
            }
        }
    

    }
}