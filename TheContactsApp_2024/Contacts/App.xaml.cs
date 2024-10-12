using Microsoft.Maui.Handlers;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
#endif


namespace Contacts;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var maxWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
            var maxHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
            
            var windowWidth = (int) (3.0/10 * maxWidth);
            var windowHeight = (int) (7.0/10 * maxHeight);
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            var windowHandle = WindowNative.GetWindowHandle(nativeWindow);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(windowWidth, windowHeight));
#endif
        });


        MainPage = new AppShell();
    }
}